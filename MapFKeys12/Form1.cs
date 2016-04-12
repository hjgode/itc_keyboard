using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MapFKeys12
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnMapFKeys_Click(object sender, EventArgs e)
        {
            mapfkeys.mapKeys();
        }

        private void btn_Defaults_Click(object sender, EventArgs e)
        {
            mapfkeys.restoreDefaults();
        }
    }
}