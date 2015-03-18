namespace CUsbKeysCStest
{
    partial class ManageEventNames
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
            this.lbEvents = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtStateEventName = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtDeltaEventName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtState = new System.Windows.Forms.TextBox();
            this.txtDelta = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnRemove = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.mnuBack);
            // 
            // mnuBack
            // 
            this.mnuBack.Text = "BACK";
            this.mnuBack.Click += new System.EventHandler(this.mnuBack_Click);
            // 
            // lbEvents
            // 
            this.lbEvents.Location = new System.Drawing.Point(3, 23);
            this.lbEvents.Name = "lbEvents";
            this.lbEvents.Size = new System.Drawing.Size(90, 72);
            this.lbEvents.TabIndex = 0;
            this.lbEvents.SelectedIndexChanged += new System.EventHandler(this.lbEvents_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 16);
            this.label1.Text = "Named Events:";
            // 
            // txtStateEventName
            // 
            this.txtStateEventName.Location = new System.Drawing.Point(44, 128);
            this.txtStateEventName.Name = "txtStateEventName";
            this.txtStateEventName.Size = new System.Drawing.Size(137, 21);
            this.txtStateEventName.TabIndex = 3;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(192, 155);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(43, 21);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "ADD";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtDeltaEventName
            // 
            this.txtDeltaEventName.Location = new System.Drawing.Point(44, 155);
            this.txtDeltaEventName.Name = "txtDeltaEventName";
            this.txtDeltaEventName.Size = new System.Drawing.Size(137, 21);
            this.txtDeltaEventName.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(4, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 18);
            this.label3.Text = "State:";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(4, 155);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 18);
            this.label4.Text = "Delta:";
            // 
            // txtState
            // 
            this.txtState.Location = new System.Drawing.Point(99, 25);
            this.txtState.Name = "txtState";
            this.txtState.ReadOnly = true;
            this.txtState.Size = new System.Drawing.Size(137, 21);
            this.txtState.TabIndex = 3;
            // 
            // txtDelta
            // 
            this.txtDelta.Location = new System.Drawing.Point(99, 74);
            this.txtDelta.Name = "txtDelta";
            this.txtDelta.ReadOnly = true;
            this.txtDelta.Size = new System.Drawing.Size(137, 21);
            this.txtDelta.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(99, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 18);
            this.label2.Text = "State:";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(99, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 18);
            this.label5.Text = "Delta:";
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(192, 101);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(43, 21);
            this.btnRemove.TabIndex = 4;
            this.btnRemove.Text = "del";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // ManageEventNames
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtDelta);
            this.Controls.Add(this.txtDeltaEventName);
            this.Controls.Add(this.txtState);
            this.Controls.Add(this.txtStateEventName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbEvents);
            this.Menu = this.mainMenu1;
            this.Name = "ManageEventNames";
            this.Text = "ManageEventNames";
            this.Load += new System.EventHandler(this.ManageEventNames_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem mnuBack;
        private System.Windows.Forms.ListBox lbEvents;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStateEventName;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtDeltaEventName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtState;
        private System.Windows.Forms.TextBox txtDelta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnRemove;

    }
}