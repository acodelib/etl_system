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
            string dp = null;
            etl_system_manager.startSystem();
            Job j = new Job {
                last_instance_id = 11,
                job_type_id = 1,
                sys_change_id = 12,
                last_instance_timestamp = new DateTime(2018, 01, 12),
                name = "the_bobo",
                executable_name = "bob.bat",
                max_try_count = 2,
                is_failed = false,
                delay_seconds = 12,
                latency_alert_seconds = 3,
                data_chceckpoint = 31231,
                time_checkpoint = new DateTime(2018, 01, 22),
                notifiactions_list = "andrei.gurguta@veeam.com",
                type_name = "Schedule"
            };
            etl_system_manager.jobs_catalogue.addNewJob(j);

            //string conn_str = $"Data Source = BUH0522\\SQLEXPRESS; Initial Catalog = master; User = sa; Password = Dublin22; MultipleActiveResultSets = true";
            //etl_system_manager.registerDBConnString(conn_str,null);
         //   etl_system_manager.deployDBScript(cp,dp);
            //etl_system_manager.deployDBScript(cp + "Scripts\\DB_Creation.sql");
            //etl_system_manager.registerExecutionFolderToConfig("C:\\thefolder", null);
            //etl_system_manager.addOrChangeConfigToFile("ExecutionPath", "C:\\awwk\\Pers\\pers_dev\\p_ETL\\TestJobs", null);
            //etl_system_manager.deployDBScript(cp);            
            //Console.WriteLine(etl_system_manager.jobs_catalogue.sys_change_id.ToString());

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
