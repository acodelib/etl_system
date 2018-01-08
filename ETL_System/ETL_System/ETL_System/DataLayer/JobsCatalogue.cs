using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL_System {
    public class JobsCatalogue {
        
        //=========================================FIELDS
        private ConcurrentBag<ETL_System.Job> jobs_collection;
        private int _sys_change_id;
        public int sys_change_id { get; }

        //=========================================CONSTRUCTORs
        public JobsCatalogue() {
            jobs_collection = new ConcurrentBag<Job>();
            this.sys_change_id = this.getCurrentSysChangeId();
        }

        //========================================METHODS
        private int getCurrentSysChangeId() {
            //get last change from table JobChanges
            string sql = "SELECT max(sys_change_id) FROM ETL_SYSTEM.dbo.JobChanges";
            return CoreDB.runCustomScalarSQLCommand(sql, SystemSharedData.app_db_connstring);
        }

    }
}
