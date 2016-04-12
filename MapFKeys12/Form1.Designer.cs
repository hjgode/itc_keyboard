namespace MapFKeys12
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
            this.btnMapFKeys = new System.Windows.Forms.Button();
            this.btn_Defaults = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnMapFKeys
            // 
            this.btnMapFKeys.Location = new System.Drawing.Point(59, 120);
            this.btnMapFKeys.Name = "btnMapFKeys";
            this.btnMapFKeys.Size = new System.Drawing.Size(117, 33);
            this.btnMapFKeys.TabIndex = 0;
            this.btnMapFKeys.Text = "map F keys";
            this.btnMapFKeys.Click += new System.EventHandler(this.btnMapFKeys_Click);
            // 
            // btn_Defaults
            // 
            this.btn_Defaults.Location = new System.Drawing.Point(59, 47);
            this.btn_Defaults.Name = "btn_Defaults";
            this.btn_Defaults.Size = new System.Drawing.Size(117, 33);
            this.btn_Defaults.TabIndex = 0;
            this.btn_Defaults.Text = "restore defaults";
            this.btn_Defaults.Click += new System.EventHandler(this.btn_Defaults_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.btn_Defaults);
            this.Controls.Add(this.btnMapFKeys);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnMapFKeys;
        private System.Windows.Forms.Button btn_Defaults;
    }
}

