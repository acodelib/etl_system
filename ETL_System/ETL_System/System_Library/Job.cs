using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL_System{

    public class Job : MsgAttachment {

        public int      job_id;     
        public int      last_instance_id;        
        public int      job_type_id;
        public DateTime last_instance_timestamp;
        public string   name;
        public string   executable_name;
        public int      max_try_count;
        public int      is_failed;
        public int      delay_seconds;
        public int      latency_alert_seconds;
        public long     data_chceckpoint;
        public DateTime time_checkpoint;

        public string   type_name;
        
        private Dictionary<int, string> schecdules;
    }

}
