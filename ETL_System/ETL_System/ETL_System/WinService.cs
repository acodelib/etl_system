using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using ETL_System;

namespace ETL_System
{
    public partial class WinService : ServiceBase
    {
        public WinService() {
            InitializeComponent();
        }


        public void runInDebug() {
            this.OnStart(null);
        }

        protected override void OnStart(string[] args) {

            SystemManager etl_system_manager = new SystemManager();

            etl_system_manager.startSystem();            
        }

        protected override void OnStop() {
        }
    }
}
