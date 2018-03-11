using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ETL_System {
   public class SystemSharedData {       
           
        public static string app_db_connstring;
        public static string jobs_folder;

        public static Dictionary<int?, ScheduleType> schedule_types;
        public static Dictionary<int?, DependencyType> dependency_types;
        public static Dictionary<int, string> user_roles;

        public static bool catalogue_scan_flag;
        public static bool workers_start_flag;

        private static int jobs_key_sequence;
        private static int changes_key_sequence;
        private static int job_instances_key_sequence;      


        //================ METHODS
        public static int getJobsKey() {
            jobs_key_sequence ++;
            return jobs_key_sequence;
        }

        public static int getJobChangesKey() {            
            return ++changes_key_sequence;
        }

        public static int getJobInstancesKey() {
            return ++job_instances_key_sequence;
        }

        public static void initKeyGenerators() {
            //jobs:
            string sql = "SELECT ISNULL(max(job_id),0) FROM ETL_SYSTEM.dbo.Jobs";
            jobs_key_sequence = CoreDB.runIntCustomScalarSQLCommand(sql, app_db_connstring);

            //job changes
            sql = "SELECT ISNULL(max(sys_change_id),0) FROM ETL_SYSTEM.dbo.JobChanges";
            changes_key_sequence = CoreDB.runIntCustomScalarSQLCommand(sql, app_db_connstring);

            //job changes
            sql = "SELECT ISNULL(max(job_instance_id),0) FROM ETL_SYSTEM.dbo.JobInstances";
            job_instances_key_sequence = CoreDB.runIntCustomScalarSQLCommand(sql, app_db_connstring);
        }

        public static string GetSHA1HashData(string data) {            
            SHA1 sha1 = SHA1.Create();            
            byte[] hashData = sha1.ComputeHash(Encoding.Default.GetBytes(data));                                    
            StringBuilder returnValue = new StringBuilder();

            for (int i = 0; i < hashData.Length; i++) {
                returnValue.Append(hashData[i].ToString());
            }

            
            return returnValue.ToString();
        }
    }
}
