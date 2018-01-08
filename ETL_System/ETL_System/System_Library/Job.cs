using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL_System{

    public class Job : MsgAttachment {

        private int     _job_id;
        public int      job_id              { get { return _job_id; } }

        private int     _last_instance_id;
        public int      last_instance_id    { get { return _last_instance_id; } }

        public int job_type_id;

        public DateTime last_instance_timestamp;
        public string name;
        public string execution_path;
        public int max_try_count;
        public byte is_failed;
        public int delay_seconds;
        public int latency_alert_seconds;
        public long data_chceckpoint;
        public DateTime time_checkpoint;       
        

        //private Dictionary<string, string> parms;
    }

    public class JobRegistered : Job {
        public int job_id;
    }
}
