using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETL_System;
using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;

namespace UnitTest_ETLSystem {
    [TestClass]
    public class TestSystemManager {

      

        [TestMethod]
        public void testSystemStartFiles() {

            SystemManager test_system = new SystemManager();
            test_system.startSystem();

            Assert.IsTrue(File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"ETLSystemConfig.etl"));
            Assert.IsTrue(File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"ETLSystemLog.etl"));
        }

        [TestMethod]
        public void connStringRegister() {
            SystemManager test_system = new SystemManager();
            String target_file = AppDomain.CurrentDomain.BaseDirectory + @"\UnitTesting.etl";

           StreamWriter fileobj = File.CreateText(target_file);
           fileobj.Close();

            test_system.registerDBConnString("testing a connection string", target_file);

            using (StreamReader file_reader = File.OpenText(target_file)) {
                string input = null;
                while ((input = file_reader.ReadLine()) != null) {
                    Assert.AreEqual("ConnectionString~testing a connection string", input);
                }
            }

            File.Delete(target_file);
        }

        [TestMethod]
        public void testJobManagementCommands() {
            string config_file  = @"C:\awwk\Pers\pers_dev\p_ETL\ETL_System\ETL_System\ETL_System\bin\Debug\ETLSystemConfig.etl";
            string log_file     = @"C:\awwk\Pers\pers_dev\p_ETL\ETL_System\ETL_System\ETL_System\bin\Debug\ETLSystemLog.etl";
            SystemManager etl_system_manager = new SystemManager(config_file,log_file);
            
            etl_system_manager.startSystem();

            User u = new User { user_id = 1, login = "admin" };
            Message m = new Message();
            m.msg_type = MsgTypes.MGMT_DELETE_JOB;
            m.body = "the_test_1";
            etl_system_manager.executeMgmtCommand(m, u);
            m.body = "the_test_2";
            etl_system_manager.executeMgmtCommand(m, u);

            Assert.AreEqual(0, 
                            CoreDB.runIntCustomScalarSQLCommand("Select isnull(count(name),0) from ETL_System.dbo.Jobs WHERE name in ('the_test_1','the_test_2')", 
                                                         "Data Source = BUH0522\\SQLEXPRESS; Initial Catalog = master; User = sa; Password = Dublin22; MultipleActiveResultSets = true")
                           );
            
            Job j = new Job() {
                last_instance_id = 11,
                job_type_id = 1,
                sys_change_id = null,
                last_instance_timestamp = null, // new DateTime(2018, 01, 12),
                name = "the_test_1",
                executable_name = "bob.bat",
                max_try_count = 2,
                current_failed_count = 1,
                is_failed = false,
                delay_seconds = 12,
                latency_alert_seconds = 3,
                data_chceckpoint = null, //31231,
                time_checkpoint = null, // new DateTime(2018, 01, 22),
                notifiactions_list = "andrei.gurguta@veeam.com",
                type_name = "Schedule"
            };

            m.msg_type = MsgTypes.MGMT_CREATE_JOB;
            m.attachement = j;
            etl_system_manager.executeMgmtCommand(m, u);
            Assert.AreEqual(1,
                           CoreDB.runIntCustomScalarSQLCommand("Select isnull(count(name),0) from ETL_System.dbo.Jobs WHERE name in ('the_test_1')",
                                                        "Data Source = BUH0522\\SQLEXPRESS; Initial Catalog = master; User = sa; Password = Dublin22; MultipleActiveResultSets = true")
                          );
            //simulate incomming message:
                    MemoryStream ms = new MemoryStream();
                    BinaryFormatter b = new BinaryFormatter();
                    b.Serialize(ms, j);
                    ms.Position = 0;
                    j = (Job)b.Deserialize(ms);
            j.is_failed = true;
            j.name = "the_test_2";
            m.msg_type = MsgTypes.MGMT_UPDATE_JOB;
            m.attachement = j;
            etl_system_manager.executeMgmtCommand(m, u);

           Assert.AreEqual(1,
                         CoreDB.runIntCustomScalarSQLCommand("Select isnull(count(name),0) from ETL_System.dbo.Jobs WHERE name in ('the_test_2')",
                                                      "Data Source = BUH0522\\SQLEXPRESS; Initial Catalog = master; User = sa; Password = Dublin22; MultipleActiveResultSets = true")
                        );

            m.msg_type = MsgTypes.MGMT_DELETE_JOB;
            m.body = "the_test_1";
            etl_system_manager.executeMgmtCommand(m, u);
            m.body = "the_test_2";
            etl_system_manager.executeMgmtCommand(m, u);

            Assert.AreEqual(0,
                            CoreDB.runIntCustomScalarSQLCommand("Select isnull(count(name),0) from ETL_System.dbo.Jobs WHERE name in ('the_test_1','the_test_2')",
                                                         "Data Source = BUH0522\\SQLEXPRESS; Initial Catalog = master; User = sa; Password = Dublin22; MultipleActiveResultSets = true")
                           );
           

        }
    }
}
