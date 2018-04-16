using System;
using System.Collections.Generic;
using System.Text;

using ITC_KEYBOARD;

namespace FunctionKeys2ControlKeys
{
    class MapFKeys2CtrlKeysClass2
    {
        ITC_KEYBOARD.CUSBkeys _cusbKeys;

        public MapFKeys2CtrlKeysClass2()
        {
            _cusbKeys = new CUSBkeys();
        }

        /// <summary>
        /// map the function keys:
        /// F1 would send out CONTROL, instead of F1 .
        /// F2 would send out CONTROL.
        /// F3 would send out CONTROL/
        /// F4 would send out CONTROL;
        /// F5 would send out Control'
        /// </summary>
        public void mapFKeysToControlKeys()
        {
            createMultiKeys();
        }

        /// <summary>
        /// create multikey entries with Ctrl + Digits
        /// Control is 08,02,01,14
        /// </summary>
        public void createMultiKeys()
        {
            CUsbKeyTypes.HWkeys[] keysToMap = new CUsbKeyTypes.HWkeys[] { 
                CUsbKeyTypes.HWkeys.F1_cozumel, 
                CUsbKeyTypes.HWkeys.F2_cozumel, 
                CUsbKeyTypes.HWkeys.F3_cozumel, 
                CUsbKeyTypes.HWkeys.F4_cozumel,
                CUsbKeyTypes.HWkeys.F5_cozumel,
                CUsbKeyTypes.HWkeys.F6_cozumel, 
                CUsbKeyTypes.HWkeys.F7_cozumel, 
                CUsbKeyTypes.HWkeys.F8_cozumel, 
                CUsbKeyTypes.HWkeys.F9_cozumel,
                CUsbKeyTypes.HWkeys.F10_cozumel,
            };

            PS2KEYS[] multikeys0 = new PS2KEYS[]{
                PS2KEYS.A,
                PS2KEYS.one,
                PS2KEYS.two,
                PS2KEYS.three,
                PS2KEYS.four,
                PS2KEYS.five,
                PS2KEYS.six,
                PS2KEYS.seven,
                PS2KEYS.eight,
                PS2KEYS.nine,
            };

            PS2KEYS[] multikeys = new PS2KEYS[]{
                PS2KEYS.comma,
                PS2KEYS.period,
                PS2KEYS.slash,
                PS2KEYS.semicolon,
                PS2KEYS.minus,
                PS2KEYS.equal,
                PS2KEYS.lsquarbracket,
                PS2KEYS.rsquarbracket,
                PS2KEYS.backslash,
                PS2KEYS.lquote,
            };

            //get Ctrl Modifier Key
            int iCtrlIdx = findCtrlModifier();

            //prepare a new multikey entry
            CUSBkeys.usbKeyStructShort[] uKey = new CUSBkeys.usbKeyStructShort[2];

            //map to modifier key 
            uKey[0].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey[0].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoFlag | CUsbKeyTypes.usbFlagsMid.NoChord;
            uKey[0].bFlagLow = CUsbKeyTypes.usbFlagsLow.ModifierIndex;
            uKey[0].bIntScan = (byte)iCtrlIdx;  //points to control modifier key index

            //map to '0'
            uKey[1].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey[1].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoFlag | CUsbKeyTypes.usbFlagsMid.NoChord;
            uKey[1].bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
            uKey[1].bIntScan = (byte)ITC_KEYBOARD.PS2KEYS.zero;

            CMultiKeys cmulti = new CMultiKeys();
            int iMax, iFound;
            for (int idx = 0; idx < 10;idx++)
            {
                //set the second part of multikey to current multikey [0 to 9]
                uKey[1].bIntScan = (byte)multikeys[idx];
                //try to find existing MultiKey
                cmulti = new CMultiKeys();
                iMax = cmulti.getMultiKeyCount();
                iFound = cmulti.findMultiKey(uKey);
                if (iFound == -1)//if there was no existing entry, create a new one
                {
                    iFound = cmulti.addMultiKey(uKey);
                }
                //now map the key in question to the new/existing MultiKeyEntry in all planes
                //############### map Fx KEY to the new multikey #####################
                //map the key in normal, orange and aqua/green plane to new multikey
                for (int iPlane = 0; iPlane < 3; iPlane++)
                {
                    setKeyToMultiKey((int)keysToMap[idx], (byte)iFound, (cPlanes.plane)iPlane);
                }
            }

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
