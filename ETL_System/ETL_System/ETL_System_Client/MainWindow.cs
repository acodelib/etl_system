using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace ETL_System
{
    public partial class MainWindow : Form
    {
        public ClientManager manager;
        public string lv_Jobs_selection_cycle;
        public string lv_Jobs_selected_text;

        public MainWindow() {
            InitializeComponent();
            this.lv_Jobs_selection_cycle = "Xstarted";
            this.lv_Jobs_selected_text = "";
            
            //some hose keeping                         
            tc_Job.Controls.Remove(tp_Schedules);
            tc_Job.Controls.Remove(tp_Dependencies);
            
            cb_CheckppointType.SelectedIndex = 1;

            //fire up the manager
            manager = new ClientManager(this);
        }

        /*
        private async void button1_Click(object sender, EventArgs e) {

            MsgEngine Sender = new MsgEngine(rtbDisplay.Text);
            this.rtbDisplay.Text = await Sender.communicateWithServer();
            Sender = null;
        }
        */

        private void textBox1_TextChanged(object sender, EventArgs e) {

        }

        private void rtbDisplay_TextChanged(object sender, EventArgs e) {

        }

        private void stopServerToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void tabPage6_Click(object sender, EventArgs e) {

        }

        private void ts_Login_Click(object sender, EventArgs e) {            
            new LoginForm(this).ShowDialog(this);
        }

        private void tslb_Response_Click(object sender, EventArgs e) {

        }

        private void tp_Catalogue_Click(object sender, EventArgs e) {

        }

        
        private void tc_Main_Selected(object sender,EventArgs e) {
            if (ClientManager.login_active == false)
                return;

            if (tc_Main.SelectedTab == tp_Catalogue) {
                if (ClientManager.login_active) {
                    manager.requestCatalogue();
                }            
            }
            if(tc_Main.SelectedTab == tp_Graph) {
                this.cb_RenderType.SelectedIndex = 0;
            }
            if(tc_Main.SelectedTab == tp_Queue) {
                manager.requestQueue();
            }
            if (tc_Main.SelectedTab == tabPage1) {
                //ETL details tabl
                if (tc_Job.SelectedTab == tp_JobInstances) {
                    manager.requestJobHistory();
                    return;
                }
                manager.initETLJobDefinitionTab();
               
            }

            if(tc_Main.SelectedTab == tp_Admin) {
                manager.requestAdminData();
            }
        }

        private void tc_Job_Selected(object sender, EventArgs e) {
            if (ClientManager.login_active == false)
                return;

            if (tc_Job.SelectedTab == tp_Schedules) {
                cb_ScheduleType.Items.Clear();
                foreach (ScheduleType s in ClientManager.schedulte_types.Values)
                    cb_ScheduleType.Items.Add(s.schedule_type_name);
            }
            if (tc_Job.SelectedTab == tp_Dependencies) {
                cb_DepTypes.Items.Clear();
                foreach (DependencyType s in ClientManager.dependency_types.Values)
                    cb_DepTypes.Items.Add(s.name);                
            }
            if (tc_Job.SelectedTab == tp_JobInstances) {
                manager.requestJobHistory();
            }
        }

        private void label2_Click(object sender, EventArgs e) {

        }

        private void Status_Enter(object sender, EventArgs e) {

        }

        private void lv_JobsList_SelectedIndexChanged(object sender, EventArgs e) {
            if(tc_Main.SelectedTab == tabPage1) {
                if (tc_Job.SelectedTab == tp_JobInstances) {
                    manager.requestJobHistory();
                    return;
                }
                manager.initETLJobDefinitionTab();
            }
        }

        private void cb_Type_SelectedIndexChanged(object sender, EventArgs e) {
            tc_Job.Controls.Remove(tp_Schedules);
            tc_Job.Controls.Remove(tp_Dependencies);

            if (cb_Type.SelectedIndex == 0)
                tc_Job.Controls.Add(this.tp_Schedules);
            else
                tc_Job.Controls.Add(this.tp_Dependencies);
        }

        private void tb_isActive_TextChanged(object sender, EventArgs e) {
          
        }

        private void tb_IsPaused_TextChanged(object sender, EventArgs e) {
          
        }

        private void btn_Activation_Click(object sender, EventArgs e) {
            if (btn_Activation.Text == "Activate") {
                tb_isActive.Text = "YES";
                btn_Activation.Text = "Deactivate";
                return;
            }
            if (btn_Activation.Text == "Deactivate") {
                tb_isActive.Text = "NO";
                btn_Activation.Text = "Activate";
                return;
            }
                

        }

        private void btn_Pausing_Click(object sender, EventArgs e) {
            if (btn_Pausing.Text == "Pause") {
                tb_IsPaused.Text = "YES";
                btn_Pausing.Text = "Un-Pause";
                return;
            }
            if (btn_Pausing.Text == "Un-Pause") {
                tb_IsPaused.Text = "NO";
                btn_Pausing.Text = "Pause";
                return;
            }
        }

        private void ts_NewJob_Click(object sender, EventArgs e) {
            tc_Main.SelectedTab = tabPage1;
            foreach(Control c in gb_General.Controls) {                
                if (c.GetType() == typeof(TextBox))
                        c.Text = "";
            }
            foreach (Control c in gb_Status.Controls) {
                if (c.GetType() == typeof(TextBox))
                    c.Text = "";
            }
            foreach (Control c in gb_Changes.Controls) {
                if (c.GetType() == typeof(TextBox))
                    c.Text = "";
            }
            manager.cleanSchedules();

        }

        private void btn_Save_Click(object sender, EventArgs e) {
            manager.updateJob();
        }

        private void button1_Click(object sender, EventArgs e) {
            manager.createJob();
        }

        private void btn_Delete_Click(object sender, EventArgs e) {
            manager.deleteJob();
        }

        private void tp_Queue_Click(object sender, EventArgs e) {

        }

        private void btn_AddSchedule_Click(object sender, EventArgs e) {
            manager.addNewSchedule();
        }

        private void lv_JobsList_SelectedIndexChanged_1(object sender, EventArgs e) {
            //1.check for unfocused selection
            if (lv_JobsList.SelectedItems.Count == 0)
                return;
            //2.keep the data
            this.lv_Jobs_selected_text = lv_JobsList.SelectedItems[0].Text;

            //3.escape in selection cycle
            if (this.lv_Jobs_selection_cycle == "Xexit") {
                this.lv_Jobs_selection_cycle = "Xstarted";
                return;
            }

            
            //4.React
            if (tc_Main.SelectedTab == tabPage1) {
                //ETL details tabl
                if (tc_Job.SelectedTab == tp_JobInstances) {
                    manager.requestJobHistory();
                    return;
                }
                manager.initETLJobDefinitionTab();                               
            }
            
            if(tc_Main.SelectedTab == tp_Graph) {              
                //load graph view
                manager.rederDependencyGraphView(this.lv_Jobs_selected_text);
            }
        }

        private void button1_Click_1(object sender, EventArgs e) {
            manager.deleteSchedules();
        }

        private void btn_AddDependency_Click(object sender, EventArgs e) {
            manager.addNewDependency();
        }

        private void btn_DelDependency_Click(object sender, EventArgs e) {
            manager.deleteDependency();
        }

        private void cb_CheckppointType_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void cb_RenderType_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.lv_Jobs_selected_text != "" && this.lv_Jobs_selected_text != null)
                manager.rederDependencyGraphView(this.lv_Jobs_selected_text);
        }

        private void nud_Depth_ValueChanged(object sender, EventArgs e) {
            if (this.lv_Jobs_selected_text != "" && this.lv_Jobs_selected_text != null)
                manager.rederDependencyGraphView(this.lv_Jobs_selected_text);
        }

        private void ts_Logout_Click(object sender, EventArgs e) {
            manager.logoutProcedure();
        }

        private void label24_Click(object sender, EventArgs e) {

        }

        private void groupBox10_Enter(object sender, EventArgs e) {

        }

        private void btn_NewUser_Click(object sender, EventArgs e) {
            manager.adminAddUser();
        }

        private void btn_DelUser_Click(object sender, EventArgs e) {
            manager.adminDeleteUser();
        }

        private void button3_Click(object sender, EventArgs e) {
            manager.adminUpdateConfigs();
        }

        private void dgv_Catalogue_CellContentClick(object sender, DataGridViewCellEventArgs e) {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) {

        }

        private void rowSelectionChange(object sender, EventArgs e) {
            manager.requestInstanceOutput();
        }
    }
}
