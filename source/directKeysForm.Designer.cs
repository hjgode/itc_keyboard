namespace CUsbKeysCStest
{
    partial class directKeysForm
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
            this.mnuEventNames = new System.Windows.Forms.MenuItem();
            this.cboKeys = new System.Windows.Forms.ComboBox();
            this.btnGet = new System.Windows.Forms.Button();
            this.lbEvents = new System.Windows.Forms.ListBox();
            this.txtRawText = new System.Windows.Forms.TextBox();
            this.btnSet = new System.Windows.Forms.Button();
            this.lbVKeys = new System.Windows.Forms.ComboBox();
            this.btnSetVKey = new System.Windows.Forms.Button();
            this.chkShifted = new System.Windows.Forms.CheckBox();
            this.lbMultikeys = new System.Windows.Forms.ListBox();
            this.lbAppButtons = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.mnuBack);
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // mnuBack
            // 
            this.mnuBack.Text = "BACK";
            this.mnuBack.Click += new System.EventHandler(this.mnuBack_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.mnuEventNames);
            this.menuItem1.Text = "Options";
            // 
            // mnuEventNames
            // 
            this.mnuEventNames.Text = "Event Names";
            this.mnuEventNames.Click += new System.EventHandler(this.mnuEventNames_Click);
            // 
            // cboKeys
            // 
            this.cboKeys.Location = new System.Drawing.Point(31, 3);
            this.cboKeys.Name = "cboKeys";
            this.cboKeys.Size = new System.Drawing.Size(189, 22);
            this.cboKeys.TabIndex = 0;
            this.cboKeys.SelectedIndexChanged += new System.EventHandler(this.cboKeys_SelectedIndexChanged);
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(31, 31);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(44, 19);
            this.btnGet.TabIndex = 1;
            this.btnGet.Text = "GET";
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // lbEvents
            // 
            this.lbEvents.Location = new System.Drawing.Point(3, 58);
            this.lbEvents.Name = "lbEvents";
            this.lbEvents.Size = new System.Drawing.Size(111, 44);
            this.lbEvents.TabIndex = 2;
            this.lbEvents.SelectedIndexChanged += new System.EventHandler(this.lbEvents_SelectedIndexChanged);
            // 
            // txtRawText
            // 
            this.txtRawText.Location = new System.Drawing.Point(87, 31);
            this.txtRawText.Name = "txtRawText";
            this.txtRawText.ReadOnly = true;
            this.txtRawText.Size = new System.Drawing.Size(133, 21);
            this.txtRawText.TabIndex = 3;
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(122, 58);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(115, 20);
            this.btnSet.TabIndex = 4;
            this.btnSet.Text = "Set key to Event";
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // lbVKeys
            // 
            this.lbVKeys.Location = new System.Drawing.Point(3, 108);
            this.lbVKeys.Name = "lbVKeys";
            this.lbVKeys.Size = new System.Drawing.Size(111, 22);
            this.lbVKeys.TabIndex = 5;
            // 
            // btnSetVKey
            // 
            this.btnSetVKey.Location = new System.Drawing.Point(122, 108);
            this.btnSetVKey.Name = "btnSetVKey";
            this.btnSetVKey.Size = new System.Drawing.Size(115, 20);
            this.btnSetVKey.TabIndex = 4;
            this.btnSetVKey.Text = "Set key to Event";
            this.btnSetVKey.Click += new System.EventHandler(this.btnSetVKey_Click);
            // 
            // chkShifted
            // 
            this.chkShifted.Location = new System.Drawing.Point(3, 136);
            this.chkShifted.Name = "chkShifted";
            this.chkShifted.Size = new System.Drawing.Size(113, 19);
            this.chkShifted.TabIndex = 6;
            this.chkShifted.Text = "Shifted VKey";
            // 
            // lbMultikeys
            // 
            this.lbMultikeys.Location = new System.Drawing.Point(3, 165);
            this.lbMultikeys.Name = "lbMultikeys";
            this.lbMultikeys.Size = new System.Drawing.Size(110, 44);
            this.lbMultikeys.TabIndex = 7;
            // 
            // lbAppButtons
            // 
            this.lbAppButtons.Location = new System.Drawing.Point(3, 215);
            this.lbAppButtons.Name = "lbAppButtons";
            this.lbAppButtons.Size = new System.Drawing.Size(110, 44);
            this.lbAppButtons.TabIndex = 7;
            // 
            // directKeysForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.lbAppButtons);
            this.Controls.Add(this.lbMultikeys);
            this.Controls.Add(this.chkShifted);
            this.Controls.Add(this.lbVKeys);
            this.Controls.Add(this.btnSetVKey);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.txtRawText);
            this.Controls.Add(this.lbEvents);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.cboKeys);
            this.Menu = this.mainMenu1;
            this.Name = "directKeysForm";
            this.Text = "Direct Keys";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem mnuBack;
        private System.Windows.Forms.ComboBox cboKeys;
        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.ListBox lbEvents;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem mnuEventNames;
        private System.Windows.Forms.TextBox txtRawText;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.ComboBox lbVKeys;
        private System.Windows.Forms.Button btnSetVKey;
        private System.Windows.Forms.CheckBox chkShifted;
        private System.Windows.Forms.ListBox lbMultikeys;
        private System.Windows.Forms.ListBox lbAppButtons;
    }
}