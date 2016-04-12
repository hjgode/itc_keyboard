using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using ITC_KEYBOARD;

namespace MapFKeys12
{
    class mapfkeys
    {
        public static void mapKeys()
        {
            ITC_KEYBOARD.CUSBkeys cusb = new ITC_KEYBOARD.CUSBkeys();
            byte[] keystomap = new byte[]{
                (byte)HardwareKeys.AllKeys.ITC_Standard_F1_Key,
                (byte)HardwareKeys.AllKeys.ITC_Standard_F2_Key,
                (byte)HardwareKeys.AllKeys.ITC_Standard_F3_Key,
                (byte)HardwareKeys.AllKeys.ITC_Standard_F4_Key,
                (byte)HardwareKeys.AllKeys.ITC_Standard_F5_Key,
                (byte)HardwareKeys.AllKeys.ITC_Standard_F6_Key,
                (byte)HardwareKeys.AllKeys.ITC_Standard_F7_Key,
                (byte)HardwareKeys.AllKeys.ITC_Standard_F8_Key,
                (byte)HardwareKeys.AllKeys.ITC_Standard_F9_Key,
                (byte)HardwareKeys.AllKeys.ITC_Standard_F10_Key,
                (byte)HardwareKeys.AllKeys.ITC_Standard_F11_Key,
                (byte)HardwareKeys.AllKeys.ITC_Standard_F11_Key,
            };

            byte[] keystomapto1 = new byte[]{
                (byte)VKEY.undef_0xE6,
                (byte)VKEY.undef_0xE7,
                (byte)VKEY.undef_0xE8,
                (byte)VKEY.undef_0xE9,
                (byte)VKEY.undef_0xEA,
                (byte)VKEY.undef_0xEB,
                (byte)VKEY.undef_0xEC,
                (byte)VKEY.undef_0xED,
                (byte)VKEY.undef_0xEE,
                (byte)VKEY.undef_0xEF,
                (byte)VKEY.undef_0xF0,
                (byte)VKEY.undef_0xF1,
            };
            byte[] keystomapto = new byte[]{
                (byte)VKEY.VK_1,
                (byte)VKEY.VK_2,
                (byte)VKEY.VK_3,
                (byte)VKEY.undef_0xE9,
                (byte)VKEY.undef_0xEA,
                (byte)VKEY.undef_0xEB,
                (byte)VKEY.undef_0xEC,
                (byte)VKEY.undef_0xED,
                (byte)VKEY.undef_0xEE,
                (byte)VKEY.undef_0xEF,
                (byte)VKEY.undef_0xF0,
                (byte)VKEY.undef_0xF1,
            };

            CUSBkeys.usbKeyStruct uKey = new CUSBkeys.usbKeyStruct();

            uKey.bFlagHigh=CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey.bFlagMid=CUsbKeyTypes.usbFlagsMid.VKEY;
            uKey.bFlagLow=CUsbKeyTypes.usbFlagsLow.NormalKey;
            uKey.bHID=0x07;
            int iRes = 0;
            for (int p = 0; p < cusb.getNumPlanes(); p++)
            {
                for (int k = 0; k < keystomap.Length; k++)
                {
                    uKey.bIntScan=keystomapto[k];
                    uKey.bScanKey=(CUsbKeyTypes.HWkeys) keystomap[k];
                    iRes = cusb.setKey((int)p, keystomap[k], uKey);
                    System.Diagnostics.Debug.WriteLine("keymap " + p.ToString() + "/" + k.ToString() + "/" + iRes.ToString());
                }
            }
            iRes = cusb.writeKeyTables();            
        }

        public static void restoreDefaults()
        {
            ITC_KEYBOARD.CUSBkeys cusb = new ITC_KEYBOARD.CUSBkeys();
            int iRes = cusb.resetKeyDefaults();
            iRes = cusb.writeKeyTables();
        }
    }
}
