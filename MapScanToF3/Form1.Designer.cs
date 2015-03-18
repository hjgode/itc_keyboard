namespace MapScanToF3
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
            this.btnDoMap = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDoMap
            // 
            this.btnDoMap.Location = new System.Drawing.Point(37, 63);
            this.btnDoMap.Name = "btnDoMap";
            this.btnDoMap.Size = new System.Drawing.Size(163, 39);
            this.btnDoMap.TabIndex = 0;
            this.btnDoMap.Text = "map Scan to F3";
            this.btnDoMap.Click += new System.EventHandler(this.btnDoMap_Click);
            // 
            // btnRestore
            // 
            this.btnRestore.Location = new System.Drawing.Point(37, 156);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(163, 39);
            this.btnRestore.TabIndex = 0;
            this.btnRestore.Text = "restore default";
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.btnDoMap);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDoMap;
        private System.Windows.Forms.Button btnRestore;
    }
}

