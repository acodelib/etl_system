using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL_System {
    class SystemSharedData {

        public static ConcurrentDictionary<string, string> clients_table;        

        public static string app_db_connstring;

        public static Dictionary<int, ScheduleType> schedule_types;
        public static Dictionary<int, DependencyType> dependency_types;
    }
}
