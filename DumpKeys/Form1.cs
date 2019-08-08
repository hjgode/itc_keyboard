using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ITC_KEYBOARD;

namespace DumpKeys
{
    public partial class Form1 : Form
    {
        ITC_KEYBOARD.CUSBkeys cusb;
        public Form1()
        {
            InitializeComponent();
            cusb = new ITC_KEYBOARD.CUSBkeys();
            readDump();
        }

        void readDump()
        {
            StringBuilder sb= cusb.dumpAllKeys();
            textBox1.Text = sb.ToString();
            
        }
        private void menuItem3_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                using (System.IO.TextWriter sw = new System.IO.StreamWriter(dlg.FileName) )
                {
                    sw.WriteLine(textBox1.Text);
                    sw.Flush();                    
                }
            }
        }

        private void menuItem4_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure", "Restore default keyboard", MessageBoxButtons.OKCancel, MessageBoxIcon.Question,MessageBoxDefaultButton.Button1)==DialogResult.OK){
                cusb.resetKeyDefaults();                
            }
        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            readDump();
        }
    }
}