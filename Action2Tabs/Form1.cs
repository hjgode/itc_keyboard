using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Action2Tabs;

namespace Action2Tabs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ActionToTABSTABS mapKeys = new ActionToTABSTABS();
            if(mapKeys.mapActionToTABTABS()==0)
                textBox1.Text+="ActionKey remapped. Changes applied\r\n";
            else
                textBox1.Text += "ActionKey remapped. Please reboot!\r\n";
            textBox1.Text += mapKeys.dumpKey() + "\r\n";
            textBox1.Text+= mapKeys.dumpMultiKey()+"\r\n";
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            ITC_KEYBOARD.CUSBkeys cusb = new ITC_KEYBOARD.CUSBkeys();
            if (cusb.resetKeyDefaults() != 0)
            {
                textBox1.Text+="Default loaded. Please reboot!\r\n";
                MessageBox.Show("You must reboot befor the changes become active\r\n");
            }
            else
            {
                textBox1.Text += "Default loaded. Changes applied.";
            }
            
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            
            Application.Exit();
        }
    }
}