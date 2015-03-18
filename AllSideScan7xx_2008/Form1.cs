using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ITC_KEYBOARD;

namespace AllSideScan7xx_2008
{
    public partial class Form1 : Form
    {
        CDirectKeys cdirKeys;
        CDirectHWKeys hwKeys;
        public Form1()
        {
            InitializeComponent();
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnDoMap_Click(object sender, EventArgs e)
        {
            addLog("Creating new object...");
            cdirKeys = new CDirectKeys();
            hwKeys = new CDirectHWKeys();
            CDirectKeys.directKeyStruct keyStruct;
            int iCount = cdirKeys.getKeyCount();
            addLog("found " + iCount.ToString() + " keys");
            for (int i = 0; i < iCount; i++)
            {
                keyStruct = cdirKeys.getKey(i);// getKeyString(i));
                string sNameKey = hwKeys.vkPairs[i].sKey;
                byte x = (byte)keyStruct.keyVal;
                VKEY vKey = (VKEY)x;
                addLog (i.ToString("000") + "= " + sNameKey + "/0x"+x.ToString("X") + "("+ vKey.ToString() + ")");
            }
            
        }
        delegate void SetTextCallback(string text);
        private void addLog(string text)
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
                if (txtLog.Text.Length > 6000)
                    txtLog.Text = "";
                txtLog.Text += text + "\r\n";
                txtLog.SelectionLength = text.Length;
                txtLog.SelectionStart = txtLog.Text.Length - text.Length;
                txtLog.ScrollToCaret();
            }
        }
    }
}