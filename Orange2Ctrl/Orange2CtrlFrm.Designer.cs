namespace Orange2Ctrl
{
    partial class Orange2Ctrl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnMapToCtrlSticky = new System.Windows.Forms.Button();
            this.btnMapToCtrl = new System.Windows.Forms.Button();
            this.btnRestoreDefaults = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(4, 141);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.Size = new System.Drawing.Size(233, 124);
            this.txtLog.TabIndex = 0;
            // 
            // btnMapToCtrlSticky
            // 
            this.btnMapToCtrlSticky.Location = new System.Drawing.Point(4, 7);
            this.btnMapToCtrlSticky.Name = "btnMapToCtrlSticky";
            this.btnMapToCtrlSticky.Size = new System.Drawing.Size(232, 29);
            this.btnMapToCtrlSticky.TabIndex = 1;
            this.btnMapToCtrlSticky.Text = "map Orange to CTRL sticky";
            this.btnMapToCtrlSticky.Click += new System.EventHandler(this.btnMapToCtrlSticky_Click);
            // 
            // btnMapToCtrl
            // 
            this.btnMapToCtrl.Location = new System.Drawing.Point(3, 42);
            this.btnMapToCtrl.Name = "btnMapToCtrl";
            this.btnMapToCtrl.Size = new System.Drawing.Size(232, 29);
            this.btnMapToCtrl.TabIndex = 1;
            this.btnMapToCtrl.Text = "map Orange to CTRL normal";
            this.btnMapToCtrl.Click += new System.EventHandler(this.btnMapToCtrl_Click);
            // 
            // btnRestoreDefaults
            // 
            this.btnRestoreDefaults.Location = new System.Drawing.Point(3, 94);
            this.btnRestoreDefaults.Name = "btnRestoreDefaults";
            this.btnRestoreDefaults.Size = new System.Drawing.Size(232, 29);
            this.btnRestoreDefaults.TabIndex = 1;
            this.btnRestoreDefaults.Text = "restore defaults";
            this.btnRestoreDefaults.Click += new System.EventHandler(this.btnRestoreDefaults_Click);
            // 
            // Orange2Ctrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.btnRestoreDefaults);
            this.Controls.Add(this.btnMapToCtrl);
            this.Controls.Add(this.btnMapToCtrlSticky);
            this.Controls.Add(this.txtLog);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "Orange2Ctrl";
            this.Text = "Orange2Ctrl";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnMapToCtrlSticky;
        private System.Windows.Forms.Button btnMapToCtrl;
        private System.Windows.Forms.Button btnRestoreDefaults;
    }
}

