using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

using ITC_KEYBOARD;

namespace MapF5toCtrl_L
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
                }
            }
            else
            {
                Application.Run(new Form1());
//                mapKeys();
            }
        }
        static void mapKeys()
        {
            remapF5toCtrlL mapKeys = new remapF5toCtrlL();
            mapKeys.mapKeys();

        }
    }
}