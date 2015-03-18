using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;

using ITC_KEYBOARD;

namespace map_orange_1_to_f11
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnMapKey_Click(object sender, EventArgs e)
        {
            ITC_KEYBOARD.CUSBkeys _cusbKeys = new CUSBkeys();
            CUSBkeys.usbKeyStruct usbKey = new CUSBkeys.usbKeyStruct();
            int iRes = _cusbKeys.getKeyStruct((int)ITC_KEYBOARD.cPlanes.plane.orange, (int)ITC_KEYBOARD.HardwareKeys.AllKeys.ITC_Standard_1_Key, ref usbKey);
            if (iRes != -1)
            {
                usbKey.bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
                usbKey.bFlagMid = CUsbKeyTypes.usbFlagsMid.VKEY;
                usbKey.bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
                usbKey.bIntScan = (byte)ITC_KEYBOARD.VKEY.VK_F11;
                iRes = _cusbKeys.setKey(cPlanes.plane.orange, (int)ITC_KEYBOARD.HardwareKeys.AllKeys.ITC_Standard_1_Key, usbKey);
                if (iRes == 0)
                {
                    iRes = _cusbKeys.writeKeyTables();
                    if (iRes == 0)
                        MessageBox.Show("Mapping Orange+1 to F11 Succeeded");
                    else
                        MessageBox.Show("Mapping Orange+1 to F11 FAILED in writeKeyTables()");
                }
                else
                    MessageBox.Show("Mapping Orange+1 to F11 FAILED in setKey()");
            }
            else
                MessageBox.Show("Mapping Orange+1 to F11 FAILED in getKeyStruct");            
        }

        private void btnRestoreDefaults_Click(object sender, EventArgs e)
        {
            ITC_KEYBOARD.CUSBkeys _cusbKeys = new CUSBkeys();
            int iRes = _cusbKeys.resetKeyDefaults();
            if (iRes == 0)
                MessageBox.Show("Reset to Default Succeeded");
            else
                MessageBox.Show("Reset to Default FAILED");
        }

        private void btnUnregisterFunc1_Click(object sender, EventArgs e)
        {
            if(UnregisterFunc1(0x00, (uint)ITC_KEYBOARD.VKEY.VK_F11))
                MessageBox.Show("UnregisterFunc1 Succeeded");
            else
                MessageBox.Show("UnregisterFunc1 FAILED");
        }

        [DllImport("coredll.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool UnregisterFunc1(MODIFIERS fsModifiers, uint id);

        enum MODIFIERS:uint{
            MOD_NONE = 0x0,
            MOD_ALT = 0x1,
            MOD_CONTROL = 0x2,
            MOD_SHIFT = 0x4,
            MOD_WIN = 0x8,
            WM_HOTKEY = 0x312
        }
    }
}