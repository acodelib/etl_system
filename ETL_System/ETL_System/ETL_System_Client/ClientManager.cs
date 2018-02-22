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
        //public static Guid      session_id;
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
                    //ClientManager.session_id = r.session_channel;
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
                //parent.lv_JobsList.Items[0].Selected = true;
            }
        }
        

        public void initETLJobDefinitionTab() {
            //1.Request Data From server
            Message m = new Message();
            m.msg_type = MsgTypes.REQUEST_JOB;
            m.header["user"] = this_user;
            m.session_channel = this_user.this_sessions_id;
            m.body = parent.lv_JobsList.SelectedItems[0].Text;
            Message r;
            Job j;
            try {
                r = Message.getMessageFromBytes(message_engine.sendMessageAndListenForReply(m.encodeToBytes()));
                if (r.msg_type == MsgTypes.REPLY_SUCCESS) {
                    this.refreshTasksListRoutine((List<string>)r.header["jobs_list"]);

                    //2.Populate controls with data from message
                    j = (Job)r.attachement;
                    parent.tb_Name.Text             = j.name;
                    parent.tb_Executable.Text       = j.executable_name;
                    parent.tb_MaxAttempts.Text      = j.max_try_count.ToString();
                    parent.tb_LatencyAlert.Text     = j.latency_alert_seconds.ToString();
                    parent.tb_DelaySecs.Text        = j.delay_seconds.ToString();
                    parent.tb_Notifications.Text    = j.notifiactions_list;
                    parent.tb_IsFailed.Text         = j.is_failed == false ? "NO" : "YES";
                    parent.tb_FailedAttempts.Text   = j.current_failed_count.ToString();
                    parent.tb_isActive.Text         = j.is_active == true ? "YES" : "NO";
                    parent.tb_IsPaused.Text         = j.is_paused == true ? "YES" : "NO";
                    parent.cb_Type.SelectedIndex    = j.job_type_id == 1 ? 0 : 1;

                    parent.tb_Who.Text              = j.last_job_change.login;
                    parent.tb_When.Text             = j.last_job_change.change_timestamp.ToString();

                    parent.tb_IsFailed.BackColor    = parent.tb_IsFailed.Text == "NO" ? System.Drawing.Color.Chartreuse : System.Drawing.Color.Red;
                    parent.btn_Activation.Text      = parent.tb_isActive.Text == "NO" ? "Activate" : "Deactivate";
                    parent.btn_Pausing.Text         = parent.tb_IsPaused.Text == "NO" ? "Pause" : "Un-Pause";


                }

            }
            catch (Exception e) {
                MessageBox.Show($"There was a communications problem.\nOriginal system error:{e.Message}");
            }           
          
            //3.Apply visual rules
        }
        public void createJob() {
            //1.first check to see that job is not in the view
            string ln = parent.tb_Name.Text;
            Job j;

            //for (int i = 0 ; i < parent.lv_JobsList.Items.Count;i++) {
            foreach(ListViewItem i in parent.lv_JobsList.Items) { 
                if (i.SubItems[0].Text == ln) {
                    MessageBox.Show("Name is already used by a job.");
                    return;
                }
            }
            //2.make sure the user wants to do this action
            if (MessageBox.Show("A new job will be created on the server. Continue?", "", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            //3.instantiate and fill in a job
            try {
                j = new Job(this_user) {
                    job_type_id = parent.cb_Type.Text == "Schedule" ? 1 : 2,
                    last_instance_id = 0,
                    type_name = parent.cb_Type.Text,
                    name = ln,
                    executable_name = parent.tb_Executable.Text,
                    max_try_count = Int32.Parse(parent.tb_MaxAttempts.Text),
                    delay_seconds = Int32.Parse(parent.tb_DelaySecs.Text),
                    latency_alert_seconds = Int32.Parse(parent.tb_LatencyAlert.Text),
                    notifiactions_list = parent.tb_Notifications.Text,
                    is_failed = false,
                    is_active = parent.tb_isActive.Text == "YES" ? true : false,
                    is_paused = parent.tb_IsPaused.Text == "YES" ? true : false,
                };
            } catch (System.FormatException e) {
                MessageBox.Show("Please make sure that #Max Attempts,Latency Alert Seconds and Delay Seconds are valid numbers.");
                return;
            }
            //4.attach to message and send
            Message m = new Message(j);
            m.msg_type = MsgTypes.MGMT_CREATE_JOB;
            m.header["user"] = this_user;
            m.session_channel = this_user.this_sessions_id;
            //m.session_channel = ClientManager.session_id;
            try {
                Message r = Message.getMessageFromBytes(message_engine.sendMessageAndListenForReply(m.encodeToBytes()));
                if (r.msg_type == MsgTypes.REPLY_SUCCESS) {
                    //5.refresh list view
                    this.refreshTasksListRoutine((List<string>)r.header["jobs_list"]);
                }
            }
            catch (Exception e) {
                MessageBox.Show($"There was a communications problem.\nOriginal system error:{e.Message}");
            }
        }

        //---------------------------------------Static------------------------------------------------
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
