namespace AllSideScan7xx_2008
{
    partial class Form1
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
            this.mnuExit = new System.Windows.Forms.MenuItem();
            this.btnDoMap = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.mnuExit);
            // 
            // mnuExit
            // 
            this.mnuExit.Text = "EXIT";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // btnDoMap
            // 
            this.btnDoMap.Location = new System.Drawing.Point(29, 55);
            this.btnDoMap.Name = "btnDoMap";
            this.btnDoMap.Size = new System.Drawing.Size(191, 34);
            this.btnDoMap.TabIndex = 0;
            this.btnDoMap.Text = "Map Side Keys to Scan";
            this.btnDoMap.Click += new System.EventHandler(this.btnDoMap_Click);
            // 
            // txtLog
            // 
            this.txtLog.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtLog.Location = new System.Drawing.Point(3, 145);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.Size = new System.Drawing.Size(237, 111);
            this.txtLog.TabIndex = 1;
            this.txtLog.WordWrap = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnDoMap);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "Map all side keys to scan";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem mnuExit;
        private System.Windows.Forms.Button btnDoMap;
        private System.Windows.Forms.TextBox txtLog;
    }
}

