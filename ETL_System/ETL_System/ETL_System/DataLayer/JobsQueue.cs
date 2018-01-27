using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL_System {
    public class JobsQueue {

        //=====================================FIELDS
        private Dictionary<int, QueuedJob> queued;
        private System.Object _locker = new System.Object();
        //=====================================CONSTRUCTORS


        //=====================================METHODS
        public void enqueueJob(Job job) {            
            lock (_locker) {
                this.queued.Add(job.job_id, new QueuedJob { is_running = false, worker_name = null, job = job});
            }       
        }

        public void dequeueJob(int job_id) {
            lock(_locker)
                this.queued.Remove(job_id);
        }

        public QueuedJob provideJobToRun() {            
            foreach(int jid in this.queued.Keys) {
                lock (_locker) {
                    if (!this.queued[jid].is_running) {
                        QueuedJob qj = queued[jid];
                        qj.is_running = true;                        
                        return qj;
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
