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
    public partial class RotateKeysForm : Form
    {
        CUSBkeys myKeytables;
        CUsbHwKeys _cusbhwkeys;
        CRotateKeys _cRotateKeys;

        vkPair[] _vkPairs;

        public RotateKeysForm()
        {
            InitializeComponent();
            myKeytables = new CUSBkeys();
            _cusbhwkeys = new CUsbHwKeys();
            _cRotateKeys = new CRotateKeys();

            _vkPairs = _cusbhwkeys.vkPairs;

            string sPlatform =ITC_KEYBOARD.ITC_Tools.getPlatformModel();
            try
            {
                fillLists(sPlatform);
            }
            catch (Exception)
            {
                MessageBox.Show("No RotateKeys currently supported");
                this.Close();
            }
        }

        private void fillLists(string sPlatform)
        {
            //the number of planes
            int iCountPlanes = myKeytables.getNumPlanes();
            cboPlanes.Items.Clear();
            for (int i = 0; i < iCountPlanes; i++)
                cboPlanes.Items.Insert(i, "ShiftPlane" + i.ToString());
            cboPlanes.SelectedIndex = 0;

            //the keys
            //if (sPlatform == "CN3" || sPlatform == "CN4")
            //{
            //    _vkPairs =  cn3Keys.vkPairs;
            //    //the planes
            //}
            //else if (sPlatform == "CN50")
            //{
            //    _vkPairs = cn50Keys.vkPairs;
            //}
            //else if (sPlatform == "CK3")
            //{
            //    _vkPairs = ck3Keys.vkPairs;
            //}
            cboKeys.Items.Clear();
            for (int i = 0; i < _vkPairs.Length; i++)
            {
                cboKeys.Items.Insert(i, _vkPairs[i]);
            }
            cboKeys.SelectedIndex = 0;

            //rotate keys
            cboEvents.Items.Clear();
            //ITC_KEYBOARD.RotateKeys.RotateKeys _rotateKeys = new ITC_KEYBOARD.RotateKeys.RotateKeys();
            int irCount = _cRotateKeys.getRotateKeyCount();
            int irNumber;
            for (int i = 0; i < irCount; i++)
            {
                irNumber = i + 1;
                cboEvents.Items.Insert(i, "RotateKey" + irNumber.ToString());
            }
            if(irCount>0)
                cboEvents.SelectedIndex = 0;
        }
        private void btnGet_Click(object sender, EventArgs e)
        {

        }

        private void btnSetKey_Click(object sender, EventArgs e)
        {

        }

        private void mnuBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGet_Click_1(object sender, EventArgs e)
        {
            int iPlane = cboPlanes.SelectedIndex;
            int iIndex = cboKeys.SelectedIndex;

            vkPair vkp = (vkPair)cboKeys.Items[iIndex];

            int iScanCode = vkp.iVal;
            
            //myKeytables.readKeyTable(iPlane);

            CUSBkeys.usbKeyStruct theKeyBytes = new CUSBkeys.usbKeyStruct();
            int iRes = myKeytables.getKeyStruct(iPlane, iScanCode, ref theKeyBytes);
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

        private void menuItem1_Click(object sender, EventArgs e)
        {
            RotateKeysForm mf = new RotateKeysForm();
            mf.ShowDialog();
            mf.Dispose();
        }

        private void btnSetKey_Click_1(object sender, EventArgs e)
        {
            int iPlane = cboPlanes.SelectedIndex;
            int iIndex = cboKeys.SelectedIndex;

            vkPair vkp = (vkPair)cboKeys.Items[iIndex];

            int iScanCode = vkp.iVal;

            //myKeytables.readKeyTable(iPlane);

            CUSBkeys.usbKeyStruct theKeyBytes = new CUSBkeys.usbKeyStruct();
            int iRes = myKeytables.getKeyStruct(iPlane, iScanCode, ref theKeyBytes);
            if (iRes < 0)
            {
                txtKeysHex.Text = "unknown key code";
                return;
            }

            int iRotate = cboEvents.SelectedIndex + 1;
            theKeyBytes.bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            theKeyBytes.bFlagMid = CUsbKeyTypes.usbFlagsMid.NoRepeat;
            theKeyBytes.bFlagLow = CUsbKeyTypes.usbFlagsLow.RotateKeyIndex;
            theKeyBytes.bIntScan = (byte)iRotate;

            if (myKeytables.setKey(iPlane, iScanCode, theKeyBytes) == 0)
                MessageBox.Show("Key set to Rotate");
            else
                MessageBox.Show("Key setting failed");
        }
    }
}