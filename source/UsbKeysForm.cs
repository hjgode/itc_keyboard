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
    public partial class UsbKeysForm : Form
    {
        CUSBkeys _cusb;
        CUsbHwKeys _cusbhwkeys;
        vkPair[] vkPairs;
        private bool oldITEstatus = false;
        public UsbKeysForm()
        {
            InitializeComponent();
            _cusb = new CUSBkeys();
            fillForm();
        }

        private void refreshData()
        {
            if (_cusb != null)
            {
                _cusb = null;
                if (mnu_UseITEtables.Checked)
                {
                    if (oldITEstatus != true)
                    {
                        _cusb = new CUSBkeys(true);
                        oldITEstatus = true;
                    }
                }
                else
                {
                    if (oldITEstatus != false)
                    {
                        _cusb = new CUSBkeys();
                        oldITEstatus = false;
                    }
                }
            }
            fillForm();
        }
        private void fillForm()
        {
            _cusbhwkeys = new CUsbHwKeys();
            vkPairs = _cusbhwkeys.vkPairs;

            //fill the combo lists
            //the keys
            cboKeys.Items.Clear();
            for (int i = 0; i < vkPairs.Length; i++)
            {
                cboKeys.Items.Insert(i, vkPairs[i]);
            }
            cboKeys.SelectedIndex = 0;

            //the planes
            int iCountPlanes = _cusb.getNumPlanes();
            cboPlanes.Items.Clear();
            for (int i = 0; i < iCountPlanes; i++)
                cboPlanes.Items.Insert(i, "ShiftPlane" + i.ToString());
            cboPlanes.SelectedIndex = 0;

        }

        private void mnuBack_Click(object sender, EventArgs e)
        {
            _cusb = null;
            this.Close();
        }

        private void mnuResetDefault_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to reset the keytables?", "USB Keys", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;
            Cursor.Current = Cursors.WaitCursor;
            _cusb.resetKeyDefaults();
            Cursor.Current = Cursors.Default;
        }

        private void mnuDumpKeys_Click(object sender, EventArgs e)
        {
            DumpForm df = new DumpForm();
            df.ShowDialog();
            df.Dispose();
        }

        private void cboKeys_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iPlane = cboPlanes.SelectedIndex;
            int iKey = cboKeys.SelectedIndex;
            if(iPlane == -1 || iKey == -1)
                return;
            vkPair vk = (vkPair)cboKeys.SelectedItem;
            txtKeysHex.Text = _cusb.dumpKey(iPlane, vk.iVal);
        }

        private void cboPlanes_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboKeys_SelectedIndexChanged(sender, e);
        }

        private void btnEventKey_Click(object sender, EventArgs e)
        {
            usbkeysEventForm ev = new usbkeysEventForm();
            ev.ShowDialog();
            ev.Dispose();
            refreshData();
        }

        private void btnRotate_Click(object sender, EventArgs e)
        {
            RotateKeysForm rf = new RotateKeysForm();
            rf.ShowDialog();
            rf.Dispose();
            refreshData();
        }

        private void btnNoop_Click(object sender, EventArgs e)
        {
            int iPlane = cboPlanes.SelectedIndex;
            int iKeyList = cboKeys.SelectedIndex;
            vkPair vk = (vkPair)cboKeys.SelectedItem;
            ITC_KEYBOARD.CUSBkeys.usbKeyStruct newKeyVal=new CUSBkeys.usbKeyStruct();
            //get the key structure filled with the current key values
            if ( _cusb.getKeyStruct(iPlane, vk.iVal, ref newKeyVal) == -1 )
            {
                //the key does not exist, fill the struct manually
                newKeyVal.bHID = 0x07;
                newKeyVal.bScanKey = (CUsbKeyTypes.HWkeys) vk.iVal;
                newKeyVal.bIntScan = (byte) vk.iVal;
            }
            
            //set the flags for NOOP
            newKeyVal.bFlagHigh=CUsbKeyTypes.usbFlagsHigh.NoFlag;
            newKeyVal.bFlagMid=CUsbKeyTypes.usbFlagsMid.NOOP;
            newKeyVal.bFlagLow=CUsbKeyTypes.usbFlagsLow.NormalKey;
            if (_cusb.setKey(iPlane, vk.iVal, newKeyVal) != 0)
            {
                //try to add key
                _cusb.addKey(iPlane, newKeyVal);
            }
            _cusb.writeKeyTables();
            refreshData();
            cboPlanes.SelectedIndex = iPlane;
            cboKeys.SelectedIndex = iKeyList;
        }

        private void btn_ModifierKey_Click(object sender, EventArgs e)
        {
            ModifierKeysForm mf = new ModifierKeysForm();
            mf.ShowDialog();
            mf.Dispose();
        }

        private void mnu_UseITEtables_Click(object sender, EventArgs e)
        {
            mnu_UseITEtables.Checked = !mnu_UseITEtables.Checked;
            if (mnu_UseITEtables.Checked)
            {
                _cusb.useITEtables = true;
                refreshData();
            }
        }
    }
}