namespace CUsbKeysCStest
{
    partial class ModifierKeysForm
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
            this.mnuOptions = new System.Windows.Forms.MenuItem();
            this.mnuModifierEntries = new System.Windows.Forms.MenuItem();
            this.cboEvents = new System.Windows.Forms.ComboBox();
            this.txtKeysHex = new System.Windows.Forms.TextBox();
            this.btnGet = new System.Windows.Forms.Button();
            this.btnSetKey = new System.Windows.Forms.Button();
            this.cboPlanes = new System.Windows.Forms.ComboBox();
            this.cboKeys = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.mnuBack);
            this.mainMenu1.MenuItems.Add(this.mnuOptions);
            // 
            // mnuBack
            // 
            this.mnuBack.Text = "BACK";
            this.mnuBack.Click += new System.EventHandler(this.mnuBack_Click);
            // 
            // mnuOptions
            // 
            this.mnuOptions.MenuItems.Add(this.mnuModifierEntries);
            this.mnuOptions.Text = "Options";
            // 
            // mnuModifierEntries
            // 
            this.mnuModifierEntries.Text = "Modifier Entries";
            this.mnuModifierEntries.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // cboEvents
            // 
            this.cboEvents.Location = new System.Drawing.Point(17, 199);
            this.cboEvents.Name = "cboEvents";
            this.cboEvents.Size = new System.Drawing.Size(205, 22);
            this.cboEvents.TabIndex = 17;
            // 
            // txtKeysHex
            // 
            this.txtKeysHex.Location = new System.Drawing.Point(17, 134);
            this.txtKeysHex.Name = "txtKeysHex";
            this.txtKeysHex.ReadOnly = true;
            this.txtKeysHex.Size = new System.Drawing.Size(206, 21);
            this.txtKeysHex.TabIndex = 16;
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(100, 107);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(39, 21);
            this.btnGet.TabIndex = 15;
            this.btnGet.Text = "GET";
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click_1);
            // 
            // btnSetKey
            // 
            this.btnSetKey.Location = new System.Drawing.Point(17, 227);
            this.btnSetKey.Name = "btnSetKey";
            this.btnSetKey.Size = new System.Drawing.Size(205, 30);
            this.btnSetKey.TabIndex = 14;
            this.btnSetKey.Text = "Set key to Modifier";
            this.btnSetKey.Click += new System.EventHandler(this.btnSetKey_Click_1);
            // 
            // cboPlanes
            // 
            this.cboPlanes.Location = new System.Drawing.Point(17, 33);
            this.cboPlanes.Name = "cboPlanes";
            this.cboPlanes.Size = new System.Drawing.Size(205, 22);
            this.cboPlanes.TabIndex = 13;
            // 
            // cboKeys
            // 
            this.cboKeys.Location = new System.Drawing.Point(17, 79);
            this.cboKeys.Name = "cboKeys";
            this.cboKeys.Size = new System.Drawing.Size(205, 22);
            this.cboKeys.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(17, 178);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(206, 18);
            this.label3.Text = "3. Select ModifiersIndex to map:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(17, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(206, 18);
            this.label2.Text = "1. Select Keyboard Plane:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(17, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 18);
            this.label1.Text = "2. Select Key:";
            // 
            // ModifierKeysForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
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
            this.Name = "ModifierKeysForm";
            this.Text = "ModifiersKeys";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboEvents;
        private System.Windows.Forms.TextBox txtKeysHex;
        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.Button btnSetKey;
        private System.Windows.Forms.ComboBox cboPlanes;
        private System.Windows.Forms.ComboBox cboKeys;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuItem mnuBack;
        private System.Windows.Forms.MenuItem mnuOptions;
        private System.Windows.Forms.MenuItem mnuModifierEntries;
    }
}