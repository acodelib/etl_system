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

        public string addNewJob(Job new_job, User changer) {                        
            lock (_locker) {
                if (jobs_collection.ContainsKey(new_job.name))
                    return "Job NAME already exists. Try a different name.";
                new_job.job_id = SystemSharedData.getJobsKey();
                new_job.sys_change_id = SystemSharedData.getJobChangesKey();
                new_job.setLastJobChange(changer.login, null);
                this.jobs_collection.Add(new_job.name, new_job);
            }
            this.data_layer.addNewJob(new_job,changer);
            this._sys_change_id = this.getCurrentSysChangeId();
            return null;
        }

        public string deleteJob(string job_name,User changer) {
            lock (_locker) {
                if (!jobs_collection.ContainsKey(job_name))
                    return "Job doesn't exists.";
                this.jobs_collection.Remove(job_name);
            }
            this.data_layer.deleteJob(job_name,changer); // change audit not implemented currently for delete           
            return null;
        }
        
        public string updateJob(Job target_job, User changer) {
            lock (_locker) {
                
                //referential integrity check first:
                bool searcher = false;
                foreach (var k in jobs_collection.Values) {
                    if (k.job_id == target_job.job_id) {
                        searcher = true;
                        break;
                    }
                }
                if (!searcher || jobs_collection.ContainsKey(target_job.name))
                    return "Job doesn't exists or there is another job already named like this.";                

                //if integrity check passes then do updates:
                this.jobs_collection.Remove(target_job.name);
                target_job.sys_change_id = SystemSharedData.getJobChangesKey();
                target_job.setLastJobChange(changer.login, null);
                this.jobs_collection.Add(target_job.name,target_job);
            }
            this.data_layer.updateJob(target_job,changer);
            this._sys_change_id = this.getCurrentSysChangeId();
            return null;
        }

        private int refreshJobsCollection() {
            data_layer.fillJobsCollection(this.jobs_collection);
            return 1;
        }
    }
}
