using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CUsbKeysCStest
{
    public partial class DumpForm : Form
    {
        public DumpForm()
        {
            InitializeComponent();
            ITC_KEYBOARD.CUSBkeys _cusbKeys = new ITC_KEYBOARD.CUSBkeys();
            string sReg = ITC_KEYBOARD.CUSBkeys.getRegLocation();
            textBox1.Text = sReg + "\r\n" + _cusbKeys.dumpAllKeys().ToString();
        }

        private void mnuBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sd = new SaveFileDialog();
            if (sd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(sd.FileName))
                    {
                        // Add some text to the file.
                        sw.WriteLine(textBox1.Text);
                        sw.Flush();
                    }
                    MessageBox.Show("Dump saved to '" + sd.FileName + "'");
                }
                catch (System.IO.IOException iox)
                {
                    MessageBox.Show("Exception in write file: " + iox.Message);
                }
            }
        }
    }
}