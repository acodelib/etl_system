using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace ETL_System {
    public class SystemManager {

        //=================================== FIELDS
        private System.Object _locker = new System.Object();  // local critical zone locking

        private string _path_to_log;
        private string _path_to_config;

        public string path_to_log { get { return _path_to_log; } }
        public string path_to_config { get { return _path_to_config; } }

        public JobsCatalogue        jobs_catalogue;
        public CoreDB               data_layer;
        public QueueManager         queue_manager;
        public JobsQueue            jobs_queue;
        public SessionManager       session_manager;
        public CommsManager         the_msg_handler;
        public WorkersController    workers_manager;
        public NotificationsManager emailer;


        //==================================CONSTRUCTORS
        public SystemManager(string config_file = null, string log_file = null) {
            this._path_to_config    = config_file   == null ? AppDomain.CurrentDomain.BaseDirectory + "ETLSystemConfig.etl" : config_file;
            this._path_to_log       = log_file      == null ? AppDomain.CurrentDomain.BaseDirectory + "ETLSystemLog.etl" : log_file;

            SystemSharedData.schedule_types         = new Dictionary<int?, ScheduleType>();
            SystemSharedData.dependency_types       = new Dictionary<int?, DependencyType>();
            SystemSharedData.user_roles             = new Dictionary<int, string>();
            SystemSharedData.catalogue_scan_flag    = true;       
        }      

        //==================================METHODS
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
                //Launch  Data Layer, Jobs Catalogue, JobsQueue, Queue Manager and ETL Worker
                if (SystemSharedData.app_db_connstring != null && CoreDB.checkDBisDeployed(SystemSharedData.app_db_connstring) > 0) {
                    this.data_layer     = new CoreDB(SystemSharedData.app_db_connstring);
                    this.jobs_catalogue = new JobsCatalogue(this.data_layer);
                    SystemSharedData.initKeyGenerators();
                    SystemSharedData.jobs_folder = this.readConfigFromFile(this._path_to_config, "ExecutionPath");
                    //Console.WriteLine("Jobs Catalogue is initialised");
                    LogManager.writeStartEvent("Jobs Catalogue is initialised", this._path_to_log);

                    this.jobs_queue         = new JobsQueue();
                    Console.WriteLine("Jobs Queue is initialised, STARTED successfully");
                    LogManager.writeStartEvent("Jobs Queue is initialised", this._path_to_log);
                    this.queue_manager      = new QueueManager(jobs_catalogue, jobs_queue, 10);
                    this.session_manager    = new SessionManager(this.data_layer);
                    this.emailer            = new NotificationsManager();
                    this.workers_manager    = new WorkersController(4,8, jobs_queue, emailer,data_layer);
                    
                }
                //Launch the CommsManager                
                this.the_msg_handler = new CommsManager(this,this.session_manager);
                Console.WriteLine("SERVER started OK");
                LogManager.writeStartEvent("SERVER started OK", this._path_to_log);
            }
            catch(Exception e) {
                Console.WriteLine(e.Message.ToString());
                return e.Message;
            }
            return null;
        }
        public void pauseQueue() {

        }
        public string startQueueing() {
            SystemSharedData.catalogue_scan_flag = true;
            this.queue_manager.startWork();
            Console.WriteLine("Queueing Service started OK.");
            return "Queueing Service started OK.";
        }
        public void startETLWorkers() {
            SystemSharedData.workers_start_flag = true;
            this.workers_manager.startWorkers();
            Console.WriteLine("Execution Engine started OK.");
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
                            if (configs[i].IndexOf("~") > 0) {
                                string[] key_val = configs[i].Split('~');
                                if (key_val[0] == "ConnectionString") {                                    
                                    configs[i] = "ConnectionString~" + conn_string;
                                    found = true;
                                }
                            }
                        }
                        if (!found)                            
                           configs.Add( "ConnectionString~" + conn_string);
                    }
                    else
                        configs.Add("ConnectionString~" + conn_string);

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
                string[] key_val = configs[i].Split('~');
                if (key_val[0] == config_name) {
                    conn_str = key_val[1];
                    break;
                }
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
                            if (configs[i].IndexOf("~") > 0) {
                                string[] key_val = configs[i].Split('~');
                                if (key_val[0] == config_name) {
                                    configs[i] = config_name +"~" + config_value;
                                    found = true;
                                }
                            }
                        }
                        if (!found)
                            configs.Add(config_name + "~" + config_value);
                    }
                    else
                        configs.Add(config_name + "~" + config_value);

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
            SystemSharedData.jobs_folder = path;
        }

        public string deployDBScript (string db_creation_path = null,string db_deployment_path = null,string db_view_path = null) {
            // use defualt script path if none is provided
            string creation     = db_creation_path    == null ? AppDomain.CurrentDomain.BaseDirectory + $"Scripts\\DB_Creation.sql" : db_creation_path;
            string deployment   = db_deployment_path  == null ? AppDomain.CurrentDomain.BaseDirectory + $"Scripts\\DB_Deployment.sql" : db_deployment_path;
            string views        = db_view_path        == null ? AppDomain.CurrentDomain.BaseDirectory + $"Scripts\\DB_Views.sql" : db_view_path;

            //Read SQL from File
            try {
                string script = File.ReadAllText(creation);
                CoreDB.runCustomSQLCommand(script, SystemSharedData.app_db_connstring);

                script = File.ReadAllText(deployment);
                CoreDB.runCustomSQLCommand(script, SystemSharedData.app_db_connstring);

                script = File.ReadAllText(views);
                CoreDB.runCustomSQLCommandInETLSystem(script, SystemSharedData.app_db_connstring);
            }
            catch (Exception e){
                Console.WriteLine(e.Message.ToString());
                return $"\n Server Error: Could not deploy the App DB schema. Original runtime err: {e.Message.ToString()}";                
            }
            return null;
        }

                           ////////// CLIENT REQUESTS //////
        public Dictionary<int,string> pipeTasksRawList() {
            return this.jobs_catalogue.produceJobsList();
        }
        public Message handleClientMessage(Message msg) {
            Message result;
            if (msg.msg_type >= (MsgTypes)1 && msg.msg_type <= (MsgTypes)4) {
                result = this.executeDataRequest(msg);
                return result;
            }
            if (msg.msg_type >= (MsgTypes)5 && msg.msg_type <= (MsgTypes)11) {
                result = this.executeMgmtCommand(msg, (User)msg.header["user"]);
                return result;
            }
            return null;
            
        }
        public Message executeMgmtCommand(Message msg, User user_context) {
            Message outcome = new Message();
            MsgTypes msgtype = msg.msg_type;   
            switch (msgtype) {
                case MsgTypes.MGMT_CREATE_JOB:
                    try {
                        this.jobs_catalogue.addNewJob((Job)msg.attachement, user_context);
                        outcome.msg_type = MsgTypes.REPLY_SUCCESS;
                        outcome.header["jobs_list"] = pipeTasksRawList();
                        return outcome;
                    }catch(Exception e) {
                        Console.WriteLine(e.Message);
                        outcome.msg_type = MsgTypes.REPLY_FAIL;
                        outcome.body = $"Failed to create job. Original system message: {e.Message}";
                        return outcome;
                    }                    
                    
                case MsgTypes.MGMT_DELETE_JOB:
                    try {
                        this.jobs_catalogue.deleteJob(msg.body, user_context);
                        outcome.msg_type = MsgTypes.REPLY_SUCCESS;
                        outcome.header["jobs_list"] = pipeTasksRawList();
                        return outcome;
                    }
                    catch (Exception e) {
                        Console.WriteLine(e.Message);
                        outcome.msg_type = MsgTypes.REPLY_FAIL;
                        outcome.header["jobs_list"] = pipeTasksRawList();
                        outcome.body = $"Failed to delete job. Original system message: {e.Message}";
                        return outcome;
                    }
                    
                case MsgTypes.MGMT_UPDATE_JOB:
                    try {
                        this.jobs_catalogue.updateJob((Job)msg.attachement, user_context);
                        outcome.msg_type = MsgTypes.REPLY_SUCCESS;
                        outcome.header["jobs_list"] = pipeTasksRawList();
                        return outcome;
                    }
                    catch (Exception e) {
                        Console.WriteLine(e.Message);
                        outcome.msg_type = MsgTypes.REPLY_FAIL;
                        outcome.body = $"Failed to UPDATE job. Original system message: {e.Message}";
                        return outcome;
                    }
                case MsgTypes.MGMT_CREATE_SCHEDULE:
                    try {
                        outcome.attachement = this.jobs_catalogue.addNewSchedule((Job)msg.attachement, user_context);
                        outcome.msg_type = MsgTypes.REPLY_SUCCESS;
                        outcome.header["jobs_list"] = pipeTasksRawList();
                        return outcome;
                    }
                    catch (Exception e) {
                        Console.WriteLine(e.Message);
                        outcome.body = e.Message;
                        outcome.msg_type = MsgTypes.REPLY_FAIL;
                        outcome.body = $"Failed to UPDATE job with new schedule. Original system message: {e.Message}";
                        return outcome;
                    }
                case MsgTypes.MGMT_DELETE_SCHEDULE:
                    try {
                        outcome.attachement = this.jobs_catalogue.deleteSchedules((Job)msg.attachement, user_context);
                        outcome.msg_type = MsgTypes.REPLY_SUCCESS;
                        outcome.header["jobs_list"] = pipeTasksRawList();
                        return outcome;
                    }
                    catch (Exception e) {
                        Console.WriteLine(e.Message);
                        outcome.body = e.Message;
                        outcome.msg_type = MsgTypes.REPLY_FAIL;
                        outcome.body = $"Failed to DELETE schedules. Original system message: {e.Message}";
                        return outcome;
                    }
                case MsgTypes.MGMG_CREATE_DEPENDENCY:
                    try {
                        outcome.attachement = this.jobs_catalogue.addNewDependency((Job)msg.attachement, user_context);
                        outcome.msg_type = MsgTypes.REPLY_SUCCESS;
                        outcome.header["jobs_list"] = pipeTasksRawList();
                        return outcome;
                    }
                    catch (Exception e) {
                        Console.WriteLine(e.Message);
                        outcome.body = e.Message;
                        outcome.msg_type = MsgTypes.REPLY_FAIL;
                        outcome.body = $"Failed to UPDATE job with new schedule. Original system message: {e.Message}";
                        return outcome;
                    }
                case MsgTypes.MGMT_DELETE_DEPENDENCY:
                    try {
                        outcome.attachement = this.jobs_catalogue.deleteDependency((Job)msg.attachement, user_context);
                        outcome.msg_type = MsgTypes.REPLY_SUCCESS;
                        outcome.header["jobs_list"] = pipeTasksRawList();
                        return outcome;
                    }
                    catch (Exception e) {
                        Console.WriteLine(e.Message);
                        outcome.body = e.Message;
                        outcome.msg_type = MsgTypes.REPLY_FAIL;
                        outcome.body = $"Failed to DELETE schedules. Original system message: {e.Message}";
                        return outcome;
                    }

                default:
                    outcome.msg_type = MsgTypes.REPLY_FAIL;
                    outcome.body = $"Unknown COMMAND TYPE";
                    return outcome;                                                                            

            }            
        }
        //public string executeDataRequest(MsgTypes msgtype, string job_name, User user_context,ref MsgAttachment att) {
        public Message executeDataRequest(Message m) {
            Message outcome = new Message();
            switch (m.msg_type) {
                case MsgTypes.REQUEST_JOB_CATALOGUE_DISPLAY:
                    try {
                        //>>>>>>>> HERE TO ADD SYS_CHANGE_ID comparison <<<<<
                        MsgAttachment att = new JobsCatalogueDisplay(this.jobs_catalogue.sys_change_id, this.jobs_catalogue.produceDisplay());
                        outcome.attachement = att;
                        outcome.msg_type = MsgTypes.REPLY_SUCCESS;
                        outcome.header["jobs_list"] = pipeTasksRawList();
                        return outcome;
                    }
                    catch (Exception e) {
                        Console.WriteLine(e.Message);
                        outcome.msg_type = MsgTypes.REPLY_FAIL;
                        outcome.body = $"Failed to retrieve Jobs Catalogue. Original system message: {e.Message}";
                        return outcome;
                    }
                    
                case MsgTypes.REQUEST_JOB:
                    try {
                        MsgAttachment att = this.jobs_catalogue.getJob(m.body);
                        outcome.attachement = att;
                        outcome.msg_type = MsgTypes.REPLY_SUCCESS;
                        outcome.header["jobs_list"] = pipeTasksRawList();
                        return outcome;
                    }
                    catch (Exception e) {
                        Console.WriteLine(e.Message);
                        outcome.msg_type = MsgTypes.REPLY_FAIL;
                        outcome.body = $"Failed to retrieve Job. Original system message: {e.Message}";
                        return outcome;
                    }
                case MsgTypes.REQUEST_DEPENDENCY_CATALOGUE:
                    try {
                        //>>>>>>>> HERE TO ADD SYS_CHANGE_ID comparison <<<<<
                        MsgAttachment att = new DependencyCatalogueDisplay(this.jobs_catalogue.sys_change_id, this.jobs_catalogue.produceDependencyDisplay());
                        outcome.attachement = att;
                        outcome.msg_type = MsgTypes.REPLY_SUCCESS;
                        outcome.header["jobs_list"] = pipeTasksRawList();
                        return outcome;
                    }
                    catch (Exception e) {
                        Console.WriteLine(e.Message);
                        outcome.msg_type = MsgTypes.REPLY_FAIL;
                        outcome.body = $"Failed to retrieve Dependency Catalogue. Original system message: {e.Message}";
                        return outcome;
                    }
                case MsgTypes.REQUEST_JOB_QUEUE_DISPLAY:
                    try {
                        //>>>>>>>> HERE TO ADD SYS_CHANGE_ID comparison <<<<<
                        MsgAttachment att = new JobsQueueDisplay(this.jobs_catalogue.sys_change_id, this.jobs_queue.produceQueueDisplay());
                        outcome.attachement = att;
                        outcome.msg_type = MsgTypes.REPLY_SUCCESS;
                        outcome.header["jobs_list"] = pipeTasksRawList();
                        return outcome;
                    }
                    catch (Exception e) {
                        Console.WriteLine(e.Message);
                        outcome.msg_type = MsgTypes.REPLY_FAIL;
                        outcome.body = $"Failed to retrieve Jobs Queue Catalogue. Original system message: {e.Message}";
                        return outcome;
                    }
                default:
                    outcome.msg_type = MsgTypes.REPLY_FAIL;
                    outcome.body = $"Unknown COMMAND TYPE";
                    return outcome;
            }            
        }
 

    }
}
