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

        private Dictionary<string, SqlDataAdapter> adapters_table;
        
        //===================== CONSTRUCTORS
        public CoreDB(string conn_string) {
            this.connection_string = conn_string;
            this.etl_database = new DataSet("ETL_System");
            this.adapters_table = new Dictionary<string, SqlDataAdapter>();
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
                SqlCommand select = new SqlCommand("Select * from ETL_System.dbo.Jobs");
                db_adapter.SelectCommand = select;
                var builder = new SqlCommandBuilder(db_adapter);
                this.adapters_table.Add("Jobs", db_adapter);
                
            }
            fill_query = "Select * from ETL_System.dbo.JobTypes";
            using (SqlDataAdapter db_adapter = new SqlDataAdapter(fill_query, this.connection_string))
                db_adapter.Fill(this.etl_database, "JobTypes");
            
            fill_query = "Select * from ETL_System.dbo.JobDependency";
            using (SqlDataAdapter db_adapter = new SqlDataAdapter(fill_query, this.connection_string)) {
                db_adapter.Fill(this.etl_database, "JobDependency");
                var builder = new SqlCommandBuilder(db_adapter);
                this.adapters_table.Add("JobDependency", db_adapter);
            }
            fill_query = "Select * from ETL_System.dbo.JobSchedules";
            using (SqlDataAdapter db_adapter = new SqlDataAdapter(fill_query, this.connection_string)) {
                db_adapter.Fill(this.etl_database, "JobSchedules");
                var builder = new SqlCommandBuilder(db_adapter);
                this.adapters_table.Add("JobSchedule", db_adapter);
            }
            fill_query = "Select * from ETL_System.dbo.ScheduleTypes";
            using (SqlDataAdapter db_adapter = new SqlDataAdapter(fill_query, this.connection_string))
                db_adapter.Fill(this.etl_database, "ScheduleTypes");


            fill_query = "Select * from ETL_System.dbo.DependencyTypes";
            using (SqlDataAdapter db_adapter = new SqlDataAdapter(fill_query, this.connection_string))
                db_adapter.Fill(this.etl_database, "DependencyTypes");

            foreach(DataRow r in etl_database.Tables["DependencyTypes"].Rows) {
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

            foreach (DataRow r in jobs.Rows) {                                
                Job j = new Job {
                    job_id                  = (int)r["job_id"],
                    last_instance_id        = (int)r["last_instance_id"],
                    job_type_id             = (int)r["job_type_id"],
                    last_instance_timestamp = (DateTime)r["last_instance_timestamp"],
                    name                    = (string)r["name"],
                    executable_name         = (string)r["executable_name"],
                    max_try_count           = (int)r["max_try_count"],
                    is_failed               = (bool)r["is_failed"],
                    delay_seconds           = (int)r["delay_seconds"],
                    latency_alert_seconds   = (int)r["latency_alert_seconds"],
                    data_chceckpoint        = (long)r["data_chceckpoint"],
                    time_checkpoint         = (DateTime)r["time_checkpoint"],
                    notifiactions_list      = (string)r["notifications_list"],
                    type_name               = job_types.Select($"job_type_id = {(int)r["job_type_id"]}").Count() > 0 ? (string)job_types.Select($"job_type_id = {(int)r["job_type_id"]}")[0]["type_name"] :null
                };

                //add Dependencies
                if (j.type_name == "Dependency" && etl_database.Tables["JobDependency"].Select($"job_id = {j.job_id}").Count() > 0) {
                    Dictionary<int, Dependency> s = new Dictionary<int, Dependency>();
                    foreach(DataRow rw in etl_database.Tables["JobDependency"].Select($"job_id = {j.job_id}")) {
                        s.Add((int)rw["job_dependency_id"], new Dependency {
                           job_dependency_id    = (int)rw["job_dependency_id"],
                           job_id               = (int)rw["job_id"],
                           depending_job_id     = (int)rw["depending_job_id"],
                           dependency_type_id   = (int)rw["dependency_type_id"]
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
                collection.Add(j.name, j);
            }
        }

        public void insertNewJob(Job new_job) {
            DataTable job_types = etl_database.Tables["JobTypes"];
            DataRow r;
            try {
                lock (_locker) {
                    r = etl_database.Tables["Jobs"].NewRow();
                }

                r["job_id"] = new_job.job_id;
                r["last_instance_id"] = new_job.last_instance_id;
                r["job_type_id"] = new_job.job_type_id;
                r["last_instance_timestamp"] = new_job.last_instance_timestamp;
                r["name"] = new_job.name;
                r["executable_name"] = new_job.executable_name;
                r["max_try_count"] = new_job.max_try_count;
                r["is_failed"] = new_job.is_failed;
                r["delay_seconds"] = new_job.delay_seconds;
                r["latency_alert_seconds"] = new_job.latency_alert_seconds;
                r["data_chceckpoint"] = new_job.data_chceckpoint;
                r["time_checkpoint"] = new_job.time_checkpoint;
                r["notifications_list"] = new_job.notifiactions_list;
                //r["job_type_id"] = new_job.type_name;//(int)job_types.Select($"type_name = {new_job.type_name}")[0]["job_type_id"];
                
                using (SqlDataAdapter db_adapter = new SqlDataAdapter("Select * from ETL_System.dbo.Jobs", SystemSharedData.app_db_connstring)) {                                                                        
                        var builder = new SqlCommandBuilder(db_adapter);
                    lock (_locker) {
                        //etl_database.Tables["Jobs"].Rows.InsertAt(r, etl_database.Tables["Jobs"].Rows.Count);
                        etl_database.Tables["Jobs"].Rows.Add(r);
                        DataRow[] target = { r };
                        db_adapter.Update(target);
                    }
                }
            }catch(Exception e) {
                Console.WriteLine(e.Message);
            }
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

        public static string runStringCustomScalarSQLCommand(string command, string conn_string) {
            using (SqlConnection conn = new SqlConnection(conn_string)) {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(command, conn)) {
                    string r = (string)cmd.ExecuteScalar();
                    return r;
                }
                conn.Close();
            }
        }
            

        public static int runIntCustomScalarSQLCommand(string command, string conn_string) {
            using (SqlConnection conn = new SqlConnection(conn_string)) {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(command, conn)) {
                    int r = (int)cmd.ExecuteScalar();
                    return r;
                }      
            }                
        }      
            
        
    }
}
