using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;


namespace ITC_KEYBOARD
{
    /// <summary>
    /// helper class to manage platform specific functions
    /// </summary>
    public static class ITC_Tools
    {
        //CN3kbdRemap.cpl
        [DllImport("CN3kbdRemap.cpl", EntryPoint = "ITC_ResetKeyDefaults", CharSet = CharSet.Unicode)]
        private static extern int ITC_ResetKeyDefaultsCN3();
        //CK3kbdRemap.cpl
        [DllImport("CK3kbdRemap.cpl", EntryPoint = "ITC_ResetKeyDefaults", CharSet = CharSet.Unicode)]
        private static extern int ITC_ResetKeyDefaultsCK3();
        //KbdRemapCN4.cpl
        [DllImport("KbdRemapCN4.cpl", EntryPoint = "ITC_ResetKeyDefaults", CharSet = CharSet.Unicode)]
        private static extern int ITC_ResetKeyDefaultsCN4();
        //KbdRemapCN50.cpl
        [DllImport("KbdRemapCN50.cpl", EntryPoint = "ITC_ResetKeyDefaults", CharSet = CharSet.Unicode)]
        private static extern int ITC_ResetKeyDefaultsCN50();

        /// <summary>
        /// reset key tables of device to factory default
        /// </summary>
        public static void resetKeyDefaults()
        {
            try
            {
                if (System.IO.File.Exists(@"\Windows\CK3kbdRemap.cpl"))
                    ITC_ResetKeyDefaultsCK3();
                if (System.IO.File.Exists(@"\Windows\CN3kbdRemap.cpl"))
                    ITC_ResetKeyDefaultsCN3();
                if (System.IO.File.Exists(@"\Windows\KbdRemapCN4.cpl"))
                    ITC_ResetKeyDefaultsCN4();
                if (System.IO.File.Exists(@"\Windows\KbdRemapCN50.cpl"))
                    ITC_ResetKeyDefaultsCN50();

            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Did you install/copy the keymap applet?");
            }
        }

        /// <summary>
        /// reads value of HKLM\\Platform
        /// </summary>
        /// <returns>normally 3 letter code with with device model number</returns>
        public static string getPlatformModel()
        {
            string sTemp = "";
            try
            {
                Microsoft.Win32.RegistryKey tempKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"\Platform");
                sTemp = (string)tempKey.GetValue("Model", "");
            }
            catch (Exception)
            {

            }
            return sTemp;
        }
        /// <summary>
        /// read the full model code of device
        /// </summary>
        /// <returns>a model code like CK3B36211058091</returns>
        public static string getModelNumber()
        {
            /*
            [HKEY_LOCAL_MACHINE\Ident]
            "Desc"="Intermec CK3 Device"
            "Name"="CK3A36010858282"
            "OrigName"="IntermecCK3"
             * */
            string sTemp = "";
            try
            {
                Microsoft.Win32.RegistryKey tempKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"\Ident");
                sTemp = (string)tempKey.GetValue("Name", "");
            }
            catch (Exception)
            {

            }
            return sTemp;
        }
    }
}
