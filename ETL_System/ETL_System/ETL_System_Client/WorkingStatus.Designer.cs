namespace ETL_System {
    partial class WorkingStatus {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkingStatus));
            this.pb_Command = new System.Windows.Forms.ProgressBar();
            this.lb_StatusMsg = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pb_Command
            // 
            this.pb_Command.Location = new System.Drawing.Point(27, 178);
            this.pb_Command.MarqueeAnimationSpeed = 15;
            this.pb_Command.Name = "pb_Command";
            this.pb_Command.Size = new System.Drawing.Size(437, 23);
            this.pb_Command.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pb_Command.TabIndex = 0;
            // 
            // lb_StatusMsg
            // 
            this.lb_StatusMsg.AutoSize = true;
            this.lb_StatusMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lb_StatusMsg.Location = new System.Drawing.Point(24, 157);
            this.lb_StatusMsg.Name = "lb_StatusMsg";
            this.lb_StatusMsg.Size = new System.Drawing.Size(108, 18);
            this.lb_StatusMsg.TabIndex = 1;
            this.lb_StatusMsg.Text = "Working on it...";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(398, 23);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(66, 60);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // WorkingStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(491, 232);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lb_StatusMsg);
            this.Controls.Add(this.pb_Command);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WorkingStatus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "WorkingStatus";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pb_Command;
        private System.Windows.Forms.Label lb_StatusMsg;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}