namespace MultiKeyTest1
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnDoMap = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.btnMap2EnterF1FDEnter = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1, 163);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(236, 69);
            this.textBox1.TabIndex = 0;
            // 
            // btnDoMap
            // 
            this.btnDoMap.Location = new System.Drawing.Point(117, 13);
            this.btnDoMap.Name = "btnDoMap";
            this.btnDoMap.Size = new System.Drawing.Size(111, 28);
            this.btnDoMap.TabIndex = 1;
            this.btnDoMap.Text = "Map Button";
            this.btnDoMap.Click += new System.EventHandler(this.btnDoMap_Click);
            // 
            // btnRestore
            // 
            this.btnRestore.Location = new System.Drawing.Point(126, 237);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(111, 28);
            this.btnRestore.TabIndex = 1;
            this.btnRestore.Text = "Restore";
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // btnMap2EnterF1FDEnter
            // 
            this.btnMap2EnterF1FDEnter.Location = new System.Drawing.Point(97, 126);
            this.btnMap2EnterF1FDEnter.Name = "btnMap2EnterF1FDEnter";
            this.btnMap2EnterF1FDEnter.Size = new System.Drawing.Size(140, 31);
            this.btnMap2EnterF1FDEnter.TabIndex = 2;
            this.btnMap2EnterF1FDEnter.Text = "map to \\nF1FD\\n";
            this.btnMap2EnterF1FDEnter.Click += new System.EventHandler(this.btnMap2EnterF1FDEnter_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.Location = new System.Drawing.Point(62, 73);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(174, 22);
            this.comboBox1.TabIndex = 3;
            // 
            // comboBox2
            // 
            this.comboBox2.Location = new System.Drawing.Point(117, 98);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(119, 22);
            this.comboBox2.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(4, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 19);
            this.label1.Text = "Plane:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(5, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 19);
            this.label2.Text = "Plane:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(4, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 19);
            this.label3.Text = "Key:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnMap2EnterF1FDEnter);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.btnDoMap);
            this.Controls.Add(this.textBox1);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnDoMap;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Button btnMap2EnterF1FDEnter;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

