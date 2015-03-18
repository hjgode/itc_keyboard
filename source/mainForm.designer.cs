namespace CUsbKeysCStest
{
    partial class mainForm
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
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.mnuUpdateDriver = new System.Windows.Forms.MenuItem();
            this.mnuExit = new System.Windows.Forms.MenuItem();
            this.mnuOptions = new System.Windows.Forms.MenuItem();
            this.mnuRotateDefaults = new System.Windows.Forms.MenuItem();
            this.mnuMapGreenW2Ctrl = new System.Windows.Forms.MenuItem();
            this.btnDirect = new System.Windows.Forms.Button();
            this.btnUSBkeysEvents = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnUsbKeys = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.mnuOptions);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.mnuUpdateDriver);
            this.menuItem1.MenuItems.Add(this.mnuExit);
            this.menuItem1.Text = "File";
            // 
            // mnuUpdateDriver
            // 
            this.mnuUpdateDriver.Text = "UpdateDriver";
            this.mnuUpdateDriver.Click += new System.EventHandler(this.mnuUpdateDriver_Click);
            // 
            // mnuExit
            // 
            this.mnuExit.Text = "Exit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // mnuOptions
            // 
            this.mnuOptions.MenuItems.Add(this.mnuRotateDefaults);
            this.mnuOptions.MenuItems.Add(this.mnuMapGreenW2Ctrl);
            this.mnuOptions.Text = "Options";
            // 
            // mnuRotateDefaults
            // 
            this.mnuRotateDefaults.Text = "Write RotateTables";
            this.mnuRotateDefaults.Click += new System.EventHandler(this.mnuOptions_Click);
            // 
            // mnuMapGreenW2Ctrl
            // 
            this.mnuMapGreenW2Ctrl.Text = "MapGreenW2Ctrl";
            this.mnuMapGreenW2Ctrl.Click += new System.EventHandler(this.mnuMapGreenW2Ctrl_Click);
            // 
            // btnDirect
            // 
            this.btnDirect.Location = new System.Drawing.Point(41, 34);
            this.btnDirect.Name = "btnDirect";
            this.btnDirect.Size = new System.Drawing.Size(159, 27);
            this.btnDirect.TabIndex = 0;
            this.btnDirect.Text = "Direct Keys";
            this.btnDirect.Click += new System.EventHandler(this.btnDirect_Click);
            // 
            // btnUSBkeysEvents
            // 
            this.btnUSBkeysEvents.Location = new System.Drawing.Point(41, 171);
            this.btnUSBkeysEvents.Name = "btnUSBkeysEvents";
            this.btnUSBkeysEvents.Size = new System.Drawing.Size(159, 27);
            this.btnUSBkeysEvents.TabIndex = 0;
            this.btnUSBkeysEvents.Text = "USB Keys (Events)";
            this.btnUSBkeysEvents.Click += new System.EventHandler(this.btnUSBkeys_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(41, 204);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(159, 27);
            this.button1.TabIndex = 0;
            this.button1.Text = "USB Keys (Rotate)";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnUsbKeys
            // 
            this.btnUsbKeys.Location = new System.Drawing.Point(41, 91);
            this.btnUsbKeys.Name = "btnUsbKeys";
            this.btnUsbKeys.Size = new System.Drawing.Size(159, 27);
            this.btnUsbKeys.TabIndex = 0;
            this.btnUsbKeys.Text = "USB Keys";
            this.btnUsbKeys.Click += new System.EventHandler(this.btnUsbKeys_Click_1);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnUsbKeys);
            this.Controls.Add(this.btnUSBkeysEvents);
            this.Controls.Add(this.btnDirect);
            this.Menu = this.mainMenu1;
            this.Name = "mainForm";
            this.Text = "Edit Keyboard Tables";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDirect;
        private System.Windows.Forms.Button btnUSBkeysEvents;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MenuItem mnuOptions;
        private System.Windows.Forms.MenuItem mnuRotateDefaults;
        private System.Windows.Forms.MenuItem mnuMapGreenW2Ctrl;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem mnuUpdateDriver;
        private System.Windows.Forms.MenuItem mnuExit;
        private System.Windows.Forms.Button btnUsbKeys;
    }
}