using System;
//using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections.Generic;
//using System.Collections.Concurrent;


namespace ETL_System {
    public class JobsCatalogue {

        //=========================================FIELDS
        private System.Object _locker = new System.Object();  // local critical zone locking
        private Dictionary<string,ETL_System.Job> jobs_collection;        
        private int _sys_change_id;
        public int sys_change_id { get; }
        private CoreDB data_layer;

        //=========================================CONSTRUCTORs
        public JobsCatalogue(CoreDB data_layer) {
            this.data_layer = data_layer;
            jobs_collection = new Dictionary<string, Job>();
            this.sys_change_id = this.getCurrentSysChangeId();
            this.data_layer.fillJobsCollection(this.jobs_collection);
        }

        //========================================METHODS
        private int getCurrentSysChangeId() {
            //get last change from table JobChanges
            string sql = "SELECT max(sys_change_id) FROM ETL_SYSTEM.dbo.JobChanges";
            return CoreDB.runIntCustomScalarSQLCommand(sql, SystemSharedData.app_db_connstring);           
        }

        public string addNewJob(Job new_job) {

            //*************** should deal with this as a transactions in a Try Catch
            lock (_locker) {
                if (jobs_collection.ContainsKey(new_job.name))
                    return "Job NAME already exists. Try a different name.";
                this.jobs_collection.Add(new_job.name, new_job);
            }
            this.data_layer.insertNewJob(new_job);
            return null;
        }
        

        private int refreshJobsCollection() {
            data_layer.fillJobsCollection(this.jobs_collection);
            return 1;
        }
    }
}
