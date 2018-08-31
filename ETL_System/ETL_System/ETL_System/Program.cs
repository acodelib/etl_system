using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using ETL_System;
using System.Collections.Concurrent;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ETL_System
{

    
    public static class Program  {       


        /// <summary>
        /// 
        /// </summary>
        static void Main() {

#if DEBUG

            //WinService ETLSystemService = new WinService();
            //ETLSystemService.runInDebug(); 

            try {
                SystemManager etl_system_manager = new SystemManager();
                etl_system_manager.startSystem();

               
                etl_system_manager.startQueueing();
                etl_system_manager.startETLWorkers();

            }catch(Exception e) {
                LogManager.writeErrorToLog("Fatal Error! System shuts down with the message: "+ e.Message, AppDomain.CurrentDomain.BaseDirectory + "ETLSystemLog.etl");
            }
           



#else

            start the service
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new WinService()
            };
            ServiceBase.Run(ServicesToRun);

#endif



        }
    }
}
