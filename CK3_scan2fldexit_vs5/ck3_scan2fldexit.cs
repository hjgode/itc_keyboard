using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ITC_KEYBOARD;

namespace CK3_scan2fldexit_vs5
{
    public partial class ck3_scan2fldexit : Form
    {
        CUSBkeys cusb;
        public ck3_scan2fldexit()
        {
            InitializeComponent();
            cusb = new CUSBkeys();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //find scan button
            CUSBkeys.usbKeyStruct usbKey = new CUSBkeys.usbKeyStruct();
            int iRes = cusb.getKeyStruct(0, CUsbKeyTypes.HWkeys.SCAN_Button_KeyLang1, ref usbKey);
            if (iRes != -1)
            {
                //map to FldExit
                usbKey.bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
                usbKey.bFlagMid = CUsbKeyTypes.usbFlagsMid.VKEY; //FieldExit produces a vk_key with value 0x95
                usbKey.bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
                usbKey.bIntScan = 0x95;
                iRes = cusb.setKey(0, CUsbKeyTypes.HWkeys.SCAN_Button_KeyLang1, usbKey);
                if (iRes == 0)
                    MessageBox.Show("Scan key should now be same as FldExit key");
                iRes = cusb.writeKeyTables();
                if (iRes == 0)
                    MessageBox.Show("Changes saved.");
                else if(iRes==-1)
                    MessageBox.Show("Changes saved. You have to warmboot the device to apply change.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            DumpForm df = new DumpForm();
            df.ShowDialog();
        }
    }
}