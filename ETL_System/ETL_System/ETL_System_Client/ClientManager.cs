using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace ETL_System {
    public class ClientManager {

        //======================================= FIELDS ===========================================
        
        public static string    pass;
        public static string    server_ip;
        public static Guid      session_id;
        public static bool      login_active;
        public static User      this_user;

        public static Dictionary<int, ScheduleType> schedulte_types;
        public static Dictionary<int, DependencyType> dependency_types;
        public static Dictionary<int, string> user_roles;
        

        public MsgEngine message_engine;
        private MainWindow parent;

        //======================================= CONSTRUCTORS =======================================
        public ClientManager(MainWindow parent) {
            this.message_engine = new MsgEngine();
            this.parent = parent;            
        }



        //======================================= METHODS =============================================

        public void loginProcedure(String user, String pass,string Ip, int port) {
            this.message_engine.initialiseMsgEngine(Ip, port);
            Message m = new Message();
            string hash_pass = GetSHA1HashData(pass);
            m.msg_type = MsgTypes.TRY_CONNECT;
            m.body = $"User:{user};Pass:{hash_pass}";
            try {
                Message r = Message.getMessageFromBytes(this.message_engine.sendMessageAndListenForReply(m.encodeToBytes()));
                if (r.msg_type == MsgTypes.REPLY_SUCCESS) {
                    ClientManager.login_active = true;
                    this_user = (User)r.header["user"];                    
                    parent.tslb_Status.Text = $"Connected to: {Ip} login:{user}";
                    parent.tslb_Response.Visible = true;
                    parent.tslb_Response.Text = $"Last server reply:{r.body} @({r.timestamp})";
                    this.refreshTasksListRoutine((List<string>)r.header["jobs_list"]);
                    this.parent.tp_Definition.Select();
                }
            }catch(Exception e) {
                MessageBox.Show($"There was a communications problem.\nOriginal system error:{e.Message}");
            }
                
        }

        public void requestCatalogue() {
            Message m = new Message();
            m.msg_type = MsgTypes.REQUEST_JOB_CATALOGUE_DISPLAY;
            m.header["user"] = this_user;
            m.session_channel = this_user.this_sessions_id;
            JobsCatalogueDisplay c;
            try {
                Message r = Message.getMessageFromBytes(message_engine.sendMessageAndListenForReply(m.encodeToBytes()));
                if(r.msg_type == MsgTypes.REPLY_SUCCESS) {
                    c = (JobsCatalogueDisplay)r.attachement;
                    parent.dgv_Catalogue.DataSource = c.data;
                    this.refreshTasksListRoutine((List<string>)r.header["jobs_list"]);                    
                }
            }catch(Exception e) {
                MessageBox.Show($"There was a communications problem.\nOriginal system error:{e.Message}");
            }
}

        public void refreshTasksListRoutine(List<string> jobs) {
            if (jobs != null) {
                parent.lv_JobsList.Clear();
                foreach (string s in jobs)
                    parent.lv_JobsList.Items.Add(s);
                parent.lv_JobsList.Items[0].Selected = true;
            }
        }

        public static string GetSHA1HashData(string data) {
            SHA1 sha1 = SHA1.Create();
            byte[] hashData = sha1.ComputeHash(Encoding.Default.GetBytes(data));
            StringBuilder returnValue = new StringBuilder();

            for (int i = 0; i < hashData.Length; i++) {
                returnValue.Append(hashData[i].ToString());
            }


            return returnValue.ToString();
        }

    }
}
