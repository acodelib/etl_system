using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ETL_System {
    [Serializable]
    public class JobsQueueDisplay : MsgAttachment {

        public DataTable data;

        public JobsQueueDisplay() { }

        public JobsQueueDisplay(int sys_change_id, DataTable data) {
            this.sys_change_id = sys_change_id;
            this.data = data;
        }
    }
}