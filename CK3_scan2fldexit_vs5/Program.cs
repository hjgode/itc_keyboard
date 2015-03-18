using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CK3_scan2fldexit_vs5
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            Application.Run(new ck3_scan2fldexit());
        }
    }
}