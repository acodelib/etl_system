using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ETL_System {
    public partial class LoginForm : Form {

        MainWindow parent_window;
        ClientManager sys_manager;

        public LoginForm(MainWindow parent) {
            this.parent_window = parent;
            this.sys_manager = parent.manager;
            InitializeComponent();
            this.tb_Server.Text = "192.168.56.1";
            this.tb_Port.Text = "6868";
            this.tb_UserName.Text = "admin";
            this.tb_Pass.Text = "admin";
            
        }

        private void doCheckServer() {
            int port;
            if (this.tb_Server.Text != null && this.tb_Port.Text != null && Int32.TryParse(this.tb_Port.Text, out port))
                sys_manager.message_engine.tryConnect(this.tb_Server.Text, port);
        }

        private void doLogin() {
            int port;
            if (this.tb_Server.Text != null && this.tb_Port.Text != null && Int32.TryParse(this.tb_Port.Text, out port) && this.tb_UserName.Text != null && this.tb_Pass.Text != null)
                sys_manager.loginProcedure(this.tb_UserName.Text, this.tb_Pass.Text, this.tb_Server.Text, port);
        }

        private void button1_Click(object sender, EventArgs e) {            
                using (Form f = new WorkingStatus(this.doCheckServer,"Searching for server...")) {
                f.ShowDialog(this);
            }              
        }

        private void btn_Login_Click(object sender, EventArgs e) {
            if (!ClientManager.login_active) {
                using (Form f = new WorkingStatus(this.doLogin, "Contacting ETL System...")) {
                    f.ShowDialog(this);
                }                
            }else {
                MessageBox.Show("You are already logged in. Loggout first, then retry.");
            }
            this.Close();
        }
    }
}
