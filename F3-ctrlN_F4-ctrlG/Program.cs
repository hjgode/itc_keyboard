using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

using ITC_KEYBOARD;

namespace F4_ctrlN_F5_ctrlG
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// F4-key to act as if I had entered ctrl+n and the F5-key as if I had entered ctrl+g.
        /// </summary>
        [MTAThread]
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
                mapKeys();
            }
            Application.Run(new F4_ctrlN_F5_ctrlG_Form());
        }
        static void mapKeys()
        {
            remapF4F5 mapKeys = new remapF4F5();
            mapKeys.mapKeys();

        }
    }
    class remapF4F5
    {
        ITC_KEYBOARD.CUSBkeys _cusbKeys;
        CUsbKeyTypes.HWkeys[] keysToMap = new CUsbKeyTypes.HWkeys[] { CUsbKeyTypes.HWkeys.Left_Control };

        public remapF4F5()
        {
            _cusbKeys = new CUSBkeys();
        }

        /// <summary>
        /// map the function keys:
        /// F4 to Ctrl+n
        /// F5 to Ctrl+g
        /// </summary>
        public void mapKeys()
        {
            keysToMap = new CUsbKeyTypes.HWkeys[2];
            keysToMap[0] = CUsbKeyTypes.HWkeys.F4_cozumel;
            keysToMap[1] = CUsbKeyTypes.HWkeys.F5_cozumel;
            
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
            //F1 key:   = 07 3A 00 00 00 05
            // ########## as multi4 key:        07,3a,00,02,04,04

            //prepare a new multikey entry
            CUSBkeys.usbKeyStructShort[] uKey = new CUSBkeys.usbKeyStructShort[2];  // we need Ctrl+n

            //map to Ctrl 
            //find Ctrl modifier index
            ITC_KEYBOARD.CModifiersKeys _cModis = new CModifiersKeys();
            int iModiCtrlIndex = findCtrlModifier();

            //create a key struct that points to the ctrl key modifier entry
            uKey[0].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey[0].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoFlag;
            uKey[0].bFlagLow = CUsbKeyTypes.usbFlagsLow.ModifierIndex;
            uKey[0].bIntScan = (byte)iModiCtrlIndex;  //point to modifier index

            //map to n
            uKey[1].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey[1].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoFlag;
            uKey[1].bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
            uKey[1].bIntScan = (byte)ITC_KEYBOARD.PS2KEYS.N;  //point to N key

            //try to find existing MultiKey
            CMultiKeys cmulti = new CMultiKeys();
            int iMax = cmulti.getMultiKeyCount();
            int iFoundCtrlN = cmulti.findMultiKey(uKey);
            if (iFoundCtrlN == -1)//if there was no existing entry, create a new one
            {
                iFoundCtrlN = cmulti.addMultiKey(uKey);

            }

            //create another multikey entry for Ctrl+G
            //create a key struct that points to the ctrl key modifier entry
            uKey[0].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey[0].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoFlag;
            uKey[0].bFlagLow = CUsbKeyTypes.usbFlagsLow.ModifierIndex;
            uKey[0].bIntScan = (byte)iModiCtrlIndex;  //point to modifier index
            //map to g
            uKey[1].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey[1].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoFlag;
            uKey[1].bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
            uKey[1].bIntScan = (byte)ITC_KEYBOARD.PS2KEYS.G;  //point to G key
            //is this already known?
            int iFoundCtrlG = cmulti.findMultiKey(uKey);
            if (iFoundCtrlG == -1)//if there was no existing entry, create a new one
            {
                iFoundCtrlG = cmulti.addMultiKey(uKey);
            }

            //now map the key(s) in question to the new/existing MultiKeyEntry in all planes
            //CUSBkeys.usbKeyStruct remapKey = new CUSBkeys.usbKeyStruct();

            //############### map 2 key #####################
            //map F1 key in plane x to new multikey
#if DEBUG
            keysToMap[0] = CUsbKeyTypes.HWkeys.g;
            keysToMap[1] = CUsbKeyTypes.HWkeys.h;
            setKeyToMultiKey((int)keysToMap[0], (byte)(iFoundCtrlN), cPlanes.plane.green);
            setKeyToMultiKey((int)keysToMap[1], (byte)(iFoundCtrlG), cPlanes.plane.green);
#else
            setKeyToMultiKey((int)keysToMap[0], (byte)(iFoundCtrlN), cPlanes.plane.normal);
            setKeyToMultiKey((int)keysToMap[1], (byte)(iFoundCtrlG), cPlanes.plane.normal);
#endif
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