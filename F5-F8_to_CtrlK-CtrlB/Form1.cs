using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using KeyMapper;

namespace F5_F8_to_CtrlK_CtrlB
{
    public partial class Form1 : LogForm.CallBackForm
    {
        bool _bAutorun = true;

        public Form1(bool bAutorun)
        {
            InitializeComponent();
            _bAutorun = bAutorun;
            mnuExit.Click += new EventHandler(mnuExit_Click);
            mnuRestore.Click += new EventHandler(mnuRestore_Click);
            mnuMapKeys.Click += new EventHandler(mnuMapKeys_Click);
        }

        void mnuMapKeys_Click(object sender, EventArgs e)
        {
            mapKeys();
        }

        void mnuRestore_Click(object sender, EventArgs e)
        {
            ITC_KEYBOARD.CUSBkeys cusb = new ITC_KEYBOARD.CUSBkeys();
            if (cusb.resetKeyDefaults() != 0)
                MessageBox.Show("You must reboot befor the changes become active");
            else
                MessageBox.Show("Keyboard reset to default");
        }

        void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        static int count=0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!_bAutorun)
                timer1.Enabled = false;
            count++;
            if (count == 1 && _bAutorun)
                mapKeys();
            if(count>=10)
                if(_bAutorun)
                    Application.Exit();
        }

        void mapKeys()
        {
            KeyMapper.remapF5_to_F8_to_CtrlKLNB mapKeys = new remapF5_to_F8_to_CtrlKLNB(this);
            mapKeys.mapKeys();
            if(_bAutorun)
                addLog("will exit automatically ...");

        }

        delegate void SetTextCallback(string text);
        public override void addLog(string text)
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