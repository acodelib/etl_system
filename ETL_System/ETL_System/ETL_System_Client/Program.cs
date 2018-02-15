using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ETL_System
{
    public static class Program
    {

        static ClientManager manager;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {

            ClientManager.schedulte_types = new Dictionary<int, ScheduleType>();
            ClientManager.dependency_types = new Dictionary<int, DependencyType>();
            ClientManager.user_roles = new Dictionary<int, string>();


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainWindow main_window = new MainWindow();
            Application.Run(main_window);
        }
    }
}
