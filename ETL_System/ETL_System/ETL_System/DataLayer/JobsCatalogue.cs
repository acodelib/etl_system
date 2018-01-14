using System;
//using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections.Generic;

namespace ETL_System {
    public class JobsCatalogue {

        //=========================================FIELDS

        private Dictionary<string,ETL_System.Job> jobs_collection;        
        private int _sys_change_id;
        public int sys_change_id { get; }
        private CoreDB data_layer;
        //=========================================CONSTRUCTORs
        public JobsCatalogue(CoreDB data_layer) {
            jobs_collection = new Dictionary<string, Job>();
            this.sys_change_id = this.getCurrentSysChangeId();
            data_layer.fillJobsCollection(this.jobs_collection);
        }

        //========================================METHODS
        private int getCurrentSysChangeId() {
            //get last change from table JobChanges
            string sql = "SELECT max(sys_change_id) FROM ETL_SYSTEM.dbo.JobChanges";
            return CoreDB.runIntCustomScalarSQLCommand(sql, SystemSharedData.app_db_connstring);
            data_layer.fillJobsCollection(this.jobs_collection);
        }

        private int refreshJobsCollection() {
            data_layer.fillJobsCollection(this.jobs_collection);
            return 1;
        }
    }
}
