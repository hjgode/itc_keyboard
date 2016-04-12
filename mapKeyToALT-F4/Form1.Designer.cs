namespace mapKeyToALT_F4
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
            this.cboKeyList = new System.Windows.Forms.ComboBox();
            this.btnMapKey = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cboPlanes = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cboKeyList
            // 
            this.cboKeyList.Location = new System.Drawing.Point(42, 58);
            this.cboKeyList.Name = "cboKeyList";
            this.cboKeyList.Size = new System.Drawing.Size(149, 22);
            this.cboKeyList.TabIndex = 0;
            this.cboKeyList.SelectedIndexChanged += new System.EventHandler(this.cboKeyList_SelectedIndexChanged);
            // 
            // btnMapKey
            // 
            this.btnMapKey.Location = new System.Drawing.Point(42, 128);
            this.btnMapKey.Name = "btnMapKey";
            this.btnMapKey.Size = new System.Drawing.Size(148, 25);
            this.btnMapKey.TabIndex = 1;
            this.btnMapKey.Text = "map to ALT+F4";
            this.btnMapKey.Click += new System.EventHandler(this.btnMapKey_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(234, 42);
            this.label1.Text = "-";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cboPlanes
            // 
            this.cboPlanes.Location = new System.Drawing.Point(42, 193);
            this.cboPlanes.Name = "cboPlanes";
            this.cboPlanes.Size = new System.Drawing.Size(147, 22);
            this.cboPlanes.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(3, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(185, 19);
            this.label2.Text = "Select native Key:";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(3, 171);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(185, 19);
            this.label3.Text = "Select Plane for Mapping:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 242);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "restore defaults";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboPlanes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnMapKey);
            this.Controls.Add(this.cboKeyList);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Map Key to ALT+F4";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboKeyList;
        private System.Windows.Forms.Button btnMapKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboPlanes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
    }
}

