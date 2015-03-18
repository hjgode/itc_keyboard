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
    public partial class usbkeysEventForm : Form
    {
        CUSBkeys _cusb;
        CUsbHwKeys _cusbhwkeys;
        vkPair[] vkPairs;

        CkeybNamedEvents myNamedEvents;
        public usbkeysEventForm()
        {
            InitializeComponent();

            _cusb = new CUSBkeys();
            _cusbhwkeys = new CUsbHwKeys();
            myNamedEvents = new CkeybNamedEvents(false);
            vkPairs = _cusbhwkeys.vkPairs; //get the HW keynames of the current device

            //fill the combo lists
                cboKeys.Items.Clear();
                for (int i = 0; i < vkPairs.Length; i++)
                {
                    cboKeys.Items.Insert(i, vkPairs[i]);
                }
                cboKeys.SelectedIndex = 0;
            //string sPlatform = ITC_KEYBOARD.ITC_Tools.getPlatformModel();
            //if (sPlatform == "CN3")
            //{
            //    //the planes
            //    cboKeys.Items.Clear();
            //    for (int i = 0; i < cn3Keys.vkPairs.Length; i++)
            //    {
            //        cboKeys.Items.Insert(i, cn3Keys.vkPairs[i]);
            //    }
            //    cboKeys.SelectedIndex = 0;
            //}
            //else if (sPlatform == "CN50")
            //{
            //    cboKeys.Items.Clear();
            //    for (int i = 0; i < cn50Keys.vkPairs.Length; i++)
            //    {
            //        cboKeys.Items.Insert(i, cn50Keys.vkPairs[i]);
            //    }
            //    cboKeys.SelectedIndex = 0;
            //}
            //else if (sPlatform == "CK3")
            //{
            //    cboKeys.Items.Clear();
            //    for (int i = 0; i < ck3Keys.vkPairs.Length; i++)
            //    {
            //        cboKeys.Items.Insert(i, ck3Keys.vkPairs[i]);
            //    }
            //    cboKeys.SelectedIndex = 0;
            //}

            //the keys
            int iCountPlanes = _cusb.getNumPlanes();
            cboPlanes.Items.Clear();
            for (int i = 0; i < iCountPlanes; i++)
                cboPlanes.Items.Insert(i, "ShiftPlane" + i.ToString());
            cboPlanes.SelectedIndex = 0;

            //the state events
            updateEventNamesList();
        }
        //private string getPlatformModel()
        //{
        //    string sTemp = "";
        //    try
        //    {
        //        Microsoft.Win32.RegistryKey tempKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"\Platform");
        //        sTemp = (string)tempKey.GetValue("Model", "");
        //    }
        //    catch (Exception)
        //    {

        //    }
        //    return sTemp;
        //}

        private void updateEventNamesList(){
            //reread tables
            if (myNamedEvents != null)
            {
                myNamedEvents = null;
                myNamedEvents = new CkeybNamedEvents(false); ;
            }
            cboEvents.Items.Clear();
            string[] sStateEventNames = myNamedEvents.getStateEventNames();
            SortedList<string, string> theList=new SortedList<string,string>();
            for (int i = 0; i < sStateEventNames.Length; i++)
            {
                theList.Add(sStateEventNames[i], sStateEventNames[i]);
            }
            for (int i = 0; i < sStateEventNames.Length; i++)
            {
                cboEvents.Items.Insert(i, theList.Values[i]);
            }
            cboEvents.SelectedIndex = 0;
        }
        private void menuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSetKey_Click(object sender, EventArgs e)
        {
            int iPlane = cboPlanes.SelectedIndex;
            int iIndex = cboKeys.SelectedIndex;
            int iScanCode = vkPairs[iIndex].iVal;
            //myKeytables.readKeyTable(iPlane);
            CUSBkeys.usbKeyStruct theKeyBytes = new CUSBkeys.usbKeyStruct();
            int iRes = _cusb.getKeyStruct(iPlane, iScanCode, ref theKeyBytes);
            if (iRes < 0)
            {
                txtKeysHex.Text = "unknown key code";
                return;
            }
            //now change the codes to let the key point to a named event
            //Bit 1: 00,00,02: Named Event Index (Sets/Resets a Named Event Level based
            //                    on Key State and Sets a Named Event at every Key State
            //                    Change)
            //Bit 9: 00,02,00: Non Repeating (Key does not Auto Repeat)
            theKeyBytes.bFlagHigh = 0x00;
            theKeyBytes.bFlagMid = (CUsbKeyTypes.usbFlagsMid)0x02;
            theKeyBytes.bFlagLow = (CUsbKeyTypes.usbFlagsLow)0x02;
            //set the index into the named events table
            string sTemp=cboEvents.Items[cboEvents.SelectedIndex].ToString();
            sTemp=sTemp.Substring(sTemp.Length-1,1);
            int idx = Convert.ToInt16(sTemp);
            theKeyBytes.bIntScan = (byte)idx;
            if (MessageBox.Show("Are you sure to set the key to a named event", "Change Keytable", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
            {
                return;
            }
            _cusb.setKey(iPlane, iScanCode, theKeyBytes);
            //myKeytables.saveKeyTable(iPlane);
            _cusb.writeKeyTables();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            int iPlane = cboPlanes.SelectedIndex;
            int iIndex = cboKeys.SelectedIndex;

            //change code to be platform aware!
            int iScanCode = vkPairs[iIndex].iVal;

            //myKeytables.readKeyTable(iPlane);
            CUSBkeys.usbKeyStruct theKeyBytes = new CUSBkeys.usbKeyStruct();
            int iRes = _cusb.getKeyStruct(iPlane, iScanCode, ref theKeyBytes);
            if (iRes < 0)
            {
                txtKeysHex.Text = "unknown key code";
                return;
            }
            string sTemp = "0x" + theKeyBytes.bHID.ToString("x");
            sTemp += ",0x" + theKeyBytes.bScanKey.ToString("x");
            sTemp += ",0x" + theKeyBytes.bFlagHigh.ToString("x");
            sTemp += ",0x" + theKeyBytes.bFlagMid.ToString("x");
            sTemp += ",0x" + theKeyBytes.bFlagLow.ToString("x");
            sTemp += ",0x" + theKeyBytes.bIntScan.ToString("x");
            txtKeysHex.Text = sTemp;
        }

        private void mnuResetDefault_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to reset the keytables?", "USB Keys", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;
            _cusb.resetKeyDefaults();
        }

        private void mnuEventNames_Click(object sender, EventArgs e)
        {
            ManageEventNames dlg = new ManageEventNames(false);
            dlg.ShowDialog();
            updateEventNamesList();
        }

        private void mnuDumpKeys_Click(object sender, EventArgs e)
        {
            DumpForm df = new DumpForm();
            df.ShowDialog();
            df.Dispose();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {

        }
    }
}