using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using ITC_KEYBOARD;

namespace MapF5toCtrl_L
{
    public class remapF5toCtrlL
    {
        ITC_KEYBOARD.CUSBkeys _cusbKeys;
        CUsbKeyTypes.HWkeys[] keysToMap;// = new CUsbKeyTypes.HWkeys[] { CUsbKeyTypes.HWkeys.Left_Control };
        public remapF5toCtrlL()
        {
            _cusbKeys = new CUSBkeys();
        }

        /// </summary>
        public void mapKeys()
        {
            keysToMap = new CUsbKeyTypes.HWkeys[1];
            keysToMap[0] = CUsbKeyTypes.HWkeys.F5_cozumel;
            createMultiKeys();
        }

        /// <summary>
        /// create multikey entries with Ctrl + Letters
        /// Control is 08,02,01,14
        /// </summary>
        public void createMultiKeys()
        {
            /*
            leftCtrl key is 
                07,E0,00,00,08,01 'Left Control' [NoFlag,NoFlag,ModifierIndex,] 'F9' 'ModifierKey'->08,02,01,14 'Left Control' | 
            */
            //F5 key:   = 07 3E 00 00 00 05
            // ########## as multi4 key:        07,3E,00,02,04,04

            //prepare a new multikey entry
            CUSBkeys.usbKeyStructShort[] uKey = new CUSBkeys.usbKeyStructShort[2];  // we need Ctrl+a

            //map to Ctrl 
            //find Ctrl modifier index
            ITC_KEYBOARD.CModifiersKeys _cModis = new CModifiersKeys();
            int iModiCtrlIndex = findCtrlModifier();
            #region F5 to Ctrl-L
/*
================================
          ModifierKeys
================================
,01,02,08,14[StickyOnce,NoRepeat,ModifierIndex,] 'Left Control'
,01,02,08,59[StickyOnce,NoRepeat,ModifierIndex,] 'Right Shift'
,01,02,08,11[StickyOnce,NoRepeat,ModifierIndex,] 'Left Alt'
,14,02,08,58[StickyLock, LED1,NoRepeat,ModifierIndex,] 'Caps Lock'
,00,02,08,14[NoFlag,NoRepeat,ModifierIndex,] 'Left Control'
,00,02,08,11[NoFlag,NoRepeat,ModifierIndex,] 'Left Alt'
*/
            // we need a MultiKey 00,40,08,01, 00,40,00,4B

            //create a multikey entry for Ctrl
            //map first entry to control modifier index
            uKey[0].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey[0].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoChord;
            uKey[0].bFlagLow = CUsbKeyTypes.usbFlagsLow.ModifierIndex;
            uKey[0].bIntScan = (byte)iModiCtrlIndex;  //point to modifier index

            //######### the above only valid for single keys, ie press and hold Ctrl

            //map first entry to VK control key
            //uKey[0].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            //uKey[0].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoChord | CUsbKeyTypes.usbFlagsMid.VKEY;
            //uKey[0].bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
            //uKey[0].bIntScan = (byte)ITC_KEYBOARD.VKEY.VK_LCONTROL;  //do a VK_LCONTROL

            //map first entry to PS2 control key with StickyOnce set
            //uKey[0].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            //uKey[0].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoChord;
            //uKey[0].bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
            //uKey[0].bIntScan = (byte)ITC_KEYBOARD.PS2KEYS.Left_Control;  //do a VK_LCONTROL

            //map entry to PS2 key 'l'
            // 07,0F,
            // 00,00,00,4B 'l' [NoFlag,NoFlag,NormalKey,] 'l'
            uKey[1].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey[1].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoChord;
            uKey[1].bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
            uKey[1].bIntScan = (byte)ITC_KEYBOARD.PS2KEYS.L;  //point to L key // 0x4B = 75

            ////map entry to VK key 'L'
            //uKey[1].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            //uKey[1].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoChord | CUsbKeyTypes.usbFlagsMid.VKEY;
            //uKey[1].bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
            //uKey[1].bIntScan = (byte)ITC_KEYBOARD.VKEY.VK_L;  //point to L key

            //try to find existing MultiKey
            CMultiKeys cmulti = new CMultiKeys();
            int iMax = cmulti.getMultiKeyCount();
            int iFoundCtrlL = cmulti.findMultiKey(uKey);
            if (iFoundCtrlL == -1)//if there was no existing entry, create a new one
            {
                //not found, then add new entry
                iFoundCtrlL = cmulti.addMultiKey(uKey);
            }
            //map the key to the new found multikey index
            setKeyToMultiKey((int)keysToMap[0], (byte)(iFoundCtrlL), cPlanes.plane.normal);
            #endregion

            //save and update
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
                remapKey.bFlagMid = CUsbKeyTypes.usbFlagsMid.NoFlag;// NoRepeat | CUsbKeyTypes.usbFlagsMid.NoChord;
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
                remapKey.bFlagMid = CUsbKeyTypes.usbFlagsMid.NoFlag;
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
            //assemble the Ctrl modifier entry: 01,02,08,14[StickyOnce,NoRepeat,ModifierIndex,] 'Left Control'
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
