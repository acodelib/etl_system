using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;
using System.Windows.Forms;

namespace ETL_System {
    public class CoreDB {

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
            }
            return "App DB Success";
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

        public static int runCustomScalarSQLCommand(string command, string conn_string) {
            using (SqlConnection conn = new SqlConnection(conn_string)) {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(command, conn)) {
                    int r = (int)cmd.ExecuteScalar();
                    return r;
                }      
            }
                return 0;
        }      
            

    }
}
