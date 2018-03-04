using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ETL_System {

    [Serializable]
    public class DependencyCatalogueDisplay : MsgAttachment {

        public DataTable data;

        public DependencyCatalogueDisplay() { }

        public DependencyCatalogueDisplay(int sys_change_id, DataTable data) {
            this.sys_change_id = sys_change_id;
            this.data = data;
        }
    }
}
