using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using ETL_System.ServerComms;
using System.Collections.Concurrent;
using ETL_System.Library;

    
    
    namespace ETL_System
{
    
    public static class Program  {

        static ConcurrentDictionary<string,string> clients_table;


        static void Main() {


            Console.WriteLine("salut");
            MsgHandler the_msg_handler = new MsgHandler(ref Program.clients_table);
            
            
                        

            /*
            //start the service
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new WinService()
            };
            ServiceBase.Run(ServicesToRun);
            */

        

        }
    }
}
