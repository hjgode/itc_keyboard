namespace MapF1_F5toMultikeys
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
            this.btn_DoMap = new System.Windows.Forms.Button();
            this.btn_RestoreDefaults = new System.Windows.Forms.Button();
            this.logTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboHWkeys = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCharSequence = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_DoMap
            // 
            this.btn_DoMap.Location = new System.Drawing.Point(3, 105);
            this.btn_DoMap.Name = "btn_DoMap";
            this.btn_DoMap.Size = new System.Drawing.Size(135, 24);
            this.btn_DoMap.TabIndex = 0;
            this.btn_DoMap.Text = "Map keys now";
            // 
            // btn_RestoreDefaults
            // 
            this.btn_RestoreDefaults.Location = new System.Drawing.Point(102, 135);
            this.btn_RestoreDefaults.Name = "btn_RestoreDefaults";
            this.btn_RestoreDefaults.Size = new System.Drawing.Size(135, 25);
            this.btn_RestoreDefaults.TabIndex = 1;
            this.btn_RestoreDefaults.Text = "Restore Defaults";
            // 
            // logTxt
            // 
            this.logTxt.Location = new System.Drawing.Point(3, 175);
            this.logTxt.Multiline = true;
            this.logTxt.Name = "logTxt";
            this.logTxt.ReadOnly = true;
            this.logTxt.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.logTxt.Size = new System.Drawing.Size(234, 90);
            this.logTxt.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 16);
            this.label1.Text = "HW key:";
            // 
            // cboHWkeys
            // 
            this.cboHWkeys.Location = new System.Drawing.Point(74, 6);
            this.cboHWkeys.Name = "cboHWkeys";
            this.cboHWkeys.Size = new System.Drawing.Size(163, 22);
            this.cboHWkeys.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 16);
            this.label2.Text = "Char sequence:";
            // 
            // txtCharSequence
            // 
            this.txtCharSequence.Location = new System.Drawing.Point(102, 39);
            this.txtCharSequence.MaxLength = 8;
            this.txtCharSequence.Name = "txtCharSequence";
            this.txtCharSequence.Size = new System.Drawing.Size(134, 21);
            this.txtCharSequence.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.txtCharSequence);
            this.Controls.Add(this.cboHWkeys);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.logTxt);
            this.Controls.Add(this.btn_RestoreDefaults);
            this.Controls.Add(this.btn_DoMap);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Map F1 - F5 Multikeys";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_DoMap;
        private System.Windows.Forms.Button btn_RestoreDefaults;
        private System.Windows.Forms.TextBox logTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboHWkeys;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCharSequence;
    }
}

