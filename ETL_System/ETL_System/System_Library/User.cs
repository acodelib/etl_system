using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL_System {
    [Serializable]
    public class User {
        public int? user_id;
        public string login;
        public string role;
        public Guid this_sessions_id;
        public DateTime login_timestamp;
    }
}
