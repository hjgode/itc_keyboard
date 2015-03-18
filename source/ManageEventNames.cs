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
    public partial class ManageEventNames : Form
    {
        //cn3usbkeys myKeytables;
        CkeybNamedEvents myNamedEvents;
        private bool _bDirectKey = false;
        public ManageEventNames(bool bDirectKey)
        {
            //myKeytables = new cn3usbkeys();
            this._bDirectKey = bDirectKey;
            myNamedEvents = new CkeybNamedEvents(this._bDirectKey);
            InitializeComponent();
            updateLists();
        }
        private void updateLists()
        {
            if (myNamedEvents != null)
            {
                myNamedEvents = null;
                myNamedEvents = new CkeybNamedEvents(this._bDirectKey);
            }
            //the state events
            lbEvents.Items.Clear();
            CkeybNamedEvents.eventPair[] sEventList = myNamedEvents.getStateEvents();

            SortedList<string, string> theList = new SortedList<string, string>();
            for (int i = 0; i < sEventList.Length; i++)
            {
                theList.Add(sEventList[i].eventName, sEventList[i].eventName);
            }
            for (int i = 0; i < sEventList.Length; i++)
            {
                lbEvents.Items.Insert(i, theList.Values[i]);
            }

            //for (int i = 0; i < sEventList.Length; i++)
            //{
            //    int iPos = i + 1;
            //    lbEvents.Items.Insert(i, sEventList[i].eventName);
            //}
            lbEvents.SelectedIndex = 0;
            //the delta events
        }
        private void mnuBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtDeltaEventName.Text.Length == 0 || txtStateEventName.Text.Length == 0){
                MessageBox.Show("You must at least fill box name boxes");
                return;
            }
            if (txtDeltaEventName.Text== txtStateEventName.Text){
                MessageBox.Show("The event names should be different");
                return;
            }
            if (MessageBox.Show("Do you really want to add '" + txtStateEventName.Text + "' and '" + txtDeltaEventName.Text + "'?", "Add event names", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            if (myNamedEvents.addNamedEvent(txtStateEventName.Text, txtDeltaEventName.Text) != 0)
                MessageBox.Show("There was an error adding the new events names");
            else
            {
                MessageBox.Show("New event names have been add");
                updateLists();
            }
        }

        private void lbEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            CkeybNamedEvents.eventPair[] sEvents = myNamedEvents.getStateEvents();
            string sCurrentEventName = (string)lbEvents.Items[lbEvents.SelectedIndex];

            txtState.Text = myNamedEvents.getStateEvent(sCurrentEventName);

            txtDelta.Text = myNamedEvents.getDeltaEvent(sCurrentEventName);
            
            if (lbEvents.SelectedIndex == lbEvents.Items.Count - 1)
                btnRemove.Enabled = true;
            else
                btnRemove.Enabled = false;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to remove the events '" + (string)lbEvents.Items[lbEvents.SelectedIndex]+"'?", "Event Tables", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;
            myNamedEvents.removeNamedEvent((string)lbEvents.Items[lbEvents.SelectedIndex]);
            updateLists();
        }

        private void ManageEventNames_Load(object sender, EventArgs e)
        {

        }
    }
}