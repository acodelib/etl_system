using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using ETL_System;
using System.Collections.Concurrent;


        
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
            
            
            SystemManager etl_system_manager = new SystemManager();
            string cp = null;            
            etl_system_manager.startSystem();
            //etl_system_manager.deployDBScript(cp);            
            Console.WriteLine(etl_system_manager.jobs_catalogue.sys_change_id.ToString());
            //string conn_str = $"Data Source = BUH0522\\SQLEXPRESS; Initial Catalog = master; User = sa; Password = Dublin22; MultipleActiveResultSets = true";
            //etl_system_manager.registerDBConnString(conn_str,null);
            //Console.WriteLine(etl_system_manager.readDBConnStringFromFile(etl_system_manager.path_to_config));




            //Console.WriteLine(CoreDB.checkDBConnStringIsValid(SystemSharedData.app_db_connstring));



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
