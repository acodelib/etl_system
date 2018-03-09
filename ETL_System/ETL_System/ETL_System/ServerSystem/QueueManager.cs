using System;
using System.Collections.Generic;
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

        //==========================CONSTRUCTORS
        public QueueManager(JobsCatalogue cat, JobsQueue queue, int frequency) {
            this.catalogue = cat;
            this.queue = queue;
            this.scan_frequencey_seconds = frequency;
        }

        //=========================Methods
        public void startWork() {
            Thread t = new Thread(scanCatalogue);            
            t.Start();
        }

        private void scanCatalogue() {
            DateTime local_dt_operator;
            Console.WriteLine("Queue Manager has started Work");
            while (1 == 1) {
                try {
                    if (SystemSharedData.catalogue_scan_flag) {
                        foreach (Job j in this.catalogue.jobs_collection.Values) {
                            if (j.is_active && !j.is_executing && !j.is_paused && !j.is_queued && j.current_failed_count < j.max_try_count) {

                                //schedules --- do something
                                if (j.job_type_id == 1) {
                                    //1.check if job exectution is within treshold
                                    foreach (Schedule s in j.schedules.Values) {
                                        local_dt_operator = s.next_execution ?? DateTime.Parse("2017-01-01");
                                        if (DateTime.Now >= local_dt_operator.AddSeconds(-5) && DateTime.Now <= local_dt_operator.AddSeconds(SystemSharedData.schedule_types[s.schedule_type_id].execution_window_seconds)) {
                                            //2.add to queue
                                            this.queue.enqueueJob(j);
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
