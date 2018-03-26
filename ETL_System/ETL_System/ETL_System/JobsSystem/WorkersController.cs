using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL_System
{
    public class WorkersController {
        public Dictionary<string, ETLWorker> workers;
        public int work_force;

        public WorkersController() { }

        public WorkersController(int size,int run_frequency, JobsQueue queue, NotificationsManager mailer,CoreDB data_layer) {
            workers = new Dictionary<string, ETLWorker>();
            SystemSharedData.no_of_workers = size;
            SystemSharedData.worker_fetch_frequency = run_frequency;
            for(int i = 1;i<= size; i++) {
                ETLWorker e = new ETLWorker(queue, mailer,data_layer,run_frequency);
                e.name = $"Worker_no_{i}";
                workers.Add(e.name, e);
            }
        }

        public void startWorkers() {
            foreach(ETLWorker w in this.workers.Values) {
                w.startExecutionRoutine();
            }
        }
        
    }
}
