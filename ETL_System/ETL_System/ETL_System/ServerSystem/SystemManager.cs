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
        private string _path_to_log;
        private string _path_to_config;

        public string path_to_log { get; set; }
        public string path_to_config { get; set; }



        //-----------CONSTRUCTORS
        public SystemManager() {
            this._path_to_config    = AppDomain.CurrentDomain.BaseDirectory + "ETLSystemConfig.etl";
            this._path_to_log       = AppDomain.CurrentDomain.BaseDirectory + "ETLSystemLog.etl";
        }
        

        //----------METHODS
        public void startSystem() {

            //1.Check if ETLSystemLog.txt and ETLSystemConfig.txt exist
            if (!File.Exists(this._path_to_config))
                File.CreateText (this._path_to_config).Close();
            if (!File.Exists(this.path_to_log))
                File.CreateText(this._path_to_log).Close();
                
            try {
                //Launch the CommsManager
                CommsManager the_msg_handler = new CommsManager(ref SystemSharedData.clients_table);
                Console.WriteLine("SERVER started");
            }
            catch(Exception e) {

            }
        }

        public void registerDBConnString(string conn_string, string file_path) {
            
            //enable easier unit test:
            string path_to_work_with = (file_path == null) ? this._path_to_config : file_path;

            //1. write conn_string to file
            if (!File.Exists(path_to_work_with)) {                
                String file_problem = "Could not find target file ETLSystemConfig.etl. Either restart system or create the file.";
                MessageBox.Show(file_problem);
                return;
            }

            //2.assign conn_string to file
            using (StreamWriter file_writer = File.CreateText(path_to_work_with)) {
                file_writer.WriteLine("ConnectionString:" + conn_string);
            }          
        }
        
    }
}
