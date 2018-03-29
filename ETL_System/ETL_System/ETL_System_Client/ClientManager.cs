using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using Microsoft.Msagl.Core.Layout;
using Microsoft.Msagl.Core.Geometry;

namespace ETL_System {
    public static class ExtensionMethods {
        public static void DoubleBuffered(this DataGridView dgv, bool setting) {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }
    }
    public class ClientManager {

        //======================================= FIELDS ===========================================

        public static string pass;
        public static string server_ip;
        //public static Guid      session_id;
        public static bool login_active;
        public static User this_user;

        public static Dictionary<int?, ScheduleType> schedulte_types;
        public static Dictionary<int?, DependencyType> dependency_types;
        public static Dictionary<int, string> user_roles;
        public static Dictionary<int, string> jobs;


        public MsgEngine message_engine;
        private MainWindow parent;
        DataTable t;

        //======================================= CONSTRUCTORS =======================================
        public ClientManager(MainWindow parent) {
            this.message_engine = new MsgEngine();
            this.parent = parent;
        }



        //======================================= METHODS =============================================

        public void loginProcedure(String user, String pass, string Ip, int port) {
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
                    user_roles = (Dictionary<int, string>)r.header["user_roles"];
                    schedulte_types = (Dictionary<int?, ScheduleType>)r.header["schedule_types"];
                    dependency_types = (Dictionary<int?, DependencyType>)r.header["dependency_types"];
                    parent.tslb_Status.Text = $"Connected to: {Ip} login:{user}";
                    parent.tslb_Response.Visible = true;
                    parent.tslb_Response.Text = $"Last server reply:{r.body} @({r.timestamp})";
                    this.refreshTasksListRoutine((Dictionary<int, string>)r.header["jobs_list"]);
                    this.parent.tp_Definition.Select();
                    if(ClientManager.this_user.role != "admin" && parent.tc_Main.Controls.Contains(parent.tp_Admin))
                        this.parent.tc_Main.Controls.Remove(parent.tp_Admin);
                    if (ClientManager.this_user.role == "admin" && !parent.tc_Main.Controls.Contains(parent.tp_Admin))
                        this.parent.tc_Main.Controls.Add(parent.tp_Admin);
                }
            } catch (Exception e) {
                MessageBox.Show($"There was a communications problem.\nOriginal system error:{e.Message}");
            }

        }

        public void logoutProcedure() {
            Message m = new Message();
            m.msg_type = MsgTypes.TRY_DISCONECT;
            m.header["user"] = this_user;
            m.session_channel = this_user.this_sessions_id;
            try {
                Message r = Message.getMessageFromBytes(message_engine.sendMessageAndListenForReply(m.encodeToBytes()));
                if(r.msg_type == MsgTypes.REPLY_SUCCESS) {
                    ClientManager.login_active = false;

                    // and now some cleanup:
                    foreach (Control c in parent.gb_General.Controls) {
                        if (c.GetType() == typeof(TextBox))
                            c.Text = "";
                    }
                    foreach (Control c in parent.gb_Status.Controls) {
                        if (c.GetType() == typeof(TextBox))
                            c.Text = "";
                    }
                    foreach (Control c in parent.gb_Changes.Controls) {
                        if (c.GetType() == typeof(TextBox))
                            c.Text = "";
                    }
                    this.cleanSchedules();
                    this.cleanDependencies();
                    parent.lv_JobsList.Items.Clear();
                    parent.dgv_Catalogue.DataSource = null;
                    parent.dgv_Queue.DataSource = null;
                    parent.tslb_Status.Text = "Disconected";
                    parent.tslb_Response.Visible = false;
                    parent.gViewer.Graph = null; 

                }
                else {
                    MessageBox.Show("Problems during logout. Try again.");
                }
            }catch(Exception e) {
                MessageBox.Show("There was a communications problem. Try again.");
            }
        }

        private void adminPageRoutine(Message r) {
            DataTable au;
            //populate users list view
            au = (DataTable)r.header["admin_users"];
            foreach (DataRow rw in au.Rows) {
                ListViewItem i = new ListViewItem(((int)rw["user_id"]).ToString());
                i.SubItems.Add((string)rw["login"]);
                i.SubItems.Add((int)rw["role_id"] == 1 ? "admin" : "user");
                i.SubItems.Add(((bool)rw["is_active"]).ToString());
                i.SubItems.Add((string)rw["status"]);
                parent.lv_Users.Items.Add(i);
            }
        }


        public void requestAdminData() {
            
            Message m = new Message();
            m.msg_type = MsgTypes.ADMIN_REQUEST;
            m.header["user"] = this_user;
            m.session_channel = this_user.this_sessions_id;

            try {
                Message r = Message.getMessageFromBytes(message_engine.sendMessageAndListenForReply(m.encodeToBytes()));
                if(r.msg_type == MsgTypes.REPLY_SUCCESS) {
                    
                    parent.lv_Users.Items.Clear();
                    this.adminPageRoutine(r);
                    parent.tb_ConnectionString.Text = ((string)r.header["app_db_conn_string"]).ToString();
                    parent.tb_JobsFolder.Text       = ((string)r.header["job_folder"]).ToString();
                    parent.tb_NoOfWorkers.Text      = ((int)r.header["no_of_workers"]).ToString();
                    parent.tb_WorkersFrequency.Text = ((int)r.header["worker_fetch_frequency"]).ToString();
                    parent.tb_ScanFrequency.Text    = ((int)r.header["queue_scan_frequency"]).ToString();
                    parent.tb_EmailUser.Text        = (string)r.header["mail_user_name"];
                    parent.tb_EmailPass.Text        = (string)r.header["mail_password"];
                    parent.tb_Smtp.Text             = (string)r.header["mail_smtp"];
                    parent.tb_Port.Text             = ((int)r.header["mail_port"]).ToString();
                }

                else
                    MessageBox.Show(r.body);
            } catch (Exception e) {
                MessageBox.Show($"Problems while loading the Catalogue.\nOriginal system error:{e.Message}");
            }
        }

        public void adminAddUser() {            
            Message m = new Message();


            //1.make sure all details are filled in
            if (parent.tb_NewUserName.Text == "" || parent.tb_NewUserPass.Text == "" || parent.cb_NewUserRole.Text == "") {
                MessageBox.Show("Please fill in New User details before creating a new user");
                return;
            }

            //2.check to see user name does not already exist
            foreach(ListViewItem i in parent.lv_Users.Items) {
                if(i.SubItems[1].Text == parent.tb_NewUserName.Text) {
                    MessageBox.Show("User Login NAME already exists. Please use a different one.");
                    return;
                }
            }

            //3.make sure the user wants to do this action
            if (MessageBox.Show("A new new user will be created. Continue?", "", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            //3.create user and add to message
            User u = new User() {
                login = parent.tb_NewUserName.Text,
                role = parent.cb_NewUserRole.Text,
            };
            m.header["new_user"] = u;
            m.header["hidden_pass"] = GetSHA1HashData(parent.tb_NewUserPass.Text);
            m.body = "new_user";
            m.msg_type = MsgTypes.ADMIN_COMMAND;
            m.header["user"] = this_user;
            m.session_channel = this_user.this_sessions_id;

            //4.Send message
            try {
                Message r = Message.getMessageFromBytes(message_engine.sendMessageAndListenForReply(m.encodeToBytes()));
                if (r.msg_type == MsgTypes.REPLY_SUCCESS) {
                    parent.lv_Users.Items.Clear();
                    this.adminPageRoutine(r);
                }
                else
                    MessageBox.Show(r.body);
            }
            catch (Exception e) {
                MessageBox.Show($"Problems while loading the Catalogue.\nOriginal system error:{e.Message}");
            }

        }

        public void adminDeleteUser() {
            List<int> users_to_delete = new List<int>();
            Message m = new Message();
            //1.first check if any item is selected and a job is created
            if (parent.lv_Users.CheckedItems.Count == 0) {
                MessageBox.Show("Please use the checkboxes for deleting users");
                return;
            }

            //make sure isn't connected


            //2.loop through items and fill in the Users list
            foreach (ListViewItem i in parent.lv_Users.Items) {
                if (i.Checked) {
                    if (i.SubItems[4].Text == "connected") {
                        MessageBox.Show("Can't delete users while they are connected. Try again.");
                        return;
                    }
                    users_to_delete.Add(Int32.Parse(i.Text));
                }
            }

            //3.add list and prepare message                        
            m.msg_type = MsgTypes.ADMIN_COMMAND;
            m.body = "delete_user";            
            m.header["user_delete_list"] = users_to_delete;
            m.header["user"] = this_user;
            m.session_channel = this_user.this_sessions_id;

            //4.Send message
            try {
                Message r = Message.getMessageFromBytes(message_engine.sendMessageAndListenForReply(m.encodeToBytes()));
                if (r.msg_type == MsgTypes.REPLY_SUCCESS) {
                    parent.lv_Users.Items.Clear();
                    this.adminPageRoutine(r);
                }
                else
                    MessageBox.Show(r.body);
            }
            catch (Exception e) {
                MessageBox.Show($"Problems while loading the Catalogue.\nOriginal system error:{e.Message}");
            }
        }

        public void adminUpdateConfigs() {
            
            Message m = new Message();
            //1.first check if all items are filled in
            foreach(Control c in parent.gb_Configs.Controls)
            if (c.GetType() == typeof(TextBox) && c.Text == "")            {
                MessageBox.Show("Please fill in all text boxes in the Configurations group");
                return;
            }
            if (MessageBox.Show("Configurations will be updated on the server. Continue?", "", MessageBoxButtons.YesNo) == DialogResult.No)
                return;


            //2.add header and prepare message
            m.msg_type = MsgTypes.ADMIN_COMMAND;
            m.body = "update_configurations";
            m.header["app_db_conn_string"]      = parent.tb_ConnectionString.Text;
            m.header["job_folder"]              = parent.tb_JobsFolder.Text;
            m.header["no_of_workers"]           = parent.tb_NoOfWorkers.Text;
            m.header["worker_fetch_frequency"]  = parent.tb_WorkersFrequency.Text;
            m.header["queue_scan_frequency"]    = parent.tb_ScanFrequency.Text;
            m.header["user"] = this_user;
            m.session_channel = this_user.this_sessions_id;

            //4.Send message
            try            {
                Message r = Message.getMessageFromBytes(message_engine.sendMessageAndListenForReply(m.encodeToBytes()));
                if (r.msg_type == MsgTypes.REPLY_SUCCESS)
                {
                    parent.lv_Users.Items.Clear();
                    this.adminPageRoutine(r);
                }
                else
                    MessageBox.Show(r.body);
            }
            catch (Exception e)  {
                MessageBox.Show($"Problems while loading the Catalogue.\nOriginal system error:{e.Message}");
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
                if (r.msg_type == MsgTypes.REPLY_SUCCESS) {
                    c = (JobsCatalogueDisplay)r.attachement;
                    parent.dgv_Catalogue.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    t = c.data;
                    
                    //some performance optimisations:                    
                    parent.dgv_Catalogue.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;                    
                    parent.dgv_Catalogue.EditMode = DataGridViewEditMode.EditProgrammatically;
                   // parent.dgv_Catalogue.EnableHeadersVisualStyles = false;
                    parent.dgv_Catalogue.DoubleBuffered(true);

                    parent.dgv_Catalogue.DataSource = t;
                    parent.dgv_Catalogue.Columns["Job_Id"].Width = 60;
                    parent.dgv_Catalogue.Columns["Name"].Width = 190;
                    parent.dgv_Catalogue.Columns["Is Paused"].Width = 60;
                    parent.dgv_Catalogue.Columns["Is Active"].Width = 60;
                    parent.dgv_Catalogue.Columns["Next Execution"].Width =190 ;
                    parent.dgv_Catalogue.Columns["Checkpoint"].Width = 190;
                    parent.dgv_Catalogue.Columns["Is Failed"].Width = 60;
                    parent.dgv_Catalogue.Columns["Last Executed"].Width = 190;
                    parent.dgv_Catalogue.Columns["Last Duration"].Width = 80;
                    parent.dgv_Catalogue.Columns["Max Try"].Width = 60;
                    parent.dgv_Catalogue.Columns["Failed Count"].Width = 60;
                    this.refreshTasksListRoutine((Dictionary<int, string>)r.header["jobs_list"]);
                }
                else
                    MessageBox.Show(r.body);
            } catch (Exception e) {
                MessageBox.Show($"Problems while loading the Catalogue.\nOriginal system error:{e.Message}");
            }
        }

        public void requestJobHistory() {
            if (parent.lv_Jobs_selected_text == "" || parent.lv_Jobs_selected_text == null)
                return;
            parent.tb_InstanceOutput.Text = "";
           Message m = new Message();
            m.msg_type = MsgTypes.REQUEST_JOB_INSTANCES;
            m.header["user"] = this_user;
            m.session_channel = this_user.this_sessions_id;
            m.body = parent.lv_Jobs_selected_text;
            DataTable t;
            try {
                Message r = Message.getMessageFromBytes(message_engine.sendMessageAndListenForReply(m.encodeToBytes()));
                if (r.msg_type == MsgTypes.REPLY_SUCCESS) {
                    t = (DataTable)r.header["instances"];
                    parent.dgv_JobInstances.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    

                    //some performance optimisations:                    
                    parent.dgv_JobInstances.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                    parent.dgv_JobInstances.EditMode = DataGridViewEditMode.EditProgrammatically;
                    // parent.dgv_Catalogue.EnableHeadersVisualStyles = false;
                    parent.dgv_JobInstances.DoubleBuffered(true);

                    parent.dgv_JobInstances.DataSource = t;
                    parent.dgv_JobInstances.AllowUserToResizeRows = false;

                    //cleanup the view                    
                    parent.dgv_JobInstances.Columns["Instance ID"].Width = 80;
                    parent.dgv_JobInstances.Columns["Result"].Width = 60;
                    parent.dgv_JobInstances.Columns["Start@"].Width = 130;
                    parent.dgv_JobInstances.Columns["End@"].Width = 130;
                    parent.dgv_JobInstances.Columns["Duration Sec"].Width = 60;
                    parent.dgv_JobInstances.Columns["From Time Checkpoint"].Width = 130;
                    parent.dgv_JobInstances.Columns["To Time Checkpoint"].Width = 130;
                    parent.dgv_JobInstances.Columns["From Data Checkpoint"].Width = 100;
                    parent.dgv_JobInstances.Columns["To Data Checkpoint"].Width = 100;
                    parent.dgv_JobInstances.Columns["Started by"].Width = 80;
                    parent.dgv_JobInstances.Columns["Executable"].Width = 320;



                    this.refreshTasksListRoutine((Dictionary<int, string>)r.header["jobs_list"]);
                }
                else
                    MessageBox.Show(r.body);
            }
            catch (Exception e) {
                MessageBox.Show($"Problems while loading the History Catalogue.\nOriginal system error:{e.Message}");
            }
        }

        public void requestInstanceOutput() {
            if (parent.lv_Jobs_selected_text == "" || parent.lv_Jobs_selected_text == null)
                return;
            string instance_id = parent.dgv_JobInstances.Rows[parent.dgv_JobInstances.CurrentCell.RowIndex].Cells["Instance ID"].Value.ToString();
            Message m = new Message();
            m.msg_type = MsgTypes.REQUEST_JOB_INSTANCE_OUTPUT;
            m.header["user"] = this_user;
            m.session_channel = this_user.this_sessions_id;
            m.body = instance_id;
            
            try {
                Message r = Message.getMessageFromBytes(message_engine.sendMessageAndListenForReply(m.encodeToBytes()));
                if (r.msg_type == MsgTypes.REPLY_SUCCESS) {
                    parent.tb_InstanceOutput.Text = (string)r.header["output"];
                    this.refreshTasksListRoutine((Dictionary<int, string>)r.header["jobs_list"]);
                }
                else
                    MessageBox.Show(r.body);
            }
            catch (Exception e) {
                MessageBox.Show($"Problems while loading Instance Output.\nOriginal system error:{e.Message}");
            }
        }

        public void requestQueue() {
            Message m = new Message();
            m.msg_type = MsgTypes.REQUEST_JOB_QUEUE_DISPLAY;
            m.header["user"] = this_user;
            m.session_channel = this_user.this_sessions_id;
            JobsQueueDisplay c;
            try {
                Message r = Message.getMessageFromBytes(message_engine.sendMessageAndListenForReply(m.encodeToBytes()));
                if (r.msg_type == MsgTypes.REPLY_SUCCESS) {
                    c = (JobsQueueDisplay)r.attachement;
                    parent.dgv_Queue.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    t = c.data;

                    //some performance optimisations:                    
                    //parent.dgv_Queue.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                    parent.dgv_Queue.EditMode = DataGridViewEditMode.EditProgrammatically;
                    //parent.dgv_Queue.EnableHeadersVisualStyles = false;
                    parent.dgv_Queue.DoubleBuffered(true);

                    //data fill
                    parent.dgv_Queue.DataSource = t;
                    //sizing:                    
                    parent.dgv_Queue.Columns["Job ID"].Width = 80;
                    parent.dgv_Queue.Columns["Job Name"].Width = 190;
                    parent.dgv_Queue.Columns["Queued @"].Width = 190;
                    parent.dgv_Queue.Columns["Executing Since"].Width = 190;                    


                    this.refreshTasksListRoutine((Dictionary<int, string>)r.header["jobs_list"]);
                }
                else
                    MessageBox.Show(r.body);
            }
            catch (Exception e) {
                MessageBox.Show($"Problems while loading the Queue.\nOriginal system error:{e.Message}");
            }
        }

        public DataTable requestDependencyCatalogue() {
            Message m = new Message();
            m.msg_type = MsgTypes.REQUEST_DEPENDENCY_CATALOGUE;
            m.header["user"] = this_user;
            m.session_channel = this_user.this_sessions_id;
            DependencyCatalogueDisplay c;
            try {
                Message r = Message.getMessageFromBytes(message_engine.sendMessageAndListenForReply(m.encodeToBytes()));
                if (r.msg_type == MsgTypes.REPLY_SUCCESS) {
                    c = (DependencyCatalogueDisplay)r.attachement;                                        
                    this.refreshTasksListRoutine((Dictionary<int, string>)r.header["jobs_list"]);
                    return c.data;
                }else {
                    MessageBox.Show(r.body);
                }
            }
            catch (Exception e) {
                MessageBox.Show($"There was a communications problem.\nOriginal system error:{e.Message}");
            }
            return null;
        }

        public void rederDependencyGraphView(string selected_item) {
            DataTable local_table = this.requestDependencyCatalogue();
            Graph graph = new Graph("graph");
            graph.Directed = true;
            graph.Attr.LayerDirection = LayerDirection.LR;
            string selected_job_name;
            int selected_job_id = 0;
            

            //1.Get the id of the selected job in lv_jobs
            //selected_job_name = parent.lv_JobsList.SelectedItems[0].Text;
            selected_job_name = selected_item;
            foreach (int key in ClientManager.jobs.Keys)
                if (jobs[key] == selected_job_name) {
                    selected_job_id = key;
                    break;
            }
            try {
                if (parent.cb_RenderType.Text == "All") {
                    leftRecursiveScanAndRender(local_table, selected_job_id, 0, Int32.Parse(parent.nud_Depth.Value.ToString()), parent.gViewer, graph);
                    rightRecursiveScanAndRender(local_table, selected_job_id, 0, Int32.Parse(parent.nud_Depth.Value.ToString()), parent.gViewer, graph);
                }

                if (parent.cb_RenderType.Text == "Only Left") {
                    leftRecursiveScanAndRender(local_table, selected_job_id, 0, Int32.Parse(parent.nud_Depth.Value.ToString()), parent.gViewer, graph);
                }

                if (parent.cb_RenderType.Text == "Only Right") {
                    rightRecursiveScanAndRender(local_table, selected_job_id, 0, Int32.Parse(parent.nud_Depth.Value.ToString()), parent.gViewer, graph);
                }

                parent.gViewer.Graph = graph;
            }catch(Exception e) {
                MessageBox.Show($"The system encoutered a problem. Original system error:{e.Message}");
            }
        }

        private void leftRecursiveScanAndRender (DataTable tbl, int job_id, int this_depth,int max_depth,GViewer gViewer, Graph graph) {
            DataRow[] rows = tbl.Select($"job_id = '{job_id}'");
            string right_name = jobs[job_id];
            string left_name;
            
            //anchor:
            if (rows.Length < 1 || this_depth >= max_depth)
                return;

            //base set;
            this_depth++;


            for (int i = 0;i< rows.Length; i++) {
                left_name = jobs[(int)rows[i]["depending_job_id"]];
                graph.AddEdge(left_name,right_name);                
                leftRecursiveScanAndRender(tbl, (int)rows[i]["depending_job_id"], this_depth, max_depth, gViewer, graph);
            }
        }

        private void rightRecursiveScanAndRender(DataTable tbl, int job_id, int this_depth, int max_depth, GViewer gViewer, Graph graph) {
            DataRow[] rows = tbl.Select($"depending_job_id = '{job_id}'");
            string right_name;
            string left_name = jobs[job_id];
            int target_job_id;

            //anchor:
            if (rows.Length < 1 || this_depth >= max_depth)
                return;

            //base set;
            this_depth++;


            for (int i = 0; i < rows.Length; i++) {
                target_job_id = (int)rows[i]["job_id"];
                right_name = jobs[target_job_id];
                graph.AddEdge( left_name,right_name);                
                rightRecursiveScanAndRender(tbl, target_job_id, this_depth, max_depth, gViewer, graph);
            }
        }

        public void refreshTasksListRoutine(Dictionary<int, string> jobs) {
            if (jobs != null) {
                if (parent.lv_JobsList.SelectedItems.Count > 0) {                
                    parent.lv_Jobs_selection_cycle = parent.lv_JobsList.SelectedItems[0].Text;
                }
                parent.lv_JobsList.Items.Clear();
                parent.cb_DepJobs.Items.Clear();                
                foreach (int id in jobs.Keys) {
                    ListViewItem l = new ListViewItem(jobs[id]);
                    
                    parent.lv_JobsList.Items.Add(l);
                    parent.cb_DepJobs.Items.Add(jobs[id]);
                }
                ClientManager.jobs = jobs;
                if (parent.lv_Jobs_selection_cycle != "Xstarted")
                    foreach (ListViewItem i in parent.lv_JobsList.Items)
                        if (i.Text == parent.lv_Jobs_selection_cycle) {
                            parent.lv_Jobs_selection_cycle = "Xexit";
                            i.Focused = true;
                            i.Selected = true;
                            i.Focused = true;
                        }                
            }
        }

        public void cleanSchedules() {
            parent.lv_Schedules.Items.Clear();

        }

        public void cleanDependencies() {
            parent.lv_Dependencies.Items.Clear();
        }

        public void initETLJobDefinitionTab() {
            //1.Request Data From server
            Message m = new Message();
            m.msg_type = MsgTypes.REQUEST_JOB;
            m.header["user"] = this_user;
            m.session_channel = this_user.this_sessions_id;            
            m.body = parent.lv_Jobs_selected_text;
            Message r;
            Job j;
            try {
                r = Message.getMessageFromBytes(message_engine.sendMessageAndListenForReply(m.encodeToBytes()));
                if (r.msg_type == MsgTypes.REPLY_SUCCESS) {
                    this.refreshTasksListRoutine((Dictionary<int, string>)r.header["jobs_list"]);
                    this.cleanSchedules();
                    this.cleanDependencies();
                    //2.Populate controls with data from message
                    j = (Job)r.attachement;
                    parent.tb_Id.Text               = j.job_id.ToString();
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
                    parent.cb_CheckppointType.SelectedIndex = j.checkpoint_type == 1 ? 0 : 1;
                    parent.tb_Checkpoint.Text = j.checkpoint_type == 1 ? j.data_chceckpoint.ToString() : String.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", j.time_checkpoint);

                    parent.tb_Who.Text              = j.last_job_change.login;
                    parent.tb_When.Text             = j.last_job_change.change_timestamp.ToString();

                    parent.tb_IsFailed.BackColor    = parent.tb_IsFailed.Text == "NO" ? System.Drawing.Color.Chartreuse : System.Drawing.Color.Red;
                    parent.btn_Activation.Text      = parent.tb_isActive.Text == "NO" ? "Activate" : "Deactivate";
                    parent.btn_Pausing.Text         = parent.tb_IsPaused.Text == "NO" ? "Pause" : "Un-Pause";

                    //3.populate either Schedules or Dependencies
                    if (j.job_type_id == 1) {
                        if (j.schedules.Count > 0) {
                            foreach (Schedule s in j.schedules.Values) {
                                ListViewItem i = new ListViewItem(s.job_schedule_id.ToString());
                                i.SubItems.Add(schedulte_types[s.schedule_type_id].schedule_type_name);
                                i.SubItems.Add(String.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", s.next_execution));
                                parent.lv_Schedules.Items.Add(i);
                            }

                        }
                    } else if (j.job_type_id == 2) {
                        if (j.dependencies.Count > 0) {
                            foreach (Dependency d in j.dependencies.Values) {
                                ListViewItem i = new ListViewItem(d.job_dependency_id.ToString());
                                i.SubItems.Add(dependency_types[d.dependency_type_id].name);
                                i.SubItems.Add(jobs[d.depending_job_id.Value]);
                                parent.lv_Dependencies.Items.Add(i);
                            }

                        }
                    }
                }

            }
            catch (Exception e) {
                MessageBox.Show($"There was a communications problem.\nOriginal system error:{e.Message}");
            }           

        }

        public void createJob() {
            //1.first check to see that job is not in the view
            string ln = parent.tb_Name.Text;
            Job j;

            //for (int i = 0 ; i < parent.lv_JobsList.Items.Count;i++) {
            foreach (ListViewItem i in parent.lv_JobsList.Items) {
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

                    job_type_id                     = parent.cb_Type.Text == "Schedule" ? 1 : 2,
                    last_instance_id                = 0,
                    type_name                       = parent.cb_Type.Text,
                    name                            = ln,
                    executable_name                 = parent.tb_Executable.Text,
                    max_try_count                   = Int32.Parse(parent.tb_MaxAttempts.Text),
                    delay_seconds                   = Int32.Parse(parent.tb_DelaySecs.Text),
                    latency_alert_seconds           = Int32.Parse(parent.tb_LatencyAlert.Text),
                    notifiactions_list              = parent.tb_Notifications.Text,
                    is_failed                       = false,
                    is_active                       = parent.tb_isActive.Text == "YES" ? true : false,
                    is_paused                       = parent.tb_IsPaused.Text == "YES" ? true : false,
                    checkpoint_type                 = parent.cb_CheckppointType.SelectedIndex == 0 ? 1 : 2,
                    data_chceckpoint                = parent.cb_CheckppointType.SelectedIndex == 0 ? (long?)Int64.Parse(parent.tb_Checkpoint.Text) : null,
                    time_checkpoint                 = parent.cb_CheckppointType.SelectedIndex == 1 ? (DateTime?)DateTime.Parse(parent.tb_Checkpoint.Text) : null,
                    current_failed_count            = Int32.Parse(parent.tb_FailedAttempts.Text)
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
                    this.refreshTasksListRoutine((Dictionary<int, string>)r.header["jobs_list"]);
                }
                else
                    MessageBox.Show(r.body);
            }
            catch (Exception e) {
                MessageBox.Show($"There was a communications problem.\nOriginal system error:{e.Message}");
            }
        }

        public void updateJob() {
            //1.first check to see that job exists in the view
            string ln = parent.tb_Name.Text;
            bool job_found = false;
            Job j;

            //for (int i = 0 ; i < parent.lv_JobsList.Items.Count;i++) {
            foreach (ListViewItem i in parent.lv_JobsList.Items) {
                if (i.SubItems[0].Text == ln) {
                    job_found = true;
                    break;
                }
            }
            if (!job_found) {
                MessageBox.Show("Can't find what to update. Job name is not in list.");
                return;
            }
            //2.make sure the user wants to do this action
            if (MessageBox.Show($"Job {ln} will be modified. Continue with sending this update to the server?", "", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            //3.instantiate and fill in a job
            try {
                j = new Job(this_user) {
                    job_id                  = Int32.Parse(parent.tb_Id.Text),
                    job_type_id             = parent.cb_Type.Text == "Schedule" ? 1 : 2,
                    last_instance_id        = 0,
                    type_name               = parent.cb_Type.Text,
                    name = ln,
                    executable_name         = parent.tb_Executable.Text,
                    max_try_count           = Int32.Parse(parent.tb_MaxAttempts.Text),
                    delay_seconds           = Int32.Parse(parent.tb_DelaySecs.Text),
                    latency_alert_seconds   = Int32.Parse(parent.tb_LatencyAlert.Text),
                    notifiactions_list      = parent.tb_Notifications.Text,
                    is_failed               = parent.tb_IsFailed.Text == "NO"? true:false,
                    is_active               = parent.tb_isActive.Text == "YES" ? true : false,
                    is_paused               = parent.tb_IsPaused.Text == "YES" ? true : false,
                    checkpoint_type         = parent.cb_CheckppointType.SelectedIndex == 0 ? 1 : 2,
                    data_chceckpoint        = parent.cb_CheckppointType.SelectedIndex == 0 ? (long?)Int64.Parse(parent.tb_Checkpoint.Text) : null,
                    time_checkpoint         = parent.cb_CheckppointType.SelectedIndex == 1 ? (DateTime?)DateTime.Parse(parent.tb_Checkpoint.Text) : null,
                    current_failed_count    = Int32.Parse(parent.tb_FailedAttempts.Text)
                };
            }
            catch (System.FormatException e) {
                MessageBox.Show("Please make sure that #Max Attempts,Latency Alert Seconds and Delay Seconds are valid numbers.");
                return;
            }
            //4.attach to message and send
            Message m = new Message(j);
            m.msg_type = MsgTypes.MGMT_UPDATE_JOB;
            m.header["user"] = this_user;
            m.session_channel = this_user.this_sessions_id;
            //m.session_channel = ClientManager.session_id;
            try {
                Message r = Message.getMessageFromBytes(message_engine.sendMessageAndListenForReply(m.encodeToBytes()));
                if (r.msg_type == MsgTypes.REPLY_SUCCESS) {
                    //5.refresh list view
                    this.refreshTasksListRoutine((Dictionary<int, string>)r.header["jobs_list"]);
                }
                else
                    MessageBox.Show(r.body);
            }
            catch (Exception e) {
                MessageBox.Show($"There was a communications problem.\nOriginal system error:{e.Message}");
            }
        }

        public void deleteJob() {
            //1.first check to see that job exists in the view
            string ln = parent.tb_Name.Text;
            bool job_found = false;
            Job j;

            //for (int i = 0 ; i < parent.lv_JobsList.Items.Count;i++) {
            foreach (ListViewItem i in parent.lv_JobsList.Items) {
                if (i.SubItems[0].Text == ln) {
                    job_found = true;
                    break;
                }
            }
            if (!job_found) {
                MessageBox.Show("Can't find what to update. Job name is not in list.");
                return;
            }
            //2.make sure the user wants to do this action
            if (MessageBox.Show($"Job {ln} will be deleted. Continue with sending this update to the server?", "", MessageBoxButtons.YesNo) == DialogResult.No)
                return;


            //3.Send the message
            Message m = new Message();
            m.msg_type = MsgTypes.MGMT_DELETE_JOB;
            m.body = ln;
            m.header["user"] = this_user;
            m.session_channel = this_user.this_sessions_id;
            //m.session_channel = ClientManager.session_id;
            try {
                Message r = Message.getMessageFromBytes(message_engine.sendMessageAndListenForReply(m.encodeToBytes()));
                if (r.msg_type == MsgTypes.REPLY_SUCCESS) {
                    //5.refresh list view
                    this.refreshTasksListRoutine((Dictionary<int, string>)r.header["jobs_list"]);
                }
                else
                    MessageBox.Show(r.body);
            }
            catch (Exception e) {
                MessageBox.Show($"There was a communications problem.\nOriginal system error:{e.Message}");
            }
        }

        public void addNewSchedule() {
            //1. do some checks
            if (parent.cb_ScheduleType.Text == "") {
                MessageBox.Show("Select a Schedule Type please.");
                return;
            }
            if (parent.tb_Id.Text == "") {
                MessageBox.Show("Please create the Job first, then add schedules to it.");
                return;
            }

            //2.instantiate dummy job with new schedule
            Job j = new Job() { job_id = Int32.Parse(parent.tb_Id.Text),
                name = parent.tb_Name.Text
            };
            Schedule sch = new Schedule { job_id = j.job_id,
                schedule_type_id = getScheduleTypeName(parent.cb_ScheduleType.Text),
                next_execution = DateTime.Parse(parent.dtp_Schedule.Text)

            };
            j.schedules.Add(0, sch);

            //3.send it over to the server
            Message m = new Message(j);
            m.msg_type = MsgTypes.MGMT_CREATE_SCHEDULE;
            m.header["user"] = this_user;
            m.session_channel = this_user.this_sessions_id;
            try {
                Message r = Message.getMessageFromBytes(message_engine.sendMessageAndListenForReply(m.encodeToBytes()));
                if (r.msg_type == MsgTypes.REPLY_SUCCESS) {
                    this.cleanSchedules();
                    j = (Job)r.attachement;

                    //4.display schedules
                    foreach (Schedule s in j.schedules.Values) {
                        ListViewItem i = new ListViewItem(s.job_schedule_id.ToString());
                        i.SubItems.Add(schedulte_types[s.schedule_type_id].schedule_type_name);
                        i.SubItems.Add(String.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", s.next_execution));
                        parent.lv_Schedules.Items.Add(i);
                    }
                    this.refreshTasksListRoutine((Dictionary<int, string>)r.header["jobs_list"]);
                }
                else {
                    MessageBox.Show(r.body);
                }
            }
            catch (Exception e) {
                MessageBox.Show($"There was a communications problem.\nOriginal system error:{e.Message}");
            }
        }

        public void deleteSchedules() {
            Job j;
            Dictionary<int, Schedule> s = new Dictionary<int, Schedule>();


            //1.first check if any item is selected and a job is created
            if (parent.lv_Schedules.CheckedItems.Count == 0) {
                MessageBox.Show("Please use the checkboxes for deleting schedules");
            }

            if (parent.tb_Id.Text == "") {
                MessageBox.Show("No job is selected");
                return;
            }

            //2. job for which to deleted.
            j = new Job() {
                job_id = Int32.Parse(parent.tb_Id.Text),
                name = parent.tb_Name.Text
            };


            //3.loop through items and fill in the schedules dictionary
            foreach (ListViewItem i in parent.lv_Schedules.Items) {
                if (i.Checked) {
                    Schedule sch = new Schedule() {
                        job_schedule_id = Int32.Parse(i.Text),
                        job_id = Int32.Parse(parent.tb_Id.Text)
                    };
                    s.Add((int)sch.job_schedule_id, sch);
                }
            }
            j.setSchedules(s);

            //4.send over to server to process
            Message m = new Message(j);
            m.msg_type = MsgTypes.MGMT_DELETE_SCHEDULE;
            m.header["user"] = this_user;
            m.session_channel = this_user.this_sessions_id;
            m.attachement = j;
            try {
                Message r = Message.getMessageFromBytes(message_engine.sendMessageAndListenForReply(m.encodeToBytes()));
                if (r.msg_type == MsgTypes.REPLY_SUCCESS) {
                    this.cleanSchedules();
                    j = (Job)r.attachement;

                    //5.display remaining schedules
                    foreach (Schedule sc in j.schedules.Values) {
                        ListViewItem i = new ListViewItem(sc.job_schedule_id.ToString());
                        i.SubItems.Add(schedulte_types[sc.schedule_type_id].schedule_type_name);
                        i.SubItems.Add(String.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", sc.next_execution));
                        parent.lv_Schedules.Items.Add(i);
                    }
                    this.refreshTasksListRoutine((Dictionary<int, string>)r.header["jobs_list"]);
                }
                else {
                    MessageBox.Show(r.body);
                }
            }
            catch (Exception e) {
                MessageBox.Show($"There was a communications problem.\nOriginal system error:{e.Message}");
            }

        }

        public void addNewDependency() {
            //1. do some checks
            if (parent.cb_DepTypes.Text == "") {
                MessageBox.Show("Select a Schedule Type please.");
                return;
            }
            if (parent.tb_Id.Text == "") {
                MessageBox.Show("Please create the Job first, then add schedules to it.");
                return;
            }

            //2.instantiate dummy job with new schedule
            Job j = new Job() {
                job_id = Int32.Parse(parent.tb_Id.Text),
                name = parent.tb_Name.Text
            };
            Dependency dep = new Dependency {
                job_id = j.job_id,
                dependency_type_id = getDependencyTypeName(parent.cb_DepTypes.Text),
                depending_job_id = getJobId(parent.cb_DepJobs.Text)
            };
            j.dependencies.Add(0, dep);

            //3.send it over to the server
            Message m = new Message(j);
            m.msg_type = MsgTypes.MGMG_CREATE_DEPENDENCY;
            m.header["user"] = this_user;
            m.session_channel = this_user.this_sessions_id;
            try {
                Message r = Message.getMessageFromBytes(message_engine.sendMessageAndListenForReply(m.encodeToBytes()));
                if (r.msg_type == MsgTypes.REPLY_SUCCESS) {
                    this.cleanDependencies();
                    j = (Job)r.attachement;

                    //4.display schedules
                    foreach (Dependency d in j.dependencies.Values) {
                        ListViewItem i = new ListViewItem(d.job_dependency_id.ToString());
                        i.SubItems.Add(dependency_types[d.dependency_type_id].name);
                        i.SubItems.Add(jobs[d.depending_job_id.Value]);
                        parent.lv_Dependencies.Items.Add(i);
                    }
                    this.refreshTasksListRoutine((Dictionary<int, string>)r.header["jobs_list"]);
                }
                else {
                    MessageBox.Show(r.body);
                }
            }
            catch (Exception e) {
                MessageBox.Show($"There was a communications problem.\nOriginal system error:{e.Message}");
            }
        }

        public void deleteDependency() {
            Job j;
            Dictionary<int, Dependency> dep = new Dictionary<int, Dependency>();


            //1.first check if any item is selected and a job is created
            if (parent.lv_Dependencies.CheckedItems.Count == 0)
                MessageBox.Show("Please use the checkboxes for deleting dependencies");

            if (parent.tb_Id.Text == "") {
                MessageBox.Show("No job is selected");
                return;
            }

            //2. job for which to deleted.
            j = new Job() {
                job_id = Int32.Parse(parent.tb_Id.Text),
                name = parent.tb_Name.Text
            };


            //3.loop through items and fill in the dep dictionary
            foreach (ListViewItem i in parent.lv_Dependencies.Items) {
                if (i.Checked) {
                    Dependency d = new Dependency() {
                        job_dependency_id = Int32.Parse(i.Text),
                        job_id = Int32.Parse(parent.tb_Id.Text)
                    };
                    dep.Add((int)d.job_dependency_id, d);
                }
            }
            j.setDependencies(dep);

            //4.send over to server to process
            Message m = new Message(j);
            m.msg_type = MsgTypes.MGMT_DELETE_DEPENDENCY;
            m.header["user"] = this_user;
            m.session_channel = this_user.this_sessions_id;
            m.attachement = j;
            try {
                Message r = Message.getMessageFromBytes(message_engine.sendMessageAndListenForReply(m.encodeToBytes()));
                if (r.msg_type == MsgTypes.REPLY_SUCCESS) {
                    this.cleanDependencies();
                    j = (Job)r.attachement;

                    //5.display remaining deps
                    foreach (Dependency d in j.dependencies.Values) {
                        ListViewItem i = new ListViewItem(d.job_dependency_id.ToString());
                        i.SubItems.Add(dependency_types[d.dependency_type_id].name);
                        i.SubItems.Add(jobs[d.depending_job_id.Value]);
                        parent.lv_Dependencies.Items.Add(i);
                    }
                    this.refreshTasksListRoutine((Dictionary<int, string>)r.header["jobs_list"]);
                }
                else {
                    MessageBox.Show(r.body);
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
        public static int getScheduleTypeName(string selected_name) {
            foreach(int i in schedulte_types.Keys) {
                if (schedulte_types[i].schedule_type_name == selected_name)
                    return i;
            }
            return 0;
        }

        public static int getDependencyTypeName(string selected_name) {
            foreach (int i in dependency_types.Keys) {
                if (dependency_types[i].name == selected_name)
                    return i;
            }
            return 0;
        }
        public static int getJobId(string name) {
            foreach(int id in ClientManager.jobs.Keys) {
                if (ClientManager.jobs[id] == name)
                    return id;
            }
            return 0;
        }

    }
}
