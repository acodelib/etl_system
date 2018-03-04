using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
//using System.Windows.Forms;
//Microsoft.SqlServer.ConnectionInfo.dll


namespace ETL_System {
    public class CoreDB {

        //==================== Fields
        private System.Object _locker = new System.Object();  // local critical zone locking

        private string connection_string;
        private DataSet etl_database;        
        
        //===================== CONSTRUCTORS
        public CoreDB(string conn_string) {
            this.connection_string = conn_string;
            this.etl_database = new DataSet("ETL_System");            
            this.initializeCoreDB();            
        }

        //==================== METHODS
        private void initializeCoreDB() {
            string fill_query;
            //SqlConnection cn = new SqlConnection(this.connection_string);
            //Jobs          
            
            fill_query = "Select * from ETL_System.dbo.Jobs";
            using (SqlDataAdapter db_adapter = new SqlDataAdapter(fill_query, this.connection_string)) {  //wierdly enogh you can set connection string only from constructor                                
                db_adapter.Fill(this.etl_database, "Jobs");                                                                               
            }
            fill_query = "Select * from ETL_System.dbo.JobTypes";
            using (SqlDataAdapter db_adapter = new SqlDataAdapter(fill_query, this.connection_string))
                db_adapter.Fill(this.etl_database, "JobTypes");
            
            fill_query = "Select * from ETL_System.dbo.JobDependency";
            using (SqlDataAdapter db_adapter = new SqlDataAdapter(fill_query, this.connection_string)) {
                db_adapter.Fill(this.etl_database, "JobDependency");                
            }
            fill_query = "Select * from ETL_System.dbo.JobSchedules";
            using (SqlDataAdapter db_adapter = new SqlDataAdapter(fill_query, this.connection_string)) {
                db_adapter.Fill(this.etl_database, "JobSchedules");                
            }
            fill_query = "Select * from ETL_System.dbo.ScheduleTypes";
            using (SqlDataAdapter db_adapter = new SqlDataAdapter(fill_query, this.connection_string))
                db_adapter.Fill(this.etl_database, "ScheduleTypes");


            fill_query = "Select * from ETL_System.dbo.DependencyTypes";
            using (SqlDataAdapter db_adapter = new SqlDataAdapter(fill_query, this.connection_string))
                db_adapter.Fill(this.etl_database, "DependencyTypes");

            fill_query = "WITH tops as (SELECT max(sys_change_id) sys_change_id from ETL_System.dbo.JobChanges GROUP BY job_id) Select jc.job_id,u.login user_name,jc.change_timestamp from ETL_System.dbo.JobChanges jc JOIN ETL_System.dbo.[User] u on jc.user_id = u.user_id WHERE EXISTS (SELECT sys_change_id FROM tops t WHERE t.sys_change_id = jc.sys_change_id)";
            using (SqlDataAdapter db_adapter = new SqlDataAdapter(fill_query, this.connection_string))
                db_adapter.Fill(this.etl_database, "LastJobChanges");


            fill_query = "Select * from ETL_System.dbo.[User]";
            using (SqlDataAdapter db_adapter = new SqlDataAdapter(fill_query, this.connection_string))
                db_adapter.Fill(this.etl_database, "User");


            fill_query = "Select * from ETL_System.dbo.UserRoles";
            using (SqlDataAdapter db_adapter = new SqlDataAdapter(fill_query, this.connection_string))
                db_adapter.Fill(this.etl_database, "UserRoles");


            foreach (DataRow r in etl_database.Tables["UserRoles"].Rows) {
                SystemSharedData.user_roles.Add((int)r["role_id"], (string)r["name"]);
            }            

        
           foreach (DataRow r in etl_database.Tables["DependencyTypes"].Rows) {
                SystemSharedData.dependency_types.Add((int)r["dependency_type_id"], new DependencyType {
                    name = (string)r["name"],
                    description = (string)r["description"]
                });
            }

            foreach (DataRow r in etl_database.Tables["ScheduleTypes"].Rows) {                                        
                SystemSharedData.schedule_types.Add((int)r["schedule_type_id"], new ScheduleType {
                    schedule_type_name          = (string)r["schedule_type_name"],
                    frequency_seconds           = (long)r["frequency_seconds"],
                    execution_window_seconds    = (int)r["execution_window_seconds"]
                });
            }
            
    }
              
        public void fillJobsCollection(Dictionary<string,Job> collection) {            
            DataTable jobs = etl_database.Tables["Jobs"];
            DataTable job_types = etl_database.Tables["JobTypes"];            
            Dictionary<int, Dependency> d = new Dictionary<int, Dependency>();
            try {
                foreach (DataRow r in jobs.Rows) {
                    Job j = new Job() {
                        job_id = (int)r["job_id"],
                        last_instance_id = (int)r["last_instance_id"],
                        job_type_id = (int)r["job_type_id"],
                        last_instance_timestamp = (DateTime?)(r["last_instance_timestamp"] == DBNull.Value ? null : r["last_instance_timestamp"]),
                        name = (string)r["name"],
                        executable_name = (string)r["executable_name"],
                        max_try_count = (int)r["max_try_count"],
                        current_failed_count = (int)r["current_failed_count"],
                        is_failed = (bool)r["is_failed"],
                        delay_seconds = (int)r["delay_seconds"],
                        latency_alert_seconds = (int)r["latency_alert_seconds"],
                        data_chceckpoint = (long?)(r["data_chceckpoint"] == DBNull.Value ? null : r["data_chceckpoint"]),
                        time_checkpoint = (DateTime?)(r["time_checkpoint"] == DBNull.Value ? null : r["time_checkpoint"]),
                        notifiactions_list = (string)r["notifications_list"],
                        type_name = job_types.Select($"job_type_id = {(int)r["job_type_id"]}").Count() > 0 ? (string)job_types.Select($"job_type_id = {(int)r["job_type_id"]}")[0]["type_name"] : null,
                        is_executing = false,
                        is_queued = false,
                        is_active = (bool)r["is_active"],
                        is_paused = (bool)r["is_paused"],
                        checkpoint_type = (int)r["checkpoint_type"]
                    };

                    //add Dependencies
                    if (j.type_name == "Dependency" && etl_database.Tables["JobDependency"].Select($"job_id = {j.job_id}").Count() > 0) {
                        Dictionary<int, Dependency> s = new Dictionary<int, Dependency>();
                        foreach (DataRow rw in etl_database.Tables["JobDependency"].Select($"job_id = {j.job_id}")) {
                            s.Add((int)rw["job_dependency_id"], new Dependency {
                                job_dependency_id = (int)rw["job_dependency_id"],
                                job_id = (int)rw["job_id"],
                                depending_job_id = (int)rw["depending_job_id"],
                                dependency_type_id = (int)rw["dependency_type_id"]
                            });
                        }
                        j.setDependencies(s);
                    }               
                    //add Schedules
                    if (j.type_name == "Schedule" && etl_database.Tables["JobSchedules"].Select($"job_id = {j.job_id}").Count() > 0) {
                        Dictionary<int, Schedule> s = new Dictionary<int, Schedule>();
                        foreach (DataRow rw in etl_database.Tables["JobSchedules"].Select($"job_id = {j.job_id}")) {
                            s.Add((int)rw["job_schedule_id"], new Schedule {
                                job_schedule_id = (int)rw["job_schedule_id"],
                                job_id = (int)rw["job_id"],
                                schedule_type_id = (int)rw["schedule_type_id"],
                                next_execution = (DateTime)rw["next_execution"]
                            });
                        }
                        j.setSchedules(s);
                    }

                    //add last job change for audit
                    if (etl_database.Tables["LastJobChanges"].Select($"job_id = {j.job_id}").Count() > 0) {
                        j.setLastJobChange((string)etl_database.Tables["LastJobChanges"].Select($"job_id = {j.job_id}")[0]["user_name"],
                                              (DateTime)etl_database.Tables["LastJobChanges"].Select($"job_id = {j.job_id}")[0]["change_timestamp"]
                                            );
                    }

                    collection.Add(j.name, j);
                }
            }
            catch (Exception e) { }

        }

        public void addNewJob(Job new_job, User changer) {
            DataTable job_types = etl_database.Tables["JobTypes"];
            DataRow r;          
            lock (_locker) {
                r = etl_database.Tables["Jobs"].NewRow();
            }

            object lit = null;  // care for nullable inserts
            object lid = null;
            object dc = null;
            object tc = null;

            if (new_job.last_instance_timestamp == null) lit = DBNull.Value;
            else lit = new_job.last_instance_timestamp;

            if (new_job.last_instance_id == null) lid = DBNull.Value;
            else lid = new_job.last_instance_id;

            if (new_job.data_chceckpoint == null) dc = DBNull.Value;
            else dc = new_job.data_chceckpoint;

            if (new_job.time_checkpoint == null) tc = DBNull.Value;
            else tc = new_job.time_checkpoint;

            r["job_id"]                     = new_job.job_id;
            r["last_instance_id"]           = lid;
            r["job_type_id"]                = new_job.job_type_id;
            r["last_instance_timestamp"]    = lit;
            r["sys_change_id"]              = new_job.sys_change_id;
            r["name"]                       = new_job.name;
            r["executable_name"]            = new_job.executable_name;
            r["max_try_count"]              = new_job.max_try_count;
            r["current_failed_count"]       = new_job.current_failed_count;            
            r["delay_seconds"]              = new_job.delay_seconds;
            r["latency_alert_seconds"]      = new_job.latency_alert_seconds;
            r["data_chceckpoint"]           = dc;
            r["time_checkpoint"]            = tc;
            r["notifications_list"]         = new_job.notifiactions_list;
            r["is_failed"]                  = new_job.is_failed;
            r["is_active"]                  = new_job.is_active;
            r["is_paused"]                  = new_job.is_paused;
            r["checkpoint_type"]            = new_job.checkpoint_type;

            using (SqlDataAdapter db_adapter = new SqlDataAdapter("Select * from ETL_System.dbo.Jobs", SystemSharedData.app_db_connstring)) {                                                                        
                    var builder = new SqlCommandBuilder(db_adapter);
                lock (_locker) {                        
                    etl_database.Tables["Jobs"].Rows.Add(r);
                    DataRow[] target = { r };
                    db_adapter.Update(target);
                }
            }
            //record job change:
            string sql = $"INSERT INTO ETL_System.dbo.JobChanges SELECT {new_job.sys_change_id},{new_job.job_id},{changer.user_id},'{String.Format("{0:yyyy-MM-dd HH:mm:ss.fff}",new_job.last_job_change.change_timestamp)}'";
            runCustomSQLCommand(sql,SystemSharedData.app_db_connstring);
        }        

        public void deleteJob(string job_name, User changer) {
            using (SqlDataAdapter db_adapter = new SqlDataAdapter("Select * from ETL_System.dbo.Jobs", SystemSharedData.app_db_connstring)) {
                var builder = new SqlCommandBuilder(db_adapter);
                try {
                    lock (_locker) {
                        DataRow r = etl_database.Tables["Jobs"].Select($"name = '{job_name}'")[0];                        
                        r.Delete();
                        DataRow[] target = { r };
                        db_adapter.Update(target);
                    }
                }catch(Exception e) {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public void updateJob(Job target_job,User changer) {
            DataTable job_types = etl_database.Tables["JobTypes"];
            DataRow r;

            object lit = null;  // care for nullable inserts
            object dc = null;
            object tc = null;

            if (target_job.last_instance_timestamp == null) lit = DBNull.Value;
            else lit = target_job.last_instance_timestamp;

            if (target_job.data_chceckpoint == null) dc = DBNull.Value;
            else dc = target_job.data_chceckpoint;

            if (target_job.time_checkpoint == null) tc = DBNull.Value;
            else tc = target_job.time_checkpoint;

            lock (_locker) {
                r = etl_database.Tables["Jobs"].Select($"job_id = {target_job.job_id}")[0];
            
                r["last_instance_id"]               = target_job.last_instance_id;
                r["job_type_id"]                    = target_job.job_type_id;
                r["last_instance_timestamp"]        = lit;
                r["sys_change_id"]                  = target_job.sys_change_id;
                r["name"]                           = target_job.name;
                r["executable_name"]                = target_job.executable_name;
                r["max_try_count"]                  = target_job.max_try_count;
                r["current_failed_count"]           = target_job.current_failed_count;                
                r["delay_seconds"]                  = target_job.delay_seconds;
                r["latency_alert_seconds"]          = target_job.latency_alert_seconds;
                r["data_chceckpoint"]               = dc;
                r["time_checkpoint"]                = tc;
                r["notifications_list"]             = target_job.notifiactions_list;
                r["is_active"]                      = target_job.is_active;
                r["is_paused"]                      = target_job.is_paused;
                r["checkpoint_type"]                = target_job.checkpoint_type;
            //r["job_type_id"] = new_job.type_name;//(int)job_types.Select($"type_name = {new_job.type_name}")[0]["job_type_id"];

            
                using (SqlDataAdapter db_adapter = new SqlDataAdapter("Select * from ETL_System.dbo.Jobs", SystemSharedData.app_db_connstring)) {
                    var builder = new SqlCommandBuilder(db_adapter);                                           
                        DataRow[] target = { r };
                        db_adapter.Update(target);                    
                }
            }
            string sql = $"INSERT INTO ETL_System.dbo.JobChanges SELECT {target_job.sys_change_id},{target_job.job_id},{changer.user_id},'{String.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", target_job.last_job_change.change_timestamp)}'";
            runCustomSQLCommand(sql, SystemSharedData.app_db_connstring);         
                
        }

        public Dictionary<int,Schedule> addSingleScheduleToJob(Job j,User changer, bool record_system_change = true) {
            DataRow r;
            Schedule sch = j.schedules[0];
            lock (_locker) {
                r = etl_database.Tables["JobSchedules"].NewRow();
               // r["job_schedule_id"] = sch.job_schedule_id;
                r["job_id"] = sch.job_id;
                r["schedule_type_id"] = sch.schedule_type_id;
                r["next_execution"] = sch.next_execution;

                using (SqlDataAdapter db_adapter = new SqlDataAdapter("Select * from ETL_System.dbo.JobSchedules", SystemSharedData.app_db_connstring)) {
                    var builder = new SqlCommandBuilder(db_adapter);                 
                        etl_database.Tables["JobSchedules"].Rows.Add(r);
                        DataRow[] target = { r };
                        db_adapter.Update(target);                 
                }
            }
            if (record_system_change) {
                string sql = $"INSERT INTO ETL_System.dbo.JobChanges SELECT {j.sys_change_id},{j.job_id},{changer.user_id},'{String.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", j.last_job_change.change_timestamp)}'";
                runCustomSQLCommand(sql, SystemSharedData.app_db_connstring);
            }

            //now return a fresh copy of the schedules
            Dictionary<int, Schedule> s = new Dictionary<int, Schedule>();
            etl_database.Tables["JobSchedules"].Reset();            
            using (SqlDataAdapter db_adapter = new SqlDataAdapter("Select * from ETL_System.dbo.JobSchedules", this.connection_string)) {
                db_adapter.Fill(this.etl_database, "JobSchedules");
            }
            foreach (DataRow rw in etl_database.Tables["JobSchedules"].Select($"job_id = {j.job_id}")) {
                s.Add((int)rw["job_schedule_id"], new Schedule {
                    job_schedule_id = (int)rw["job_schedule_id"],
                    job_id = (int)rw["job_id"],
                    schedule_type_id = (int)rw["schedule_type_id"],
                    next_execution = (DateTime)rw["next_execution"]
                });
            }
            return s;
        }
        
      
        public Dictionary<int, Schedule> removeSchedulesFromJob(Job j, User changer, bool record_system_change = true) {
            List<DataRow> deleted_rows = new List<DataRow>();
            //1.remove from DataTable element by element
            using (SqlDataAdapter db_adapter = new SqlDataAdapter("Select * from ETL_System.dbo.JobSchedules", SystemSharedData.app_db_connstring)) {
                var builder = new SqlCommandBuilder(db_adapter);
                try {
                    lock (_locker) {
                        foreach (Schedule s in j.schedules.Values) {
                            DataRow r = etl_database.Tables["JobSchedules"].Select($"job_schedule_id = '{s.job_schedule_id}'")[0];                            
                            r.Delete();
                            deleted_rows.Add(r);                           
                        }
                        //2.propagate to DB
                        DataRow[] target = deleted_rows.ToArray();
                        db_adapter.Update(target);
                    }
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
            }

            //3.now return a fresh copy of the schedules
            Dictionary<int, Schedule> scd = new Dictionary<int, Schedule>();
            etl_database.Tables["JobSchedules"].Reset();
            using (SqlDataAdapter db_adapter = new SqlDataAdapter("Select * from ETL_System.dbo.JobSchedules", this.connection_string)) {
                db_adapter.Fill(this.etl_database, "JobSchedules");
            }
            foreach (DataRow rw in etl_database.Tables["JobSchedules"].Select($"job_id = {j.job_id}")) {
                scd.Add((int)rw["job_schedule_id"], new Schedule {
                    job_schedule_id         = (int)rw["job_schedule_id"],
                    job_id                  = (int)rw["job_id"],
                    schedule_type_id        = (int)rw["schedule_type_id"],
                    next_execution          = (DateTime)rw["next_execution"]
                });
            }
            return scd;
        }

        public Dictionary<int, Dependency> addSingleDependencyToJob(Job j, User changer, bool record_system_change = true) {            
            DataRow r;
            Dependency dep = j.dependencies[0];
            lock (_locker) {
                r = etl_database.Tables["JobDependency"].NewRow();
                
                r["job_id"]             = dep.job_id;
                r["depending_job_id"] = dep.depending_job_id;
                r["dependency_type_id"] = dep.dependency_type_id;

                using (SqlDataAdapter db_adapter = new SqlDataAdapter("Select * from ETL_System.dbo.JobDependency", SystemSharedData.app_db_connstring)) {
                    var builder = new SqlCommandBuilder(db_adapter);
                    etl_database.Tables["JobDependency"].Rows.Add(r);
                    DataRow[] target = { r };
                    db_adapter.Update(target);
                }
            }
            if (record_system_change) {
                string sql = $"INSERT INTO ETL_System.dbo.JobChanges SELECT {j.sys_change_id},{j.job_id},{changer.user_id},'{String.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", j.last_job_change.change_timestamp)}'";
                runCustomSQLCommand(sql, SystemSharedData.app_db_connstring);
            }

            //now return a fresh copy of the Dependencies
            Dictionary<int, Dependency> d = new Dictionary<int, Dependency>();
            etl_database.Tables["JobDependency"].Reset();
            using (SqlDataAdapter db_adapter = new SqlDataAdapter("Select * from ETL_System.dbo.JobDependency", this.connection_string)) {
                db_adapter.Fill(this.etl_database, "JobDependency");
            }
            foreach (DataRow rw in etl_database.Tables["JobDependency"].Select($"job_id = {j.job_id}")) {
                d.Add((int)rw["job_dependency_id"], new Dependency {
                    job_dependency_id   = (int)rw["job_dependency_id"],
                    job_id              = (int)rw["job_id"],
                    depending_job_id    = (int)rw["depending_job_id"],
                    dependency_type_id  = (int)rw["dependency_type_id"]
                });
            }
            return d;
        }

        public Dictionary<int, Dependency> removeDependencyFromJob(Job j, User changer, bool record_system_change = true) {
            List<DataRow> deleted_rows = new List<DataRow>();
            //1.remove from DataTable element by element
            using (SqlDataAdapter db_adapter = new SqlDataAdapter("Select * from ETL_System.dbo.JobDependency", SystemSharedData.app_db_connstring)) {
                var builder = new SqlCommandBuilder(db_adapter);
                try {
                    lock (_locker) {
                        foreach (Dependency d in j.dependencies.Values) {
                            DataRow r = etl_database.Tables["JobDependency"].Select($"job_dependency_id = '{d.job_dependency_id}'")[0];
                            r.Delete();
                            deleted_rows.Add(r);
                        }
                        //2.propagate to DB
                        DataRow[] target = deleted_rows.ToArray();
                        db_adapter.Update(target);
                    }
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
            }

            //3.now return a fresh copy of the schedules
            Dictionary<int, Dependency> dep = new Dictionary<int, Dependency>();
            etl_database.Tables["JobDependency"].Reset();
            using (SqlDataAdapter db_adapter = new SqlDataAdapter("Select * from ETL_System.dbo.JobDependency", this.connection_string)) {
                db_adapter.Fill(this.etl_database, "JobDependency");
            }
            foreach (DataRow rw in etl_database.Tables["JobDependency"].Select($"job_id = {j.job_id}")) {
                dep.Add((int)rw["job_dependency_id"], new Dependency {
                    job_dependency_id       = (int)rw["job_dependency_id"],
                    job_id                  = (int)rw["job_id"],
                    dependency_type_id      = (int)rw["dependency_type_id"],
                    depending_job_id        = (int)rw["depending_job_id"]
                });
            }
            return dep;
        }

        public DataTable getJobsDefaultView() {

            string fill_query;
            fill_query = "Select * from ETL_System.dbo.JobsCatalogueDisplay";
            using (SqlDataAdapter db_adapter = new SqlDataAdapter(fill_query, this.connection_string)) {
                DataSet ds = new DataSet();
                db_adapter.Fill(ds = new DataSet(), "JobsView");                
                return ds.Tables["JobsView"];
            }    
            
        }

        public DataTable getDependencyDisplay() {
            string fill_query;
            fill_query = "SELECT jd.*,j.data_chceckpoint,j.time_checkpoint,j.is_active,j.is_failed,j.is_paused FROM ETL_System.dbo.JobDependency jd JOIN ETL_System.dbo.Jobs j on j.job_id = jd.depending_job_id";
            using (SqlDataAdapter db_adapter = new SqlDataAdapter(fill_query, this.connection_string)) {
                DataSet ds = new DataSet();
                db_adapter.Fill(ds = new DataSet(), "DependencyCatalogue");                
                return ds.Tables["DependencyCatalogue"];
            }
        }

        public DataRow validateUserLineExists(string user, string pass) {
            if (etl_database.Tables["User"].Select($"login = '{user}' and password = '{pass}'").Count()>0)
                return etl_database.Tables["User"].Select($"login = '{user}' and password = '{pass}'")[0];
            return null;            
        }


        //===================== HELPER METHODS
        public static string checkDBConnStringIsValid(string conn_string) {
            //method to validate that the connection string is good
            
            using (SqlConnection conn = new SqlConnection()) {
                try {
                    conn.ConnectionString = conn_string;
                    conn.Open();
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                    return e.Message;
                }
                conn.Close();
            }
            return "App DB Success";
        }

        public static int checkDBisDeployed(string conn_string) {

            if (checkDBConnStringIsValid(conn_string) == "App DB Success") {
                return runStringCustomScalarSQLCommand("select name from sys.databases where name = 'ETL_System'", conn_string) == null ? 0 : 1;
            }
            return 0;
        }

        public static void runCustomSQLCommand(string command, string conn_string) {
            using (SqlConnection conn = new SqlConnection(conn_string)) {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(command, conn)) {
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }

        }
        public static void runCustomSQLCommandInETLSystem(string command, string conn_string) {
            using (SqlConnection conn = new SqlConnection(conn_string)) {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("USE ETL_System", conn)) {                    
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = command;
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }

        }
        public static string runStringCustomScalarSQLCommand(string command, string conn_string) {
            using (SqlConnection conn = new SqlConnection(conn_string)) {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(command, conn)) {
                    string r = (string)cmd.ExecuteScalar();
                    conn.Close();
                    return r;                
                }
                
            }
        }            
        public static int runIntCustomScalarSQLCommand(string command, string conn_string) {
            using (SqlConnection conn = new SqlConnection(conn_string)) {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(command, conn)) {
                    int r = (int)cmd.ExecuteScalar();
                    conn.Close();
                    return r;
                }      
            }                
        }      
                  
        
    }
}
