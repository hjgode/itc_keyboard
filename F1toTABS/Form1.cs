using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace F1toTABS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            fillList();
        }

        void fillList()
        {
            comboBox1.Items.Clear();
            ITC_KEYBOARD.CUsbHwKeys _hwKeys = new ITC_KEYBOARD.CUsbHwKeys();
            foreach(ITC_KEYBOARD.vkPair p in _hwKeys.vkPairs){
                comboBox1.Items.Add(p);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            KeytoTABSTABS mapKeys = new KeytoTABSTABS();
            int i = comboBox1.SelectedIndex;
            if (i == -1)
                return;
            ITC_KEYBOARD.vkPair _vkPair = (ITC_KEYBOARD.vkPair)comboBox1.SelectedItem;
            mapKeys.mapKeytoTABTABS(_vkPair.iVal);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ITC_KEYBOARD.CUSBkeys cusb = new ITC_KEYBOARD.CUSBkeys();
            if (cusb.resetKeyDefaults() != 0)
                MessageBox.Show("You must reboot befor the changes become active");
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}