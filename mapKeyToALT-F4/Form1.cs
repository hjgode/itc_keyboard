using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ITC_KEYBOARD;

namespace mapKeyToALT_F4
{
    public partial class Form1 : Form
    {
        List<string> doNotUse = new List<string> { "Action", "Green Shift Button", "Front Scan Button", "Orange Shift Button", "Aqua Shift Button", "Power Button" };
        ITC_KEYBOARD.CUSBkeys _cusbKeys;

        public Form1()
        {
            InitializeComponent();

            initKeys();

            int iModiALTIndex = findALTModifier();

        }

        void initKeys()
        {
            _cusbKeys = new ITC_KEYBOARD.CUSBkeys();
            cboKeyList.Items.Clear();
            vkPair[] ck3KeyList = ITC_KEYBOARD.CUsbHwKeys.ck3Keys.vkPairs;
            foreach (vkPair vP in ck3KeyList)
            {
                if(!doNotUse.Contains(vP.sKey))
                    cboKeyList.Items.Add(vP);
            }
            cboKeyList.SelectedIndex = 0;

            cboPlanes.Items.Clear();
            int iNumPlanes = _cusbKeys.getNumPlanes();
            for (int i = 0; i < iNumPlanes; i++)
                cboPlanes.Items.Add((ITC_KEYBOARD.cPlanes.plane)i);
            cboPlanes.SelectedIndex = 0;
        }

        CUsbKeyTypes.HWkeys keysToMap = CUsbKeyTypes.HWkeys.Error_Undefined;// { CUsbKeyTypes.HWkeys.Left_Control };
        CUSBkeys.usbKeyStruct keyToMapStruct = new CUSBkeys.usbKeyStruct();

        /// <summary>
        /// create multikey entries with Ctrl + Letters
        /// Control is 08,02,01,14
        /// </summary>
        public void createMultiKeyALT_F4(byte bKeyToMap, int iPlaneToUse)
        {

            //prepare a new multikey entry
            CUSBkeys.usbKeyStructShort[] uKey = new CUSBkeys.usbKeyStructShort[2];  // we need two entries

            //map to ALT + F4
            //find ALT modifier index
            ITC_KEYBOARD.CModifiersKeys _cModis = new CModifiersKeys();
            int iModiALTIndex = findALTModifier();

            //create a multikey entry for Alt
            //map first entry to control modifier index
            uKey[0].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey[0].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoFlag;
            uKey[0].bFlagLow = CUsbKeyTypes.usbFlagsLow.ModifierIndex;
            uKey[0].bIntScan = (byte)iModiALTIndex;  //point to modifier index, should be 0x03

            //map second entry to F4
            uKey[1].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey[1].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoFlag;
            uKey[1].bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
            uKey[1].bIntScan = (byte)ITC_KEYBOARD.PS2KEYS.F4;  //point to F4 key

            //try to find existing MultiKey
            CMultiKeys cmulti = new CMultiKeys();
            int iMax = cmulti.getMultiKeyCount();
            int iFoundAltF4 = cmulti.findMultiKey(uKey);
            if (iFoundAltF4 == -1)//if there was no existing entry, create a new one
            {
                //not found, then add new entry
                iFoundAltF4 = cmulti.addMultiKey(uKey);

            }
            //map the key to the new found multikey index
            setKeyToMultiKey((int)bKeyToMap, (byte)(iFoundAltF4), (ITC_KEYBOARD.cPlanes.plane)iPlaneToUse);

            //save and update
            int iResWrite=_cusbKeys.writeKeyTables();
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
        /// find the Modifiers Entry for the "Control Key"
        /// creates a new Modifier Entry if no entry is found
        /// </summary>
        /// <returns>-1 if not found
        /// pos value is index of found/created entry</returns>
        private int findALTModifier()
        {
            //  08,02,01,11 'Left Alt'
            //assemble the Alt modifier entry: 01,02,08,11[StickyOnce,NoRepeat,ModifierIndex,] 'Left ALT'
            CUSBkeys.usbKeyStructShort uKey = CUSBkeys.ModKeyLeftAlt;// controlModKey;// new CUSBkeys.usbKeyStructShort();
            int iFound = -1;
            CModifiersKeys cModis = new CModifiersKeys();
            iFound = cModis.findModifierKey(uKey);

            if (iFound == -1)
                iFound = cModis.addModifierKey(uKey);

            System.Diagnostics.Debug.WriteLine("findALTModifier(): " + iFound.ToString());
            return iFound;
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
                remapKey.bFlagMid = CUsbKeyTypes.usbFlagsMid.NoRepeat;
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
                remapKey.bIntScan = (byte)(iMultiIndex+1);
                _cusbKeys.addKey(cPlane, remapKey);
            }
            System.Diagnostics.Debug.WriteLine("setKeyToMultiKey: " + _cusbKeys.dumpKey(remapKey));
        }

        private void cboKeyList_SelectedIndexChanged(object sender, EventArgs e)
        {
            keysToMap = CUsbKeyTypes.HWkeys.Error_Undefined;
            vkPair vP=(vkPair) (cboKeyList.SelectedItem);
            CUSBkeys.usbKeyStruct uKey= new CUSBkeys.usbKeyStruct();
            int iPlane=0;
            int iRes = _cusbKeys.findHWKey((CUsbKeyTypes.HWkeys) vP.iVal, ref iPlane, ref uKey);
            if (iRes >= 0)
            {
                label1.Text = "found key: " + ((ITC_KEYBOARD.cPlanes.plane)iPlane).ToString() + ":" + uKey.bScanKey.ToString() + "/" + uKey.bIntScan.ToString();
                cboPlanes.SelectedIndex = iPlane;
                keysToMap = (CUsbKeyTypes.HWkeys)vP.iVal;
                keyToMapStruct = uKey;
            }
            else
            {
                label1.Text = "key not found";
                keysToMap = CUsbKeyTypes.HWkeys.Error_Undefined;
                keyToMapStruct.bScanKey = keysToMap;
            }
        }

        private void btnMapKey_Click(object sender, EventArgs e)
        {
            if (keysToMap == CUsbKeyTypes.HWkeys.Error_Undefined)
            {
                MessageBox.Show("No Key selected");
                return;
            }
            if(cboKeyList.SelectedIndex==-1)
            {
                MessageBox.Show("No Plane selected");
                return;
            }

            createMultiKeyALT_F4((byte)keysToMap, cboPlanes.SelectedIndex);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Restore Defaults", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                if (_cusbKeys.resetKeyDefaults() == 0)
                    MessageBox.Show("Done without error. Please reboot");
                else
                    MessageBox.Show("Error. Missing Keyboard CPL?");
        }

    }
}