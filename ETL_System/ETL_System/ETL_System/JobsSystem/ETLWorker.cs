using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        

        //================================ CONSTRUCTOR ==========================================
        public ETLWorker() { }

        public ETLWorker(JobsQueue the_queue,NotificationsManager the_mailer) {
            this.jobs_queue = the_queue;
            this.notifier = the_mailer;
        }


        //================================ METHODS ==============================================
        public void fetchAndExecute() {
            this.target_job = this.jobs_queue.provideJobToRun();
            //String exec_path = SystemSharedData.

        }

        private void recordStart() { }
        private void recordEnd() { }
        private void asyncNotification() { }
    }
}
