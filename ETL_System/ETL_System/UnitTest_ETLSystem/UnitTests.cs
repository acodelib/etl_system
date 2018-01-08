using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETL_System;
using System.IO;

namespace UnitTest_ETLSystem {
    [TestClass]
    public class TestSystemManager {

      

        [TestMethod]
        public void testSystemStartFiles() {

            SystemManager test_system = new SystemManager();
            test_system.startSystem();

            Assert.IsTrue(File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ETLSystemConfig.etl"));
            Assert.IsTrue(File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ETLSystemLog.etl"));
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
                    Assert.AreEqual("ConnectionString:testing a connection string", input);
                }
            }

            File.Delete(target_file);
        }

        [TestMethod]
        public void checkAppDBConnectionFromFile() {
            /*
            SystemManager sm = new SystemManager();
            sm.startSystem();
            Assert.AreEqual("aa", sm.path_to_config);
            string cs = sm.readDBConnStringFromFile(sm.path_to_config);
            Assert.AreEqual("App DB Success", CoreDB.checkDBConnStringIsValid(cs));
            */
        }
    }
}
