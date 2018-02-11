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
    public partial class WorkingStatus : Form {
               

        public Action Compute;
        public string label_message;


        public WorkingStatus(Action compute,string message ) {
            this.Compute = compute;
            this.label_message = message;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            this.lb_StatusMsg.Text = this.label_message;
            Task.Factory.StartNew(this.Compute).ContinueWith(t => { this.Close(); }, TaskScheduler.FromCurrentSynchronizationContext());
            //Task.Factory.StartNew()
        }
    }
}
