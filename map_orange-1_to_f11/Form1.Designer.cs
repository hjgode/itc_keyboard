namespace map_orange_1_to_f11
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
            this.btnMapKey = new System.Windows.Forms.Button();
            this.btnRestoreDefaults = new System.Windows.Forms.Button();
            this.btnUnregisterFunc1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnMapKey
            // 
            this.btnMapKey.Location = new System.Drawing.Point(33, 57);
            this.btnMapKey.Name = "btnMapKey";
            this.btnMapKey.Size = new System.Drawing.Size(176, 38);
            this.btnMapKey.TabIndex = 0;
            this.btnMapKey.Text = "map orange 1 to F11";
            this.btnMapKey.Click += new System.EventHandler(this.btnMapKey_Click);
            // 
            // btnRestoreDefaults
            // 
            this.btnRestoreDefaults.Location = new System.Drawing.Point(33, 138);
            this.btnRestoreDefaults.Name = "btnRestoreDefaults";
            this.btnRestoreDefaults.Size = new System.Drawing.Size(176, 38);
            this.btnRestoreDefaults.TabIndex = 0;
            this.btnRestoreDefaults.Text = "restore defaults";
            this.btnRestoreDefaults.Click += new System.EventHandler(this.btnRestoreDefaults_Click);
            // 
            // btnUnregisterFunc1
            // 
            this.btnUnregisterFunc1.Location = new System.Drawing.Point(33, 207);
            this.btnUnregisterFunc1.Name = "btnUnregisterFunc1";
            this.btnUnregisterFunc1.Size = new System.Drawing.Size(175, 37);
            this.btnUnregisterFunc1.TabIndex = 1;
            this.btnUnregisterFunc1.Text = "unregisterFunc1 with F11";
            this.btnUnregisterFunc1.Click += new System.EventHandler(this.btnUnregisterFunc1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.btnUnregisterFunc1);
            this.Controls.Add(this.btnRestoreDefaults);
            this.Controls.Add(this.btnMapKey);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnMapKey;
        private System.Windows.Forms.Button btnRestoreDefaults;
        private System.Windows.Forms.Button btnUnregisterFunc1;
    }
}

