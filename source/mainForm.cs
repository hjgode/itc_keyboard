using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ITC_KEYBOARD;

namespace CUsbKeysCStest
{
    public partial class mainForm : Form
    {
        CRotateKeys _cRot;
        public mainForm()
        {
            InitializeComponent();
            _cRot = new CRotateKeys();
#if EMULATE
            btnUSBkeys.Visible=false;
#endif
        }

        private void btnDirect_Click(object sender, EventArgs e)
        {
            try
            {
                directKeysForm dDlg = new directKeysForm();
                dDlg.ShowDialog();
            }
            catch (ObjectDisposedException)
            {
            }
            catch (Exception)
            {
            }
        }

        private void btnUSBkeys_Click(object sender, EventArgs e)
        {
            usbkeysEventForm uDlg = new usbkeysEventForm();
            uDlg.ShowDialog();
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_cRot.getRotateKeyCount() == 0)
            {
                MessageBox.Show("No RotateKeys!");
                return;
            }
            RotateKeysForm rDlg = new RotateKeysForm();
            rDlg.ShowDialog();
        }

        private void mnuOptions_Click(object sender, EventArgs e)
        {
            CRotateKeys.writeDefaultRotateSets();
        }

        private void mnuMapGreenW2Ctrl_Click(object sender, EventArgs e)
        {
            // 00,00,08
            ITC_KEYBOARD.CUSBkeys _cusb = new ITC_KEYBOARD.CUSBkeys();
            ITC_KEYBOARD.CUSBkeys.usbKeyStruct _theKey=new ITC_KEYBOARD.CUSBkeys.usbKeyStruct();
            _cusb.getKeyStruct(2, 0x1A, ref _theKey);
            //set to point to modifier index 4

            //
            _theKey.bFlagLow = ITC_KEYBOARD.CUsbKeyTypes.usbFlagsLow.ModifierIndex;// UsbKeyFlags3.StickyOnce;
            _theKey.bFlagMid = ITC_KEYBOARD.CUsbKeyTypes.usbFlagsMid.NoRepeat;// UsbKeyFlags2.NoRepeat;
            _theKey.bFlagHigh = ITC_KEYBOARD.CUsbKeyTypes.usbFlagsHigh.StickyOnce;// UsbKeyFlags3.ModifierIndex;
            _theKey.bIntScan = 0x04;
            string s = _cusb.dumpKey(_theKey); 
            // 3-2-1 = "07,1A,08,00,00,04-w- 'ModifierKey'"	string
            // 1-2-3 = "07,1A,00,00,08,04-w- 'F3'"	string

            _cusb.writeKeyTables();
            _cusb = null;
        }

        private void mnuUpdateDriver_Click(object sender, EventArgs e)
        {
            ITC_KEYBOARD.CUSBkeys _cusb = new ITC_KEYBOARD.CUSBkeys();
            _cusb.writeKeyTables();
            _cusb = null;
        }

        private void btnUsbKeys_Click_1(object sender, EventArgs e)
        {
            UsbKeysForm uf = new UsbKeysForm();
            uf.ShowDialog();
            uf.Dispose();
        }
    }
}