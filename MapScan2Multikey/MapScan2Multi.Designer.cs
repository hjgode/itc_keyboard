namespace MapScan2Multikey
{
    partial class MapScan2Multi
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
            this.mnuDump = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.mnuDump);
            // 
            // btnMap
            // 
            this.btnMap.Location = new System.Drawing.Point(50, 83);
            this.btnMap.Name = "btnMap";
            this.btnMap.Size = new System.Drawing.Size(138, 27);
            this.btnMap.TabIndex = 0;
            this.btnMap.Text = "MAP";
            this.btnMap.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnDefault
            // 
            this.btnDefault.Location = new System.Drawing.Point(50, 153);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(138, 27);
            this.btnDefault.TabIndex = 0;
            this.btnDefault.Text = "Restore Default";
            this.btnDefault.Click += new System.EventHandler(this.button2_Click);
            // 
            // mnuDump
            // 
            this.mnuDump.Text = "Dump";
            this.mnuDump.Click += new System.EventHandler(this.mnuDump_Click);
            // 
            // MapScan2Multi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.btnMap);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "MapScan2Multi";
            this.Text = "Map Scan button to multikey";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnMap;
        private System.Windows.Forms.Button btnDefault;
        private System.Windows.Forms.MenuItem mnuDump;
    }
}

