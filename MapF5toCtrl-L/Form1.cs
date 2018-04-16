using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MapF5toCtrl_L
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_mapF5_Click(object sender, EventArgs e)
        {
            remapF5toCtrlL mapping = new remapF5toCtrlL();
            mapping.mapKeys();
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            ITC_KEYBOARD.CUSBkeys usbKeys = new ITC_KEYBOARD.CUSBkeys();
            usbKeys.resetKeyDefaults();
        }

        
    }
}