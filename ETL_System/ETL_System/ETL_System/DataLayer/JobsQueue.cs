using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL_System {
    public class JobsQueue {

        //=====================================FIELDS
        private Dictionary<int, Job> queued;
        private System.Object _locker = new System.Object();
        //=====================================CONSTRUCTORS


        //=====================================METHODS
        public void enqueueJob(Job job) {            
            lock (_locker) {
                job.is_queued = true;
                this.queued.Add(job.job_id, job);                
                    //new QueuedJob { is_running = false, worker_name = null, job = job});
            }       
        }

        public void dequeueJob(int job_id) {
            lock (_locker) {
                this.queued.Remove(job_id);
                this.queued[job_id].is_queued = false;
            }
        }

        public Job provideJobToRun() {            
            foreach(int jid in this.queued.Keys) {
                lock (_locker) {
                    if (!this.queued[jid].is_executing) {
                        this.queued[jid].is_executing = true;               
                        return this.queued[jid];
                    }
                }
            }
            return null;
        }
        
    }



    public class QueuedJob {
        public bool is_running;
        public string worker_name;
        public Job job;
    }
}
