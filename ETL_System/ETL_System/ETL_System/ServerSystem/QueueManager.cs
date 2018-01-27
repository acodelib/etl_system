using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
     
    }
}
