using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

using ITC_KEYBOARD;

namespace MapF1toF4toCtrl_w
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
            remapF1bisF4toW_B_Up_Down mapKeys = new remapF1bisF4toW_B_Up_Down();
            mapKeys.mapKeys();

        }
    }
    class remapF1bisF4toW_B_Up_Down
    {
        ITC_KEYBOARD.CUSBkeys _cusbKeys;
        CUsbKeyTypes.HWkeys[] keysToMap;// = new CUsbKeyTypes.HWkeys[] { CUsbKeyTypes.HWkeys.Left_Control };

        public remapF1bisF4toW_B_Up_Down()
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
            keysToMap = new CUsbKeyTypes.HWkeys[4];
            keysToMap[0] = CUsbKeyTypes.HWkeys.F1_cozumel;
            keysToMap[1] = CUsbKeyTypes.HWkeys.F2_cozumel;
            keysToMap[2] = CUsbKeyTypes.HWkeys.F3_cozumel;
            keysToMap[3] = CUsbKeyTypes.HWkeys.F4_cozumel;

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
            CUSBkeys.usbKeyStructShort[] uKey = new CUSBkeys.usbKeyStructShort[2];  // we need Ctrl+w

            //map to Ctrl 
            //find Ctrl modifier index
            ITC_KEYBOARD.CModifiersKeys _cModis = new CModifiersKeys();
            int iModiCtrlIndex = findCtrlModifier();

            //create a multikey entry for Ctrl+W
            //map first entry to control modifier index
            uKey[0].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey[0].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoFlag;
            uKey[0].bFlagLow = CUsbKeyTypes.usbFlagsLow.ModifierIndex;
            uKey[0].bIntScan = (byte)iModiCtrlIndex;  //point to modifier index

            //map second entry to w
            uKey[1].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey[1].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoFlag;
            uKey[1].bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
            uKey[1].bIntScan = (byte)ITC_KEYBOARD.PS2KEYS.W;  //point to N key

            //try to find existing MultiKey
            CMultiKeys cmulti = new CMultiKeys();
            int iMax = cmulti.getMultiKeyCount();
            int iFoundCtrlW = cmulti.findMultiKey(uKey);
            if (iFoundCtrlW == -1)//if there was no existing entry, create a new one
            {
                //not found, then add new entry
                iFoundCtrlW = cmulti.addMultiKey(uKey);

            }
            //map the F1 key to the new found multikey index
            setKeyToMultiKey((int)keysToMap[0], (byte)(iFoundCtrlW), cPlanes.plane.normal);

            //################################# NEXT KEY #############################################
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
            uKey[1].bIntScan = (byte)ITC_KEYBOARD.PS2KEYS.B;  //point to B key
            //is this already known?
            int iFoundCtrlG = cmulti.findMultiKey(uKey);
            if (iFoundCtrlG == -1)//if there was no existing entry, create a new one
            {
                iFoundCtrlG = cmulti.addMultiKey(uKey);
            }
            //now map the key(s) in question to the new/existing MultiKeyEntry in all? planes
            setKeyToMultiKey((int)keysToMap[1], (byte)(iFoundCtrlG), cPlanes.plane.normal);

            //################################# NEXT KEY #############################################
            //create another multikey entry for Ctrl+ArrowUp
            //create a key struct that points to the ctrl key modifier entry
            uKey[0].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey[0].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoFlag;
            uKey[0].bFlagLow = CUsbKeyTypes.usbFlagsLow.ModifierIndex;
            uKey[0].bIntScan = (byte)iModiCtrlIndex;  //point to modifier index
            //map to g
            uKey[1].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey[1].bFlagMid = CUsbKeyTypes.usbFlagsMid.VKEY;
            uKey[1].bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
            uKey[1].bIntScan = (byte)ITC_KEYBOARD.VKEY.VK_UP;// PS2KEYS.Up_Arrow;  //point to up arrow key
            //is this already known?
            int iFoundCtrlUp = cmulti.findMultiKey(uKey);
            if (iFoundCtrlUp == -1)//if there was no existing entry, create a new one
            {
                iFoundCtrlUp = cmulti.addMultiKey(uKey);
            }
            //now map the key(s) in question to the new/existing MultiKeyEntry in all? planes
            setKeyToMultiKey((int)keysToMap[2], (byte)(iFoundCtrlUp), cPlanes.plane.normal);

            //################################# NEXT KEY #############################################
            //create another multikey entry for Ctrl+ArrowDown
            //create a key struct that points to the ctrl key modifier entry
            uKey[0].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey[0].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoFlag;
            uKey[0].bFlagLow = CUsbKeyTypes.usbFlagsLow.ModifierIndex;
            uKey[0].bIntScan = (byte)iModiCtrlIndex;  //point to modifier index
            //map to g
            uKey[1].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey[1].bFlagMid = CUsbKeyTypes.usbFlagsMid.VKEY;
            uKey[1].bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
            uKey[1].bIntScan = (byte)ITC_KEYBOARD.VKEY.VK_DOWN;// PS2KEYS.Up_Arrow;  //point to up arrow key
            //is this already known?
            int iFoundCtrlDown = cmulti.findMultiKey(uKey);
            if (iFoundCtrlDown == -1)//if there was no existing entry, create a new one
            {
                iFoundCtrlDown = cmulti.addMultiKey(uKey);
            }
            //now map the key(s) in question to the new/existing MultiKeyEntry in all? planes
            setKeyToMultiKey((int)keysToMap[3], (byte)(iFoundCtrlDown), cPlanes.plane.normal);

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