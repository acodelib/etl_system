using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ETL_System{
    public class QueueManager {
        //==========================FIELDS
        private JobsCatalogue catalogue;
        private JobsQueue queue;
        private int scan_frequencey_seconds;

        private System.Object _locker = new System.Object();  // local critical zone locking

        //==========================CONSTRUCTORS
        public QueueManager(JobsCatalogue cat, JobsQueue queue, int frequency) {
            this.catalogue = cat;
            this.queue = queue;
            this.scan_frequencey_seconds = frequency;
            SystemSharedData.queue_scan_frequency = frequency;
        }

        //=========================Methods
        public void startWork() {
            Thread t = new Thread(scanCatalogue);            
            t.Start();
        }

        private void scanCatalogue() {
            DateTime local_dt_operator;
            Console.WriteLine("Queue Manager has started Work");
            
            //do bookmark search index for better perfomance on navigation
            Dictionary<int, string> bookmark_search = new Dictionary<int, string>();
            foreach (Job j in this.catalogue.jobs_collection.Values)
                bookmark_search.Add(j.job_id, j.name);

            //get the dependency index to use later in resolving Execution Dependency
            DataTable index = catalogue.produceDependencyIndex();

            while (1 == 1) {
                try {
                    if (SystemSharedData.catalogue_scan_flag) {  // check if scanning is allowed

                        foreach (Job j in this.catalogue.jobs_collection.Values) { // loop through all jobs
                            if (j.is_active && !j.is_executing && !j.is_paused && !j.is_queued && j.current_failed_count < j.max_try_count) { // 


                    //schedules --- do something
                                if (j.job_type_id == 1) {
                                    //1.check if job exectution is within treshold
                                    foreach (Schedule s in j.schedules.Values) {
                                        local_dt_operator = s.next_execution ?? DateTime.Parse("2017-01-01");
                                        if (DateTime.Now >= local_dt_operator.AddSeconds(-5) 
                                                    && DateTime.Now <= local_dt_operator.AddSeconds(SystemSharedData.schedule_types[s.schedule_type_id].execution_window_seconds)) {
                                            //2.add to queue
                                            this.queue.enqueueJob(j);
                                            lock(_locker)
                                                j.queue_timestamp = DateTime.Now;
                                            local_dt_operator = local_dt_operator.AddSeconds(SystemSharedData.schedule_types[s.schedule_type_id].frequency_seconds);
                                            catalogue.jobUpdateNextExecution(j.name, s.job_schedule_id ?? 0, local_dt_operator);
                                        }
                                        //3.check if it's schedule was missed and correct next execution
                                        else if (DateTime.Now > local_dt_operator.AddSeconds(SystemSharedData.schedule_types[s.schedule_type_id].execution_window_seconds)) {
                                            do {
                                                local_dt_operator = local_dt_operator.AddSeconds(SystemSharedData.schedule_types[s.schedule_type_id].frequency_seconds);
                                            } while (local_dt_operator < DateTime.Now);
                                            catalogue.jobUpdateNextExecution(j.name, s.job_schedule_id ?? 0, local_dt_operator);
                                        }
                                    }
                                }
                     //checkpoints --- do something
                                else {                                   
                                    bool pass = true;
                                    Job target_job;
                        //FIRST: Data Dependencies                                    
                                    foreach (Dependency d in j.dependencies.Values) {
                                        target_job = this.catalogue.jobs_collection[bookmark_search[d.depending_job_id ?? 0]]; //get info about depending job

                                        //some preliminary checks:
                                        if (target_job.is_paused || target_job.is_queued || target_job.is_executing ||
                                            target_job.current_failed_count >= target_job.max_try_count) { //1.check if any of the dependencies are paused, which blocks the whole chain
                                            pass = false;
                                            break;
                                        }
                                        if (!target_job.is_active) //2.check if any of the dependencies are disactivated, then jump over job
                                            break;                         
                                        if (d.dependency_type_id >= 1) {
                                            if (j.data_chceckpoint != null) {
                                                //value dependencies:                                            
                                                if (target_job.data_chceckpoint < j.data_chceckpoint) {
                                                    pass = false;
                                                    break;
                                                }
                                            }
                                            else {
                                                //timestamp dependencies:                                            
                                                if (target_job.time_checkpoint < j.time_checkpoint) {
                                                    pass = false;
                                                    break;
                                                }
                                            }
                                        }                                                                                                      
                                    }
                          //SECOND: execution Dependencies
                                   if( index.Select($"depending_job_id = {j.job_id} and dependency_type_id = 2").Count() > 0) {
                                        foreach(DataRow r in index.Select($"depending_job_id = {j.job_id} and dependency_type_id = 2")) {                                            
                                            target_job = this.catalogue.jobs_collection[bookmark_search[(int)r["job_id"]]];
                                            if (!target_job.is_active)
                                                continue;
                                            if (target_job.is_paused || target_job.is_queued || target_job.is_executing) {
                                                pass = false;
                                                break;
                                            }
                                            if (target_job.data_chceckpoint != null && target_job.data_chceckpoint < j.data_chceckpoint) {
                                                pass = false;
                                                break;
                                            }
                                            if (target_job.time_checkpoint != null && target_job.time_checkpoint < j.time_checkpoint) {
                                                pass = false;
                                                break;
                                            }
                                        }
                                    }
                                    //evaluat the pass variable
                                    if (pass) {
                                        this.queue.enqueueJob(j);
                                        lock (_locker)
                                            j.queue_timestamp = DateTime.Now;
                                    }
                                }
                            }
                        }
                    }
                    Console.WriteLine("QueueManager: Done some work, going for next one.");
                    Thread.Sleep(this.scan_frequencey_seconds * 1000);
                }catch(Exception e) {
                    Console.WriteLine($"Exception in QueueManager Thread: {e.Message}");
                }
            }
        }
     
    }
}
