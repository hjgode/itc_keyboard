using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ITC_KEYBOARD;

namespace MapScanToF3
{
    public partial class Form1 : Form
    {
        CUSBkeys _cusbKeys;
        public Form1()
        {
            InitializeComponent();
            _cusbKeys = new CUSBkeys();
        }

        void doMapScan2F3()
        {
            CUSBkeys.usbKeyStruct remapKey = new CUSBkeys.usbKeyStruct();
            
            cPlanes.plane[] planes = { cPlanes.plane.normal, cPlanes.plane.orange, cPlanes.plane.aqua };
            int iChangeCount=0;
            foreach (cPlanes.plane plane in planes)
            {
                if (_cusbKeys.getKeyStruct((int)plane, CUsbKeyTypes.HWkeys.SCAN_Button_KeyLang1, ref remapKey) != -1)
                {
                    remapKey.bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
                    //remapKey.bFlagMid = CUsbKeyTypes.usbFlagsMid.VKEY;    // for use with VK_ key generation
                    remapKey.bFlagMid = CUsbKeyTypes.usbFlagsMid.NoFlag;    // for use with PS/2 key generation
                    remapKey.bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
                    //remapKey.bIntScan = (byte)VKEY.VK_F3;    // for use with VK_ key generation
                    remapKey.bIntScan = (byte)ITC_KEYBOARD.PS2KEYS.F3;    // for use with PS/2 key generation

                    if (_cusbKeys.setKey((int)plane, CUsbKeyTypes.HWkeys.SCAN_Button_KeyLang1, remapKey) == 0)
                        iChangeCount++;
                }
            }
            if(iChangeCount>0)
                _cusbKeys.writeKeyTables();
            
            MessageBox.Show(iChangeCount.ToString() + " keys CHANGED");
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (_cusbKeys.resetKeyDefaults() == 0)
                MessageBox.Show("All keys restored");
            else
                MessageBox.Show("Key restore FAILED");
        }

        private void btnDoMap_Click(object sender, EventArgs e)
        {
            doMapScan2F3();
        }
    }
}