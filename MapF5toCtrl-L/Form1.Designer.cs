namespace MapF5toCtrl_L
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
            this.btn_mapF5 = new System.Windows.Forms.Button();
            this.btnDefault = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_mapF5
            // 
            this.btn_mapF5.Location = new System.Drawing.Point(48, 61);
            this.btn_mapF5.Name = "btn_mapF5";
            this.btn_mapF5.Size = new System.Drawing.Size(139, 34);
            this.btn_mapF5.TabIndex = 0;
            this.btn_mapF5.Text = "F5 to Ctrl-L";
            this.btn_mapF5.Click += new System.EventHandler(this.btn_mapF5_Click);
            // 
            // btnDefault
            // 
            this.btnDefault.Location = new System.Drawing.Point(48, 152);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(138, 31);
            this.btnDefault.TabIndex = 1;
            this.btnDefault.Text = "Reset Default";
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.btn_mapF5);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_mapF5;
        private System.Windows.Forms.Button btnDefault;
    }
}

