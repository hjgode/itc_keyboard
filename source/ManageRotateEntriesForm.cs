using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CUsbKeysCStest
{
    public partial class ManageRotateEntriesForm : Form
    {
        ITC_KEYBOARD.CRotateKeys _rotateKeys;
        public ManageRotateEntriesForm()
        {
            InitializeComponent();
            _rotateKeys = new ITC_KEYBOARD.CRotateKeys();
            fillForm();
        }
        
        private void fillForm()
        {
            listboxRotates.Items.Clear();
            int iRot = _rotateKeys.getRotateKeyCount();
            int j=0;
            for (int i = 0; i < iRot; i++)
            {
                j=i+1;
                listboxRotates.Items.Insert(i, "RotateKey" + j.ToString());
            }
            listboxRotates.SelectedIndex = 0;
        }

        private void mnuBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listboxRotates_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDump.Text = _rotateKeys.dumpRotateKey(listboxRotates.SelectedIndex + 1);
            txtRotateChars.Text = _rotateKeys.dumpRotateChars(listboxRotates.SelectedIndex + 1);
            txtRotateShiftedChars.Text = _rotateKeys.dumpRotateShiftedChars(listboxRotates.SelectedIndex + 1);
        }
    }
}