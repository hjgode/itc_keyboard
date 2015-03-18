using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ITC_KEYBOARD;

namespace MapScan2Multikey
{
    public partial class MapScan2Multi : Form
    {
        CUSBkeys _cusbKeys;
        public MapScan2Multi()
        {
            InitializeComponent();
            _cusbKeys = new CUSBkeys();
        }

        void mapScanButton2Multikey()
        {
            CUsbKeyTypes.HWkeys[] keysToMap = new CUsbKeyTypes.HWkeys[] { CUsbKeyTypes.HWkeys.SCAN_Button_KeyLang1 };

            //get Ctrl Modifier Key
            int iScanLeftIdx = findScanLeftEvent();

            //read the multikeys
            //string[] multikeys = _cusbKeys.getMultiKeys();

            //Multi3    = 00 00 08 02;                     00 08 00 14
            //          = NoFlag,NoFlag,ModIdx, 0x02;      NoFlag,VKey,NormalKey,0x14 (VK_CAPITAL)
            //Mod2      = 00 02 08 58
            //          = NoFlag,NoRepeat,ModIdx,0x58

            //new
            //########### Multi4    = 00 08 00 11                       00 00 00 15
            //          = NoFlag,VKey,NormalKey,VK_CONTROL  NoFlag,NoFlag,NormalKey,Q-Key
            //          = 00 00 00 15

            //F1 key:   = 07 3A 00 00 00 05
            // ########## as multi4 key:        07,3a,00,02,04,04

            //prepare a new multikey entry
            CUSBkeys.usbKeyStructShort[] uKey = new CUSBkeys.usbKeyStructShort[2];

            //map to nomal key (ie F13) 
            uKey[0].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey[0].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoRepeat;
            uKey[0].bFlagLow = CUsbKeyTypes.usbFlagsLow.NamedEventIndex;
            uKey[0].bIntScan = (byte)iScanLeftIdx;  //points to named scan event index

            //map to 'scroll lock'
            uKey[1].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey[1].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoFlag;
            uKey[1].bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;                      
            uKey[1].bIntScan = (byte)0x7E; //Cusbps2key("Scroll Lock",

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

            //############### map scan button key #####################
            //map scan button to new multikey
            setKeyToMultiKey((int)keysToMap[0], (byte)iFound, cPlanes.plane.normal);

            _cusbKeys.writeKeyTables();

            _cusbKeys = new CUSBkeys(); //re-init
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
        private int findScanLeftEvent()
        {
            //  08,02,01,14 'Left Control'
            CUSBkeys.usbKeyStructShort uKey = CUSBkeys.controlModKey;// new CUSBkeys.usbKeyStructShort();
            int iFound = -1;
            CkeybNamedEvents cNamedEvents = new CkeybNamedEvents(false);
            iFound = cNamedEvents.findEventIndex("StateLeftScan");

            if (iFound == -1)
                iFound = cNamedEvents.addNamedEvent("StateLeftScan", "DeltaLeftScan");

            return iFound;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mapScanButton2Multikey();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int iRes = _cusbKeys.resetKeyDefaults();
            if (iRes != 0)
                MessageBox.Show("Change needs reboot");
        }

        private void mnuDump_Click(object sender, EventArgs e)
        {
            ITC_KEYBOARD.DumpForm df = new DumpForm();
            df.ShowDialog();
        }


    }
}