using System.Windows.Forms;

namespace ETL_System {
    partial class MainWindow {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tslb_Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslb_Response = new System.Windows.Forms.ToolStripStatusLabel();
            this.tc_Main = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tc_Job = new System.Windows.Forms.TabControl();
            this.tp_Definition = new System.Windows.Forms.TabPage();
            this.tp_Schedules = new System.Windows.Forms.TabPage();
            this.tp_Dependencies = new System.Windows.Forms.TabPage();
            this.tp_Catalogue = new System.Windows.Forms.TabPage();
            this.dgv_Catalogue = new System.Windows.Forms.DataGridView();
            this.tp_Queue = new System.Windows.Forms.TabPage();
            this.tp_Graph = new System.Windows.Forms.TabPage();
            this.tp_Admin = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_clear = new System.Windows.Forms.Button();
            this.lv_JobsList = new System.Windows.Forms.ListView();
            this.btn_searchjob = new System.Windows.Forms.Button();
            this.tb_Search = new System.Windows.Forms.TextBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.ts_User = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_Login = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_Logout = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_Server = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_PauseQ = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_ResumeQ = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_PauseW = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_ResumeW = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_NewJob = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip.SuspendLayout();
            this.tc_Main.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tc_Job.SuspendLayout();
            this.tp_Catalogue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Catalogue)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslb_Status,
            this.tslb_Response});
            this.statusStrip.Location = new System.Drawing.Point(0, 632);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1276, 24);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusStrip1";
            // 
            // tslb_Status
            // 
            this.tslb_Status.AutoToolTip = true;
            this.tslb_Status.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tslb_Status.Name = "tslb_Status";
            this.tslb_Status.Size = new System.Drawing.Size(83, 19);
            this.tslb_Status.Text = "Disconected";
            this.tslb_Status.ToolTipText = "Shows server communications status\r\n";
            // 
            // tslb_Response
            // 
            this.tslb_Response.Name = "tslb_Response";
            this.tslb_Response.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tslb_Response.Size = new System.Drawing.Size(95, 19);
            this.tslb_Response.Text = "toolStripStatusLabel2";
            this.tslb_Response.Visible = false;
            this.tslb_Response.Click += new System.EventHandler(this.tslb_Response_Click);
            // 
            // tc_Main
            // 
            this.tc_Main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tc_Main.Controls.Add(this.tabPage1);
            this.tc_Main.Controls.Add(this.tp_Catalogue);
            this.tc_Main.Controls.Add(this.tp_Queue);
            this.tc_Main.Controls.Add(this.tp_Graph);
            this.tc_Main.Controls.Add(this.tp_Admin);
            this.tc_Main.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tc_Main.Location = new System.Drawing.Point(238, 56);
            this.tc_Main.Name = "tc_Main";
            this.tc_Main.SelectedIndex = 0;
            this.tc_Main.Size = new System.Drawing.Size(1026, 571);
            this.tc_Main.TabIndex = 7;
            this.tc_Main.SelectedIndexChanged += new System.EventHandler(this.tc_Main_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tc_Job);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1018, 542);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "ETL Job";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tc_Job
            // 
            this.tc_Job.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tc_Job.Controls.Add(this.tp_Definition);
            this.tc_Job.Controls.Add(this.tp_Schedules);
            this.tc_Job.Controls.Add(this.tp_Dependencies);
            this.tc_Job.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tc_Job.Location = new System.Drawing.Point(6, 6);
            this.tc_Job.Name = "tc_Job";
            this.tc_Job.SelectedIndex = 0;
            this.tc_Job.Size = new System.Drawing.Size(1012, 533);
            this.tc_Job.TabIndex = 2;
            // 
            // tp_Definition
            // 
            this.tp_Definition.Location = new System.Drawing.Point(4, 24);
            this.tp_Definition.Name = "tp_Definition";
            this.tp_Definition.Padding = new System.Windows.Forms.Padding(3);
            this.tp_Definition.Size = new System.Drawing.Size(1004, 505);
            this.tp_Definition.TabIndex = 0;
            this.tp_Definition.Text = "Definition";
            this.tp_Definition.UseVisualStyleBackColor = true;
            this.tp_Definition.Click += new System.EventHandler(this.tabPage6_Click);
            // 
            // tp_Schedules
            // 
            this.tp_Schedules.Location = new System.Drawing.Point(4, 24);
            this.tp_Schedules.Name = "tp_Schedules";
            this.tp_Schedules.Padding = new System.Windows.Forms.Padding(3);
            this.tp_Schedules.Size = new System.Drawing.Size(1004, 505);
            this.tp_Schedules.TabIndex = 1;
            this.tp_Schedules.Text = "Schedules";
            this.tp_Schedules.UseVisualStyleBackColor = true;
            // 
            // tp_Dependencies
            // 
            this.tp_Dependencies.Location = new System.Drawing.Point(4, 24);
            this.tp_Dependencies.Name = "tp_Dependencies";
            this.tp_Dependencies.Padding = new System.Windows.Forms.Padding(3);
            this.tp_Dependencies.Size = new System.Drawing.Size(1004, 505);
            this.tp_Dependencies.TabIndex = 2;
            this.tp_Dependencies.Text = "Dependencies";
            this.tp_Dependencies.UseVisualStyleBackColor = true;
            // 
            // tp_Catalogue
            // 
            this.tp_Catalogue.Controls.Add(this.dgv_Catalogue);
            this.tp_Catalogue.Location = new System.Drawing.Point(4, 25);
            this.tp_Catalogue.Name = "tp_Catalogue";
            this.tp_Catalogue.Padding = new System.Windows.Forms.Padding(3);
            this.tp_Catalogue.Size = new System.Drawing.Size(1018, 542);
            this.tp_Catalogue.TabIndex = 1;
            this.tp_Catalogue.Text = "Catalogue";
            this.tp_Catalogue.UseVisualStyleBackColor = true;
            this.tp_Catalogue.Click += new System.EventHandler(this.tp_Catalogue_Click);
            // 
            // dgv_Catalogue
            // 
            this.dgv_Catalogue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_Catalogue.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Catalogue.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_Catalogue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_Catalogue.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_Catalogue.GridColor = System.Drawing.SystemColors.Control;
            this.dgv_Catalogue.Location = new System.Drawing.Point(22, 13);
            this.dgv_Catalogue.Name = "dgv_Catalogue";
            this.dgv_Catalogue.ReadOnly = true;
            this.dgv_Catalogue.RowHeadersVisible = false;
            this.dgv_Catalogue.Size = new System.Drawing.Size(972, 512);
            this.dgv_Catalogue.TabIndex = 0;
            // 
            // tp_Queue
            // 
            this.tp_Queue.Location = new System.Drawing.Point(4, 25);
            this.tp_Queue.Name = "tp_Queue";
            this.tp_Queue.Padding = new System.Windows.Forms.Padding(3);
            this.tp_Queue.Size = new System.Drawing.Size(1018, 542);
            this.tp_Queue.TabIndex = 2;
            this.tp_Queue.Text = "Queue";
            this.tp_Queue.UseVisualStyleBackColor = true;
            // 
            // tp_Graph
            // 
            this.tp_Graph.Location = new System.Drawing.Point(4, 25);
            this.tp_Graph.Name = "tp_Graph";
            this.tp_Graph.Padding = new System.Windows.Forms.Padding(3);
            this.tp_Graph.Size = new System.Drawing.Size(1018, 542);
            this.tp_Graph.TabIndex = 3;
            this.tp_Graph.Text = "Dependency Graph";
            this.tp_Graph.UseVisualStyleBackColor = true;
            // 
            // tp_Admin
            // 
            this.tp_Admin.Location = new System.Drawing.Point(4, 25);
            this.tp_Admin.Name = "tp_Admin";
            this.tp_Admin.Padding = new System.Windows.Forms.Padding(3);
            this.tp_Admin.Size = new System.Drawing.Size(1018, 542);
            this.tp_Admin.TabIndex = 4;
            this.tp_Admin.Text = "Administer";
            this.tp_Admin.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.btn_clear);
            this.groupBox1.Controls.Add(this.lv_JobsList);
            this.groupBox1.Controls.Add(this.btn_searchjob);
            this.groupBox1.Controls.Add(this.tb_Search);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox1.Location = new System.Drawing.Point(12, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(224, 590);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Jobs List";
            // 
            // btn_clear
            // 
            this.btn_clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_clear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_clear.Location = new System.Drawing.Point(120, 53);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(100, 23);
            this.btn_clear.TabIndex = 8;
            this.btn_clear.Text = "clear";
            this.btn_clear.UseVisualStyleBackColor = true;
            // 
            // lv_JobsList
            // 
            this.lv_JobsList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lv_JobsList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lv_JobsList.Location = new System.Drawing.Point(6, 91);
            this.lv_JobsList.Name = "lv_JobsList";
            this.lv_JobsList.Size = new System.Drawing.Size(214, 493);
            this.lv_JobsList.TabIndex = 5;
            this.lv_JobsList.UseCompatibleStateImageBehavior = false;
            ColumnHeader header = new ColumnHeader();
            header.Text = "";
            header.Name = "col1";
            header.Width = lv_JobsList.Width;
            this.lv_JobsList.Columns.Clear();
            this.lv_JobsList.Columns.Add(header);
            this.lv_JobsList.View = System.Windows.Forms.View.List;
            // 
            // btn_searchjob
            // 
            this.btn_searchjob.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_searchjob.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_searchjob.Location = new System.Drawing.Point(6, 53);
            this.btn_searchjob.Name = "btn_searchjob";
            this.btn_searchjob.Size = new System.Drawing.Size(100, 23);
            this.btn_searchjob.TabIndex = 7;
            this.btn_searchjob.Text = "search";
            this.btn_searchjob.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_searchjob.UseVisualStyleBackColor = true;
            // 
            // tb_Search
            // 
            this.tb_Search.Location = new System.Drawing.Point(6, 27);
            this.tb_Search.Name = "tb_Search";
            this.tb_Search.Size = new System.Drawing.Size(214, 24);
            this.tb_Search.TabIndex = 6;
            this.tb_Search.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_User,
            this.ts_Server,
            this.ts_NewJob});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1276, 38);
            this.menuStrip.TabIndex = 8;
            this.menuStrip.Text = "menuStrip1";
            // 
            // ts_User
            // 
            this.ts_User.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_Login,
            this.ts_Logout});
            this.ts_User.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.ts_User.Name = "ts_User";
            this.ts_User.Padding = new System.Windows.Forms.Padding(12, 5, 12, 5);
            this.ts_User.Size = new System.Drawing.Size(66, 34);
            this.ts_User.Text = "User";
            // 
            // ts_Login
            // 
            this.ts_Login.Name = "ts_Login";
            this.ts_Login.Size = new System.Drawing.Size(125, 24);
            this.ts_Login.Text = "Login";
            this.ts_Login.Click += new System.EventHandler(this.ts_Login_Click);
            // 
            // ts_Logout
            // 
            this.ts_Logout.Name = "ts_Logout";
            this.ts_Logout.Size = new System.Drawing.Size(125, 24);
            this.ts_Logout.Text = "Logout";
            // 
            // ts_Server
            // 
            this.ts_Server.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_PauseQ,
            this.ts_ResumeQ,
            this.ts_PauseW,
            this.ts_ResumeW});
            this.ts_Server.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.ts_Server.Name = "ts_Server";
            this.ts_Server.Size = new System.Drawing.Size(115, 34);
            this.ts_Server.Text = "Server Control";
            // 
            // ts_PauseQ
            // 
            this.ts_PauseQ.Name = "ts_PauseQ";
            this.ts_PauseQ.Size = new System.Drawing.Size(215, 24);
            this.ts_PauseQ.Text = "Pause Queue";
            // 
            // ts_ResumeQ
            // 
            this.ts_ResumeQ.Name = "ts_ResumeQ";
            this.ts_ResumeQ.Size = new System.Drawing.Size(215, 24);
            this.ts_ResumeQ.Text = "Resume Queue";
            // 
            // ts_PauseW
            // 
            this.ts_PauseW.Name = "ts_PauseW";
            this.ts_PauseW.Size = new System.Drawing.Size(215, 24);
            this.ts_PauseW.Text = "Pause ETL Workers";
            this.ts_PauseW.Click += new System.EventHandler(this.stopServerToolStripMenuItem_Click);
            // 
            // ts_ResumeW
            // 
            this.ts_ResumeW.Name = "ts_ResumeW";
            this.ts_ResumeW.Size = new System.Drawing.Size(215, 24);
            this.ts_ResumeW.Text = "Resume ETL Workers";
            // 
            // ts_NewJob
            // 
            this.ts_NewJob.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.ts_NewJob.Name = "ts_NewJob";
            this.ts_NewJob.Padding = new System.Windows.Forms.Padding(12, 5, 12, 5);
            this.ts_NewJob.Size = new System.Drawing.Size(94, 34);
            this.ts_NewJob.Text = "New Job";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1276, 656);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tc_Main);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainWindow";
            this.Text = "ETL System Client";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tc_Main.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tc_Job.ResumeLayout(false);
            this.tp_Catalogue.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Catalogue)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.StatusStrip statusStrip;
        public System.Windows.Forms.TabControl tc_Main;
        public System.Windows.Forms.TabPage tabPage1;
        public System.Windows.Forms.TabPage tp_Catalogue;
        public System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.ListView lv_JobsList;
        public System.Windows.Forms.TextBox tb_Search;
        public System.Windows.Forms.Button btn_searchjob;
        public System.Windows.Forms.Button btn_clear;
        public System.Windows.Forms.TabPage tp_Queue;
        public System.Windows.Forms.MenuStrip menuStrip;
        public System.Windows.Forms.ToolStripMenuItem ts_User;
        public System.Windows.Forms.ToolStripMenuItem ts_Login;
        public System.Windows.Forms.ToolStripMenuItem ts_Logout;
        public System.Windows.Forms.ToolStripMenuItem ts_NewJob;
        public System.Windows.Forms.TabPage tp_Graph;
        public System.Windows.Forms.TabPage tp_Admin;
        public System.Windows.Forms.TabControl tc_Job;
        public System.Windows.Forms.TabPage tp_Definition;
        public System.Windows.Forms.TabPage tp_Schedules;
        public System.Windows.Forms.TabPage tp_Dependencies;
        public System.Windows.Forms.ToolStripStatusLabel tslb_Status;
        public System.Windows.Forms.ToolStripMenuItem ts_Server;
        public System.Windows.Forms.ToolStripMenuItem ts_PauseQ;
        public System.Windows.Forms.ToolStripMenuItem ts_ResumeQ;
        public System.Windows.Forms.ToolStripMenuItem ts_PauseW;
        public System.Windows.Forms.ToolStripStatusLabel tslb_Response;
        public System.Windows.Forms.ToolStripMenuItem ts_ResumeW;
        public System.Windows.Forms.DataGridView dgv_Catalogue;
    }
}

