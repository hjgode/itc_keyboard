using System;

using System.Collections.Generic;
using System.Text;

using ITC_KEYBOARD;

namespace Action2Tabs
{
    class ActionToTABSTABS
    {
        ITC_KEYBOARD.CUSBkeys _cusbKeys;
        int iMultikeyIndex = -1; //the MultiKey added or mapped to
        CUSBkeys.usbKeyStruct usbKey;

        public ActionToTABSTABS()
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
        public int mapActionToTABTABS()
        {
            return (createMultiKeys());
        }

        /// <summary>
        /// create multikey entries with Ctrl + Letters
        /// Control is 08,02,01,14
        /// </summary>
        public int createMultiKeys()
        {
            CUsbKeyTypes.HWkeys[] keysToMap = new CUsbKeyTypes.HWkeys[] { 
                CUsbKeyTypes.HWkeys.F1,
                CUsbKeyTypes.HWkeys.Action_OK
                };
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
            iMultikeyIndex = iFound+1;

            //now map the key in question to the new/existing MultiKeyEntry in all planes
            //CUSBkeys.usbKeyStruct remapKey = new CUSBkeys.usbKeyStruct();

            //############### map 2 key #####################
            //map F1 key in normal plane to new multikey
            //setKeyToMultiKey((int)keysToMap[0], (byte)(iFound), cPlanes.plane.normal);

            //map Action(OK) key in normal plane to new multikey
            setKeyToMultiKey((int)keysToMap[1], (byte)(iFound), cPlanes.plane.normal);

            return (_cusbKeys.writeKeyTables());
        }

        public string dumpMultiKey(int iMKey)
        {
            string s = "";
            s = _cusbKeys.dumpMultiKey(iMKey);
            return s;
        }
        public string dumpMultiKey()
        {
            string s = "";
            if(iMultikeyIndex>0)
                s = _cusbKeys.dumpMultiKey(iMultikeyIndex);
            return s;
        }
        public string dumpKey()
        {
            string s = "";
            s = _cusbKeys.dumpKey(usbKey);
            return s;
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
                remapKey.bIntScan = (byte)(iMultiIndex+1); //the idx is zero based! but it is Multi1, Multi2...


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
            usbKey = remapKey;
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
