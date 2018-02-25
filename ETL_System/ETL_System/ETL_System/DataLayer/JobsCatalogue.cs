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
        public Dictionary<string,ETL_System.Job> jobs_collection;        
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

        public Job getJob(string name) {
            try {
                return this.jobs_collection[name];
            }catch(KeyNotFoundException e) {
                throw new Exception("Job doesn't exist! ");
            } 

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
                if (!jobs_collection.ContainsKey(job_name) || jobs_collection[job_name].is_queued)
                    return "Can't remove job. Job either doesn't exists or is executing.";
                this.jobs_collection.Remove(job_name);
            }
            this.data_layer.deleteJob(job_name,changer); // change audit not implemented currently for delete           
            return null;
        }
        
        public string updateJob(Job target_job, User changer) {
            lock (_locker) {
                
                //referential integrity check first:
                bool searcher = false;
                string named_changed = null;
                foreach (var k in jobs_collection.Values) {
                    if (k.job_id == target_job.job_id) {
                        if (k.is_queued)
                            return "Can't change job, it is executing or about to execute!";
                        searcher = true;
                        named_changed = k.name;
                        break;
                    }
                }
                if (!(searcher || jobs_collection.ContainsKey(target_job.name)))
                    return "Job doesn't exists.";                

                //if integrity check passes then do updates:
                this.jobs_collection.Remove(named_changed);
                target_job.sys_change_id = SystemSharedData.getJobChangesKey();
                target_job.setLastJobChange(changer.login, null);
                this.jobs_collection.Add(target_job.name,target_job);
            }
            this.data_layer.updateJob(target_job,changer);
            this._sys_change_id = this.getCurrentSysChangeId();
            return "Job Update success!";
        }

        public Job addNewSchedule(Job target_job,User changer) {
            lock (_locker) {
                bool searcher = false;
                string named_changed = null;
                foreach (var k in jobs_collection.Values) {
                    if (k.job_id == target_job.job_id) {
                        if (k.is_queued)
                            throw new Exception("Job is in queue, can't change now.");
                        searcher = true;
                        named_changed = k.name;
                        break;
                    }
                }
                if (!(searcher || jobs_collection.ContainsKey(target_job.name)))
                    throw new Exception("Can't find job");
                
                //this.jobs_collection.Add(target_job.name, target_job);
                target_job.sys_change_id = SystemSharedData.getJobChangesKey();
                target_job.setLastJobChange(changer.login, null);
                jobs_collection[target_job.name].setSchedules(this.data_layer.addSingleScheduleToJob(target_job, changer));
                this._sys_change_id = this.getCurrentSysChangeId();

                return jobs_collection[target_job.name];
            }           
        }

        public Job deleteSchedules(Job target_job,User changer) {
            lock (_locker) {
                bool searcher = false;
                string named_changed = null;
                foreach (var k in jobs_collection.Values) {
                    if (k.job_id == target_job.job_id) {
                        if (k.is_queued)
                            throw new Exception("Job is in queue, can't change now.");
                        searcher = true;
                        named_changed = k.name;
                        break;
                    }
                }
                if (!(searcher || jobs_collection.ContainsKey(target_job.name)))
                    throw new Exception("Can't find job");

                //this.jobs_collection.Add(target_job.name, target_job);
                target_job.sys_change_id = SystemSharedData.getJobChangesKey();
                target_job.setLastJobChange(changer.login, DateTime.Now);
                jobs_collection[target_job.name].setSchedules(this.data_layer.removeSchedulesFromJob(target_job, changer));
                this._sys_change_id = this.getCurrentSysChangeId();

                return jobs_collection[target_job.name];

            }
        }
                
        public Job addNewDependency(Job target_job,User changer) {
            lock (_locker) {
                bool searcher = false;
                string named_changed = null;
                foreach (var k in jobs_collection.Values) {
                    if (k.job_id == target_job.job_id) {
                        if (k.is_queued)
                            throw new Exception("Job is in queue, can't change now.");
                        searcher = true;
                        named_changed = k.name;
                        break;
                    }
                }
                if (!(searcher || jobs_collection.ContainsKey(target_job.name)))
                    throw new Exception("Can't find job");

                //this.jobs_collection.Add(target_job.name, target_job);
                target_job.sys_change_id = SystemSharedData.getJobChangesKey();
                target_job.setLastJobChange(changer.login, null);
                jobs_collection[target_job.name].setDependencies(this.data_layer.addSingleDependencyToJob(target_job, changer));
                this._sys_change_id = this.getCurrentSysChangeId();

                return jobs_collection[target_job.name];
            }
        }
        public Job deleteDependency(Job target_job, User changer) {
            lock (_locker) {
                bool searcher = false;
                string named_changed = null;
                foreach (var k in jobs_collection.Values) {
                    if (k.job_id == target_job.job_id) {
                        if (k.is_queued)
                            throw new Exception("Job is in queue, can't change now.");
                        searcher = true;
                        named_changed = k.name;
                        break;
                    }
                }
                if (!(searcher || jobs_collection.ContainsKey(target_job.name)))
                    throw new Exception("Can't find job");

                //this.jobs_collection.Add(target_job.name, target_job);
                target_job.sys_change_id = SystemSharedData.getJobChangesKey();
                target_job.setLastJobChange(changer.login, DateTime.Now);
                jobs_collection[target_job.name].setDependencies(this.data_layer.removeDependencyFromJob(target_job, changer));
                this._sys_change_id = this.getCurrentSysChangeId();
                return jobs_collection[target_job.name];
            }
        }

        private int refreshJobsCollection() {
            data_layer.fillJobsCollection(this.jobs_collection);
            return 1;
        }

        public DataTable produceDisplay() {              
            DataTable view = this.data_layer.getJobsDefaultView();
            DataRow[] r;            
            view.Columns.Add("State", typeof(string));
            foreach(Job j in this.jobs_collection.Values) {                
                r = view.Select($"job_id ={j.job_id}");
                r[0]["State"] = j.is_executing ? "EXECUTING" : j.is_queued ? "IN QUEUE" : "WAITING"; 
            }
            return view;
        }
        public Dictionary<int,string> produceJobsList() {
            Dictionary<int,string> jobs_list = new Dictionary<int, string>();
            foreach (Job j in this.jobs_collection.Values)
                jobs_list.Add(j.job_id,j.name);
            return jobs_list;
        }
    }
}
