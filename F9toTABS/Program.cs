using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using ITC_KEYBOARD;

namespace F9toTABS
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (args[0].Contains("default"))
                {
                    ITC_KEYBOARD.CUSBkeys cusb = new ITC_KEYBOARD.CUSBkeys();
                    if (cusb.resetKeyDefaults() != 0)
                        MessageBox.Show("You must reboot befor the changes become active");
                    else
                        MessageBox.Show("Keyboard reset to default");
                }
            }
            else
            {
                /*
                new vkPair ( 0xEA, "F1 (Soft1)" ), //"VK_F1"
                new vkPair ( 0xEB, "F2 (Soft2)" ), //"VK_F2"
                new vkPair ( 0xEC, "F3 (Send)" ), //"VK_F3"
                new vkPair ( 0xED, "F4 (End)" ), //"VK_F4"
                new vkPair ( 0xEE, "F5" ), //"VK_F5"
                new vkPair ( 0xEF, "F6 (VolUp)" ), //"VK_F6"
                new vkPair ( 0xF0, "F7 (VolDn)" ), //"VK_F7"
                new vkPair ( 0xF1, "F8" ), //"VK_F8"
                new vkPair ( 0xF2, "F9" ), //"VK_F9"
                new vkPair ( 0xF3, "F10" ), //"VK_F10"
                new vkPair ( 0xF4, "F11" ), //"VK_F11"
                new vkPair ( 0xF5, "F12" ), //"VK_F12"
                */
#if DEBUG
                int iKey = 0xF2;
#else
                int iKey = 0xEE;
#endif
                mapKey(iKey);
            }
        }

        static void mapKey(int iUSBHID)
        {
            KeytoTABSTABS mapKeys = new KeytoTABSTABS();
            mapKeys.mapKeytoTABTABS(iUSBHID);

        }
    }
    class KeytoTABSTABS
    {
        ITC_KEYBOARD.CUSBkeys _cusbKeys;
        CUsbKeyTypes.HWkeys[] keysToMap = new CUsbKeyTypes.HWkeys[] { CUsbKeyTypes.HWkeys.Left_Control };

        public KeytoTABSTABS()
        {
            _cusbKeys = new CUSBkeys();
        }

        /// <summary>
        /// map the function keys:
        /// F1 would send out CONTROLQ instead of F1 .
        /// F2 would send out CONTROLX
        /// F3 would send out CONTROLE
        /// F4 would send out CONTROLP
        /// </summary>
        public void mapKeytoTABTABS(CUsbKeyTypes.HWkeys KeyToRemap)
        {
            keysToMap[0] = KeyToRemap;
            createMultiKeys();
        }
        public void mapKeytoTABTABS(int iKeyToRemap)
        {
            CUsbKeyTypes.HWkeys hwKey = (CUsbKeyTypes.HWkeys)iKeyToRemap;
            mapKeytoTABTABS(hwKey);
        }

        /// <summary>
        /// create multikey entries with Ctrl + Letters
        /// Control is 08,02,01,14
        /// </summary>
        public void createMultiKeys()
        {
            /*
                        
            07,3A,00,42,04,06 'F1' [NoFlag,NoRepeat, NoChord,MultiKeyIndex,] 'F2' 'MultiIndex'->00,00,00,0D 'Tab' | 00,00,00,0D 'Tab' | 00,00,00,0D 'Tab' | 00,00,00,0D 'Tab' | 00,00,00,5A 'Return' |             

            MultiKey6: ,00,00,00,0D[NoFlag,NoFlag,NormalKey,] 'Tab' + ,00,00,00,0D[NoFlag,NoFlag,NormalKey,] 'Tab' + ,00,00,00,0D[NoFlag,NoFlag,NormalKey,] 'Tab' + ,00,00,00,0D[NoFlag,NoFlag,NormalKey,] 'Tab' + ,00,00,00,5A[NoFlag,NoFlag,NormalKey,] 'Return' + 

            */
            //F1 key:   = 07 3A 00 00 00 05
            // ########## as multi4 key:        07,3a,00,02,04,04

            //prepare a new multikey entry
            CUSBkeys.usbKeyStructShort[] uKey = new CUSBkeys.usbKeyStructShort[5];

            //map to TAB 
            uKey[0].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey[0].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoFlag;
            uKey[0].bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
            uKey[0].bIntScan = (byte)ITC_KEYBOARD.PS2KEYS.Tab;  //point to tab key

            //map to TAB 
            uKey[1].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey[1].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoFlag;
            uKey[1].bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
            uKey[1].bIntScan = (byte)ITC_KEYBOARD.PS2KEYS.Tab;  //point to tab key

            //map to TAB 
            uKey[2].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey[2].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoFlag;
            uKey[2].bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
            uKey[2].bIntScan = (byte)ITC_KEYBOARD.PS2KEYS.Tab;  //point to tab key

            //map to TAB 
            uKey[3].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey[3].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoFlag;
            uKey[3].bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
            uKey[3].bIntScan = (byte)ITC_KEYBOARD.PS2KEYS.Tab;  //point to tab key

            //map to TAB 
            uKey[4].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey[4].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoFlag;
            uKey[4].bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
            uKey[4].bIntScan = (byte)ITC_KEYBOARD.PS2KEYS.Return;  //point to return key

            //try to find existing MultiKey
            CMultiKeys cmulti = new CMultiKeys();
            int iMax = cmulti.getMultiKeyCount();
            int iFound = cmulti.findMultiKey(uKey);
            if (iFound == -1)//if there was no existing entry, create a new one
            {
                iFound = cmulti.addMultiKey(uKey);
            }

            //now map the key in question to the new/existing MultiKeyEntry in all planes
            //CUSBkeys.usbKeyStruct remapKey = new CUSBkeys.usbKeyStruct();

            //############### map 2 key #####################
            //map F1 key in normal plane to new multikey
            setKeyToMultiKey((int)keysToMap[0], (byte)(iFound), cPlanes.plane.normal);

            _cusbKeys.writeKeyTables();
        }

        /// <summary>
        /// map a key to a multikey index
        /// </summary>
        /// <param name="iKey">the key to map</param>
        /// <param name="iMultiIndex">zero based index to Multi1, Multi2...</param>
        /// <param name="cPlane">keyboard plane</param>
        private void setKeyToMultiKey(int iKey, byte iMultiIndex, cPlanes.plane cPlane)
        {
            CUSBkeys.usbKeyStruct remapKey = new CUSBkeys.usbKeyStruct();

            if (_cusbKeys.getKeyStruct((int)cPlane, iKey, ref remapKey) != -1)
            {   //key exists
                remapKey.bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
                remapKey.bFlagMid = CUsbKeyTypes.usbFlagsMid.NoRepeat | CUsbKeyTypes.usbFlagsMid.NoChord;
                remapKey.bFlagLow = CUsbKeyTypes.usbFlagsLow.MultiKeyIndex;
                remapKey.bIntScan = (byte)(iMultiIndex + 1); //the idx is zero based! but it is Multi1, Multi2...


                _cusbKeys.setKey((int)cPlane, iKey, remapKey);
            }
            else
            {
                //add key
                remapKey.bHID = 0x07;
                remapKey.bScanKey = (CUsbKeyTypes.HWkeys)iKey;
                remapKey.bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
                remapKey.bFlagMid = CUsbKeyTypes.usbFlagsMid.NoRepeat | CUsbKeyTypes.usbFlagsMid.NoChord;
                remapKey.bFlagLow = CUsbKeyTypes.usbFlagsLow.MultiKeyIndex;
                remapKey.bIntScan = (byte)iMultiIndex;
                _cusbKeys.addKey(cPlane, remapKey);
            }
            System.Diagnostics.Debug.WriteLine("setKeyToMultiKey: " + _cusbKeys.dumpKey(remapKey));
        }

        /// <summary>
        /// find the Modifiers Entry for the "Control Key"
        /// creates a new Modifier Entry if no entry is found
        /// </summary>
        /// <returns>-1 if not found
        /// pos value is index of found/created entry</returns>
        private int findCtrlModifier()
        {
            //  08,02,01,14 'Left Control'
            CUSBkeys.usbKeyStructShort uKey = CUSBkeys.controlModKey;// new CUSBkeys.usbKeyStructShort();
            int iFound = -1;
            CModifiersKeys cModis = new CModifiersKeys();
            iFound = cModis.findModifierKey(uKey);

            if (iFound == -1)
                iFound = cModis.addModifierKey(uKey);

            return iFound;
        }

        public void restoreDefaultMappings()
        {
            CUSBkeys cusb = new CUSBkeys();
            cusb.resetKeyDefaults();
        }
    }

}
