using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL_System {
    public class ClientSystemManager {
        public MsgEngine message_engine;

        public ClientSystemManager() {
            this.message_engine = new MsgEngine();
        }
    }
}
