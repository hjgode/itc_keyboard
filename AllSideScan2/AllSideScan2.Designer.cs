namespace AllSideScan2
{
    partial class AllSideScan2
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
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.mnuAllNoop = new System.Windows.Forms.MenuItem();
            this.mnuAllScan = new System.Windows.Forms.MenuItem();
            this.mnuRestDefaults = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "Exit";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // textBox1
            // 
            this.textBox1.AcceptsReturn = true;
            this.textBox1.AcceptsTab = true;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(240, 268);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "textBox1";
            this.textBox1.WordWrap = false;
            // 
            // menuItem2
            // 
            this.menuItem2.MenuItems.Add(this.mnuAllNoop);
            this.menuItem2.MenuItems.Add(this.mnuAllScan);
            this.menuItem2.MenuItems.Add(this.mnuRestDefaults);
            this.menuItem2.Text = "Options";
            // 
            // mnuAllNoop
            // 
            this.mnuAllNoop.Text = "All Side Btns NOOP";
            this.mnuAllNoop.Click += new System.EventHandler(this.mnuAllNoop_Click);
            // 
            // mnuAllScan
            // 
            this.mnuAllScan.Text = "All Side Btns SCAN";
            this.mnuAllScan.Click += new System.EventHandler(this.mnuAllScan_Click);
            // 
            // mnuRestDefaults
            // 
            this.mnuRestDefaults.Text = "Restore Defaults";
            this.mnuRestDefaults.Click += new System.EventHandler(this.mnuRestDefaults_Click);
            // 
            // AllSideScan2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.textBox1);
            this.Menu = this.mainMenu1;
            this.Name = "AllSideScan2";
            this.Text = "AllSideScan2";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem mnuAllNoop;
        private System.Windows.Forms.MenuItem mnuAllScan;
        private System.Windows.Forms.MenuItem mnuRestDefaults;
    }
}

