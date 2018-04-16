using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MapFKeystoCtrl_Number
{
    public partial class Form1 : Form
    {
        ITC_KEYBOARD.KdbToolsClass kdbTools = null;
        public Form1()
        {
            InitializeComponent();
            try
            {
                kdbTools = new ITC_KEYBOARD.KdbToolsClass();
                kdbTools.KeyboardChanged += new EventHandler(kdbTools_KeyboardChanged);
                addLog("KdbToolsClass load OK. Version " + kdbTools.sKeybdVersion);
            }
            catch (Exception ex)
            {
                addLog("KdbToolsClass failed: " + ex.Message);
            }
        }

        void kdbTools_KeyboardChanged(object sender, EventArgs e)
        {
            addLog("Keyboard changed");
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
                if (txtLog.Text.Length > 2000)
                    txtLog.Text = "";
                txtLog.Text += text + "\r\n";
                txtLog.SelectionLength = text.Length;
                txtLog.SelectionStart = txtLog.Text.Length - text.Length;
                txtLog.ScrollToCaret();
            }
        }

        private void btnMapKeys_Click_1(object sender, EventArgs e)
        {
            FunctionKeys2ControlKeys.MapFKeys2CtrlKeysClass2 mypClass = new FunctionKeys2ControlKeys.MapFKeys2CtrlKeysClass2();
            mypClass.createMultiKeys();
        }

        private void Form1_Closing(object sender, CancelEventArgs e)
        {
            if (kdbTools != null)
                kdbTools.Dispose();            
        }


    }
}