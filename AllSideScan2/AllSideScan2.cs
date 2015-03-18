using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ITC_KEYBOARD;

namespace AllSideScan2
{
    public partial class AllSideScan2 : Form
    {
        ITC_KEYBOARD.CUSBkeys _cusbKeys = new CUSBkeys();

        public AllSideScan2()
        {
            InitializeComponent();

            updateDump();
        }
        private void updateDump()
        {
            textBox1.Text = "";
            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();
            _cusbKeys = new CUSBkeys(); //reread keytables
            StringBuilder sb = _cusbKeys.dumpAllKeys();
            textBox1.Text = sb.ToString();
            //            System.Diagnostics.Debug.WriteLine(sDump); //text will be cut by debug!
            Cursor.Current = Cursors.Default;
            Application.DoEvents();
        }
        private void mapAllSide2NOOP()
        {
            //init the class
//            ITC_KEYBOARD.CUSBkeys _cusbKeys = new ITC_KEYBOARD.CUSBkeys();

            //struct to hold key definition
            CUSBkeys.usbKeyStruct usbKey = new CUSBkeys.usbKeyStruct();

            //NORMAL Plane = 0x00
            //orange plane = 0x01
            //green/aqua plane = 0x02
            int iCount = _cusbKeys.getNumPlanes();
            for (int iPlane = 0; iPlane < iCount; iPlane++) //do for all planes
            {
                //remap F6 to NOOP
                //new use: _cusbKeys.getKeyStruct(iPlane, HardwareKeys.CK70Keys.ITC_Standard_UpperRight_Btn, ref usbKey);
                _cusbKeys.getKeyStruct(iPlane, ITC_KEYBOARD.CUsbKeyTypes.HWkeys.F6_VOL_UP, ref usbKey);
                usbKey.bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
                usbKey.bFlagMid = CUsbKeyTypes.usbFlagsMid.NOOP;
                usbKey.bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
                _cusbKeys.setKey(iPlane, CUsbKeyTypes.HWkeys.F6_VOL_UP, usbKey);

                // F7
                _cusbKeys.getKeyStruct(iPlane, ITC_KEYBOARD.CUsbKeyTypes.HWkeys.F7_VOL_DN, ref usbKey);
                usbKey.bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
                usbKey.bFlagMid = CUsbKeyTypes.usbFlagsMid.NOOP;
                usbKey.bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
                _cusbKeys.setKey(iPlane, CUsbKeyTypes.HWkeys.F7_VOL_DN, usbKey);

                //Side Scan button: dec145, 0x91
                _cusbKeys.getKeyStruct(iPlane, 0x91, ref usbKey);
                usbKey.bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
                usbKey.bFlagMid = CUsbKeyTypes.usbFlagsMid.NOOP;
                usbKey.bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
                _cusbKeys.setKey(iPlane, 0x91, usbKey);

                //APP key: dec67, 0x43
                _cusbKeys.getKeyStruct(iPlane, 0x43, ref usbKey);
                usbKey.bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
                usbKey.bFlagMid = CUsbKeyTypes.usbFlagsMid.NOOP;
                usbKey.bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
                _cusbKeys.setKey(iPlane, 0x43, usbKey);

            }
            _cusbKeys.writeKeyTables();
        }

        private void mapAllSide2SCAN()
        {
            //init the class
            //ITC_KEYBOARD.CUSBkeys _cusbKeys = new ITC_KEYBOARD.CUSBkeys();

            //struct to hold key definition
            CUSBkeys.usbKeyStruct usbKey = new CUSBkeys.usbKeyStruct();

            //NORMAL Plane = 0x00
            //orange plane = 0x01
            //green/aqua plane = 0x02
            int iCount = _cusbKeys.getNumPlanes();

            //struct to hold key definition
            CUSBkeys.usbKeyStruct usbScanKey = new CUSBkeys.usbKeyStruct();
            //get main scan button 
            //int iIndex = _cusbKeys.getKeyIndex(0, (int)ITC_KEYBOARD.CUsbKeyTypes.HWkeys.SCAN_Button_KeyLang1 /*0x90*/);
            _cusbKeys.getKeyStruct(0, CUsbKeyTypes.HWkeys.SCAN_Button_KeyLang1, ref usbScanKey);


            for (int iPlane = 0; iPlane < iCount; iPlane++) //do for all planes
            {
                //remap F6 to NOOP
                _cusbKeys.getKeyStruct(iPlane, ITC_KEYBOARD.CUsbKeyTypes.HWkeys.F6_VOL_UP, ref usbKey);
                usbKey.bFlagHigh = usbScanKey.bFlagHigh;
                usbKey.bFlagMid = usbScanKey.bFlagMid;
                usbKey.bFlagLow = usbScanKey.bFlagLow;
                _cusbKeys.setKey(iPlane, CUsbKeyTypes.HWkeys.F6_VOL_UP, usbKey);

                // F7
                _cusbKeys.getKeyStruct(iPlane, ITC_KEYBOARD.CUsbKeyTypes.HWkeys.F7_VOL_DN, ref usbKey);
                usbKey.bFlagHigh = usbScanKey.bFlagHigh;
                usbKey.bFlagMid = usbScanKey.bFlagMid;
                usbKey.bFlagLow = usbScanKey.bFlagLow;
                _cusbKeys.setKey(iPlane, CUsbKeyTypes.HWkeys.F7_VOL_DN, usbKey);

                //Side Scan button: dec145, 0x91
                _cusbKeys.getKeyStruct(iPlane, 0x91, ref usbKey);
                usbKey.bFlagHigh = usbScanKey.bFlagHigh;
                usbKey.bFlagMid = usbScanKey.bFlagMid;
                usbKey.bFlagLow = usbScanKey.bFlagLow;
                _cusbKeys.setKey(iPlane, 0x91, usbKey);

                //APP key: dec67, 0x43
                _cusbKeys.getKeyStruct(iPlane, 0x43, ref usbKey);
                usbKey.bFlagHigh = usbScanKey.bFlagHigh;
                usbKey.bFlagMid = usbScanKey.bFlagMid;
                usbKey.bFlagLow = usbScanKey.bFlagLow;
                _cusbKeys.setKey(iPlane, 0x43, usbKey);

            }
            _cusbKeys.writeKeyTables();
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuAllNoop_Click(object sender, EventArgs e)
        {
            mapAllSide2NOOP();
            updateDump();
        }

        private void mnuAllScan_Click(object sender, EventArgs e)
        {
            mapAllSide2SCAN();
            updateDump();
        }

        private void mnuRestDefaults_Click(object sender, EventArgs e)
        {
            //init the class
            //ITC_KEYBOARD.CUSBkeys _cusbKeys = new ITC_KEYBOARD.CUSBkeys();
            _cusbKeys.resetKeyDefaults();
            updateDump();
        }
    }
}