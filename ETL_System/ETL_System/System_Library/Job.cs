using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL_System{
    [Serializable]
    public class Job : MsgAttachment {

        public int       job_id;     
        public int?      last_instance_id;        
        public int       job_type_id;
        public DateTime? last_instance_timestamp;
        public string    name;
        public string    executable_name;
        public int       max_try_count;
        public int       current_failed_count;        
        public int       delay_seconds;
        public int       latency_alert_seconds;
        public long?     data_chceckpoint;
        public DateTime? time_checkpoint;
        public string    notifiactions_list;
        public bool      is_failed;
        public bool      is_active;
        public bool      is_paused;

        public string   type_name;
        public bool     is_executing;
        public bool     is_queued;
        
        public Dictionary<int, Schedule> schedules;
        public Dictionary<int, Dependency> dependencies;
        public  LastJobChange last_job_change;

        public Job() {
            this.last_job_change = new LastJobChange();
            this.schedules = new Dictionary<int, Schedule>();
            this.dependencies = new Dictionary<int, Dependency>();
        }

        public Job(User auser) {
            this.last_job_change = new LastJobChange();
            this.schedules = new Dictionary<int, Schedule>();
            this.dependencies = new Dictionary<int, Dependency>();
            this.last_job_change.login = auser.login;
            this.last_job_change.change_timestamp = DateTime.Now;
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

    [Serializable]
    public struct Dependency {
        public int? job_dependency_id;
        public int? job_id;
        public int? depending_job_id;
        public int? dependency_type_id;
    }

    [Serializable]
    public struct Schedule {
        public int? job_schedule_id;
        public int? job_id;
        public int? schedule_type_id;
        public DateTime? next_execution;
    }

    [Serializable]
    public struct ScheduleType {        
        public string schedule_type_name;
        public long frequency_seconds;
        public int execution_window_seconds;
    }

    [Serializable]
    public struct DependencyType {
        public string name;
        public string description;        
    }

    [Serializable]
    public struct LastJobChange {
        public string login;
        public DateTime? change_timestamp;
    }
}
