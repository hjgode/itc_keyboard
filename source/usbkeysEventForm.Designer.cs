namespace CUsbKeysCStest
{
    partial class usbkeysEventForm
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
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.mnuResetDefault = new System.Windows.Forms.MenuItem();
            this.mnuEventNames = new System.Windows.Forms.MenuItem();
            this.mnuDumpKeys = new System.Windows.Forms.MenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.cboKeys = new System.Windows.Forms.ComboBox();
            this.btnSetKey = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cboPlanes = new System.Windows.Forms.ComboBox();
            this.btnGet = new System.Windows.Forms.Button();
            this.txtKeysHex = new System.Windows.Forms.TextBox();
            this.cboEvents = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.mnuBack);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            // 
            // mnuBack
            // 
            this.mnuBack.Text = "BACK";
            this.mnuBack.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.MenuItems.Add(this.mnuResetDefault);
            this.menuItem2.MenuItems.Add(this.mnuEventNames);
            this.menuItem2.MenuItems.Add(this.mnuDumpKeys);
            this.menuItem2.Text = "Options";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // mnuResetDefault
            // 
            this.mnuResetDefault.Text = "Reset to default";
            this.mnuResetDefault.Click += new System.EventHandler(this.mnuResetDefault_Click);
            // 
            // mnuEventNames
            // 
            this.mnuEventNames.Text = "Event Names";
            this.mnuEventNames.Click += new System.EventHandler(this.mnuEventNames_Click);
            // 
            // mnuDumpKeys
            // 
            this.mnuDumpKeys.Text = "Dump Keys";
            this.mnuDumpKeys.Click += new System.EventHandler(this.mnuDumpKeys_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(14, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 18);
            this.label1.Text = "2. Select Key to map to Event:";
            // 
            // cboKeys
            // 
            this.cboKeys.Location = new System.Drawing.Point(14, 78);
            this.cboKeys.Name = "cboKeys";
            this.cboKeys.Size = new System.Drawing.Size(205, 22);
            this.cboKeys.TabIndex = 1;
            // 
            // btnSetKey
            // 
            this.btnSetKey.Location = new System.Drawing.Point(14, 226);
            this.btnSetKey.Name = "btnSetKey";
            this.btnSetKey.Size = new System.Drawing.Size(205, 30);
            this.btnSetKey.TabIndex = 3;
            this.btnSetKey.Text = "Set key to event";
            this.btnSetKey.Click += new System.EventHandler(this.btnSetKey_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(14, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(206, 18);
            this.label2.Text = "1. Select Keyboard Plane:";
            // 
            // cboPlanes
            // 
            this.cboPlanes.Location = new System.Drawing.Point(14, 32);
            this.cboPlanes.Name = "cboPlanes";
            this.cboPlanes.Size = new System.Drawing.Size(205, 22);
            this.cboPlanes.TabIndex = 1;
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(167, 135);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(39, 21);
            this.btnGet.TabIndex = 6;
            this.btnGet.Text = "GET";
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // txtKeysHex
            // 
            this.txtKeysHex.Location = new System.Drawing.Point(25, 135);
            this.txtKeysHex.Name = "txtKeysHex";
            this.txtKeysHex.ReadOnly = true;
            this.txtKeysHex.Size = new System.Drawing.Size(136, 21);
            this.txtKeysHex.TabIndex = 7;
            // 
            // cboEvents
            // 
            this.cboEvents.Location = new System.Drawing.Point(14, 198);
            this.cboEvents.Name = "cboEvents";
            this.cboEvents.Size = new System.Drawing.Size(205, 22);
            this.cboEvents.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(14, 177);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(206, 18);
            this.label3.Text = "3. Select Event to map:";
            // 
            // usbkeysEventForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.cboEvents);
            this.Controls.Add(this.txtKeysHex);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.btnSetKey);
            this.Controls.Add(this.cboPlanes);
            this.Controls.Add(this.cboKeys);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.Name = "usbkeysEventForm";
            this.Text = "USB Key Event mapping";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboKeys;
        private System.Windows.Forms.MenuItem mnuBack;
        private System.Windows.Forms.Button btnSetKey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboPlanes;
        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.TextBox txtKeysHex;
        private System.Windows.Forms.ComboBox cboEvents;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem mnuResetDefault;
        private System.Windows.Forms.MenuItem mnuEventNames;
        private System.Windows.Forms.MenuItem mnuDumpKeys;
    }
}

