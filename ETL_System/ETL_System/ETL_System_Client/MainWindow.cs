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

        public MainWindow() {
            InitializeComponent();

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
            if (tc_Main.SelectedTab == tp_Catalogue) {
                if (ClientManager.login_active) {
                    manager.requestCatalogue();
                }
            }
        }

        private void label2_Click(object sender, EventArgs e) {

        }

        private void Status_Enter(object sender, EventArgs e) {

        }

        private void lv_JobsList_SelectedIndexChanged(object sender, EventArgs e) {
            if(tc_Main.SelectedTab == tabPage1) {
                manager.initETLJobDefinitionTab();
            }
        }

        private void cb_Type_SelectedIndexChanged(object sender, EventArgs e) {
            foreach (Control c in tc_Job.Controls) {
                if (c.Name != "tp_Definition")
                    tc_Job.Controls.Remove(c);
            }
            if(cb_Type.SelectedIndex == 0)
                tc_Job.Controls.Add(this.tp_Schedules);
            else
                tc_Job.Controls.Add(this.tp_Dependencies);
        }

        private void tb_isActive_TextChanged(object sender, EventArgs e) {
            /*
            switch (tb_isActive.Text) {
                case "NO":
                    btn_Activation.Text = "Activate";
                    break;
                case "YES":
                    btn_Activation.Text = "Deactivate";
                    break;
                default:
                    btn_Activation.Text = "Deactivate";
                    break;
            }
            */
        }

        private void tb_IsPaused_TextChanged(object sender, EventArgs e) {
            /*
            switch (tb_IsPaused.Text) {
                case "NO":
                    btn_Pausing.Text = "Pause";
                    break;
                case "YES":
                    btn_Pausing.Text = "Un-Pause";
                    break;
                default:
                    btn_Pausing.Text = "Pause";
                    break;
            }
            */
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

        }

        private void btn_Save_Click(object sender, EventArgs e) {

        }

        private void button1_Click(object sender, EventArgs e) {
            manager.createJob();
        }
    }
}
