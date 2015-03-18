using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ITC_KEYBOARD;

namespace CUsbKeysCStest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            readReg();
        }
        private void readReg(){
            ITC_KEYBOARD.CUSBkeys _cusbKeys= new ITC_KEYBOARD.CUSBkeys();
            //_cusbKeys.readKeyTable(0);
            CUSBkeys.usbKeyStruct _usbKeyStruct = new CUSBkeys.usbKeyStruct();

            _cusbKeys.getKeyStruct(0, 0x8b, ref _usbKeyStruct);
            if (_usbKeyStruct.bScanKey == 0x00)
            {
                //("No green button defined");

                //add new Key
                /*
                 * 0C,E9,00,02,01,01 ; Shift Plane 1
                 * 07,8B,00,02,01,02 ; Shift Plane 2
                 * "ShiftKey1" = hex:24,02,01,01 ; Shift Plane 1 Gold Plane (Lockable)
                 * "ShiftKey2" = hex:14,02,01,02 ; Shift Plane 2 Alpha Rotate Plane (Lockable)
                ;    07,8B,00,10,00,00,\
                ;    0C,E9,00,10,00,00,\
                ; 07,8B is green
                ; 0c,E9 is orange
                ; 0c,B6 is aqua
                ; 07,90 is front scan button
                */
                _usbKeyStruct = new CUSBkeys.usbKeyStruct();

                //new aqua key
                _usbKeyStruct.bHID = 0x07;
                _usbKeyStruct.bScanKey = CUsbKeyTypes.HWkeys.Aqua_Plane;// (CUsbKeyTypes.HWkeys)0x8b;
                _usbKeyStruct.bFlagHigh = (CUsbKeyTypes.usbFlagsHigh) 0x01; //shift key index
                _usbKeyStruct.bFlagMid = (CUsbKeyTypes.usbFlagsMid) 0x10; //10=ignore key
                _usbKeyStruct.bFlagLow = (CUsbKeyTypes.usbFlagsLow) 0x04; //04 = sticky manual lock
                _usbKeyStruct.bIntScan = 0x01;

                _cusbKeys.addKey(0, _usbKeyStruct);
                //_cusbKeys.saveKeyTable(0);

                //modify the sticky flag
            }
            else
            {
                //change the existing key
                _usbKeyStruct.bHID = 0x07;
                _usbKeyStruct.bScanKey = CUsbKeyTypes.HWkeys.Aqua_Plane; //0x8b;
                _usbKeyStruct.bFlagHigh = (CUsbKeyTypes.usbFlagsHigh)0x01; //shift key index
                _usbKeyStruct.bFlagMid = (CUsbKeyTypes.usbFlagsMid)0x10; //10=ignore key
                _usbKeyStruct.bFlagLow = (CUsbKeyTypes.usbFlagsLow)0x04; //04 = sticky manual lock
                _usbKeyStruct.bIntScan = 0x02;

                textBox1.Text =  _cusbKeys.dumpKey(_usbKeyStruct);
                _cusbKeys.setKey(0, 0x8b, _usbKeyStruct);
                
                //_cusbKeys.saveKeyTable(0);
            }
            _cusbKeys.writeKeyTables();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ITC_KEYBOARD.CUSBkeys _cusbKeys = new ITC_KEYBOARD.CUSBkeys();
            _cusbKeys.resetKeyDefaults();
        }

        private void mnuDumpKeys_Click(object sender, EventArgs e)
        {
            DumpForm df = new DumpForm();
            df.ShowDialog();
            df.Dispose();
            //ITC_KEYBOARD.CUSBkeys _cusbKeys = new ITC_KEYBOARD.CUSBkeys();
            //int iMax=_cusbKeys.getNumPlanes();
            //textBox1.Text="";
            //for (int i = 0; i < iMax; i++)
            //{
            //    textBox1.Text += "\r\n================================\r\n";
            //    textBox1.Text += "ShiftPlane" + i.ToString()+"\r\n================================\r\n";
            //    _cusbKeys.readKeyTable(i);
            //    textBox1.Text += _cusbKeys.dumpAllKeys();
            //}
        }
    }
}