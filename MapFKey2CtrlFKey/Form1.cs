//#define ALPHA_NUM

using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ITC_KEYBOARD;

namespace MapFKey2CtrlFKey
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnDoMapping_Click(object sender, EventArgs e)
        {
            mapKeys();
        }

        private void btnRestoreDefaults_Click(object sender, EventArgs e)
        {
            ITC_KEYBOARD.CUSBkeys cusb = new ITC_KEYBOARD.CUSBkeys();
            if (cusb.resetKeyDefaults() != 0)
                MessageBox.Show("You must reboot befor the changes become active");
            else
                MessageBox.Show("Keyboard reset to default");
        }

        static void mapKeys()
        {
            remapFKeysToCtrlFKeys mapKeys = new remapFKeysToCtrlFKeys();
            mapKeys.mapKeys();
            MessageBox.Show("All done. Please test.");
        }

    }
    class remapFKeysToCtrlFKeys
    {
        ITC_KEYBOARD.CUSBkeys _cusbKeys;
        CUsbKeyTypes.HWkeys[] keysToMap;// = new CUsbKeyTypes.HWkeys[] { CUsbKeyTypes.HWkeys.Left_Control };

        public remapFKeysToCtrlFKeys()
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
#if ALPHA_NUM
            keysToMap = new CUsbKeyTypes.HWkeys[] {
                CUsbKeyTypes.HWkeys.d,
                CUsbKeyTypes.HWkeys.e,
                CUsbKeyTypes.HWkeys.f,
                CUsbKeyTypes.HWkeys.g,
                CUsbKeyTypes.HWkeys.h,
                CUsbKeyTypes.HWkeys.i,
                CUsbKeyTypes.HWkeys.k,
                CUsbKeyTypes.HWkeys.l,
                CUsbKeyTypes.HWkeys.m,
                CUsbKeyTypes.HWkeys.p,
                CUsbKeyTypes.HWkeys.q,
                CUsbKeyTypes.HWkeys.r,
            };
#else
            keysToMap = new CUsbKeyTypes.HWkeys[] {
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
                CUsbKeyTypes.HWkeys.F11_cozumel,
                CUsbKeyTypes.HWkeys.F12_cozumel,
            };
#endif
            createMultiKeys();
        }

        /// <summary>
        /// create an usbShortStruct for our multikey
        /// </summary>
        /// <param name="ps2Key">FKey value to generate</param>
        /// <param name="iModiCtrlIndex">the index of the CTRL modifier</param>
        /// <returns></returns>
        CUSBkeys.usbKeyStructShort[] makeMultikeyForFKey(ITC_KEYBOARD.PS2KEYS ps2Key, int iModiCtrlIndex)
        {
            CUSBkeys.usbKeyStructShort[] uKeyShort = new CUSBkeys.usbKeyStructShort[2];

            //create a key struct that points to the ctrl key modifier entry
            uKeyShort[0].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKeyShort[0].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoFlag;
            uKeyShort[0].bFlagLow = CUsbKeyTypes.usbFlagsLow.ModifierIndex;
            uKeyShort[0].bIntScan = (byte)iModiCtrlIndex;  //point to modifier index of CTRL key

            //map to ps2Key
            uKeyShort[1].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKeyShort[1].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoFlag;
            uKeyShort[1].bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
            uKeyShort[1].bIntScan = (byte)ps2Key;  //point to PS/2 key

            return uKeyShort;
        }

        /// <summary>
        /// return the index of a multikey entry matching ps2Key and using the Ctrl index
        /// </summary>
        /// <param name="ps2Key">the ps2Key value to look for</param>
        /// <param name="iModiCtrlIndex">the index of the Ctrl modifier</param>
        /// <returns>the index of the added or existing multikey entry</returns>
        int getMultiKeyIndexForFKey(ITC_KEYBOARD.PS2KEYS ps2Key, int iModiCtrlIndex)
        {
            //get the structures for the multikey entry with PS/2 key
            CUSBkeys.usbKeyStructShort[] uKey = makeMultikeyForFKey(ps2Key, iModiCtrlIndex);

            //try to find existing MultiKey
            CMultiKeys cmulti = new CMultiKeys();
            int iMax = cmulti.getMultiKeyCount();
            int iFoundCtrlN = cmulti.findMultiKey(uKey);

            if (iFoundCtrlN == -1)//if there was no existing entry, create a new one
            {
                iFoundCtrlN = cmulti.addMultiKey(uKey);
            }
            return iFoundCtrlN;
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
            
            //create a list of PS2 keys for F1 to Fxx
            ITC_KEYBOARD.PS2KEYS[] ps2KeysToMap =new PS2KEYS[] { 
                PS2KEYS.F1,
                PS2KEYS.F2,
                PS2KEYS.F3,
                PS2KEYS.F4,
                PS2KEYS.F5,
                PS2KEYS.F6,
                PS2KEYS.F7,
                PS2KEYS.F8,
                PS2KEYS.F9,
                PS2KEYS.F10,
                PS2KEYS.F11,
                PS2KEYS.F12,
            };
            
            int[] iMapArray = new int[keysToMap.Length];

            //map all keys found in keysToMap array
            for (int i = 0; i < keysToMap.Length; i++)
            {
                iMapArray[i] = getMultiKeyIndexForFKey(ps2KeysToMap[i], iModiCtrlIndex);
#if ALPHA_NUM
                setKeyToMultiKey((int)keysToMap[i], (byte)iMapArray[i], cPlanes.plane.green);
#else
                setKeyToMultiKey((int)keysToMap[i], (byte)iMapArray[i], cPlanes.plane.normal);
#endif
            }

            //save and update
            _cusbKeys.writeKeyTables();
        }

        /// <summary>
        /// map a key to a multikey index
        /// </summary>
        /// <param name="iKey">the key to map</param>
        /// <param name="iMultiIndex">zero based index to Multi1, Multi2...</param>
        /// <param name="cPlane">keyboard plane</param>
        private int setKeyToMultiKey(int iKey, byte iMultiIndex, cPlanes.plane cPlane)
        {
            CUSBkeys.usbKeyStruct remapKey = new CUSBkeys.usbKeyStruct();

            int iResult = 0;
            if (_cusbKeys.getKeyStruct((int)cPlane, iKey, ref remapKey) != -1)
            {   //key exists
                remapKey.bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
                remapKey.bFlagMid = CUsbKeyTypes.usbFlagsMid.NoRepeat | CUsbKeyTypes.usbFlagsMid.NoChord;
                remapKey.bFlagLow = CUsbKeyTypes.usbFlagsLow.MultiKeyIndex;
                remapKey.bIntScan = (byte)(iMultiIndex + 1); //the idx is zero based! but it is Multi1, Multi2...
                iResult += _cusbKeys.setKey((int)cPlane, iKey, remapKey);
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
                iResult += _cusbKeys.setKey((int)cPlane, iKey, remapKey);
            }
            System.Diagnostics.Debug.WriteLine("setKeyToMultiKey: " + _cusbKeys.dumpKey(remapKey));
            return iResult;
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