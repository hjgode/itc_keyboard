namespace CUsbKeysCStest
{
    partial class ManageRotateEntriesForm
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
            this.mnuBack = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.mnuReset = new System.Windows.Forms.MenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDump = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRotateChars = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRotateShiftedChars = new System.Windows.Forms.TextBox();
            this.listboxRotates = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.mnuBack);
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // mnuBack
            // 
            this.mnuBack.Text = "Back";
            this.mnuBack.Click += new System.EventHandler(this.mnuBack_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.mnuReset);
            this.menuItem1.Text = "Options";
            // 
            // mnuReset
            // 
            this.mnuReset.Text = "Reset Default";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 20);
            this.label1.Text = "Rotate entry:";
            // 
            // txtDump
            // 
            this.txtDump.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular);
            this.txtDump.Location = new System.Drawing.Point(11, 74);
            this.txtDump.Multiline = true;
            this.txtDump.Name = "txtDump";
            this.txtDump.ReadOnly = true;
            this.txtDump.Size = new System.Drawing.Size(210, 48);
            this.txtDump.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(11, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(210, 20);
            this.label2.Text = "RotateKey:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(11, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(210, 20);
            this.label3.Text = "RotateChars:";
            // 
            // txtRotateChars
            // 
            this.txtRotateChars.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular);
            this.txtRotateChars.Location = new System.Drawing.Point(11, 148);
            this.txtRotateChars.Multiline = true;
            this.txtRotateChars.Name = "txtRotateChars";
            this.txtRotateChars.ReadOnly = true;
            this.txtRotateChars.Size = new System.Drawing.Size(210, 28);
            this.txtRotateChars.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(11, 179);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(210, 20);
            this.label4.Text = "RotateShiftedChars:";
            // 
            // txtRotateShiftedChars
            // 
            this.txtRotateShiftedChars.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular);
            this.txtRotateShiftedChars.Location = new System.Drawing.Point(11, 202);
            this.txtRotateShiftedChars.Multiline = true;
            this.txtRotateShiftedChars.Name = "txtRotateShiftedChars";
            this.txtRotateShiftedChars.ReadOnly = true;
            this.txtRotateShiftedChars.Size = new System.Drawing.Size(210, 30);
            this.txtRotateShiftedChars.TabIndex = 2;
            // 
            // listboxRotates
            // 
            this.listboxRotates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.listboxRotates.Location = new System.Drawing.Point(11, 33);
            this.listboxRotates.Name = "listboxRotates";
            this.listboxRotates.Size = new System.Drawing.Size(210, 22);
            this.listboxRotates.TabIndex = 6;
            this.listboxRotates.SelectedIndexChanged += new System.EventHandler(this.listboxRotates_SelectedIndexChanged);
            // 
            // ManageRotateEntriesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.listboxRotates);
            this.Controls.Add(this.txtRotateShiftedChars);
            this.Controls.Add(this.txtRotateChars);
            this.Controls.Add(this.txtDump);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.Name = "ManageRotateEntriesForm";
            this.Text = "Manage Rotate Entries";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem mnuBack;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem mnuReset;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDump;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRotateChars;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRotateShiftedChars;
        private System.Windows.Forms.ComboBox listboxRotates;
    }
}