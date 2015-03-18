using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

using ITC_KEYBOARD;

namespace F5_F8_to_CtrlK_CtrlB
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (args[0].Contains("default"))
                {
                    ITC_KEYBOARD.CUSBkeys cusb = new ITC_KEYBOARD.CUSBkeys();
                    if (cusb.resetKeyDefaults() != 0)
                        MessageBox.Show("You must reboot befor the changes become active");
                    else
                        MessageBox.Show("Keyboard reset to default");
                    Application.Exit();
                }
                if (args[0].Contains("noautorun"))
                {
                    Application.Run(new Form1(false));                     
                }
            }
            else
                Application.Run(new Form1(true));
        }

    }
}