using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETL_System;
using System.Data;

namespace ETL_System {
    public class JobsCatalogueDisplay : MsgAttachment {
        
        public DataTable data;

        public JobsCatalogueDisplay() { }

        public JobsCatalogueDisplay(int sys_change_id, DataTable data) {
            this.sys_change_id = sys_change_id;
            this.data = data;
        }
    }
}
