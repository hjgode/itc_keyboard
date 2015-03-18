using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Orange2Ctrl
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main(String[] args)
        {
            bool bAutoClose = true;
            bool bStickyCtrl = false;
            if (args.Length > 0)
            {
                if(args.Contains<String>("noautoclose"))
                    bAutoClose = ! testOption("noautoclose", ref args);
                if (args.Contains<String>("sticky"))
                    bStickyCtrl = testOption("sticky", ref args);

                Application.Run(new Orange2Ctrl(bAutoClose, bStickyCtrl));
            }
            else
            {
                Application.Run(new Orange2Ctrl(bAutoClose, bStickyCtrl));
            }
        }

        static bool testOption(String opt, ref String[] args)
        {
            bool bRet = false;
            if (args.Contains<String>(opt))
                bRet = true;
            return bRet;
        }
    }
}