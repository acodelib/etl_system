using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace ETL_System {
    public class SystemManager {

        //----------- FIELDS

        private System.Object _locker = new System.Object();  // local critical zone locking

        private string _path_to_log;
        private string _path_to_config;

        public string path_to_log { get { return _path_to_log; } }
        public string path_to_config { get { return _path_to_config; } }

        public JobsCatalogue jobs_catalogue;


        //-----------CONSTRUCTORS
        public SystemManager() {
            this._path_to_config    = AppDomain.CurrentDomain.BaseDirectory + "ETLSystemConfig.etl";
            this._path_to_log       = AppDomain.CurrentDomain.BaseDirectory + "ETLSystemLog.etl";
        }
        

        //----------METHODS
        public string startSystem() {

            //1.Check if ETLSystemLog.txt and ETLSystemConfig.txt exist
            if (!File.Exists(this._path_to_config))
                File.CreateText(this._path_to_config).Close();
            else { //initialise Global DB connection
                SystemSharedData.app_db_connstring = this.readDBConnStringFromFile(this.path_to_config);
                string db_boot_test = CoreDB.checkDBConnStringIsValid(SystemSharedData.app_db_connstring);
                Console.WriteLine(db_boot_test);
                LogManager.writeStartEvent(db_boot_test, this.path_to_log);
            }

            if (!File.Exists(this._path_to_log))
                File.CreateText(this._path_to_log).Close();

            try {
                //Launch the JobsCatalogue
                if (SystemSharedData.app_db_connstring != null) {
                    this.jobs_catalogue = new JobsCatalogue();
                    Console.WriteLine("Jobs Catalogue is initialised");
                }
                //Launch the CommsManager
                CommsManager the_msg_handler = new CommsManager(ref SystemSharedData.clients_table);
                Console.WriteLine("SERVER started OK");
            }
            catch(Exception e) {
                return e.Message;
            }
            return null;
        }

                ///////
        public string registerDBConnString(string conn_string, string file_path) {
            StringBuilder return_message = new StringBuilder();            
            //enable option to write to different file:
            string path_to_work_with = (file_path == null) ? this._path_to_config : file_path;            
            //1. write conn_string to file
            if (!File.Exists(path_to_work_with)) {
                return_message.Append("\n Server Error: Could not find target file ETLSystemConfig.etl; Either restart system or create the file.");
                LogManager.writeErrorToLog(return_message.ToString().Replace("\n", ""),this._path_to_log);
                return return_message.ToString();
            }

            try {
                lock (_locker) {
                    //2.assign conn_string to file
                    using (StreamWriter file_writer = File.CreateText(path_to_work_with)) {
                        file_writer.WriteLine("ConnectionString:" + conn_string);
                    }                    
                }           
            } catch (Exception e) {
                return_message.Append($"\n Server Error: Could not save the Connection String. Original runtime err: {e.Message}");
            }
            
            return return_message.ToString();
        }
               ///////
        public string readDBConnStringFromFile(string path) {
            string conn_str = null;

            string[] configs = File.ReadAllLines(path);
            for (int i=0;i<configs.Length;i++) {
                string[] key_val = configs[i].Split(':');
                conn_str = key_val[0] == "ConnectionString" ? key_val[1] : null;
            }            
            return conn_str;
        }

        public string deployDBScript (string script_path) {
            // use defualt script path if none is provided
            string internal_path = script_path == null ? AppDomain.CurrentDomain.BaseDirectory + $"Scripts\\DB_Creation.sql" : script_path;

            //Read SQL from File
            try {
                string script = File.ReadAllText(internal_path);
                CoreDB.runCustomSQLCommand(script, SystemSharedData.app_db_connstring);
            }
            catch (Exception e){
                return $"\n Server Error: Could not deploy the App DB schema. Original runtime err: {e.Message}";
            }
            return null;
        }
    }
}
