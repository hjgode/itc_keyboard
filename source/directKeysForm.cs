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
    public partial class directKeysForm : Form
    {
        CDirectKeys _directKeys;
        CkeybNamedEvents _myNamedEvents;
        CDirectHWKeys _directHWkeys;
        public directKeysForm()
        {
            InitializeComponent();
            //test if direct keys are available
            _directKeys = new CDirectKeys();
            if (_directKeys.getKeyCount() == 0)
            {
                MessageBox.Show("No direct key definitions");
                this.Close();
            }
            else
            {
                updateKeyList();
                updateLists();
            }
        }

        private void updateKeyList()
        {
            if (_directKeys != null)
            {
                _directKeys = null;
                _directKeys = new CDirectKeys();
            }
            else
                _directKeys = new CDirectKeys();

            if (_directHWkeys != null)
            {
                _directHWkeys = new CDirectHWKeys();
            }
            else
            {
                _directHWkeys = null;
                _directHWkeys = new CDirectHWKeys();
            }

            int oldIndex = -1;
            if (cboKeys.SelectedIndex > -1)
            {
                oldIndex = cboKeys.SelectedIndex;
            }
            //fill the key list
            cboKeys.Items.Clear();
            int iKcount = _directKeys.getKeyCount();
            for (int i = 0; i < iKcount; i++)
            {
                cboKeys.Items.Insert(i, _directHWkeys.vkPairs[i].sKey);
                //cboKeys.Items.Insert(i, _directKeys.getKeyString(i));
            }
            if (oldIndex != -1)
                cboKeys.SelectedIndex = oldIndex;
            else
                cboKeys.SelectedIndex = 0;
        }
        private void updateLists()
        {
            //reread tables
            if (_myNamedEvents != null)
            {
                _myNamedEvents = null;
                _myNamedEvents = new CkeybNamedEvents(true); ;
            }
            else
                _myNamedEvents = new CkeybNamedEvents(true); ;

            lbEvents.Items.Clear();
            string[] sStateEventNames = _myNamedEvents.getStateEventNames();
            SortedList<string, string> theList = new SortedList<string, string>();
            for (int i = 0; i < sStateEventNames.Length; i++)
            {
                theList.Add(sStateEventNames[i], sStateEventNames[i]);
            }
            for (int i = 0; i < sStateEventNames.Length; i++)
            {
                lbEvents.Items.Insert(i, theList.Values[i] + " ("+
                    _myNamedEvents.getStateEvent(theList.Values[i]) + ")");
            }
            lbEvents.SelectedIndex = 0;

            //fill VKEY List
            lbVKeys.Items.Clear();
            for (int i = 0; i < CvkMap.vkValues.Length; i++)
            {
                lbVKeys.Items.Insert(i, (VKEY)CvkMap.vkValues[i].b);
            }
            lbVKeys.SelectedIndex = 0;

            //fill MultiKeys
            lbMultikeys.Items.Clear();
            CDirectKeys.multiDirectKeyStruct[] _multiKeys = _directKeys.getMultiKeys();
            for (int i = 0; i < _multiKeys.Length; i++ )
            {
                lbMultikeys.Items.Insert(i, _multiKeys[i].sName);
            }
            lbMultikeys.SelectedIndex = 0;

            //fill the AppButton list
            /*
                #define VK_APP1         0xC1 
                #define VK_APP2         0xC2 
                #define VK_APP3         0xC3
                #define VK_APP4         0xC4
                #define VK_APP5         0xC5
                #define VK_APP6         0xC6
            */
            lbAppButtons.Items.Clear();
            for (int i = 0; i < 6; i++)
            {
                lbAppButtons.Items.Insert(i, (CDirectKeys.appButtons)(i + 0xc1));
            }
            lbAppButtons.SelectedIndex = 0;
        }

        private void mnuBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboKeys_SelectedIndexChanged(object sender, EventArgs e)
        {
            CDirectKeys.directKeyStruct _theKey = _directKeys.getKey(cboKeys.SelectedIndex);
            System.Diagnostics.Debug.WriteLine("_theKey: " + _theKey.keyType.ToString() + ", " + _theKey.keyVal.ToString() );
            int iV = (int)_theKey.keyVal;
            if (iV == 0x00)
            {
                txtRawText.Text = "not used";
                lbEvents.Enabled = false;
                lbVKeys.Enabled = false;
                btnSet.Enabled = false;
                btnSetVKey.Enabled = false;
                chkShifted.Enabled = false;
                return;
            }
            else
            {
                lbEvents.Enabled = true;
                lbVKeys.Enabled = true;
                btnSet.Enabled = true;
                btnSetVKey.Enabled = true;
                chkShifted.Enabled = true;
            }
            switch (_theKey.keyType)
            {
                case CDirectKeys.directKeyType.kTypeNamedEvent:
                    txtRawText.Text = "EventKey->" + iV.ToString();
                    lbEvents.SelectedIndex = iV - 1;
                    break;
                case CDirectKeys.directKeyType.kTypeVirtualKey:
                    txtRawText.Text = "VKey->" + iV.ToString();
                    chkShifted.Checked = false;
                    lbVKeys.SelectedIndex = iV;
                    break;
                case CDirectKeys.directKeyType.kTypeShiftdVirtualKey:
                    txtRawText.Text = "ShiftedVKey->" + iV.ToString();
                    chkShifted.Checked = true;
                    lbVKeys.SelectedIndex = iV;
                    break;
                case CDirectKeys.directKeyType.kTypeAppButton:
                    txtRawText.Text = "AppKey->" + iV.ToString();
                    int ix = iV - 0xc1;
                    lbAppButtons.SelectedIndex = ix;
                    break;
                case CDirectKeys.directKeyType.kTypeMultiKey:
                    txtRawText.Text = "MultiKey->" + iV.ToString();
                    break;
                default:
                    txtRawText.Text = "undefined";
                    break;
            }
        }

        private void mnuEventNames_Click(object sender, EventArgs e)
        {
            ManageEventNames dlg = new ManageEventNames(true);
            dlg.ShowDialog();
            updateLists();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            int i = cboKeys.SelectedIndex;
            txtRawText.Text = _directKeys.getKeyString(i);
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            int idxKey = cboKeys.SelectedIndex;
            int idxEvent = lbEvents.SelectedIndex;

            if(idxEvent==-1 || idxKey==-1){
                MessageBox.Show("Please select a valid key and event");
                return;
            }
            if(cboKeys.Items[idxKey].ToString()=="0x0,0x0"){
                MessageBox.Show("Please select a valid key");
                return;
            }

            //events are named from 1 to x in contrast to the listbox items starting at zero
            //idxKey;
            idxEvent++;

            if (MessageBox.Show("Do you really want to change the key at '" + idxKey.ToString() + "' to Event"+ idxEvent.ToString()+"?", "Direct Key Table", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;
            _directKeys.setKey(idxKey, idxEvent, CDirectKeys.directKeyType.kTypeNamedEvent);
            updateKeyList();
        }

        private void lbEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
//            txtStateEventValue.Text = _myNamedEvents.getStateEvent(lbEvents.Items[lbEvents.SelectedIndex].ToString());
//            txtDeltaEventValue.Text = _myNamedEvents.getDeltaEvent(lbEvents.Items[lbEvents.SelectedIndex].ToString());
        }

        private void btnSetVKey_Click(object sender, EventArgs e)
        {
            int iKey = cboKeys.SelectedIndex;
            VKEY _vkey = (VKEY)lbVKeys.SelectedItem;
            
            if (MessageBox.Show("Do you really want to change the key at '" + iKey.ToString() + 
                "' to VKEY"+ _vkey.ToString() + "?", 
                "Direct Key Table", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;
            if(chkShifted.Checked)
                _directKeys.setKey(iKey, _vkey, CDirectKeys.directKeyType.kTypeShiftdVirtualKey);
            else
                _directKeys.setKey(iKey, _vkey, CDirectKeys.directKeyType.kTypeVirtualKey);
            updateKeyList();
        }
    }
}