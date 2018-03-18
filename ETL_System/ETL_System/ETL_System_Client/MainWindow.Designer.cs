using System.Windows.Forms;
using Microsoft.Msagl.GraphViewerGdi;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tslb_Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslb_Response = new System.Windows.Forms.ToolStripStatusLabel();
            this.tc_Main = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tc_Job = new System.Windows.Forms.TabControl();
            this.tp_Definition = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btn_Create = new System.Windows.Forms.Button();
            this.gb_General = new System.Windows.Forms.GroupBox();
            this.cb_CheckppointType = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.tb_Checkpoint = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tb_Id = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cb_Type = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_Name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_Executable = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_MaxAttempts = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tb_DelaySecs = new System.Windows.Forms.TextBox();
            this.tb_Notifications = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_LatencyAlert = new System.Windows.Forms.TextBox();
            this.gb_Status = new System.Windows.Forms.GroupBox();
            this.btn_Activation = new System.Windows.Forms.Button();
            this.btn_Pausing = new System.Windows.Forms.Button();
            this.tb_IsFailed = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tb_IsPaused = new System.Windows.Forms.TextBox();
            this.tb_isActive = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tb_FailedAttempts = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.gb_Changes = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tb_When = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tb_Who = new System.Windows.Forms.TextBox();
            this.tp_JobInstances = new System.Windows.Forms.TabPage();
            this.tp_Schedules = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lv_Schedules = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.btn_AddSchedule = new System.Windows.Forms.Button();
            this.dtp_Schedule = new System.Windows.Forms.DateTimePicker();
            this.cb_ScheduleType = new System.Windows.Forms.ComboBox();
            this.tp_Dependencies = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btn_DelDependency = new System.Windows.Forms.Button();
            this.lv_Dependencies = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.cb_DepJobs = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.btn_AddDependency = new System.Windows.Forms.Button();
            this.cb_DepTypes = new System.Windows.Forms.ComboBox();
            this.tp_Catalogue = new System.Windows.Forms.TabPage();
            this.dgv_Catalogue = new System.Windows.Forms.DataGridView();
            this.tp_Queue = new System.Windows.Forms.TabPage();
            this.dgv_Queue = new System.Windows.Forms.DataGridView();
            this.tp_Graph = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.cb_RenderType = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.nud_Depth = new System.Windows.Forms.NumericUpDown();
            this.label21 = new System.Windows.Forms.Label();
            this.gViewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            this.tp_Admin = new System.Windows.Forms.TabPage();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.tb_ConnectionString = new System.Windows.Forms.TextBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.cb_SSL = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.tb_EmailPass = new System.Windows.Forms.TextBox();
            this.tb_Smtp = new System.Windows.Forms.TextBox();
            this.tb_Port = new System.Windows.Forms.TextBox();
            this.tb_EmailUser = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.cb_UserIsActive = new System.Windows.Forms.CheckBox();
            this.label25 = new System.Windows.Forms.Label();
            this.tb_NewUserPass = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.btn_NewUser = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.cb_NewUserRole = new System.Windows.Forms.ComboBox();
            this.tb_NewUserName = new System.Windows.Forms.TextBox();
            this.btn_DelUser = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lv_JobsList = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_clear = new System.Windows.Forms.Button();
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
            this.lv_Users = new System.Windows.Forms.ListView();
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStrip.SuspendLayout();
            this.tc_Main.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tc_Job.SuspendLayout();
            this.tp_Definition.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.gb_General.SuspendLayout();
            this.gb_Status.SuspendLayout();
            this.gb_Changes.SuspendLayout();
            this.tp_Schedules.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tp_Dependencies.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tp_Catalogue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Catalogue)).BeginInit();
            this.tp_Queue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Queue)).BeginInit();
            this.tp_Graph.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Depth)).BeginInit();
            this.tp_Admin.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslb_Status,
            this.tslb_Response});
            this.statusStrip.Location = new System.Drawing.Point(0, 808);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1796, 24);
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
            this.tslb_Response.Size = new System.Drawing.Size(118, 19);
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
            this.tc_Main.Size = new System.Drawing.Size(1546, 747);
            this.tc_Main.TabIndex = 7;
            this.tc_Main.SelectedIndexChanged += new System.EventHandler(this.tc_Main_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tc_Job);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1538, 718);
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
            this.tc_Job.Controls.Add(this.tp_JobInstances);
            this.tc_Job.Controls.Add(this.tp_Schedules);
            this.tc_Job.Controls.Add(this.tp_Dependencies);
            this.tc_Job.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tc_Job.Location = new System.Drawing.Point(6, 6);
            this.tc_Job.Name = "tc_Job";
            this.tc_Job.SelectedIndex = 0;
            this.tc_Job.Size = new System.Drawing.Size(1532, 709);
            this.tc_Job.TabIndex = 2;
            this.tc_Job.SelectedIndexChanged += new System.EventHandler(this.tc_Job_Selected);
            // 
            // tp_Definition
            // 
            this.tp_Definition.Controls.Add(this.groupBox4);
            this.tp_Definition.Controls.Add(this.gb_Changes);
            this.tp_Definition.Location = new System.Drawing.Point(4, 24);
            this.tp_Definition.Name = "tp_Definition";
            this.tp_Definition.Padding = new System.Windows.Forms.Padding(3);
            this.tp_Definition.Size = new System.Drawing.Size(1524, 681);
            this.tp_Definition.TabIndex = 0;
            this.tp_Definition.Text = "Definition";
            this.tp_Definition.UseVisualStyleBackColor = true;
            this.tp_Definition.Click += new System.EventHandler(this.tabPage6_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btn_Create);
            this.groupBox4.Controls.Add(this.gb_General);
            this.groupBox4.Controls.Add(this.gb_Status);
            this.groupBox4.Controls.Add(this.btn_Delete);
            this.groupBox4.Controls.Add(this.btn_Save);
            this.groupBox4.Location = new System.Drawing.Point(18, 21);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(779, 644);
            this.groupBox4.TabIndex = 29;
            this.groupBox4.TabStop = false;
            // 
            // btn_Create
            // 
            this.btn_Create.BackColor = System.Drawing.Color.Transparent;
            this.btn_Create.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Create.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_Create.Image = global::ETL_System_Client.Properties.Resources.create;
            this.btn_Create.Location = new System.Drawing.Point(155, 564);
            this.btn_Create.Name = "btn_Create";
            this.btn_Create.Size = new System.Drawing.Size(135, 39);
            this.btn_Create.TabIndex = 29;
            this.btn_Create.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Create.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_Create.UseVisualStyleBackColor = false;
            this.btn_Create.Click += new System.EventHandler(this.button1_Click);
            // 
            // gb_General
            // 
            this.gb_General.Controls.Add(this.cb_CheckppointType);
            this.gb_General.Controls.Add(this.label20);
            this.gb_General.Controls.Add(this.tb_Checkpoint);
            this.gb_General.Controls.Add(this.label17);
            this.gb_General.Controls.Add(this.tb_Id);
            this.gb_General.Controls.Add(this.label14);
            this.gb_General.Controls.Add(this.cb_Type);
            this.gb_General.Controls.Add(this.label3);
            this.gb_General.Controls.Add(this.tb_Name);
            this.gb_General.Controls.Add(this.label1);
            this.gb_General.Controls.Add(this.tb_Executable);
            this.gb_General.Controls.Add(this.label2);
            this.gb_General.Controls.Add(this.tb_MaxAttempts);
            this.gb_General.Controls.Add(this.label4);
            this.gb_General.Controls.Add(this.label8);
            this.gb_General.Controls.Add(this.tb_DelaySecs);
            this.gb_General.Controls.Add(this.tb_Notifications);
            this.gb_General.Controls.Add(this.label7);
            this.gb_General.Controls.Add(this.label6);
            this.gb_General.Controls.Add(this.tb_LatencyAlert);
            this.gb_General.Location = new System.Drawing.Point(10, 16);
            this.gb_General.Name = "gb_General";
            this.gb_General.Size = new System.Drawing.Size(758, 284);
            this.gb_General.TabIndex = 28;
            this.gb_General.TabStop = false;
            this.gb_General.Text = "General";
            // 
            // cb_CheckppointType
            // 
            this.cb_CheckppointType.FormattingEnabled = true;
            this.cb_CheckppointType.Items.AddRange(new object[] {
            "Data-Int",
            "Timestamp"});
            this.cb_CheckppointType.Location = new System.Drawing.Point(130, 185);
            this.cb_CheckppointType.Name = "cb_CheckppointType";
            this.cb_CheckppointType.Size = new System.Drawing.Size(127, 23);
            this.cb_CheckppointType.TabIndex = 6;
            this.cb_CheckppointType.SelectedIndexChanged += new System.EventHandler(this.cb_CheckppointType_SelectedIndexChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(27, 191);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(100, 15);
            this.label20.TabIndex = 33;
            this.label20.Text = "Checkpoint Type:";
            // 
            // tb_Checkpoint
            // 
            this.tb_Checkpoint.Location = new System.Drawing.Point(350, 187);
            this.tb_Checkpoint.Name = "tb_Checkpoint";
            this.tb_Checkpoint.Size = new System.Drawing.Size(139, 21);
            this.tb_Checkpoint.TabIndex = 7;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(276, 191);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(71, 15);
            this.label17.TabIndex = 31;
            this.label17.Text = "Checkpoint:";
            // 
            // tb_Id
            // 
            this.tb_Id.Location = new System.Drawing.Point(627, 38);
            this.tb_Id.Name = "tb_Id";
            this.tb_Id.ReadOnly = true;
            this.tb_Id.Size = new System.Drawing.Size(117, 21);
            this.tb_Id.TabIndex = 28;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(535, 41);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(86, 15);
            this.label14.TabIndex = 29;
            this.label14.Text = "Job System Id:";
            // 
            // cb_Type
            // 
            this.cb_Type.FormattingEnabled = true;
            this.cb_Type.Items.AddRange(new object[] {
            "Schedule",
            "Dependency"});
            this.cb_Type.Location = new System.Drawing.Point(130, 85);
            this.cb_Type.Name = "cb_Type";
            this.cb_Type.Size = new System.Drawing.Size(304, 23);
            this.cb_Type.TabIndex = 1;
            this.cb_Type.SelectedIndexChanged += new System.EventHandler(this.cb_Type_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(68, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 15);
            this.label3.TabIndex = 27;
            this.label3.Text = "Job Type:";
            // 
            // tb_Name
            // 
            this.tb_Name.Location = new System.Drawing.Point(130, 41);
            this.tb_Name.Name = "tb_Name";
            this.tb_Name.Size = new System.Drawing.Size(304, 21);
            this.tb_Name.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(83, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name:";
            // 
            // tb_Executable
            // 
            this.tb_Executable.Location = new System.Drawing.Point(130, 136);
            this.tb_Executable.Name = "tb_Executable";
            this.tb_Executable.Size = new System.Drawing.Size(304, 21);
            this.tb_Executable.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(59, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Executable:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // tb_MaxAttempts
            // 
            this.tb_MaxAttempts.Location = new System.Drawing.Point(627, 87);
            this.tb_MaxAttempts.Name = "tb_MaxAttempts";
            this.tb_MaxAttempts.Size = new System.Drawing.Size(117, 21);
            this.tb_MaxAttempts.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(530, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "#Max Attempts:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 251);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(122, 15);
            this.label8.TabIndex = 15;
            this.label8.Text = "Notifications mail list:";
            // 
            // tb_DelaySecs
            // 
            this.tb_DelaySecs.Location = new System.Drawing.Point(627, 185);
            this.tb_DelaySecs.Name = "tb_DelaySecs";
            this.tb_DelaySecs.Size = new System.Drawing.Size(117, 21);
            this.tb_DelaySecs.TabIndex = 5;
            // 
            // tb_Notifications
            // 
            this.tb_Notifications.Location = new System.Drawing.Point(144, 248);
            this.tb_Notifications.Name = "tb_Notifications";
            this.tb_Notifications.Size = new System.Drawing.Size(600, 21);
            this.tb_Notifications.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(529, 188);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 15);
            this.label7.TabIndex = 11;
            this.label7.Text = "Delay Seconds:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(491, 139);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(130, 15);
            this.label6.TabIndex = 13;
            this.label6.Text = "Latency Alert Seconds:";
            // 
            // tb_LatencyAlert
            // 
            this.tb_LatencyAlert.Location = new System.Drawing.Point(627, 136);
            this.tb_LatencyAlert.Name = "tb_LatencyAlert";
            this.tb_LatencyAlert.Size = new System.Drawing.Size(117, 21);
            this.tb_LatencyAlert.TabIndex = 4;
            // 
            // gb_Status
            // 
            this.gb_Status.Controls.Add(this.btn_Activation);
            this.gb_Status.Controls.Add(this.btn_Pausing);
            this.gb_Status.Controls.Add(this.tb_IsFailed);
            this.gb_Status.Controls.Add(this.label13);
            this.gb_Status.Controls.Add(this.label11);
            this.gb_Status.Controls.Add(this.tb_IsPaused);
            this.gb_Status.Controls.Add(this.tb_isActive);
            this.gb_Status.Controls.Add(this.label12);
            this.gb_Status.Controls.Add(this.tb_FailedAttempts);
            this.gb_Status.Controls.Add(this.label5);
            this.gb_Status.Location = new System.Drawing.Point(148, 319);
            this.gb_Status.Name = "gb_Status";
            this.gb_Status.Size = new System.Drawing.Size(483, 148);
            this.gb_Status.TabIndex = 25;
            this.gb_Status.TabStop = false;
            this.gb_Status.Text = "Status";
            this.gb_Status.Enter += new System.EventHandler(this.Status_Enter);
            // 
            // btn_Activation
            // 
            this.btn_Activation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Activation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_Activation.Location = new System.Drawing.Point(276, 65);
            this.btn_Activation.Name = "btn_Activation";
            this.btn_Activation.Size = new System.Drawing.Size(75, 23);
            this.btn_Activation.TabIndex = 10;
            this.btn_Activation.Text = "Deactivate";
            this.btn_Activation.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_Activation.UseVisualStyleBackColor = true;
            this.btn_Activation.Click += new System.EventHandler(this.btn_Activation_Click);
            // 
            // btn_Pausing
            // 
            this.btn_Pausing.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Pausing.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_Pausing.Location = new System.Drawing.Point(276, 105);
            this.btn_Pausing.Name = "btn_Pausing";
            this.btn_Pausing.Size = new System.Drawing.Size(75, 23);
            this.btn_Pausing.TabIndex = 12;
            this.btn_Pausing.Text = "Pause";
            this.btn_Pausing.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_Pausing.UseVisualStyleBackColor = true;
            this.btn_Pausing.Click += new System.EventHandler(this.btn_Pausing_Click);
            // 
            // tb_IsFailed
            // 
            this.tb_IsFailed.BackColor = System.Drawing.Color.Chartreuse;
            this.tb_IsFailed.Location = new System.Drawing.Point(120, 29);
            this.tb_IsFailed.Name = "tb_IsFailed";
            this.tb_IsFailed.ReadOnly = true;
            this.tb_IsFailed.Size = new System.Drawing.Size(69, 21);
            this.tb_IsFailed.TabIndex = 9;
            this.tb_IsFailed.Text = "NO";
            this.tb_IsFailed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(101, 110);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(68, 15);
            this.label13.TabIndex = 24;
            this.label13.Text = "Is Paused?";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(44, 32);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(60, 15);
            this.label11.TabIndex = 20;
            this.label11.Text = "Is Failed?";
            // 
            // tb_IsPaused
            // 
            this.tb_IsPaused.Location = new System.Drawing.Point(185, 107);
            this.tb_IsPaused.Name = "tb_IsPaused";
            this.tb_IsPaused.ReadOnly = true;
            this.tb_IsPaused.Size = new System.Drawing.Size(69, 21);
            this.tb_IsPaused.TabIndex = 12;
            this.tb_IsPaused.TextChanged += new System.EventHandler(this.tb_IsPaused_TextChanged);
            // 
            // tb_isActive
            // 
            this.tb_isActive.Location = new System.Drawing.Point(185, 67);
            this.tb_isActive.Name = "tb_isActive";
            this.tb_isActive.ReadOnly = true;
            this.tb_isActive.Size = new System.Drawing.Size(69, 21);
            this.tb_isActive.TabIndex = 10;
            this.tb_isActive.TextChanged += new System.EventHandler(this.tb_isActive_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(114, 70);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(57, 15);
            this.label12.TabIndex = 22;
            this.label12.Text = "Is Active?";
            // 
            // tb_FailedAttempts
            // 
            this.tb_FailedAttempts.Location = new System.Drawing.Point(324, 26);
            this.tb_FailedAttempts.Name = "tb_FailedAttempts";
            this.tb_FailedAttempts.Size = new System.Drawing.Size(69, 21);
            this.tb_FailedAttempts.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(217, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "#Failed Attempts:";
            // 
            // btn_Delete
            // 
            this.btn_Delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Delete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_Delete.Image = global::ETL_System_Client.Properties.Resources.if_trash_can_delete_44014;
            this.btn_Delete.Location = new System.Drawing.Point(503, 564);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(135, 39);
            this.btn_Delete.TabIndex = 18;
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.BackColor = System.Drawing.Color.Transparent;
            this.btn_Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Save.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_Save.Image = global::ETL_System_Client.Properties.Resources.if_Loadsave;
            this.btn_Save.Location = new System.Drawing.Point(331, 564);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(135, 39);
            this.btn_Save.TabIndex = 11;
            this.btn_Save.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_Save.UseVisualStyleBackColor = false;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // gb_Changes
            // 
            this.gb_Changes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_Changes.Controls.Add(this.label10);
            this.gb_Changes.Controls.Add(this.tb_When);
            this.gb_Changes.Controls.Add(this.label9);
            this.gb_Changes.Controls.Add(this.tb_Who);
            this.gb_Changes.Location = new System.Drawing.Point(1290, 571);
            this.gb_Changes.Name = "gb_Changes";
            this.gb_Changes.Size = new System.Drawing.Size(228, 104);
            this.gb_Changes.TabIndex = 16;
            this.gb_Changes.TabStop = false;
            this.gb_Changes.Text = "Audit | Last Job Change";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 73);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(46, 15);
            this.label10.TabIndex = 5;
            this.label10.Text = "When?";
            // 
            // tb_When
            // 
            this.tb_When.Location = new System.Drawing.Point(66, 70);
            this.tb_When.Name = "tb_When";
            this.tb_When.ReadOnly = true;
            this.tb_When.Size = new System.Drawing.Size(145, 21);
            this.tb_When.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 35);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 15);
            this.label9.TabIndex = 3;
            this.label9.Text = "Who?";
            // 
            // tb_Who
            // 
            this.tb_Who.Location = new System.Drawing.Point(66, 32);
            this.tb_Who.Name = "tb_Who";
            this.tb_Who.ReadOnly = true;
            this.tb_Who.Size = new System.Drawing.Size(145, 21);
            this.tb_Who.TabIndex = 2;
            // 
            // tp_JobInstances
            // 
            this.tp_JobInstances.Location = new System.Drawing.Point(4, 24);
            this.tp_JobInstances.Name = "tp_JobInstances";
            this.tp_JobInstances.Padding = new System.Windows.Forms.Padding(3);
            this.tp_JobInstances.Size = new System.Drawing.Size(1524, 681);
            this.tp_JobInstances.TabIndex = 3;
            this.tp_JobInstances.Text = "History";
            this.tp_JobInstances.UseVisualStyleBackColor = true;
            // 
            // tp_Schedules
            // 
            this.tp_Schedules.Controls.Add(this.groupBox3);
            this.tp_Schedules.Controls.Add(this.groupBox2);
            this.tp_Schedules.Location = new System.Drawing.Point(4, 24);
            this.tp_Schedules.Name = "tp_Schedules";
            this.tp_Schedules.Padding = new System.Windows.Forms.Padding(3);
            this.tp_Schedules.Size = new System.Drawing.Size(1524, 681);
            this.tp_Schedules.TabIndex = 1;
            this.tp_Schedules.Text = "Schedules";
            this.tp_Schedules.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.lv_Schedules);
            this.groupBox3.Location = new System.Drawing.Point(411, 22);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(433, 574);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Saved Schedules";
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.Image = global::ETL_System_Client.Properties.Resources.if_trash_can_delete_44014;
            this.button1.Location = new System.Drawing.Point(292, 525);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 39);
            this.button1.TabIndex = 19;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // lv_Schedules
            // 
            this.lv_Schedules.CheckBoxes = true;
            this.lv_Schedules.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lv_Schedules.FullRowSelect = true;
            this.lv_Schedules.GridLines = true;
            this.lv_Schedules.HideSelection = false;
            this.lv_Schedules.Location = new System.Drawing.Point(12, 20);
            this.lv_Schedules.Name = "lv_Schedules";
            this.lv_Schedules.Size = new System.Drawing.Size(415, 484);
            this.lv_Schedules.TabIndex = 0;
            this.lv_Schedules.UseCompatibleStateImageBehavior = false;
            this.lv_Schedules.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            this.columnHeader1.Width = 78;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Type";
            this.columnHeader2.Width = 119;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Next Execution";
            this.columnHeader3.Width = 212;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.btn_AddSchedule);
            this.groupBox2.Controls.Add(this.dtp_Schedule);
            this.groupBox2.Controls.Add(this.cb_ScheduleType);
            this.groupBox2.Location = new System.Drawing.Point(21, 22);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(359, 190);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Add New";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(27, 98);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(67, 15);
            this.label16.TabIndex = 32;
            this.label16.Text = "Start From:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(58, 39);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(36, 15);
            this.label15.TabIndex = 31;
            this.label15.Text = "Type:";
            // 
            // btn_AddSchedule
            // 
            this.btn_AddSchedule.BackColor = System.Drawing.Color.Transparent;
            this.btn_AddSchedule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_AddSchedule.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_AddSchedule.Image = global::ETL_System_Client.Properties.Resources.create;
            this.btn_AddSchedule.Location = new System.Drawing.Point(216, 140);
            this.btn_AddSchedule.Name = "btn_AddSchedule";
            this.btn_AddSchedule.Size = new System.Drawing.Size(135, 39);
            this.btn_AddSchedule.TabIndex = 30;
            this.btn_AddSchedule.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_AddSchedule.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_AddSchedule.UseVisualStyleBackColor = false;
            this.btn_AddSchedule.Click += new System.EventHandler(this.btn_AddSchedule_Click);
            // 
            // dtp_Schedule
            // 
            this.dtp_Schedule.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtp_Schedule.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_Schedule.Location = new System.Drawing.Point(113, 93);
            this.dtp_Schedule.Name = "dtp_Schedule";
            this.dtp_Schedule.Size = new System.Drawing.Size(238, 21);
            this.dtp_Schedule.TabIndex = 4;
            // 
            // cb_ScheduleType
            // 
            this.cb_ScheduleType.FormattingEnabled = true;
            this.cb_ScheduleType.Location = new System.Drawing.Point(113, 36);
            this.cb_ScheduleType.Name = "cb_ScheduleType";
            this.cb_ScheduleType.Size = new System.Drawing.Size(238, 23);
            this.cb_ScheduleType.TabIndex = 3;
            // 
            // tp_Dependencies
            // 
            this.tp_Dependencies.Controls.Add(this.groupBox5);
            this.tp_Dependencies.Controls.Add(this.groupBox6);
            this.tp_Dependencies.Location = new System.Drawing.Point(4, 24);
            this.tp_Dependencies.Name = "tp_Dependencies";
            this.tp_Dependencies.Padding = new System.Windows.Forms.Padding(3);
            this.tp_Dependencies.Size = new System.Drawing.Size(1524, 681);
            this.tp_Dependencies.TabIndex = 2;
            this.tp_Dependencies.Text = "Dependencies";
            this.tp_Dependencies.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btn_DelDependency);
            this.groupBox5.Controls.Add(this.lv_Dependencies);
            this.groupBox5.Location = new System.Drawing.Point(411, 22);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(433, 574);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Saved Dependencies";
            // 
            // btn_DelDependency
            // 
            this.btn_DelDependency.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DelDependency.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_DelDependency.Image = global::ETL_System_Client.Properties.Resources.if_trash_can_delete_44014;
            this.btn_DelDependency.Location = new System.Drawing.Point(292, 525);
            this.btn_DelDependency.Name = "btn_DelDependency";
            this.btn_DelDependency.Size = new System.Drawing.Size(135, 39);
            this.btn_DelDependency.TabIndex = 19;
            this.btn_DelDependency.UseVisualStyleBackColor = true;
            this.btn_DelDependency.Click += new System.EventHandler(this.btn_DelDependency_Click);
            // 
            // lv_Dependencies
            // 
            this.lv_Dependencies.CheckBoxes = true;
            this.lv_Dependencies.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.lv_Dependencies.FullRowSelect = true;
            this.lv_Dependencies.GridLines = true;
            this.lv_Dependencies.HideSelection = false;
            this.lv_Dependencies.Location = new System.Drawing.Point(12, 20);
            this.lv_Dependencies.Name = "lv_Dependencies";
            this.lv_Dependencies.Size = new System.Drawing.Size(415, 484);
            this.lv_Dependencies.TabIndex = 0;
            this.lv_Dependencies.UseCompatibleStateImageBehavior = false;
            this.lv_Dependencies.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "ID";
            this.columnHeader5.Width = 78;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Type";
            this.columnHeader6.Width = 119;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Job Dependency";
            this.columnHeader7.Width = 212;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.cb_DepJobs);
            this.groupBox6.Controls.Add(this.label18);
            this.groupBox6.Controls.Add(this.label19);
            this.groupBox6.Controls.Add(this.btn_AddDependency);
            this.groupBox6.Controls.Add(this.cb_DepTypes);
            this.groupBox6.Location = new System.Drawing.Point(21, 22);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(359, 190);
            this.groupBox6.TabIndex = 6;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Add New";
            // 
            // cb_DepJobs
            // 
            this.cb_DepJobs.FormattingEnabled = true;
            this.cb_DepJobs.Location = new System.Drawing.Point(113, 91);
            this.cb_DepJobs.Name = "cb_DepJobs";
            this.cb_DepJobs.Size = new System.Drawing.Size(238, 23);
            this.cb_DepJobs.TabIndex = 33;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(27, 98);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(67, 15);
            this.label18.TabIndex = 32;
            this.label18.Text = "Job Name:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(58, 39);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(36, 15);
            this.label19.TabIndex = 31;
            this.label19.Text = "Type:";
            // 
            // btn_AddDependency
            // 
            this.btn_AddDependency.BackColor = System.Drawing.Color.Transparent;
            this.btn_AddDependency.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_AddDependency.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_AddDependency.Image = global::ETL_System_Client.Properties.Resources.create;
            this.btn_AddDependency.Location = new System.Drawing.Point(216, 140);
            this.btn_AddDependency.Name = "btn_AddDependency";
            this.btn_AddDependency.Size = new System.Drawing.Size(135, 39);
            this.btn_AddDependency.TabIndex = 30;
            this.btn_AddDependency.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_AddDependency.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_AddDependency.UseVisualStyleBackColor = false;
            this.btn_AddDependency.Click += new System.EventHandler(this.btn_AddDependency_Click);
            // 
            // cb_DepTypes
            // 
            this.cb_DepTypes.FormattingEnabled = true;
            this.cb_DepTypes.Location = new System.Drawing.Point(113, 36);
            this.cb_DepTypes.Name = "cb_DepTypes";
            this.cb_DepTypes.Size = new System.Drawing.Size(238, 23);
            this.cb_DepTypes.TabIndex = 3;
            // 
            // tp_Catalogue
            // 
            this.tp_Catalogue.Controls.Add(this.dgv_Catalogue);
            this.tp_Catalogue.Location = new System.Drawing.Point(4, 25);
            this.tp_Catalogue.Name = "tp_Catalogue";
            this.tp_Catalogue.Padding = new System.Windows.Forms.Padding(3);
            this.tp_Catalogue.Size = new System.Drawing.Size(1538, 718);
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
            this.dgv_Catalogue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Catalogue.GridColor = System.Drawing.SystemColors.Control;
            this.dgv_Catalogue.Location = new System.Drawing.Point(22, 13);
            this.dgv_Catalogue.Name = "dgv_Catalogue";
            this.dgv_Catalogue.ReadOnly = true;
            this.dgv_Catalogue.RowHeadersVisible = false;
            this.dgv_Catalogue.ShowRowErrors = false;
            this.dgv_Catalogue.Size = new System.Drawing.Size(1498, 699);
            this.dgv_Catalogue.TabIndex = 0;
            // 
            // tp_Queue
            // 
            this.tp_Queue.Controls.Add(this.dgv_Queue);
            this.tp_Queue.Location = new System.Drawing.Point(4, 25);
            this.tp_Queue.Name = "tp_Queue";
            this.tp_Queue.Padding = new System.Windows.Forms.Padding(3);
            this.tp_Queue.Size = new System.Drawing.Size(1538, 718);
            this.tp_Queue.TabIndex = 2;
            this.tp_Queue.Text = "Queue";
            this.tp_Queue.UseVisualStyleBackColor = true;
            this.tp_Queue.Click += new System.EventHandler(this.tp_Queue_Click);
            // 
            // dgv_Queue
            // 
            this.dgv_Queue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgv_Queue.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgv_Queue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Queue.GridColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgv_Queue.Location = new System.Drawing.Point(22, 13);
            this.dgv_Queue.Name = "dgv_Queue";
            this.dgv_Queue.RowHeadersVisible = false;
            this.dgv_Queue.Size = new System.Drawing.Size(852, 699);
            this.dgv_Queue.TabIndex = 0;
            // 
            // tp_Graph
            // 
            this.tp_Graph.Controls.Add(this.groupBox7);
            this.tp_Graph.Controls.Add(this.gViewer);
            this.tp_Graph.Location = new System.Drawing.Point(4, 25);
            this.tp_Graph.Name = "tp_Graph";
            this.tp_Graph.Padding = new System.Windows.Forms.Padding(3);
            this.tp_Graph.Size = new System.Drawing.Size(1538, 718);
            this.tp_Graph.TabIndex = 3;
            this.tp_Graph.Text = "Dependency Graph";
            this.tp_Graph.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox7.Controls.Add(this.cb_RenderType);
            this.groupBox7.Controls.Add(this.label22);
            this.groupBox7.Controls.Add(this.nud_Depth);
            this.groupBox7.Controls.Add(this.label21);
            this.groupBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox7.Location = new System.Drawing.Point(7, 6);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(99, 706);
            this.groupBox7.TabIndex = 4;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "View Settings";
            // 
            // cb_RenderType
            // 
            this.cb_RenderType.FormattingEnabled = true;
            this.cb_RenderType.Items.AddRange(new object[] {
            "All",
            "Only Left",
            "Only Right"});
            this.cb_RenderType.Location = new System.Drawing.Point(6, 60);
            this.cb_RenderType.Name = "cb_RenderType";
            this.cb_RenderType.Size = new System.Drawing.Size(84, 23);
            this.cb_RenderType.TabIndex = 1;
            this.cb_RenderType.SelectedIndexChanged += new System.EventHandler(this.cb_RenderType_SelectedIndexChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label22.Location = new System.Drawing.Point(6, 105);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(43, 15);
            this.label22.TabIndex = 5;
            this.label22.Text = "Depth:";
            // 
            // nud_Depth
            // 
            this.nud_Depth.Location = new System.Drawing.Point(6, 130);
            this.nud_Depth.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nud_Depth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_Depth.Name = "nud_Depth";
            this.nud_Depth.Size = new System.Drawing.Size(81, 21);
            this.nud_Depth.TabIndex = 4;
            this.nud_Depth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_Depth.ValueChanged += new System.EventHandler(this.nud_Depth_ValueChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(3, 39);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(76, 15);
            this.label21.TabIndex = 3;
            this.label21.Text = "Render type:";
            // 
            // gViewer
            // 
            this.gViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gViewer.ArrowheadLength = 10D;
            this.gViewer.AsyncLayout = false;
            this.gViewer.AutoScroll = true;
            this.gViewer.BackColor = System.Drawing.Color.White;
            this.gViewer.BackwardEnabled = false;
            this.gViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gViewer.BuildHitTree = true;
            this.gViewer.CurrentLayoutMethod = Microsoft.Msagl.GraphViewerGdi.LayoutMethod.UseSettingsOfTheGraph;
            this.gViewer.EdgeInsertButtonVisible = true;
            this.gViewer.FileName = "";
            this.gViewer.ForwardEnabled = false;
            this.gViewer.Graph = null;
            this.gViewer.InsertingEdge = false;
            this.gViewer.LayoutAlgorithmSettingsButtonVisible = true;
            this.gViewer.LayoutEditingEnabled = true;
            this.gViewer.Location = new System.Drawing.Point(112, 6);
            this.gViewer.LooseOffsetForRouting = 0.25D;
            this.gViewer.MouseHitDistance = 0.05D;
            this.gViewer.Name = "gViewer";
            this.gViewer.NavigationVisible = true;
            this.gViewer.NeedToCalculateLayout = true;
            this.gViewer.OffsetForRelaxingInRouting = 0.6D;
            this.gViewer.PaddingForEdgeRouting = 8D;
            this.gViewer.PanButtonPressed = false;
            this.gViewer.SaveAsImageEnabled = true;
            this.gViewer.SaveAsMsaglEnabled = true;
            this.gViewer.SaveButtonVisible = true;
            this.gViewer.SaveGraphButtonVisible = true;
            this.gViewer.SaveInVectorFormatEnabled = true;
            this.gViewer.Size = new System.Drawing.Size(1420, 706);
            this.gViewer.TabIndex = 3;
            this.gViewer.TightOffsetForRouting = 0.125D;
            this.gViewer.ToolBarIsVisible = true;
            this.gViewer.Transform = ((Microsoft.Msagl.Core.Geometry.Curves.PlaneTransformation)(resources.GetObject("gViewer.Transform")));
            this.gViewer.UndoRedoButtonsVisible = true;
            this.gViewer.WindowZoomButtonPressed = false;
            this.gViewer.ZoomF = 1D;
            this.gViewer.ZoomWindowThreshold = 0.05D;
            // 
            // tp_Admin
            // 
            this.tp_Admin.AutoScroll = true;
            this.tp_Admin.Controls.Add(this.groupBox11);
            this.tp_Admin.Controls.Add(this.groupBox10);
            this.tp_Admin.Controls.Add(this.groupBox8);
            this.tp_Admin.Location = new System.Drawing.Point(4, 25);
            this.tp_Admin.Name = "tp_Admin";
            this.tp_Admin.Padding = new System.Windows.Forms.Padding(3);
            this.tp_Admin.Size = new System.Drawing.Size(1538, 718);
            this.tp_Admin.TabIndex = 4;
            this.tp_Admin.Text = "Administer";
            this.tp_Admin.UseVisualStyleBackColor = true;
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.button3);
            this.groupBox11.Controls.Add(this.textBox4);
            this.groupBox11.Controls.Add(this.textBox3);
            this.groupBox11.Controls.Add(this.textBox1);
            this.groupBox11.Controls.Add(this.label34);
            this.groupBox11.Controls.Add(this.label33);
            this.groupBox11.Controls.Add(this.label32);
            this.groupBox11.Controls.Add(this.label31);
            this.groupBox11.Controls.Add(this.textBox2);
            this.groupBox11.Controls.Add(this.label30);
            this.groupBox11.Controls.Add(this.tb_ConnectionString);
            this.groupBox11.Location = new System.Drawing.Point(34, 39);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(650, 604);
            this.groupBox11.TabIndex = 2;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Configurations";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button3.Image = global::ETL_System_Client.Properties.Resources.if_Loadsave;
            this.button3.Location = new System.Drawing.Point(529, 541);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(74, 39);
            this.button3.TabIndex = 42;
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button3.UseVisualStyleBackColor = false;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(276, 485);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(83, 23);
            this.textBox4.TabIndex = 9;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(520, 425);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(83, 23);
            this.textBox3.TabIndex = 8;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(153, 425);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(83, 23);
            this.textBox1.TabIndex = 7;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(49, 488);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(221, 17);
            this.label34.TabIndex = 6;
            this.label34.Text = "Queue Scan Frequency Seconds:";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(279, 428);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(235, 17);
            this.label33.TabIndex = 5;
            this.label33.Text = "Worker Fetch Frequencey Seconds:";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(40, 428);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(107, 17);
            this.label32.TabIndex = 4;
            this.label32.Text = "# ETL Workers:";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(40, 250);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(160, 17);
            this.label31.TabIndex = 3;
            this.label31.Text = "Executables folder path:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(43, 274);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(560, 107);
            this.textBox2.TabIndex = 2;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(40, 45);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(176, 17);
            this.label30.TabIndex = 1;
            this.label30.Text = "App DB Connection String:";
            // 
            // tb_ConnectionString
            // 
            this.tb_ConnectionString.Location = new System.Drawing.Point(43, 70);
            this.tb_ConnectionString.Multiline = true;
            this.tb_ConnectionString.Name = "tb_ConnectionString";
            this.tb_ConnectionString.Size = new System.Drawing.Size(560, 142);
            this.tb_ConnectionString.TabIndex = 0;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.cb_SSL);
            this.groupBox10.Controls.Add(this.button2);
            this.groupBox10.Controls.Add(this.tb_EmailPass);
            this.groupBox10.Controls.Add(this.tb_Smtp);
            this.groupBox10.Controls.Add(this.tb_Port);
            this.groupBox10.Controls.Add(this.tb_EmailUser);
            this.groupBox10.Controls.Add(this.label29);
            this.groupBox10.Controls.Add(this.label28);
            this.groupBox10.Controls.Add(this.label27);
            this.groupBox10.Controls.Add(this.label26);
            this.groupBox10.Location = new System.Drawing.Point(737, 454);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(741, 189);
            this.groupBox10.TabIndex = 1;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Email Notifications:";
            this.groupBox10.Enter += new System.EventHandler(this.groupBox10_Enter);
            // 
            // cb_SSL
            // 
            this.cb_SSL.AutoSize = true;
            this.cb_SSL.Checked = global::ETL_System_Client.Properties.Settings.Default.Checked;
            this.cb_SSL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_SSL.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ETL_System_Client.Properties.Settings.Default, "Checked", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cb_SSL.Location = new System.Drawing.Point(610, 78);
            this.cb_SSL.Name = "cb_SSL";
            this.cb_SSL.Size = new System.Drawing.Size(109, 21);
            this.cb_SSL.TabIndex = 42;
            this.cb_SSL.Text = "SSL Enabled";
            this.cb_SSL.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button2.Image = global::ETL_System_Client.Properties.Resources.if_Loadsave;
            this.button2.Location = new System.Drawing.Point(645, 126);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(74, 39);
            this.button2.TabIndex = 41;
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // tb_EmailPass
            // 
            this.tb_EmailPass.Location = new System.Drawing.Point(526, 27);
            this.tb_EmailPass.Name = "tb_EmailPass";
            this.tb_EmailPass.Size = new System.Drawing.Size(193, 23);
            this.tb_EmailPass.TabIndex = 40;
            // 
            // tb_Smtp
            // 
            this.tb_Smtp.Location = new System.Drawing.Point(120, 75);
            this.tb_Smtp.Name = "tb_Smtp";
            this.tb_Smtp.Size = new System.Drawing.Size(306, 23);
            this.tb_Smtp.TabIndex = 39;
            // 
            // tb_Port
            // 
            this.tb_Port.Location = new System.Drawing.Point(492, 75);
            this.tb_Port.Name = "tb_Port";
            this.tb_Port.Size = new System.Drawing.Size(112, 23);
            this.tb_Port.TabIndex = 38;
            // 
            // tb_EmailUser
            // 
            this.tb_EmailUser.Location = new System.Drawing.Point(120, 27);
            this.tb_EmailUser.Name = "tb_EmailUser";
            this.tb_EmailUser.Size = new System.Drawing.Size(306, 23);
            this.tb_EmailUser.TabIndex = 37;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(447, 78);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(38, 17);
            this.label29.TabIndex = 3;
            this.label29.Text = "Port:";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(447, 30);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(73, 17);
            this.label28.TabIndex = 2;
            this.label28.Text = "Password:";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(34, 78);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(50, 17);
            this.label27.TabIndex = 1;
            this.label27.Text = "SMTP:";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(34, 30);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(80, 17);
            this.label26.TabIndex = 0;
            this.label26.Text = "Email User:";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.lv_Users);
            this.groupBox8.Controls.Add(this.groupBox9);
            this.groupBox8.Controls.Add(this.btn_DelUser);
            this.groupBox8.Location = new System.Drawing.Point(737, 39);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(741, 381);
            this.groupBox8.TabIndex = 0;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Users";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.cb_UserIsActive);
            this.groupBox9.Controls.Add(this.label25);
            this.groupBox9.Controls.Add(this.tb_NewUserPass);
            this.groupBox9.Controls.Add(this.label24);
            this.groupBox9.Controls.Add(this.btn_NewUser);
            this.groupBox9.Controls.Add(this.label23);
            this.groupBox9.Controls.Add(this.cb_NewUserRole);
            this.groupBox9.Controls.Add(this.tb_NewUserName);
            this.groupBox9.Location = new System.Drawing.Point(492, 19);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(227, 353);
            this.groupBox9.TabIndex = 34;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Add new";
            // 
            // cb_UserIsActive
            // 
            this.cb_UserIsActive.AutoSize = true;
            this.cb_UserIsActive.Checked = global::ETL_System_Client.Properties.Settings.Default.Checked;
            this.cb_UserIsActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_UserIsActive.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ETL_System_Client.Properties.Settings.Default, "Checked", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cb_UserIsActive.Location = new System.Drawing.Point(22, 231);
            this.cb_UserIsActive.Name = "cb_UserIsActive";
            this.cb_UserIsActive.Size = new System.Drawing.Size(87, 21);
            this.cb_UserIsActive.TabIndex = 43;
            this.cb_UserIsActive.Text = "Is Active?";
            this.cb_UserIsActive.UseVisualStyleBackColor = true;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(7, 101);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(73, 17);
            this.label25.TabIndex = 37;
            this.label25.Text = "Password:";
            // 
            // tb_NewUserPass
            // 
            this.tb_NewUserPass.Location = new System.Drawing.Point(6, 120);
            this.tb_NewUserPass.Name = "tb_NewUserPass";
            this.tb_NewUserPass.PasswordChar = '*';
            this.tb_NewUserPass.Size = new System.Drawing.Size(204, 23);
            this.tb_NewUserPass.TabIndex = 36;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(7, 160);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(41, 17);
            this.label24.TabIndex = 35;
            this.label24.Text = "Role:";
            this.label24.Click += new System.EventHandler(this.label24_Click);
            // 
            // btn_NewUser
            // 
            this.btn_NewUser.BackColor = System.Drawing.Color.Transparent;
            this.btn_NewUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_NewUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_NewUser.Image = global::ETL_System_Client.Properties.Resources.create;
            this.btn_NewUser.Location = new System.Drawing.Point(136, 300);
            this.btn_NewUser.Name = "btn_NewUser";
            this.btn_NewUser.Size = new System.Drawing.Size(74, 39);
            this.btn_NewUser.TabIndex = 31;
            this.btn_NewUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_NewUser.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_NewUser.UseVisualStyleBackColor = false;
            this.btn_NewUser.Click += new System.EventHandler(this.btn_NewUser_Click);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(7, 37);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(49, 17);
            this.label23.TabIndex = 34;
            this.label23.Text = "Name:";
            // 
            // cb_NewUserRole
            // 
            this.cb_NewUserRole.FormattingEnabled = true;
            this.cb_NewUserRole.Items.AddRange(new object[] {
            "admin",
            "developer"});
            this.cb_NewUserRole.Location = new System.Drawing.Point(6, 183);
            this.cb_NewUserRole.Name = "cb_NewUserRole";
            this.cb_NewUserRole.Size = new System.Drawing.Size(204, 24);
            this.cb_NewUserRole.TabIndex = 33;
            // 
            // tb_NewUserName
            // 
            this.tb_NewUserName.Location = new System.Drawing.Point(6, 56);
            this.tb_NewUserName.Name = "tb_NewUserName";
            this.tb_NewUserName.Size = new System.Drawing.Size(204, 23);
            this.tb_NewUserName.TabIndex = 32;
            // 
            // btn_DelUser
            // 
            this.btn_DelUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DelUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_DelUser.Image = global::ETL_System_Client.Properties.Resources.if_trash_can_delete_44014;
            this.btn_DelUser.Location = new System.Drawing.Point(399, 319);
            this.btn_DelUser.Name = "btn_DelUser";
            this.btn_DelUser.Size = new System.Drawing.Size(74, 39);
            this.btn_DelUser.TabIndex = 30;
            this.btn_DelUser.UseVisualStyleBackColor = true;
            this.btn_DelUser.Click += new System.EventHandler(this.btn_DelUser_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.lv_JobsList);
            this.groupBox1.Controls.Add(this.btn_clear);
            this.groupBox1.Controls.Add(this.btn_searchjob);
            this.groupBox1.Controls.Add(this.tb_Search);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox1.Location = new System.Drawing.Point(12, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(224, 766);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Jobs List";
            // 
            // lv_JobsList
            // 
            this.lv_JobsList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lv_JobsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4});
            this.lv_JobsList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lv_JobsList.FullRowSelect = true;
            this.lv_JobsList.GridLines = true;
            this.lv_JobsList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lv_JobsList.HideSelection = false;
            this.lv_JobsList.Location = new System.Drawing.Point(6, 92);
            this.lv_JobsList.Name = "lv_JobsList";
            this.lv_JobsList.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lv_JobsList.Size = new System.Drawing.Size(212, 668);
            this.lv_JobsList.TabIndex = 0;
            this.lv_JobsList.UseCompatibleStateImageBehavior = false;
            this.lv_JobsList.View = System.Windows.Forms.View.Details;
            this.lv_JobsList.SelectedIndexChanged += new System.EventHandler(this.lv_JobsList_SelectedIndexChanged_1);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Jobs";
            this.columnHeader4.Width = 208;
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
            this.menuStrip.Size = new System.Drawing.Size(1796, 38);
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
            this.ts_Logout.Click += new System.EventHandler(this.ts_Logout_Click);
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
            this.ts_NewJob.Click += new System.EventHandler(this.ts_NewJob_Click);
            // 
            // lv_Users
            // 
            this.lv_Users.CheckBoxes = true;
            this.lv_Users.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12});
            this.lv_Users.FullRowSelect = true;
            this.lv_Users.GridLines = true;
            this.lv_Users.HideSelection = false;
            this.lv_Users.Location = new System.Drawing.Point(11, 24);
            this.lv_Users.Name = "lv_Users";
            this.lv_Users.Size = new System.Drawing.Size(462, 280);
            this.lv_Users.TabIndex = 35;
            this.lv_Users.UseCompatibleStateImageBehavior = false;
            this.lv_Users.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "ID";
            this.columnHeader8.Width = 40;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Login";
            this.columnHeader9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader9.Width = 137;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Role";
            this.columnHeader10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader10.Width = 62;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Is Active?";
            this.columnHeader11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader11.Width = 75;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Status";
            this.columnHeader12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader12.Width = 143;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1796, 832);
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
            this.tp_Definition.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.gb_General.ResumeLayout(false);
            this.gb_General.PerformLayout();
            this.gb_Status.ResumeLayout(false);
            this.gb_Status.PerformLayout();
            this.gb_Changes.ResumeLayout(false);
            this.gb_Changes.PerformLayout();
            this.tp_Schedules.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tp_Dependencies.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.tp_Catalogue.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Catalogue)).EndInit();
            this.tp_Queue.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Queue)).EndInit();
            this.tp_Graph.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Depth)).EndInit();
            this.tp_Admin.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
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
        public Label label2;
        public TextBox tb_Executable;
        public Label label1;
        public TextBox tb_Name;
        public Label label8;
        public TextBox tb_Notifications;
        public Label label6;
        public TextBox tb_LatencyAlert;
        public Label label7;
        public TextBox tb_DelaySecs;
        public Label label5;
        public TextBox tb_FailedAttempts;
        public Label label4;
        public TextBox tb_MaxAttempts;
        public GroupBox gb_Changes;
        public Label label10;
        public TextBox tb_When;
        public Label label9;
        public TextBox tb_Who;
        public Button btn_Delete;
        public Button btn_Save;
        public Label label13;
        public TextBox tb_IsPaused;
        public Label label12;
        public TextBox tb_isActive;
        public Label label11;
        public TextBox tb_IsFailed;
        public GroupBox gb_Status;
        public Label label3;
        public ComboBox cb_Type;
        public Button btn_Activation;
        public Button btn_Pausing;
        public GroupBox gb_General;
        public GroupBox groupBox4;
        public Button btn_Create;
        public TextBox tb_Id;
        public Label label14;
        public ListView lv_Schedules;
        public GroupBox groupBox3;
        public GroupBox groupBox2;
        public ComboBox cb_ScheduleType;
        public DateTimePicker dtp_Schedule;
        public Button button1;
        public ColumnHeader columnHeader1;
        public ColumnHeader columnHeader2;
        public ColumnHeader columnHeader3;
        public Label label16;
        public Label label15;
        public Button btn_AddSchedule;
        public TextBox tb_Checkpoint;
        public Label label17;
        public ListView lv_JobsList;
        public ColumnHeader columnHeader4;
        public GroupBox groupBox5;
        public Button btn_DelDependency;
        public ListView lv_Dependencies;
        public ColumnHeader columnHeader5;
        public ColumnHeader columnHeader6;
        public ColumnHeader columnHeader7;
        public GroupBox groupBox6;
        public ComboBox cb_DepJobs;
        public Label label18;
        public Label label19;
        public Button btn_AddDependency;
        public ComboBox cb_DepTypes;
        public ComboBox cb_CheckppointType;
        public Label label20;
        public GViewer gViewer;
        public GroupBox groupBox7;
        public ComboBox cb_RenderType;
        public Label label22;
        public NumericUpDown nud_Depth;
        public Label label21;
        public DataGridView dgv_Queue;
        public GroupBox groupBox9;
        public Label label24;
        public Label label23;
        public ComboBox cb_NewUserRole;
        public Button btn_NewUser;
        public TextBox tb_NewUserName;
        public GroupBox groupBox8;
        public Button btn_DelUser;
        public Label label25;
        public TextBox tb_NewUserPass;
        public TabPage tp_JobInstances;
        public GroupBox groupBox10;
        public Label label29;
        public Label label28;
        public Label label27;
        public Label label26;
        public TextBox tb_EmailPass;
        public TextBox tb_Smtp;
        public TextBox tb_Port;
        public TextBox tb_EmailUser;
        public Button button2;
        public CheckBox cb_SSL;
        public GroupBox groupBox11;
        public Button button3;
        public TextBox textBox4;
        public TextBox textBox3;
        public TextBox textBox1;
        public Label label34;
        public Label label33;
        public Label label32;
        public Label label31;
        public TextBox textBox2;
        public Label label30;
        public TextBox tb_ConnectionString;
        public CheckBox cb_UserIsActive;
        public ListView lv_Users;
        public ColumnHeader columnHeader8;
        public ColumnHeader columnHeader9;
        public ColumnHeader columnHeader10;
        private ColumnHeader columnHeader11;
        private ColumnHeader columnHeader12;
    }
}

