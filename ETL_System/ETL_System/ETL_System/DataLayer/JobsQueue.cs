using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL_System {
    public class JobsQueue {

        //=====================================FIELDS
        private Dictionary<int, Job> queued;
        private System.Object _locker = new System.Object();
        //=====================================CONSTRUCTORS
        public JobsQueue() {
            this.queued = new Dictionary<int, Job>();
        }

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
                this.queued[job_id].is_queued       = false;
                this.queued[job_id].queue_timestamp = null;
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

        public DataTable produceQueueDisplay() {
            
            DataTable queue_display = new DataTable("QueueDisplay");

            queue_display.Columns.Add("Job ID", typeof(Int32));
            queue_display.Columns.Add("Job Name", typeof(String));            
            queue_display.Columns.Add("Queued @", typeof(DateTime));
            queue_display.Columns.Add("Status", typeof(String));
            queue_display.Columns.Add("Executing Since", typeof(string));

            foreach(Job j in this.queued.Values) {
                DataRow r = queue_display.NewRow();
                r["Job ID"] = j.job_id;
                r["Job Name"] = j.name;
                r["Queued @"] = j.queue_timestamp;
                r["Status"] = j.is_executing ? "EXECUTING" : "QUEUED";
                r["Executing Since"] = j.last_instance_timestamp;
                queue_display.Rows.Add(r);
            }
            return queue_display;       
        }
        

    }



    public class QueuedJob {
        public bool is_running;
        public string worker_name;
        public Job job;
    }
}
