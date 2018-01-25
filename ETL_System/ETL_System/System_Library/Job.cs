using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL_System{

    public class Job : MsgAttachment {

        public int       job_id;     
        public int?      last_instance_id;        
        public int       job_type_id;
        public DateTime? last_instance_timestamp;
        public string    name;
        public string    executable_name;
        public int       max_try_count;
        public bool      is_failed;
        public int       delay_seconds;
        public int       latency_alert_seconds;
        public long?     data_chceckpoint;
        public DateTime? time_checkpoint;
        public string    notifiactions_list;

        public string   type_name;
        
        private Dictionary<int, Schedule> schedules;
        private Dictionary<int, Dependency> dependencies;
        public  LastJobChange last_job_change;

        public Job() {
            this.last_job_change = new LastJobChange();
        }

        public void setDependencies(Dictionary<int,Dependency> deps) {
            this.dependencies = deps;
        }

        public void setSchedules(Dictionary<int, Schedule> sch) {
            this.schedules = sch;
        }                

        public void setLastJobChange(string login, DateTime? when ) {
            this.last_job_change.login = login;
            this.last_job_change.change_timestamp = when == null ? DateTime.Now : when;
        }
        public DateTime? computeNextScheduleExecution() {
            return null;
        }


    }

    public struct Dependency {
        public int? job_dependency_id;
        public int? job_id;
        public int? depending_job_id;
        public int? dependency_type_id;
    }


    public struct Schedule {
        public int? job_schedule_id;
        public int? job_id;
        public int? schedule_type_id;
        public DateTime? next_execution;
    }

    public struct ScheduleType {        
        public string schedule_type_name;
        public long frequency_seconds;
        public int execution_window_seconds;
    }

    public struct DependencyType {
        public string name;
        public string description;        
    }

    public struct LastJobChange {
        public string login;
        public DateTime? change_timestamp;
    }
}
