using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using ETL_System;
using System.Collections.Concurrent;
using ETL_System.Library;

        
namespace ETL_System
{

    
    public static class Program  {       


        static void Main() {

#if DEBUG

            WinService ETLSystemService = new WinService();
            ETLSystemService.runInDebug(); 
            
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
