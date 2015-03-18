namespace MapVol2Page
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
            this.btnMap = new System.Windows.Forms.Button();
            this.btnDefault = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnMap
            // 
            this.btnMap.Location = new System.Drawing.Point(54, 14);
            this.btnMap.Name = "btnMap";
            this.btnMap.Size = new System.Drawing.Size(131, 38);
            this.btnMap.TabIndex = 0;
            this.btnMap.Text = "Map Keys";
            this.btnMap.Click += new System.EventHandler(this.btnMap_Click);
            // 
            // btnDefault
            // 
            this.btnDefault.Location = new System.Drawing.Point(54, 79);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(131, 38);
            this.btnDefault.TabIndex = 0;
            this.btnDefault.Text = "Restore default";
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(4, 139);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.Size = new System.Drawing.Size(235, 126);
            this.txtLog.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.btnMap);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "map Vol up/dn to page up/dn";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnMap;
        private System.Windows.Forms.Button btnDefault;
        private System.Windows.Forms.TextBox txtLog;
    }
}

