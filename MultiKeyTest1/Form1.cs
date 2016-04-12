using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ITC_KEYBOARD;

namespace MultiKeyTest1
{
    public partial class Form1 : Form
    {
        ITC_KEYBOARD.CUSBkeys _cusbKeys;

        public Form1()
        {
            InitializeComponent();
            _cusbKeys = new CUSBkeys();
        }
        void initKeys()
        {

        }

        CUSBkeys.usbKeyStructShort getUSBkey(ITC_KEYBOARD.PS2KEYS ps2)
        {
            CUSBkeys.usbKeyStructShort uKey = new CUSBkeys.usbKeyStructShort();
            uKey.bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey.bFlagMid = CUsbKeyTypes.usbFlagsMid.NoChord;
            uKey.bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
            uKey.bIntScan = (byte)ps2;
            return uKey;
        }
        CUSBkeys.usbKeyStructShort getUSBkeyShifted(ITC_KEYBOARD.PS2KEYS ps2)
        {
            CUSBkeys.usbKeyStructShort uKey = new CUSBkeys.usbKeyStructShort();
            uKey.bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey.bFlagMid = CUsbKeyTypes.usbFlagsMid.NoChord | CUsbKeyTypes.usbFlagsMid.Shifted;
            uKey.bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
            uKey.bIntScan = (byte)ps2;
            return uKey;
        }
        CUSBkeys.usbKeyStructShort getUSBvkey(ITC_KEYBOARD.VKEY vKey)
        {
            CUSBkeys.usbKeyStructShort uKey = new CUSBkeys.usbKeyStructShort();
            uKey.bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey.bFlagMid = CUsbKeyTypes.usbFlagsMid.NoChord | CUsbKeyTypes.usbFlagsMid.VKEY;
            uKey.bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
            uKey.bIntScan = (byte)vKey;
            return uKey;
        }
        CUSBkeys.usbKeyStructShort getUSBvkeyShifted(ITC_KEYBOARD.VKEY vKey)
        {
            CUSBkeys.usbKeyStructShort uKey = new CUSBkeys.usbKeyStructShort();
            uKey.bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey.bFlagMid = CUsbKeyTypes.usbFlagsMid.NoChord | CUsbKeyTypes.usbFlagsMid.Shifted | CUsbKeyTypes.usbFlagsMid.VKEY;
            uKey.bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
            uKey.bIntScan = (byte)vKey;
            return uKey;
        }

        /// <summary>
        /// create multikey entries with Letters
        /// 
        /// </summary>
        public void createMultiKey(byte bKeyToMap, int iPlaneToUse)
        {
            //Create Multikey with "Hello Test"
            string sTest = "Hello Test";
            //prepare a new multikey entry
            CUSBkeys.usbKeyStructShort[] uKey = new CUSBkeys.usbKeyStructShort[sTest.Length];

            //create sequence
            //using PS2 keys
#if USE_PS2KEYS
            uKey[0] = getUSBkeyShifted(ITC_KEYBOARD.PS2KEYS.H);  //point to modifier index, should be 0x03
            uKey[1] = getUSBkey(PS2KEYS.E);
            uKey[2] = getUSBkey(PS2KEYS.L);
            uKey[3] = getUSBkey(PS2KEYS.L);
            uKey[4] = getUSBkey(PS2KEYS.O);
            uKey[5] = getUSBkey(PS2KEYS.Space);
            uKey[6] = getUSBkeyShifted(PS2KEYS.T);
            uKey[7] = getUSBkey(PS2KEYS.E);
            uKey[8] = getUSBkey(PS2KEYS.S);
            uKey[9] = getUSBkey(PS2KEYS.T);
#else
            //using VKEY
            uKey[0] = getUSBvkeyShifted(VKEY.VK_H);  //point to modifier index, should be 0x03
            uKey[1] = getUSBvkey(VKEY.VK_E);
            uKey[2] = getUSBvkey(VKEY.VK_L);
            uKey[3] = getUSBvkey(VKEY.VK_L);
            uKey[4] = getUSBvkey(VKEY.VK_O);
            uKey[5] = getUSBvkey(VKEY.VK_SPACE);
            uKey[6] = getUSBvkeyShifted(VKEY.VK_T);
            uKey[7] = getUSBvkey(VKEY.VK_E);
            uKey[8] = getUSBvkey(VKEY.VK_S);
            uKey[9] = getUSBvkey(VKEY.VK_T);
#endif
            //try to find existing MultiKey
            CMultiKeys cmulti = new CMultiKeys();
            int iMax = cmulti.getMultiKeyCount();
            int iFoundMultikeyEntry = cmulti.findMultiKey(uKey);
            if (iFoundMultikeyEntry == -1)//if there was no existing entry, create a new one
            {
                //not found, then add new entry
                iFoundMultikeyEntry = cmulti.addMultiKey(uKey);

            }
            //map the key to the new found multikey index
            setKeyToMultiKey((int)bKeyToMap, (byte)(iFoundMultikeyEntry), (ITC_KEYBOARD.cPlanes.plane)iPlaneToUse);

            //save and update
            int iResWrite = _cusbKeys.writeKeyTables();
            if (iResWrite == -1)
            {
                MessageBox.Show("Change needs a reboot");
            }
            else if (iResWrite == -2)
            {
                MessageBox.Show("Internal Error. Contact author.");
            }
            else
                MessageBox.Show("Mapped key " + bKeyToMap.ToString() + " in plane " + (ITC_KEYBOARD.cPlanes.plane)iPlaneToUse + " to ALT+F4");
        }

        /// <summary>
        /// map a key to a multikey index
        /// </summary>
        /// <param name="iKey">the key to map</param>
        /// <param name="iMultiIndex">zero based index to Multi1, Multi2...</param>
        /// <param name="cPlane">keyboard plane</param>
        /* setKeyToMultiKey: 07,04,00,02,04,2A 'a' [NoFlag,NoRepeat,MultiKeyIndex,] 'v' 
         * 'MultiIndex'->
         * 00,60,00,33 'h' | 
         * 00,40,00,24 'e' | 
         * 00,40,00,4B 'l' | 
         * 00,40,00,4B 'l' | 
         * 00,00,00,00 'Overrun Error' | 
         * 00,40,00,29 'Space' | 
         * 00,60,00,2C 't' | 
         * 00,40,00,24 'e' | 
         * 00,40,00,1B 's' | 
         * 00,40,00,2C 't' |
        */

        private void setKeyToMultiKey(int iKey, byte iMultiIndex, cPlanes.plane cPlane)
        {
            CUSBkeys.usbKeyStruct remapKey = new CUSBkeys.usbKeyStruct();

            if (_cusbKeys.getKeyStruct((int)cPlane, iKey, ref remapKey) != -1)
            {   //key exists
                remapKey.bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
                remapKey.bFlagMid = CUsbKeyTypes.usbFlagsMid.NoRepeat;
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
                remapKey.bIntScan = (byte)(iMultiIndex + 1);
                _cusbKeys.addKey(cPlane, remapKey);
            }
            System.Diagnostics.Debug.WriteLine("setKeyToMultiKey: " + _cusbKeys.dumpKey(remapKey));
        }

        private void btnDoMap_Click(object sender, EventArgs e)
        {
            createMultiKey((byte)ITC_KEYBOARD.HardwareKeys.CK70Keys.ITC_Standard_Escape_Key, 0);
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Restore Defaults", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                if (_cusbKeys.resetKeyDefaults() == 0)
                    MessageBox.Show("Done without error. Please reboot");
                else
                    MessageBox.Show("Error. Missing Keyboard CPL?");

        }
    }

}