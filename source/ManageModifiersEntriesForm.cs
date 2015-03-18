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
    public partial class ManageModifiersEntriesForm : Form
    {
        ITC_KEYBOARD.CModifiersKeys _modifiersKeys;
        public ManageModifiersEntriesForm()
        {
            InitializeComponent();
            _modifiersKeys = new ITC_KEYBOARD.CModifiersKeys();
            fillForm();
        }
        
        private void fillForm()
        {
            listbox1.Items.Clear();
            int iRot = _modifiersKeys.getModifierKeyCount();
            int j=0;
            for (int i = 0; i < iRot; i++)
            {
                j=i+1;
                listbox1.Items.Insert(i, "ModKey" + j.ToString());
            }
            listbox1.SelectedIndex = 0;
        }

        private void mnuBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listbox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDump.Text = _modifiersKeys.dumpModifierKey(listbox1.SelectedIndex + 1);
        }
    }
}