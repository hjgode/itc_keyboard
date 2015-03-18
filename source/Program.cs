using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CUsbKeysCStest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            //global USBKey Class
            //ITC_KEYBOARD.CUSBkeys _gCusb = new ITC_KEYBOARD.CUSBkeys();
            Application.Run(new mainForm());
        }
    }
}