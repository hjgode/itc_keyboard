using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ITC_KEYBOARD;

namespace MapVol2Page
{
    public partial class Form1 : Form
    {
        remapVol_Up_Down_to_Pg_Up_Down cls;
        public Form1()
        {
            InitializeComponent();
            cls = new remapVol_Up_Down_to_Pg_Up_Down(this);        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            cls.restoreDefaultMappings();
        }

        private void btnMap_Click(object sender, EventArgs e)
        {

            cls.mapVolumeKeys();
        }
        delegate void SetTextCallback(string text);
        public void addLog(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.txtLog.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(addLog);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                if (txtLog.Text.Length > 2000)
                    txtLog.Text = "";
                txtLog.Text += text + "\r\n";
                txtLog.SelectionLength = 0;
                txtLog.SelectionStart = txtLog.Text.Length - 1;
                txtLog.ScrollToCaret();
            }
        }
    }
}