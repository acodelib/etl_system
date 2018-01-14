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
            using (SqlConnection cn = new SqlConnection(this.connection_string)) {
                fill_query = "Select * from ETL_System.dbo.Jobs";
                using (SqlDataAdapter db_adapter = new SqlDataAdapter(fill_query, this.connection_string))   //wierdly enogh you can set connection string only from constructor                                
                    db_adapter.Fill(this.etl_database, "Jobs");
                    fill_query = "Select * from ETL_System.dbo.JobTypes";
                using (SqlDataAdapter db_adapter = new SqlDataAdapter(fill_query, this.connection_string))
                    db_adapter.Fill(this.etl_database, "JobTypes");

                }
            }


      
        public void fillJobsCollection(Dictionary<string,Job> collection) {            
            DataTable jobs = etl_database.Tables["Jobs"];
            DataTable job_types = etl_database.Tables["JobTypes"];


            foreach(DataRow r in jobs.Rows) {
                Job j = new Job { job_id = (int)r["job_id"],
                    last_instance_id = (int)r["last_instance_id"],
                    job_type_id = (int)r["job_type_id"],
                    last_instance_timestamp = (DateTime)r["last_instance_timestamp"],
                    name = (string)r["name"],
                    executable_name = (string)r["execution_name"],
                    max_try_count = (int)r["max_try_count"],
                    is_failed = (int)r["is_failed"],
                    delay_seconds = (int)r["delay_seconds"],
                    latency_alert_seconds = (int)r["latency_alert_seconds"],
                    //      data_chceckpoint        = (int)r["data_chceckpoint"],
                    time_checkpoint = (DateTime)r["time_checkpoint"],
              //      type_name =(string)job_types.Select($"id = {(int)r["job_type_id"]}")[0]["name"] //(string)job_types.Rows.Find((int)r["job_type_id"])["name"]
      
                };
                collection.Add(j.name, j);
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
                //conn.Close();
            }

        }

        public static string runStringCustomScalarSQLCommand(string command, string conn_string) {
            using (SqlConnection conn = new SqlConnection(conn_string)) {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(command, conn)) {
                    string r = (string)cmd.ExecuteScalar();
                    return r;
                }
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
