namespace CUsbKeysCStest
{
    partial class UsbKeysForm
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
            this.mnuDumpKeys = new System.Windows.Forms.MenuItem();
            this.mnuResetDefault = new System.Windows.Forms.MenuItem();
            this.txtKeysHex = new System.Windows.Forms.TextBox();
            this.cboPlanes = new System.Windows.Forms.ComboBox();
            this.cboKeys = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEventKey = new System.Windows.Forms.Button();
            this.btnRotate = new System.Windows.Forms.Button();
            this.btnNoop = new System.Windows.Forms.Button();
            this.btn_ModifierKey = new System.Windows.Forms.Button();
            this.btn_Functionkey = new System.Windows.Forms.Button();
            this.mnu_UseITEtables = new System.Windows.Forms.MenuItem();
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
            this.menuItem1.MenuItems.Add(this.mnuDumpKeys);
            this.menuItem1.MenuItems.Add(this.mnuResetDefault);
            this.menuItem1.MenuItems.Add(this.mnu_UseITEtables);
            this.menuItem1.Text = "Options";
            // 
            // mnuDumpKeys
            // 
            this.mnuDumpKeys.Text = "Dump Keys";
            this.mnuDumpKeys.Click += new System.EventHandler(this.mnuDumpKeys_Click);
            // 
            // mnuResetDefault
            // 
            this.mnuResetDefault.Text = "Reset to default";
            this.mnuResetDefault.Click += new System.EventHandler(this.mnuResetDefault_Click);
            // 
            // txtKeysHex
            // 
            this.txtKeysHex.Location = new System.Drawing.Point(19, 104);
            this.txtKeysHex.Multiline = true;
            this.txtKeysHex.Name = "txtKeysHex";
            this.txtKeysHex.ReadOnly = true;
            this.txtKeysHex.Size = new System.Drawing.Size(206, 40);
            this.txtKeysHex.TabIndex = 12;
            // 
            // cboPlanes
            // 
            this.cboPlanes.Location = new System.Drawing.Point(19, 30);
            this.cboPlanes.Name = "cboPlanes";
            this.cboPlanes.Size = new System.Drawing.Size(205, 22);
            this.cboPlanes.TabIndex = 10;
            this.cboPlanes.SelectedIndexChanged += new System.EventHandler(this.cboPlanes_SelectedIndexChanged);
            // 
            // cboKeys
            // 
            this.cboKeys.Location = new System.Drawing.Point(19, 76);
            this.cboKeys.Name = "cboKeys";
            this.cboKeys.Size = new System.Drawing.Size(205, 22);
            this.cboKeys.TabIndex = 11;
            this.cboKeys.SelectedIndexChanged += new System.EventHandler(this.cboKeys_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(19, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(206, 18);
            this.label2.Text = "1. Select Keyboard Plane:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(19, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 18);
            this.label1.Text = "2. Select Key:";
            // 
            // btnEventKey
            // 
            this.btnEventKey.Location = new System.Drawing.Point(35, 190);
            this.btnEventKey.Name = "btnEventKey";
            this.btnEventKey.Size = new System.Drawing.Size(93, 24);
            this.btnEventKey.TabIndex = 15;
            this.btnEventKey.Text = "Event Key";
            this.btnEventKey.Click += new System.EventHandler(this.btnEventKey_Click);
            // 
            // btnRotate
            // 
            this.btnRotate.Location = new System.Drawing.Point(134, 190);
            this.btnRotate.Name = "btnRotate";
            this.btnRotate.Size = new System.Drawing.Size(91, 24);
            this.btnRotate.TabIndex = 15;
            this.btnRotate.Text = "Rotate Key";
            this.btnRotate.Click += new System.EventHandler(this.btnRotate_Click);
            // 
            // btnNoop
            // 
            this.btnNoop.Location = new System.Drawing.Point(35, 160);
            this.btnNoop.Name = "btnNoop";
            this.btnNoop.Size = new System.Drawing.Size(93, 24);
            this.btnNoop.TabIndex = 15;
            this.btnNoop.Text = "Noop";
            this.btnNoop.Click += new System.EventHandler(this.btnNoop_Click);
            // 
            // btn_ModifierKey
            // 
            this.btn_ModifierKey.Location = new System.Drawing.Point(134, 220);
            this.btn_ModifierKey.Name = "btn_ModifierKey";
            this.btn_ModifierKey.Size = new System.Drawing.Size(91, 24);
            this.btn_ModifierKey.TabIndex = 15;
            this.btn_ModifierKey.Text = "Modifier Key";
            this.btn_ModifierKey.Click += new System.EventHandler(this.btn_ModifierKey_Click);
            // 
            // btn_Functionkey
            // 
            this.btn_Functionkey.Location = new System.Drawing.Point(35, 221);
            this.btn_Functionkey.Name = "btn_Functionkey";
            this.btn_Functionkey.Size = new System.Drawing.Size(92, 22);
            this.btn_Functionkey.TabIndex = 18;
            this.btn_Functionkey.Text = "Function Key";
            // 
            // mnu_UseITEtables
            // 
            this.mnu_UseITEtables.Text = "use ITE tables";
            this.mnu_UseITEtables.Click += new System.EventHandler(this.mnu_UseITEtables_Click);
            // 
            // UsbKeysForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.btn_Functionkey);
            this.Controls.Add(this.btn_ModifierKey);
            this.Controls.Add(this.btnRotate);
            this.Controls.Add(this.btnNoop);
            this.Controls.Add(this.btnEventKey);
            this.Controls.Add(this.txtKeysHex);
            this.Controls.Add(this.cboPlanes);
            this.Controls.Add(this.cboKeys);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.Name = "UsbKeysForm";
            this.Text = "UsbKeysForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem mnuBack;
        private System.Windows.Forms.TextBox txtKeysHex;
        private System.Windows.Forms.ComboBox cboPlanes;
        private System.Windows.Forms.ComboBox cboKeys;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem mnuDumpKeys;
        private System.Windows.Forms.MenuItem mnuResetDefault;
        private System.Windows.Forms.Button btnEventKey;
        private System.Windows.Forms.Button btnRotate;
        private System.Windows.Forms.Button btnNoop;
        private System.Windows.Forms.Button btn_ModifierKey;
        private System.Windows.Forms.Button btn_Functionkey;
        private System.Windows.Forms.MenuItem mnu_UseITEtables;
    }
}