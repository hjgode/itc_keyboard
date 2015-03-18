using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace Orange2Ctrl
{
    public partial class Orange2Ctrl : Form
    {
        mapKeyOrange map = new mapKeyOrange();
        Timer _timer = new Timer();
        int timeOut = 5;
        bool _bAutoClose = true;
        bool _bSticky = false;

        public Orange2Ctrl(bool bAutoClose, bool bSticky)
        {
            InitializeComponent();
            
            _bAutoClose = bAutoClose;
            _bSticky = bSticky;

            _timer.Enabled = false;
            _timer.Interval = 1000;

            _timer.Tick += new EventHandler(_timer_Tick);

            if (_bAutoClose)
            {
                _timer.Enabled = true;
                addLog("Dialog will autoclose");
                doMap(_bSticky);
            }
            else
            {
                addLog("Dialog will not autoclose");
            }
            
        }

        void disableAutoclose()
        {
            _timer.Enabled = false;
            _bAutoClose = false;
            addLog("Autoclose disabled");
        }

        //autoclose
        void _timer_Tick(object sender, EventArgs e)
        {
            if (_bAutoClose && timeOut==5)  //first call?
            {
                doMap(_bSticky);
            }
            if (timeOut == 0 && _bAutoClose)
            {
                _timer.Enabled = false;
                _timer.Dispose();
                _timer = null;
                Application.Exit();
            }
            else if (_bAutoClose)
            {
                addLog("Dialog will close in " + timeOut + " seconds");
                timeOut--;
            }
            else
                _timer.Enabled = false;
        }

        void doMap(bool bSticky)
        {
            addLog("mapping Orange to Ctrl as normal key...");
            int iRes = map.mapKeyOrange2Ctrl(bSticky);
            addLog("function returned " + iRes.ToString());
        }

        private void btnMapToCtrlSticky_Click(object sender, EventArgs e)
        {
            disableAutoclose();
            addLog("mapping Orange to Ctrl as sticky key...");
            int iRes = map.mapKeyOrange2Ctrl(true);
            addLog("function returned " + iRes.ToString());
        }

        private void btnMapToCtrl_Click(object sender, EventArgs e)
        {
            disableAutoclose();
            addLog("mapping Orange to Ctrl as normal key...");
            int iRes = map.mapKeyOrange2Ctrl(false);
            addLog("function returned " + iRes.ToString());
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

        private void btnRestoreDefaults_Click(object sender, EventArgs e)
        {
            disableAutoclose();
            addLog("restoring defaults...");
            int iRes = map.restoreDefault();
            addLog("restoreDefaults()=" + iRes.ToString());
        }
    }
}