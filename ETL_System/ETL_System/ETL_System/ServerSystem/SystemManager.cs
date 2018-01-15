﻿using System;
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
        public CoreDB data_layer;


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
                SystemSharedData.app_db_connstring = this.readConfigFromFile(this.path_to_config,"ConnectionString");
                string db_boot_test = CoreDB.checkDBConnStringIsValid(SystemSharedData.app_db_connstring);
                Console.WriteLine(db_boot_test);
                LogManager.writeStartEvent(db_boot_test, this.path_to_log);
            }

            if (!File.Exists(this._path_to_log))
                File.CreateText(this._path_to_log).Close();

            try {
                //Launch the Data Layer and JobsCatalogue
                if (SystemSharedData.app_db_connstring != null && CoreDB.checkDBisDeployed(SystemSharedData.app_db_connstring) > 0) {
                    this.data_layer     = new CoreDB(SystemSharedData.app_db_connstring);
                    this.jobs_catalogue = new JobsCatalogue(this.data_layer);

                    Console.WriteLine("Jobs Catalogue is initialised");        
                }
                //Launch the CommsManager
                CommsManager the_msg_handler = new CommsManager(ref SystemSharedData.clients_table);
                Console.WriteLine("SERVER started OK");
            }
            catch(Exception e) {
                Console.WriteLine(e.Message.ToString());
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
                bool found = false;                
                lock (_locker) {
                    //2.Read all lines from file                    
                    var lines = File.ReadAllLines(path_to_work_with);
                    var configs = new List<string>(lines);
                    if (configs != null) {
                        for(int i = 0; i< configs.Count; i++) {
                            if (configs[i].IndexOf(":") > 0) {
                                string[] key_val = configs[i].Split(':');
                                if (key_val[0] == "ConnectionString") {                                    
                                    configs[i] = "ConnectionString:" + conn_string;
                                    found = true;
                                }
                            }
                        }
                        if (!found)                            
                           configs.Add( "ConnectionString:" + conn_string);
                    }
                    else
                        configs.Add("ConnectionString:" + conn_string);

                    //3.assign conn_string to file
                    using (StreamWriter file_writer = File.CreateText(path_to_work_with)) {
                        for (int i = 0; i < configs.Count; i++)
                            file_writer.WriteLine(configs[i]);
                    }
                }
            } catch (Exception e) {
                return_message.Append($"\n Server Error: Could not save the Connection String. Original runtime err: {e.Message}");
            }
            
            return return_message.ToString();
        }
               ///////
        public string readConfigFromFile(string path,string config_name) {
            string conn_str = null;

            string[] configs = File.ReadAllLines(path);
            for (int i=0;i<configs.Length;i++) {
                string[] key_val = configs[i].Split(':');
                if(key_val[0] == config_name)
                conn_str = key_val[1] ;
                break;
            }            
            return conn_str;
        }

        public string addOrChangeConfigToFile(string config_name,string config_value, string file_path) {
            StringBuilder return_message = new StringBuilder();
            //enable option to write to different file:
            string path_to_work_with = (file_path == null) ? this._path_to_config : file_path;
            //1. write conn_string to file
            if (!File.Exists(path_to_work_with)) {
                return_message.Append("\n Server Error: Could not find target file ETLSystemConfig.etl; Either restart system or create the file.");
                LogManager.writeErrorToLog(return_message.ToString().Replace("\n", ""), this._path_to_log);
                return return_message.ToString();
            }

            try {
                bool found = false;
                int index_of_cs;
                lock (_locker) {
                    //2.Read all lines from file                    
                    var lines = File.ReadAllLines(path_to_work_with);
                    var configs = new List<string>(lines);
                    if (configs != null) {
                        for (int i = 0; i < configs.Count; i++) {
                            if (configs[i].IndexOf(":") > 0) {
                                string[] key_val = configs[i].Split(':');
                                if (key_val[0] == config_name) {
                                    configs[i] = config_name +":" + config_value;
                                    found = true;
                                }
                            }
                        }
                        if (!found)
                            configs.Add(config_name + ":" + config_value);
                    }
                    else
                        configs.Add(config_name + ":" + config_value);

                    //3.assign conn_string to file
                    using (StreamWriter file_writer = File.CreateText(path_to_work_with)) {
                        for (int i = 0; i < configs.Count; i++)
                            file_writer.WriteLine(configs[i]);
                    }
                }
            }
            catch (Exception e) {
                return_message.Append($"\n Server Error: Could not save the Config setting. Original runtime err: {e.Message}");
            }

            return return_message.ToString();
        }   

        public void registerExecutionFolderToConfig(string path,string config_path) {
            string path_to_work_with = (config_path == null) ? this._path_to_config : config_path;
            this.addOrChangeConfigToFile("ExecutionPath", path, this.path_to_config);
        }

        public string deployDBScript (string db_creation_path,string db_deployment_path) {
            // use defualt script path if none is provided
            string creation   = db_creation_path    == null ? AppDomain.CurrentDomain.BaseDirectory + $"Scripts\\DB_Creation.sql" : db_creation_path;
            string deployment = db_deployment_path  == null ? AppDomain.CurrentDomain.BaseDirectory + $"Scripts\\DB_Deployment.sql" : db_deployment_path;

            //Read SQL from File
            try {
                string script = File.ReadAllText(creation);
                CoreDB.runCustomSQLCommand(script, SystemSharedData.app_db_connstring);

                script = File.ReadAllText(deployment);
                CoreDB.runCustomSQLCommand(script, SystemSharedData.app_db_connstring);
            }
            catch (Exception e){
                Console.WriteLine(e.Message.ToString());
                return $"\n Server Error: Could not deploy the App DB schema. Original runtime err: {e.Message.ToString()}";                
            }
            return null;
        }
    }
}
