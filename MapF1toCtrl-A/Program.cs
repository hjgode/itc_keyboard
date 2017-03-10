using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

using ITC_KEYBOARD;

namespace MapF1toCtrl_A
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
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
            //Application.Run(new Form1());
        }
        static void mapKeys()
        {
            remapF1toCtrlA mapKeys = new remapF1toCtrlA();
            mapKeys.mapKeys();

        }
    }
    class remapF1toCtrlA
    {
        ITC_KEYBOARD.CUSBkeys _cusbKeys;
        CUsbKeyTypes.HWkeys[] keysToMap;// = new CUsbKeyTypes.HWkeys[] { CUsbKeyTypes.HWkeys.Left_Control };
        public remapF1toCtrlA()
        {
            _cusbKeys = new CUSBkeys();
        }

        /// <summary>
        /*
        F1 = ctrl a 
        F2 = ctrl w 
        F3 = ctrl t 
        F4 = ctrl e 
        F5= ctrl s 
        */
        /// </summary>
        public void mapKeys()
        {
            keysToMap = new CUsbKeyTypes.HWkeys[5];
            keysToMap[0] = CUsbKeyTypes.HWkeys.F1_cozumel; //not the normal F1!
            keysToMap[1] = CUsbKeyTypes.HWkeys.F2_cozumel; //not the normal F1!
            keysToMap[2] = CUsbKeyTypes.HWkeys.F3_cozumel; //not the normal F1!
            keysToMap[3] = CUsbKeyTypes.HWkeys.F4_cozumel; //not the normal F1!
            keysToMap[4] = CUsbKeyTypes.HWkeys.F5_cozumel; //not the normal F1!

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
            CUSBkeys.usbKeyStructShort[] uKey = new CUSBkeys.usbKeyStructShort[2];  // we need Ctrl+a

            //map to Ctrl 
            //find Ctrl modifier index
            ITC_KEYBOARD.CModifiersKeys _cModis = new CModifiersKeys();
            int iModiCtrlIndex = findCtrlModifier();

            //create a multikey entry for Ctrl
            //map first entry to control modifier index
            uKey[0].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey[0].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoFlag;
            uKey[0].bFlagLow = CUsbKeyTypes.usbFlagsLow.ModifierIndex;
            uKey[0].bIntScan = (byte)iModiCtrlIndex;  //point to modifier index

            #region F1 to Ctrl-A
            //map second entry to a
            uKey[1].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey[1].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoFlag;
            uKey[1].bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
            uKey[1].bIntScan = (byte)ITC_KEYBOARD.PS2KEYS.A;  //point to N key

            //try to find existing MultiKey
            CMultiKeys cmulti = new CMultiKeys();
            int iMax = cmulti.getMultiKeyCount();
            int iFoundCtrlA = cmulti.findMultiKey(uKey);
            if (iFoundCtrlA == -1)//if there was no existing entry, create a new one
            {
                //not found, then add new entry
                iFoundCtrlA = cmulti.addMultiKey(uKey);
            }
            //map the key to the new found multikey index
            setKeyToMultiKey((int)keysToMap[0], (byte)(iFoundCtrlA), cPlanes.plane.normal);
            #endregion

            #region F2 to Ctrl-W
            //######### map second entry to w
            uKey[1].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey[1].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoFlag;
            uKey[1].bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
            uKey[1].bIntScan = (byte)ITC_KEYBOARD.PS2KEYS.W;  //point to key
            //try to find existing MultiKey
            cmulti = new CMultiKeys();
            iMax = cmulti.getMultiKeyCount();
            int iFoundCtrlW = cmulti.findMultiKey(uKey);
            if (iFoundCtrlW == -1)//if there was no existing entry, create a new one
            {
                //not found, then add new entry
                iFoundCtrlW = cmulti.addMultiKey(uKey);
            }
            //map the key to the new found multikey index
            setKeyToMultiKey((int)keysToMap[1], (byte)(iFoundCtrlW), cPlanes.plane.normal);
            #endregion

            #region F3 to Ctrl-T
            //########## map second entry to t
            uKey[1].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey[1].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoFlag;
            uKey[1].bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
            uKey[1].bIntScan = (byte)ITC_KEYBOARD.PS2KEYS.T;  //point to key
            //try to find existing MultiKey
            cmulti = new CMultiKeys();
            iMax = cmulti.getMultiKeyCount();
            int iFoundCtrlT = cmulti.findMultiKey(uKey);
            if (iFoundCtrlT == -1)//if there was no existing entry, create a new one
            {
                //not found, then add new entry
                iFoundCtrlT = cmulti.addMultiKey(uKey);
            }
            //map the key to the new found multikey index
            setKeyToMultiKey((int)keysToMap[2], (byte)(iFoundCtrlT), cPlanes.plane.normal);
            #endregion

            #region F4 to Ctrl-E
            //########## map second entry to e
            uKey[1].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey[1].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoFlag;
            uKey[1].bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
            uKey[1].bIntScan = (byte)ITC_KEYBOARD.PS2KEYS.E;  //point to key
            //try to find existing MultiKey
            cmulti = new CMultiKeys();
            iMax = cmulti.getMultiKeyCount();
            int iFoundCtrlE = cmulti.findMultiKey(uKey);
            if (iFoundCtrlE == -1)//if there was no existing entry, create a new one
            {
                //not found, then add new entry
                iFoundCtrlE = cmulti.addMultiKey(uKey);
            }
            //map the key to the new found multikey index
            setKeyToMultiKey((int)keysToMap[3], (byte)(iFoundCtrlE), cPlanes.plane.normal);
            #endregion

            #region F5 to Ctrl-S
            //########## map second entry to s
            uKey[1].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey[1].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoFlag;
            uKey[1].bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
            uKey[1].bIntScan = (byte)ITC_KEYBOARD.PS2KEYS.S;  //point to key
            //try to find existing MultiKey
            cmulti = new CMultiKeys();
            iMax = cmulti.getMultiKeyCount();
            int iFoundCtrlS = cmulti.findMultiKey(uKey);
            if (iFoundCtrlS == -1)//if there was no existing entry, create a new one
            {
                //not found, then add new entry
                iFoundCtrlS = cmulti.addMultiKey(uKey);
            }
            //map the key to the new found multikey index
            setKeyToMultiKey((int)keysToMap[4], (byte)(iFoundCtrlS), cPlanes.plane.normal);
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