using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL_System.Library{

    public class Job {
        public string name { get; set; }
        public string display_name { get; set; }
        public string folder_location { get; set; }
        public string executatble_name { get; set; }

        private Dictionary<string, string> parmas;
    }

    public class JobRegistered : Job {
        public int job_id;
    }
}
