﻿using System;
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
                }
            } catch (Exception e) {
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
                if (r.msg_type == MsgTypes.REPLY_SUCCESS) {
                    c = (JobsCatalogueDisplay)r.attachement;
                    parent.dgv_Catalogue.DataSource = c.data;
                    this.refreshTasksListRoutine((Dictionary<int, string>)r.header["jobs_list"]);
                }
            } catch (Exception e) {
                MessageBox.Show($"There was a communications problem.\nOriginal system error:{e.Message}");
            }
        }

        public void refreshTasksListRoutine(Dictionary<int, string> jobs) {
            if (jobs != null) {
                parent.lv_JobsList.Items.Clear();
                parent.cb_DepJobs.Items.Clear();
                foreach (int id in jobs.Keys) {
                    ListViewItem l = new ListViewItem(jobs[id]);
                    parent.lv_JobsList.Items.Add(l);
                    parent.cb_DepJobs.Items.Add(jobs[id]);
                }
                ClientManager.jobs = jobs;
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
            m.body = parent.lv_JobsList.SelectedItems[0].Text;
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
                    parent.tb_Id.Text = j.job_id.ToString();
                    parent.tb_Name.Text = j.name;
                    parent.tb_Executable.Text = j.executable_name;
                    parent.tb_MaxAttempts.Text = j.max_try_count.ToString();
                    parent.tb_LatencyAlert.Text = j.latency_alert_seconds.ToString();
                    parent.tb_DelaySecs.Text = j.delay_seconds.ToString();
                    parent.tb_Notifications.Text = j.notifiactions_list;
                    parent.tb_IsFailed.Text = j.is_failed == false ? "NO" : "YES";
                    parent.tb_FailedAttempts.Text = j.current_failed_count.ToString();
                    parent.tb_isActive.Text = j.is_active == true ? "YES" : "NO";
                    parent.tb_IsPaused.Text = j.is_paused == true ? "YES" : "NO";
                    parent.cb_Type.SelectedIndex = j.job_type_id == 1 ? 0 : 1;

                    parent.tb_Who.Text = j.last_job_change.login;
                    parent.tb_When.Text = j.last_job_change.change_timestamp.ToString();

                    parent.tb_IsFailed.BackColor = parent.tb_IsFailed.Text == "NO" ? System.Drawing.Color.Chartreuse : System.Drawing.Color.Red;
                    parent.btn_Activation.Text = parent.tb_isActive.Text == "NO" ? "Activate" : "Deactivate";
                    parent.btn_Pausing.Text = parent.tb_IsPaused.Text == "NO" ? "Pause" : "Un-Pause";

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

            //3.Apply visual rules
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
                    this.refreshTasksListRoutine((Dictionary<int, string>)r.header["jobs_list"]);
                }
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
                    job_id = Int32.Parse(parent.tb_Id.Text),
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
            if (parent.lv_Schedules.CheckedItems.Count == 0)
                MessageBox.Show("Please use the checkboxes for deleting schedules");

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
