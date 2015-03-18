using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FunctionKeys2ControlKeys
{
    public partial class MapFKeysToCtrlKeys : Form
    {
        ITC_KEYBOARD.KdbToolsClass kdbTools = null;
        public MapFKeysToCtrlKeys()
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

        private void btnMapKeys_Click(object sender, EventArgs e)
        {
            MapFKeys2CtrlKeysClass mypClass = new MapFKeys2CtrlKeysClass();
            mypClass.createMultiKeys();
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            if(kdbTools!=null)
                kdbTools.Dispose();
            this.Close();
        }

        private void btnRestoreDefaults_Click(object sender, EventArgs e)
        {
            ITC_KEYBOARD.CUSBkeys _usbKeys = new ITC_KEYBOARD.CUSBkeys();
            _usbKeys.resetKeyDefaults();
            _usbKeys.writeKeyTables();
        }

        private void mnuDump_Click(object sender, EventArgs e)
        {
            ITC_KEYBOARD.DumpForm frm = new ITC_KEYBOARD.DumpForm();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void mnuRestore_Click(object sender, EventArgs e)
        {
            ITC_KEYBOARD.CUSBkeys _usbKeys = new ITC_KEYBOARD.CUSBkeys();
            _usbKeys.resetKeyDefaults();
            _usbKeys.writeKeyTables();            
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

        private void button1_Click(object sender, EventArgs e)
        {
            addLog("Start TEST");
            ITC_KEYBOARD.CUSBkeys _cusb = new ITC_KEYBOARD.CUSBkeys();
            ITC_KEYBOARD.CUSBkeys.usbKeyStruct _uKey = new ITC_KEYBOARD.CUSBkeys.usbKeyStruct();
            ITC_KEYBOARD.CUSBkeys.usbKeyStruct _uKeyOrg = new ITC_KEYBOARD.CUSBkeys.usbKeyStruct();

            int iRes = 0;

            iRes = _cusb.getKeyCount(ITC_KEYBOARD.cPlanes.plane.normal);
            addLog("Found " + iRes + " keys in plane 0");

            iRes = _cusb.getKeyStruct((int)ITC_KEYBOARD.cPlanes.plane.normal, (int)ITC_KEYBOARD.HardwareKeys.AllKeys.ITC_Standard_Space_Key, ref _uKey);
            addLog("getKeyStruct  =" + iRes + " " + _cusb.dumpKey(_uKey));
            _uKeyOrg = _uKey;

            _uKey.bFlagHigh = ITC_KEYBOARD.CUsbKeyTypes.usbFlagsHigh.NoFlag;
            _uKey.bFlagMid = ITC_KEYBOARD.CUsbKeyTypes.usbFlagsMid.NoRepeat;
            _uKey.bFlagLow = ITC_KEYBOARD.CUsbKeyTypes.usbFlagsLow.MultiKeyIndex;
            _uKey.bIntScan = 1;

            iRes = _cusb.setKey(ITC_KEYBOARD.cPlanes.plane.normal, (int)ITC_KEYBOARD.HardwareKeys.AllKeys.ITC_Standard_Space_Key, _uKey);
            addLog("setKey Multi1 = " + iRes + " " + _cusb.dumpKey(_uKey));
            
            _cusb.writeKeyTables();
            System.Threading.Thread.Sleep(1000);

            iRes = _cusb.getKeyStruct((int)ITC_KEYBOARD.cPlanes.plane.normal, (int)ITC_KEYBOARD.HardwareKeys.AllKeys.ITC_Standard_Space_Key, ref _uKey);
            addLog("getKeyStruct  =" + iRes + " " + _cusb.dumpKey(_uKey));

            iRes = _cusb.setKey(ITC_KEYBOARD.cPlanes.plane.normal, (int)ITC_KEYBOARD.HardwareKeys.AllKeys.ITC_Standard_Space_Key, _uKeyOrg);
            addLog("setKey Orig  =" + iRes);

            _cusb.writeKeyTables();
            System.Threading.Thread.Sleep(1000);

            iRes = _cusb.getKeyStruct((int)ITC_KEYBOARD.cPlanes.plane.normal, (int)ITC_KEYBOARD.HardwareKeys.AllKeys.ITC_Standard_Space_Key, ref _uKey);
            addLog("getKeyStruct =" + iRes + " " + _cusb.dumpKey(_uKey));
        }
    }
}