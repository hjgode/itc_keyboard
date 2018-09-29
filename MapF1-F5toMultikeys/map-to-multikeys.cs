using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using ITC_KEYBOARD;

namespace MapF1_F5toMultikeys
{
    class map_to_multikeys
    {

        ITC_KEYBOARD.CUSBkeys cusb = null;
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
/*                (byte)HardwareKeys.AllKeys.ITC_Standard_F11_Key,
                (byte)HardwareKeys.AllKeys.ITC_Standard_F11_Key,
*/            };
        byte[][] keystomapto = new byte[2][]{
                    // 26D15000
                new byte[]{
                    (byte)VKEY.VK_2, 
                    (byte)VKEY.VK_6,
                    (byte)VKEY.VK_D,
                    (byte)VKEY.VK_1,
                    (byte)VKEY.VK_5,
                    (byte)VKEY.VK_0,
                    (byte)VKEY.VK_0,
                    (byte)VKEY.VK_0},
                    // 26D2999
                new byte[]{(byte)
                    (byte)VKEY.VK_2, 
                    (byte)VKEY.VK_6,
                    (byte)VKEY.VK_D,
                    (byte)VKEY.VK_2,
                    (byte)VKEY.VK_9,
                    (byte)VKEY.VK_9,
                    (byte)VKEY.VK_9}
        };
        USBkeys.usbKeyStruct uKey = null;

        public map_to_multikeys()
        {
            try
            {
                cusb = new ITC_KEYBOARD.CUSBkeys();

                //the key we will map
                uKey=new CUSBkeys.usbKeyStruct();
                uKey.bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
                uKey.bFlagMid = CUsbKeyTypes.usbFlagsMid.NoFlag;
                uKey.bFlagLow = CUsbKeyTypes.usbFlagsLow.MultiKeyIndex;
                uKey.bHID = 0x07;

                int iRes = 0;
                //for each plane???
                for (int p = 0; p < cusb.getNumPlanes(); p++)
                {
                    //for each multikey
                    for (int k = 0; k < keystomap.Length; k++)
                    {
                        byte[] mKey=keystomapto[k];
                        //create multiKey
                        CUSBkeys.usbKeyStructShort[] multiKey = new CUSBkeys.usbKeyStructShort[mKey.Length];
                        for (int x = 0; x < mKey.Length; x++ )
                        {
                            multiKey[x].bFlagHigh=CUsbKeyTypes.usbFlagsHigh.NoFlag;
                            multiKey[x].bFlagMid = CUsbKeyTypes.usbFlagsMid.VKEY;
                            multiKey[x].bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
                            multiKey[x].bIntScan = mKey[x];
                            //find multikey
                            //try to find existing MultiKey
                            CMultiKeys cmulti = new CMultiKeys();
                            int iMax = cmulti.getMultiKeyCount();
                            int iFoundMultiKey = cmulti.findMultiKey(multiKey);
                            if (iFoundMultiKey == -1)//if there was no existing entry, create a new one
                            {
                                iFoundMultiKey = cmulti.addMultiKey(uKey);
                            }

                            uKey.bIntScan = iFoundMultiKey;
                            uKey.bScanKey = (CUsbKeyTypes.HWkeys)keystomap[k];
                            iRes = cusb.setKey((int)p, keystomap[k], uKey);
                            System.Diagnostics.Debug.WriteLine("keymap " + p.ToString() + "/" + k.ToString() + "/" + iRes.ToString());
                        }
                    }
                }
                iRes = cusb.writeKeyTables();            

            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
