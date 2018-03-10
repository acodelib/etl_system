using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ETL_System {
    public class ETLWorker {
        //================================ FIELDS ===============================================
        public string name;

        private JobsQueue jobs_queue;
        private Job target_job;
        private int job_instance_id;
        private bool exec_result;
        private NotificationsManager notifier;
        private int work_frequency;

        private System.Object _locker = new System.Object();  // local critical zone lockingc

        //================================ CONSTRUCTOR ==========================================
        public ETLWorker() { }

        public ETLWorker(JobsQueue the_queue,NotificationsManager the_mailer,int frequency) {
            this.jobs_queue = the_queue;
            this.notifier = the_mailer;
            this.work_frequency = frequency;
        }


        //================================ METHODS ==============================================
        public void startExecutionRoutine() {
            Thread t = new Thread(fetchAndExecuteCycle);
            t.Start();
        }

        private void fetchAndExecuteCycle() {
            Thread.Sleep(2000);
            Console.WriteLine($"Worker {this.name} started.");
            string exec_path;
            while (1 == 1) {
                //fetch job
                this.target_job = this.jobs_queue.provideJobToRun();
                if (this.target_job != null) {
                    try {
                        //record start
                        recordStart(this.target_job);
                        //set execution path
                        exec_path = SystemSharedData.jobs_folder + @"\" + this.target_job.executable_name;
                        lock (_locker) {
                            target_job.executing_timestamp = DateTime.Now;
                            target_job.executor = this.name;
                        }
                        //execute
                        //receive execution output
                        //catch execution error
                    }
                    catch (Exception e) {

                    }
                    //record end with message output                    
                    Thread.Sleep(5000); //dev simulate some work;
                    this.recordEnd(target_job);
                    //send notification if case;
                }
                Console.WriteLine($"{this.name} did some work, going for more...");
                Thread.Sleep(this.work_frequency * 1000);
            }
            
        }       

        private void recordStart(Job j) {
            lock (_locker) {
                j.is_executing = true;
            }
        }
        private void recordEnd(Job j) {
            lock (_locker) {
                j.is_executing = false;
                j.executing_timestamp = null;
                j.executor = null;
                this.target_job = null;
            }
            this.jobs_queue.dequeueJob(j.job_id);
        }
        private void asyncNotification() { }
    }
}
