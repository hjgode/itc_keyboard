namespace CUsbKeysCStest
{
    partial class ManageModifiersEntriesForm
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
            this.listbox1 = new System.Windows.Forms.ComboBox();
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
            this.label1.Text = "ModKey entry:";
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
            this.label2.Text = "ModKey:";
            // 
            // listbox1
            // 
            this.listbox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.listbox1.Location = new System.Drawing.Point(11, 33);
            this.listbox1.Name = "listbox1";
            this.listbox1.Size = new System.Drawing.Size(210, 22);
            this.listbox1.TabIndex = 6;
            this.listbox1.SelectedIndexChanged += new System.EventHandler(this.listbox1_SelectedIndexChanged);
            // 
            // ManageModifiersEntriesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.listbox1);
            this.Controls.Add(this.txtDump);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.Name = "ManageModifiersEntriesForm";
            this.Text = "Manage Modifiers Entries";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem mnuBack;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem mnuReset;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDump;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox listbox1;
    }
}