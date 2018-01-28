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
            Console.WriteLine("Queue Manager has started Work");
            while (1 == 1) {
                if (SystemSharedData.catalogue_scan_flag) {
                    foreach(Job j in this.catalogue.jobs_collection.Values) {
                        //schedules --- do something
                        //checkpoints --- do something
                    }
                }
                Console.WriteLine("QueueManager: Done some work, going for next one.");
                Thread.Sleep(this.scan_frequencey_seconds * 1000);
            }
        }
     
    }
}
