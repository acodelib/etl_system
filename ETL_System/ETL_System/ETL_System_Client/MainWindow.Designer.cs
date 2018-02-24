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
            this.tp_Catalogue = new System.Windows.Forms.TabPage();
            this.dgv_Catalogue = new System.Windows.Forms.DataGridView();
            this.tp_Queue = new System.Windows.Forms.TabPage();
            this.tp_Graph = new System.Windows.Forms.TabPage();
            this.tp_Admin = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_clear = new System.Windows.Forms.Button();
            this.lv_JobsList = new System.Windows.Forms.ListView();
            this.jobs_col = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            this.tp_Definition.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.gb_General.SuspendLayout();
            this.gb_Status.SuspendLayout();
            this.gb_Changes.SuspendLayout();
            this.tp_Schedules.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.statusStrip.Location = new System.Drawing.Point(0, 798);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1324, 24);
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
            this.tc_Main.Size = new System.Drawing.Size(1074, 737);
            this.tc_Main.TabIndex = 7;
            this.tc_Main.SelectedIndexChanged += new System.EventHandler(this.tc_Main_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tc_Job);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1066, 708);
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
            this.tc_Job.Size = new System.Drawing.Size(1060, 699);
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
            this.tp_Definition.Size = new System.Drawing.Size(1052, 671);
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
            this.gb_General.Size = new System.Drawing.Size(758, 343);
            this.gb_General.TabIndex = 28;
            this.gb_General.TabStop = false;
            this.gb_General.Text = "General";
            // 
            // tb_Checkpoint
            // 
            this.tb_Checkpoint.Location = new System.Drawing.Point(627, 232);
            this.tb_Checkpoint.Name = "tb_Checkpoint";
            this.tb_Checkpoint.Size = new System.Drawing.Size(117, 21);
            this.tb_Checkpoint.TabIndex = 6;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(553, 235);
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
            this.label3.Location = new System.Drawing.Point(91, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 15);
            this.label3.TabIndex = 27;
            this.label3.Text = "Type:";
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
            this.label8.Location = new System.Drawing.Point(8, 301);
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
            this.tb_Notifications.Location = new System.Drawing.Point(136, 298);
            this.tb_Notifications.Name = "tb_Notifications";
            this.tb_Notifications.Size = new System.Drawing.Size(608, 21);
            this.tb_Notifications.TabIndex = 7;
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
            this.gb_Status.Location = new System.Drawing.Point(142, 381);
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
            this.btn_Activation.TabIndex = 11;
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
            this.btn_Pausing.TabIndex = 13;
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
            this.tb_IsFailed.TabIndex = 8;
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
            this.tb_FailedAttempts.ReadOnly = true;
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
            this.gb_Changes.Location = new System.Drawing.Point(818, 561);
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
            // tp_Schedules
            // 
            this.tp_Schedules.Controls.Add(this.groupBox3);
            this.tp_Schedules.Controls.Add(this.groupBox2);
            this.tp_Schedules.Location = new System.Drawing.Point(4, 24);
            this.tp_Schedules.Name = "tp_Schedules";
            this.tp_Schedules.Padding = new System.Windows.Forms.Padding(3);
            this.tp_Schedules.Size = new System.Drawing.Size(1052, 671);
            this.tp_Schedules.TabIndex = 1;
            this.tp_Schedules.Text = "Schedules";
            this.tp_Schedules.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.lv_Schedules);
            this.groupBox3.Location = new System.Drawing.Point(20, 36);
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
            this.groupBox2.Location = new System.Drawing.Point(521, 36);
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
            this.tp_Dependencies.Location = new System.Drawing.Point(4, 24);
            this.tp_Dependencies.Name = "tp_Dependencies";
            this.tp_Dependencies.Padding = new System.Windows.Forms.Padding(3);
            this.tp_Dependencies.Size = new System.Drawing.Size(1052, 671);
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
            this.tp_Catalogue.Size = new System.Drawing.Size(1066, 708);
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
            this.dgv_Catalogue.Size = new System.Drawing.Size(1035, 606);
            this.dgv_Catalogue.TabIndex = 0;
            // 
            // tp_Queue
            // 
            this.tp_Queue.Location = new System.Drawing.Point(4, 25);
            this.tp_Queue.Name = "tp_Queue";
            this.tp_Queue.Padding = new System.Windows.Forms.Padding(3);
            this.tp_Queue.Size = new System.Drawing.Size(1066, 708);
            this.tp_Queue.TabIndex = 2;
            this.tp_Queue.Text = "Queue";
            this.tp_Queue.UseVisualStyleBackColor = true;
            this.tp_Queue.Click += new System.EventHandler(this.tp_Queue_Click);
            // 
            // tp_Graph
            // 
            this.tp_Graph.Location = new System.Drawing.Point(4, 25);
            this.tp_Graph.Name = "tp_Graph";
            this.tp_Graph.Padding = new System.Windows.Forms.Padding(3);
            this.tp_Graph.Size = new System.Drawing.Size(1066, 708);
            this.tp_Graph.TabIndex = 3;
            this.tp_Graph.Text = "Dependency Graph";
            this.tp_Graph.UseVisualStyleBackColor = true;
            // 
            // tp_Admin
            // 
            this.tp_Admin.Location = new System.Drawing.Point(4, 25);
            this.tp_Admin.Name = "tp_Admin";
            this.tp_Admin.Padding = new System.Windows.Forms.Padding(3);
            this.tp_Admin.Size = new System.Drawing.Size(1066, 708);
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
            this.groupBox1.Size = new System.Drawing.Size(224, 756);
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
            this.lv_JobsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.jobs_col});
            this.lv_JobsList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lv_JobsList.GridLines = true;
            this.lv_JobsList.Location = new System.Drawing.Point(6, 91);
            this.lv_JobsList.Name = "lv_JobsList";
            this.lv_JobsList.Size = new System.Drawing.Size(214, 659);
            this.lv_JobsList.TabIndex = 5;
            this.lv_JobsList.UseCompatibleStateImageBehavior = false;
            this.lv_JobsList.View = System.Windows.Forms.View.List;
            this.lv_JobsList.SelectedIndexChanged += new System.EventHandler(this.lv_JobsList_SelectedIndexChanged);
            // 
            // jobs_col
            // 
            this.jobs_col.Text = "JOBS";
            this.jobs_col.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.jobs_col.Width = 214;
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
            this.menuStrip.Size = new System.Drawing.Size(1324, 38);
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
            this.ts_NewJob.Click += new System.EventHandler(this.ts_NewJob_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1324, 822);
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
        public ColumnHeader jobs_col;
        public Button button1;
        public ColumnHeader columnHeader1;
        public ColumnHeader columnHeader2;
        public ColumnHeader columnHeader3;
        public Label label16;
        public Label label15;
        public Button btn_AddSchedule;
        public TextBox tb_Checkpoint;
        public Label label17;
    }
}

