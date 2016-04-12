using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using EventNamespace;
/*! \namespace ITC_KEYBOARD
    \brief this namespace holds all functions needed to read write USB or Direct key mappings

    Use CUSBKeys class to manage USB key mappings

    Use DirectKeys class to manage Direct key mappings
*/
namespace ITC_KEYBOARD
{
    /// <summary>
    /// The CUSBkeys class enables managing ITC USB keyboard key mappings
    /// </summary>
    /*! \class CUSBkeys
        \brief The CUSBkeys class provides functions to manage USB key mappings

        When you press a key on the intermec device, the driver uses a key mapping table to
        look up for the action assigned to the key. Every key on the keypad has an unique
        USB hardware scancode. This is used to look up the action in the mapping table.
        
A USB key can produce a PS/2 key message, a VK message or is mapped to an event, a multikey
index, a rotate key index, a modifier table index, a function inside a DLL, an APP key value or just nothing. 
The 'type' and behaviour of the key is defined by the USBKeyFlags. There are three bytes defining the action for the pressed key.

        The table entries of the USB key mapping table is stored as binary data in the registry. The CUSBkeys class
        uses an array of usbKeyStruct to manage the entries.
    */
    public partial class CUSBkeys
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
        [DllImport("KbdRemapCS40.cpl", EntryPoint = "ITC_ResetKeyDefaults", CharSet = CharSet.Unicode)]
        private static extern int ITC_ResetKeyDefaultsCS40();
        //new with CN50, Cx7x
        [DllImport("KBDTools.CPL", EntryPoint = "ResetAll", CharSet = CharSet.Unicode)]
        private static extern int ITC_ResetAllCN70();

        private Boolean _bUseITEtables = false;
        public Boolean useITEtables
        {
            get { return _bUseITEtables; }
            set
            {
                if (_bUseITEtables != value)
                {
                    _bUseITEtables = value;
                    readKeyTablesITE();
                }
            }
                    
        }
        /*! \brief used for internal actual keyboard table          
            _usbKeys holds an array of all defined keys of the keyboard mapping
            \warning Do not use any more, is replaced by _usbKeysAll
        */
	    [Obsolete]
        private usbKeyStruct[] _usbKeys=null;
        /// <summary>
        /// a var to hold rotate key definitions
        /// </summary>
        private CRotateKeys rotateKeys = null;
        private CShiftKeys _shiftKeys = null;

	/*! \brief This array holds the mapping of all used keys of all used planes
        */
        private usbKeyStruct[][] _usbKeysAll;
        /// <summary>
        /// used to signal usb keyboard mapping changes
        /// </summary>
        private CkeybNamedEvents _KeybdNamedEvents;
        /// <summary>
        /// var to hold multikey definitions
        /// </summary>
        private CMultiKeys _MultiKeys;

        private CModifiersKeys _ModifiersKeys;

        private CFunctionkeys _functionKeys;

        /// <summary>
        /// unfortunately the CN50 is very different
        /// </summary>
        private bool _isCN50 = false;
        public bool isCN50
        {
            get { return _isCN50; }
        }
        #region NOTE
        /* 
        NOTE:
        in code we count this
        left byte(16-23) = high, 
        mid byte = mid, 
        right byte (0-7) = low
        */
        #endregion

        /*! \brief The usbKeyStruct holds all information about an USB HID hardware key
        
	    The members bHID, bScanKey, bFlagHigh, bFlagMid, bFlagLow and bIntScan build the complete
            description of the key mapping of an USB hardware key identified by bScanKey
	*/
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct usbKeyStruct 
        {
            /*! \brief the USB HID codepage, normally = 7
            */
            public byte bHID;
            /*! \brief the USB HID keypad hardware scan code
		\link ITC_KEYBOARD::CUsbKeyTypes::HWkeys see the enum of known HWkeys \endlink
		\see Index see also HWkeys 
            */
            public CUsbKeyTypes.HWkeys bScanKey;
            /// <summary>
            /// high byte flags for the key definition
            /// </summary>
	    /*! \link ITC_KEYBOARD::CUsbKeyTypes::usbFlagsHigh See usbFlagsHigh definition \endlink
                \see ITC_KEYBOARD::CUsbKeyTypes::usbFlagsHigh
	    */
            public CUsbKeyTypes.usbFlagsHigh /*byte*/ bFlagHigh;
            /// <summary>
            /// mid byte flags for the key definition
            /// </summary>
	    /*! \link ITC_KEYBOARD::CUsbKeyTypes::usbFlagsMid See usbFlagsMid definition \endlink
                \see ITC_KEYBOARD::CUsbKeyTypes::usbFlagsMid
	    */
            public CUsbKeyTypes.usbFlagsMid /*byte*/ bFlagMid;
            /// <summary>
            /// low byte flags for the key definition
            /// </summary>
	    /*! \link ITC_KEYBOARD::CUsbKeyTypes::usbFlagsLow See usbFlagsHigh definition \endlink
                \see ITC_KEYBOARD::CUsbKeyTypes::usbFlagsLow
	    */
            public CUsbKeyTypes.usbFlagsLow /*byte*/ bFlagLow;
            /// <summary>
            /// the value generated by the hardware key, a VKEY or an index into another table
            /// </summary>
            public byte bIntScan;
        }
        /// <summary>
        /// the short form of a USBKeyStruct holds the same information except for
        /// USB code page and USB HID scancode
        /// </summary>
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct usbKeyStructShort
        {
            //public byte bHID;
            //public CUsbKeyTypes.HWkeys bScanKey;
            /// <summary>
            /// high byte flags for the key definition
            /// </summary>
            public CUsbKeyTypes.usbFlagsHigh /*byte*/ bFlagHigh;
            /// <summary>
            /// mid byte flags for the key definition
            /// </summary>
            public CUsbKeyTypes.usbFlagsMid /*byte*/ bFlagMid;
            /// <summary>
            /// low byte flags for the key definition
            /// </summary>
            public CUsbKeyTypes.usbFlagsLow /*byte*/ bFlagLow;
            /// <summary>
            /// the value generated by the hardware key, a VKEY or an index into another table
            /// </summary>
            public byte bIntScan;
        }

        /// <summary>
        /// get a string with the data of the key
        /// </summary>
        /// <param name="iPlane">the keyboard plane</param>
        /// <param name="iHWScanCode">the USB HID hardware scancode of the key in question</param>
        /// <returns></returns>
        public string dumpKey(int iPlane, int iHWScanCode)
        {
            usbKeyStruct _ks = new usbKeyStruct();
            if(getKeyStruct(iPlane, iHWScanCode, ref _ks)!=-1)
                return dumpKey(_ks);
            else
                return "key with " + iHWScanCode.ToString("X") + " undefined";
        }

        public string dumpFlags(usbKeyStruct usbStruct)
        {
            usbKeyStructShort usbTest = new usbKeyStructShort();
            usbTest.bFlagHigh = usbStruct.bFlagHigh;
            usbTest.bFlagMid = usbStruct.bFlagMid;
            usbTest.bFlagLow = usbStruct.bFlagLow;
            return dumpFlags(usbTest);
        }

        public string dumpFlags(usbKeyStructShort uShort)
        {
            string s2 = "[";
            s2 += uShort.bFlagHigh.ToString() + ",";
            s2 += uShort.bFlagMid.ToString() + ",";
            s2 += uShort.bFlagLow.ToString() + ",";
            s2 += "]";
            return s2;
        }
        public string dumpKey(usbKeyStructShort theUsbKeyStructShort)
        {
            byte b;
            string s = "";

            b = (byte)theUsbKeyStructShort.bFlagHigh;
            s += "," + b.ToString("X02");


            b = (byte)theUsbKeyStructShort.bFlagMid;
            s += "," + b.ToString("X02");

            b = (byte)theUsbKeyStructShort.bFlagLow;
            s += "," + b.ToString("X02");

            s += "," + theUsbKeyStructShort.bIntScan.ToString("X02");
            string s2 = dumpFlags(theUsbKeyStructShort);
            s += s2;

            //the following must result in char @ ("shift"+"=")
            //07,8A,00,20,00,55-Keyboard Intl 4- '='
            if (CUsbKeyTypes.FlagsLow.isNormalkey(theUsbKeyStructShort.bFlagLow))
            {
                if (CUsbKeyTypes.FlagsMid.isNoop(theUsbKeyStructShort.bFlagMid))
                {
                    s += "NOOP";
                }
                else
                {
                    if (CUsbKeyTypes.FlagsMid.isExtendedKey(theUsbKeyStructShort.bFlagMid))
                    {
                        s += " extended:'" + ITC_KEYBOARD.CvkMap.getName(theUsbKeyStructShort.bIntScan) + "'";
                    }
                    if (CUsbKeyTypes.FlagsMid.isVkey(theUsbKeyStructShort.bFlagMid))
                    {
                        if (CUsbKeyTypes.FlagsMid.isShifted(theUsbKeyStructShort.bFlagMid))
                            s += " SHIFT+'" + ITC_KEYBOARD.CvkMap.getName(theUsbKeyStructShort.bIntScan) + "' '" +
                                ITC_KEYBOARD.CvkMap.getNameShifted(theUsbKeyStructShort.bIntScan) + "'";
                        else
                            s += " '" + ITC_KEYBOARD.CvkMap.getName(theUsbKeyStructShort.bIntScan) + "'";
                    }
                    else
                    {//no VKEY
                        if (CUsbKeyTypes.FlagsMid.isShifted(theUsbKeyStructShort.bFlagMid))
                            s += " SHIFT+'" + ITC_KEYBOARD.CUSBPS2_vals.Cusbps2key.getName(theUsbKeyStructShort.bIntScan) + "'";
                        else
                            s += " '" + ITC_KEYBOARD.CUSBPS2_vals.Cusbps2key.getName(theUsbKeyStructShort.bIntScan) + "'";
                    }
                }
            }


            //if (CUsbKeyTypes.FlagsLow.isShiftKey(theUsbKeyStructShort.bFlagLow))
            //{
            //    s += " 'ShiftIndex'";
            //    if (_shiftKeys != null)
            //        s += _shiftKeys.dumpShiftKey(theUsbKeyStructShort.bIntScan);
            //}
            //if (CUsbKeyTypes.FlagsLow.isRotateKey(theUsbKeyStructShort.bFlagLow))
            //{
            //    s += " 'RotateIndex'";
            //    if (rotateKeys != null)
            //        s += rotateKeys.dumpRotateKey(theUsbKeyStructShort.bIntScan);
            //}
            //if (CUsbKeyTypes.FlagsLow.isNamedEventKey(theUsbKeyStructShort.bFlagLow))
            //{
            //    s += " 'EventIndex'";
            //    string es = _KeybdNamedEvents.getStateEvent(theUsbKeyStructShort.bIntScan);
            //    string ed = _KeybdNamedEvents.getDeltaEvent(theUsbKeyStructShort.bIntScan);
            //    s += "|'" + es + "'|'" + ed;
            //}
            //if (CUsbKeyTypes.FlagsLow.isAppLaunchKey(theUsbKeyStructShort.bFlagLow))
            //    s += " 'AppLaunch'";
            //if (CUsbKeyTypes.FlagsLow.isFunctionKey(theUsbKeyStructShort.bFlagLow))
            //{
            //    s += " 'FunctionKey'";
            //    if (_functionKeys != null)
            //        s += _functionKeys.dumpFunctionkey(theUsbKeyStructShort.bIntScan);
            //}
            //if (CUsbKeyTypes.FlagsLow.isModifierKey(theUsbKeyStructShort.bFlagLow))
            //{
            //    s += " 'ModifierKey'";
            //    if (_ModifiersKeys != null)
            //        s += _ModifiersKeys.dumpModifierKey(theUsbKeyStructShort.bIntScan);

            //}
            //if (CUsbKeyTypes.FlagsLow.isMultiKey(theUsbKeyStructShort.bFlagLow))
            //{
            //    s += " 'MultiIndex'";
            //    if (_MultiKeys != null)
            //        s += _MultiKeys.dumpMultiKey(theUsbKeyStructShort.bIntScan);
            //}
            return s;
        }
        /// <summary>
        /// deliver a string with the setting of the key
        /// </summary>
        /// <param name="theUsbKeyStruct">the keystruct to analyse</param>
        /// <returns>string</returns>
        public string dumpKey(usbKeyStruct theUsbKeyStruct)
        {
            byte b;
            string s = theUsbKeyStruct.bHID.ToString("X02");

            b = (byte)theUsbKeyStruct.bScanKey;
            s += "," + b.ToString("X02");
            //s += "," + theUsbKeyStruct.bScanKey.ToString();

            b =(byte)theUsbKeyStruct.bFlagHigh;
            s += "," + b.ToString("X02");

            b = (byte)theUsbKeyStruct.bFlagMid;
            s += "," + b.ToString("X02");

            b = (byte)theUsbKeyStruct.bFlagLow;
            s += "," + b.ToString("X02");

            s += "," + theUsbKeyStruct.bIntScan.ToString("X02");
            //first dump the USB-HID name of the key
            s += " '" + ITC_KEYBOARD.CUSBPS2_vals.Cusbps2key.getNameUSBHID(theUsbKeyStruct.bScanKey) + "' ";

            string s2 = dumpFlags(theUsbKeyStruct);
            s += s2;

            //the following must result in char @ ("shift"+"=")
            //07,8A,00,20,00,55-Keyboard Intl 4- '='
            if (CUsbKeyTypes.FlagsLow.isNormalkey(theUsbKeyStruct.bFlagLow))
            {
                if (CUsbKeyTypes.FlagsMid.isNoop(theUsbKeyStruct.bFlagMid))
                {
                    s += "NOOP";
                }
                else
                {
                    if (CUsbKeyTypes.FlagsMid.isExtendedKey(theUsbKeyStruct.bFlagMid))
                    {
                        s += " extended:'" + ITC_KEYBOARD.CvkMap.getName(theUsbKeyStruct.bIntScan) + "'";
                    }
                    if (CUsbKeyTypes.FlagsMid.isVkey(theUsbKeyStruct.bFlagMid))
                    {
                        if (CUsbKeyTypes.FlagsMid.isShifted(theUsbKeyStruct.bFlagMid))
                            s += " SHIFT+'" + ITC_KEYBOARD.CvkMap.getName(theUsbKeyStruct.bIntScan) + "' '" +
                                ITC_KEYBOARD.CvkMap.getNameShifted(theUsbKeyStruct.bIntScan) + "'";
                        else
                            s += " '" + ITC_KEYBOARD.CvkMap.getName(theUsbKeyStruct.bIntScan) + "'";
                    }
                    else
                    {//no VKEY
                        if (CUsbKeyTypes.FlagsMid.isShifted(theUsbKeyStruct.bFlagMid))
                            s += " SHIFT+'" + ITC_KEYBOARD.CUSBPS2_vals.Cusbps2key.getName(theUsbKeyStruct.bIntScan) + "'";
                        else
                            s += " '" + ITC_KEYBOARD.CUSBPS2_vals.Cusbps2key.getName(theUsbKeyStruct.bIntScan) + "'";
                    }
                }
            }


            if (CUsbKeyTypes.FlagsLow.isShiftKey(theUsbKeyStruct.bFlagLow))
            {
                s += " 'ShiftIndex'";
                if (_shiftKeys != null)
                    s += _shiftKeys.dumpShiftKey(theUsbKeyStruct.bIntScan);
            }
            if (CUsbKeyTypes.FlagsLow.isRotateKey(theUsbKeyStruct.bFlagLow))
            {
                s += " 'RotateIndex'";
                if (rotateKeys != null)
                    s += rotateKeys.dumpRotateKey(theUsbKeyStruct.bIntScan);
            }
            if (CUsbKeyTypes.FlagsLow.isNamedEventKey(theUsbKeyStruct.bFlagLow))
            {
                s += " 'EventIndex'";
                string es = _KeybdNamedEvents.getStateEvent(theUsbKeyStruct.bIntScan);
                string ed = _KeybdNamedEvents.getDeltaEvent(theUsbKeyStruct.bIntScan);
                s += "|'" + es + "'|'" + ed;
            }
            if (CUsbKeyTypes.FlagsLow.isAppLaunchKey(theUsbKeyStruct.bFlagLow))
                s += " 'AppLaunch'";
            if (CUsbKeyTypes.FlagsLow.isFunctionKey(theUsbKeyStruct.bFlagLow))
            {
                s += " 'FunctionKey'";
                if (_functionKeys != null)
                    s += _functionKeys.dumpFunctionkey(theUsbKeyStruct.bIntScan);
            }
            if (CUsbKeyTypes.FlagsLow.isModifierKey(theUsbKeyStruct.bFlagLow))
            {
                s += " 'ModifierKey'";
                if (_ModifiersKeys != null)
                    s += _ModifiersKeys.dumpModifierKey(theUsbKeyStruct.bIntScan);

            }
            if (CUsbKeyTypes.FlagsLow.isMultiKey(theUsbKeyStruct.bFlagLow))
            {
                s += " 'MultiIndex'";
                if (_MultiKeys != null)
                    s += _MultiKeys.dumpMultiKey(theUsbKeyStruct.bIntScan);
            }
            return s;
        }

        public CUSBkeys(Boolean bUseITEtables)
        {
            if (bUseITEtables)
                _bUseITEtables = true;
            else
                _bUseITEtables = false;

            //check, if CN50 device
            if (System.IO.File.Exists(@"\Windows\KbdRemapCN50.cpl"))
                _isCN50 = true;
            else
                _isCN50 = false;

            try
            {
                rotateKeys = new CRotateKeys();
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("No RotateKeys defined");
            }
            //read all ShiftPlanes into local array
            this.readKeyTables();

            _KeybdNamedEvents = new CkeybNamedEvents(false);
            try
            {
                _MultiKeys = new CMultiKeys();
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("No MultiKeys supported");
            }
            try
            {
                _ModifiersKeys = new CModifiersKeys();
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("No ModifiersKeys supported");
            }
            try
            {
                _shiftKeys = new CShiftKeys();
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("No ShiftKeys supported");
            }
            try
            {
                _functionKeys = new CFunctionkeys();
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("No FunctionKeys supported");
            }
        }

        public CUSBkeys()
        {
            //check, if CN50 device
            if (System.IO.File.Exists(@"\Windows\KbdRemapCN50.cpl"))
                _isCN50 = true;
            else
                _isCN50 = false;

            //set ITE flag
            this._bUseITEtables = false;

            try
            {
                rotateKeys = new CRotateKeys();
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("No RotateKeys defined");
            }
            //read all ShiftPlanes into local array
            this.readKeyTables();

            _KeybdNamedEvents = new CkeybNamedEvents(false);
            try
            {
                _MultiKeys = new CMultiKeys();
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("No MultiKeys supported");
            }
            try
            {
                _ModifiersKeys = new CModifiersKeys();
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("No ModifiersKeys supported");
            }
            try
            {
                _shiftKeys = new CShiftKeys();
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("No ShiftKeys supported");
            }
            try
            {
                _functionKeys = new CFunctionkeys();
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("No FunctionKeys supported");
            }
        }

        private void readKeyTablesITE()
        {
            int iCount = getNumPlanes();
            string regKeyb = getRegLocation();
            _usbKeysAll = new usbKeyStruct[iCount][];
            Microsoft.Win32.RegistryKey tempKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(regKeyb, true);
            for (int x = 0; x < iCount; x++)
            {
                byte[] bKeys = (byte[])tempKey.GetValue("ShiftPlane" + x.ToString());

                usbKeyStruct[] _usbKeyTemp = RawDeserialize2(bKeys);

                //_usbKeysAll=new CUSBkeys2.usbKey[x][];
                //_usbKeysAll[x] = new usbKey[_usbKeyTemp.Length];
                _usbKeysAll[x] = _usbKeyTemp;

            }
            tempKey.Close();
        }
        private void readKeyTables()
        {
            int iCount = getNumPlanes();
            string regKeyb;
            
            if(_bUseITEtables)
                regKeyb=getRegLocationITE();
            else
                regKeyb = getRegLocation();

            _usbKeysAll = new usbKeyStruct[iCount][];
            Microsoft.Win32.RegistryKey tempKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(regKeyb, true);
            for (int x = 0; x < iCount; x++)
            {
                byte[] bKeys = (byte[])tempKey.GetValue("ShiftPlane" + x.ToString());

                usbKeyStruct[] _usbKeyTemp = RawDeserialize2(bKeys);

                //_usbKeysAll=new CUSBkeys2.usbKey[x][];
                //_usbKeysAll[x] = new usbKey[_usbKeyTemp.Length];
                _usbKeysAll[x] = _usbKeyTemp;

            }
            tempKey.Close();
        }
        /// <summary>
        /// reset the registry and the driver to use factory defined key mapping
        /// </summary>
        public int resetKeyDefaults()
        {
            int iRes = 0;
            try
            {
                if (System.IO.File.Exists(@"\Windows\CK3kbdRemap.cpl"))
                    iRes = ITC_ResetKeyDefaultsCK3();
                if (System.IO.File.Exists(@"\Windows\CN3kbdRemap.cpl"))
                    iRes = ITC_ResetKeyDefaultsCN3();
                if (System.IO.File.Exists(@"\Windows\KbdRemapCN4.cpl"))
                    iRes = ITC_ResetKeyDefaultsCN4();
                if (System.IO.File.Exists(@"\Windows\KbdRemapCN50.cpl"))
                    iRes = ITC_ResetKeyDefaultsCN50();
                if (System.IO.File.Exists(@"\Windows\KbdRemapCS40.cpl"))
                    iRes = ITC_ResetKeyDefaultsCS40();
                if (System.IO.File.Exists(@"\Windows\KBDTools.CPL"))
                    iRes = ITC_ResetAllCN70();
                
                iRes = this.updateDriver();

            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Did you install/copy the keymap applet?");
            }
            return iRes;
        }

        /// <summary>
        /// dump the meaning of all known key mappings
        /// </summary>
        /// <returns></returns>
        public StringBuilder dumpAllKeys()
        {
            StringBuilder s = new StringBuilder(4000);
            //s="";
            int iCount = getNumPlanes();
            for (int x = 0; x < iCount; x++)
            {
                s.Append( "\r\n================================\r\n");
                s.Append ("ShiftPlane " + x.ToString() + "\r\n================================\r\n");

                foreach (usbKeyStruct _UKS in _usbKeysAll[x])
                {

                    try
                    {
#if TEST
                        if ((int)_UKS.bScanKey == 0x13)
                        {
                            System.Diagnostics.Debug.WriteLine(dumpKey(_UKS));
                            System.Diagnostics.Debugger.Break();
                        }
#endif
                        s.Append( dumpKey(_UKS));
                        s.Append( "\r\n");
                    }
                    catch (Exception)
                    {
                        System.Diagnostics.Debug.WriteLine("Exception for dumpKey: " + _UKS.bScanKey);
                    }
                }
            }
            //dump Modifiers
            CModifiersKeys cmods = new CModifiersKeys();
            s.Append( "\r\n================================\r\n");
            s.Append(     "          ModifierKeys");
            s.Append("\r\n================================\r\n");
            int iModifierCount=cmods.getModifierKeyCount();
            for (int i = 0; i < iModifierCount; i++)
            {
                try
                {
                    s.Append(dumpKey(cmods.getModifiersKey(i)));
                    s.Append("\r\n");
                }
                catch (Exception)
                {
                    System.Diagnostics.Debug.WriteLine("Exception for dumpKey Modifier Index: " + i.ToString());
                }
            }

            s.Append( "\r\n================================\r\n");
            s.Append(     "          MultiKeys");
            s.Append("\r\n================================\r\n");
            CMultiKeys cmulti = new CMultiKeys();
            int iMultikeyCount = cmulti.getMultiKeyCount();
            for (int i = 0; i < iMultikeyCount; i++)
            {
                s.Append("MultiKey" + (i+1).ToString() + ": ");
                try
                {
                    int iRes=0;
                    usbKeyStructShort[] multiStruct = cmulti.getStructsShort(i, ref iRes);
                    for (int j = 0; j < multiStruct.Length; j++)
                    {
                        s.Append (dumpKey(multiStruct[j]));
                        s.Append(" + ");
                    }
                }
                catch (Exception)
                {
                    System.Diagnostics.Debug.WriteLine("Exception for dumpKey Multikey Index: " + i.ToString());
                }
                s.Append("\r\n");
            }

            s.Append( "\r\n================================\r\n");
            s.Append(     "          ShiftKeys");
            s.Append("\r\n================================\r\n");
            CShiftKeys cShiftKeys = new CShiftKeys();
            int iShiftKeycount = cShiftKeys.getShiftKeyCount();
            for (int i = 0; i < iShiftKeycount; i++)
            {
                try
                {
                    s.Append(dumpKey(cShiftKeys.getShiftKey(i)));
                    s.Append("\r\n");
                }
                catch (Exception)
                {
                    System.Diagnostics.Debug.WriteLine("Exception for dumpKey Modifier Index: " + i.ToString());
                }
            }

            s.Append( "\r\n================================\r\n");
            s.Append(     "          Reg Location");
            s.Append("\r\n================================\r\n");
            s.Append(CUSBkeys.getRegLocation());
            s.Append("\r\n");

            return s;
        }
        /// <summary>
        /// get the number of supported keyboard planes
        /// </summary>
        /// <returns>number of keyboard planes</returns>
        public int getNumPlanes()
        {
            string regKeybd = getRegLocation();
            Microsoft.Win32.RegistryKey tempKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(regKeybd, true);
            //try and error
            string[] s = tempKey.GetValueNames();
            int iCountPlanes = -1;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].StartsWith("ShiftPlane"))
                    iCountPlanes++;
            }
            tempKey.Close();
            return iCountPlanes+1;
        }
        /// <summary>
        /// find the index of a USB hardware scancode (a key)
        /// </summary>
        /// <param name="iPlane">the keyboard plane to search in</param>
        /// <param name="iScanCode">the integer of an USB hardware scancode</param>
        /// <returns>index of the key in the keytable</returns>
        public int getKeyIndex(int iPlane, int iScanCode)
        {
            if (_usbKeysAll[iPlane] == null)
                throw new ArgumentNullException("There is no actual struct initialized to read from");
            int iIndex = -1;
            for (int i = 0; i < _usbKeysAll[iPlane].Length; i++)
            {
                if (_usbKeysAll[iPlane][i].bScanKey == (CUsbKeyTypes.HWkeys)iScanCode)
                {
                    iIndex = i;
                    break;
                }
            }
            return iIndex;
        }
        /// <summary>
        /// find the index of a USB hardware scancode (a key)
        /// </summary>
        /// <param name="iPlane">the keyboard plane to search in</param>
        /// <param name="iScanCode">the USB hardware scan code</param>
        /// <returns>index of the key in the keytable</returns>
        public int getKeyIndex(int iPlane, CUsbKeyTypes.HWkeys iScanCode)
        {
            if (_usbKeysAll[iPlane] == null)
                throw new ArgumentNullException("There is no actual struct initialized to read from");
            int iIndex = -1;
            for (int i = 0; i < _usbKeysAll[iPlane].Length; i++)
            {
                if (_usbKeysAll[iPlane][i].bScanKey == iScanCode)
                {
                    iIndex = i;
                    break;
                }
            }
            return iIndex;
        }
        /// <summary>
        /// find the index of a USB hardware scancode (a key) in the actual table
        /// OBSOLETE: DO NOT USE
        /// </summary>
        /// <param name="iScanCode"></param>
        /// <returns></returns>
        [Obsolete]
        public int getKeyIndex(int iScanCode)
        {
            if (_usbKeys == null)
                throw new ArgumentNullException("There is no actual struct initialized to read from");
            int iIndex = -1;
            for (int i = 0; i < _usbKeys.Length; i++)
            {
                if (_usbKeys[i].bScanKey == (CUsbKeyTypes.HWkeys)iScanCode)
                {
                    iIndex = i;
                    break;
                }
            }
            return iIndex;
        }
        /// <summary>
        /// retrieve the KeyStruct of a given key scancode and the keyboard plane
        /// </summary>
        /// <param name="iPlane">the keyboard plane to search</param>
        /// <param name="iScanCode">USB HID scancode to search for</param>
        /// <param name="refUsbKeyStruct">the KeyStruct of the key</param>
        /// <returns>index of keyStruct in plane, or -1 if key is not defined</returns>
        public int getKeyStruct(int iPlane, int iScanCode, ref usbKeyStruct refUsbKeyStruct)
        {
            if (_usbKeysAll[iPlane] == null)
                throw new ArgumentNullException("There is no actual struct initialized to read from");
            int iIndex = -1;
            for (int i=0; i<_usbKeysAll[iPlane].Length; i++)
            {
                if (_usbKeysAll[iPlane][i].bScanKey == (CUsbKeyTypes.HWkeys)iScanCode)
                {
                    iIndex = i;
                    refUsbKeyStruct = _usbKeysAll[iPlane][iIndex];
                    break;
                }
            }
            return iIndex;
        }

        public int findHWKey(CUsbKeyTypes.HWkeys iScanCode, ref int iPlane, ref usbKeyStruct refUsbKeyStruct)
        {
            int iRet = -1;
            int iIndex = -1;
            int maxPlane = getNumPlanes();

            for (int p = 0; p < maxPlane; p++)
            {
                if (_usbKeysAll[p] == null)
                    continue;

                for (int i = 0; i < _usbKeysAll[p].Length; i++)
                {
                    if (_usbKeysAll[p][i].bScanKey == iScanCode)
                    {
                        iIndex = i;
                        iPlane = p;
                        refUsbKeyStruct = _usbKeysAll[iPlane][iIndex];
                        break;//exit for
                    }
                }
                if (iIndex != -1)
                {
                    iRet = 0;
                    break; //exit for
                }
            }
            return iRet;
        }

        /// <summary>
        /// find plane and usbkey for a VKEY
        /// </summary>
        /// <param name="iScanCode">VKEY value to look for</param>
        /// <param name="refUsbKeyStruct">will get the found key struct</param>
        /// <param name="iPlane">will get the plane where the vkey is defined</param>
        /// <returns>-1 for error, 0 for success</returns>
        public int findVKey(byte iScanCode, ref int iPlane, ref usbKeyStruct refUsbKeyStruct)
        {
            int iRet = -1;
            int iIndex = -1;
            int maxPlane = getNumPlanes();
            for (int p = 0; p < maxPlane; p++)
            {
                if (_usbKeysAll[p] == null)
                    continue;

                for (int i = 0; i < _usbKeysAll[p].Length; i++)
                {
                    if (_usbKeysAll[p][i].bFlagLow == CUsbKeyTypes.usbFlagsLow.NormalKey)
                    {
                        if (_usbKeysAll[p][i].bIntScan == iScanCode)
                        {
                            iIndex = i;
                            iPlane = p;
                            refUsbKeyStruct = _usbKeysAll[iPlane][iIndex];
                            continue;
                        }
                    }
                }
                if (iIndex != -1)
                {
                    iRet = 0;
                    continue;
                }
            }
            return iRet;
        }

        /// <summary>
        /// get the actual keyStruct for the key with iScanCode
        /// </summary>
        /// <param name="iPlane">the keyboard plane to read</param>
        /// <param name="iScanCode">the USB HID scancode of the key to read</param>
        /// <param name="refUsbKeyStruct">the structure to fill with the data</param>
        /// <returns>index of keyStruct in plane, or -1 if key is not defined</returns>
        public int getKeyStruct(int iPlane, CUsbKeyTypes.HWkeys iScanCode, ref usbKeyStruct refUsbKeyStruct)
        {
            if (_usbKeysAll[iPlane] == null)
                throw new ArgumentNullException("There is no actual struct initialized to read from");
            int iIndex = -1;
            for (int i=0; i<_usbKeysAll[iPlane].Length; i++)
            {
                if (_usbKeysAll[iPlane][i].bScanKey == iScanCode)
                {
                    iIndex = i;
                    refUsbKeyStruct = _usbKeysAll[iPlane][iIndex];
                    break;
                }
            }
            return iIndex;
        }
        
        public int getKeyCount(cPlanes.plane cPlane)
        {
            if ((int)cPlane > this.getNumPlanes())
                return -1;
            return _usbKeysAll[((int)cPlane)].Length;
        }

        /// <summary>
        /// get the actual keyStruct for the key with iScanCode in the actual keytable
        /// </summary>
        /// <param name="iScanCode">the USB HID scancode of the key to read</param>
        /// <param name="refUsbKeyStruct">the structure to fill with the data</param>
        /// <returns>-1 for failure
        /// 0 or positive number for success</returns>
        [Obsolete("use getKeyStruct(int iPlane, CUsbKeyTypes.HWkeys iScanCode, ref usbKeyStruct refUsbKeyStruct) instead")]
        public int getKeyStruct(int iScanCode, ref usbKeyStruct refUsbKeyStruct)
        {
            if (_usbKeys == null)
                throw new ArgumentNullException("There is no actual struct initialized to read from");
            int iIndex = -1;
            for (int i=0; i<_usbKeys.Length; i++)
            {
                if (_usbKeys[i].bScanKey == (CUsbKeyTypes.HWkeys) iScanCode)
                {
                    iIndex = i;
                    refUsbKeyStruct = _usbKeys[iIndex];
                    break;
                }
            }
            return iIndex;
        }

        /// <summary>
        /// Send the driver an event to let read the actual key tables 
        /// </summary>
        /// <returns>0 for success</returns>
        private int updateDriver()
        {
            EventHandling.setNamedEvent("ITC_KEYBOARD_CHANGE_USB");
            uint iRes = 99;
            try
            {
                iRes = EventNamespace.EventHandling.WaitForUsbLoadComplete();
                System.Diagnostics.Debug.WriteLine("WaitForUsbLoadComplete = " + iRes.ToString());
            }
            catch (ArgumentOutOfRangeException ax)
            {
                System.Diagnostics.Debug.WriteLine("ArgumentOutOfRangeException in WaitForUsbLoadComplete" + ax.Message);
            }
            catch (Exception x)
            {
                System.Diagnostics.Debug.WriteLine("Exception in WaitForUsbLoadComplete" + x.Message);
            }
            if (iRes != 0)
                return -1;
            else
                return 0;
        }

        private int getEventCount()
        {
            string sRegEventsState = getRegLocation() + @"\Events\State";
            Microsoft.Win32.RegistryKey tempKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(sRegEventsState, true);
            //the keys are 'numbered' Event1 to Eventx
            int iValCount = tempKey.ValueCount;
            tempKey.Close();
            return iValCount;
        }
        private int getRotateKeyCount()
        {

            string sReg = getRegLocation() + @"\RotateKeys";
            Microsoft.Win32.RegistryKey tempKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(sReg, true);
            //the keys are 'numbered' Multi1 to Multix
            int iValCount = tempKey.ValueCount;
            tempKey.Close();
            return iValCount/3;
        }
        /// <summary>
        /// read the delta events from registry
        /// </summary>
        /// <returns>a string array with the names of the delta events</returns>
        public string[] getDeltaEvents()
        {
            string sRegEventsDelta = getRegLocation() + @"\Events\Delta";
            //the events are below this in branches called Events\State and Events\Delta
            //open the State branch
            Microsoft.Win32.RegistryKey tempKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(sRegEventsDelta, true);
            //the keys are 'numbered' Event1 to Eventx
            int iValCount = tempKey.ValueCount;
            string[] sDeltaEvents = new string[iValCount];
            for (int i = 0; i < iValCount; i++)
            {
                string sEventVal = "Event" + (i + 1).ToString();
                sDeltaEvents[i] = (string)tempKey.GetValue(sEventVal);
            }
            tempKey.Close();
            return sDeltaEvents;
        }
        /// <summary>
        /// read the multikey entries from registry
        /// </summary>
        /// <returns>a string array with the names of the delta events</returns>
        public string[] getMultiKeys()
        {
            string sRegMultikeys = getRegLocation() + @"\Multikeys";
            //the events are below this in branches called Events\State and Events\Delta
            //open the State branch
            Microsoft.Win32.RegistryKey tempKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(sRegMultikeys, true);
            //the keys are 'numbered' Event1 to Eventx
            int iValCount = tempKey.ValueCount;
            string[] sMultikeys = new string[iValCount];
            for (int i = 0; i < iValCount; i++)
            {
                string sEventVal = "Multi" + (i + 1).ToString();
                sMultikeys[i] = (string)tempKey.GetValue(sEventVal).ToString();
            }
            tempKey.Close();
            return sMultikeys;
        }
        /// <summary>
        /// read one multikey entry from registry
        /// </summary>
        /// <param name="iMKey">Multikey index (starting at 1) to read</param>
        /// <returns>a string with the names of the multikey values</returns>
        public string dumpMultiKey(int iMKey)
        {
            return (_MultiKeys.dumpMultiKey(iMKey));
        }

        /// <summary>
        /// read the modifiers from registry
        /// </summary>
        /// <returns>a string array with the names of the delta events</returns>
        public string[] getModifiersKeys()
        {
            string sRegMultikeys = getRegLocation() + @"\ModifiersKeys";
            //the events are below this in branches called Events\State and Events\Delta
            //open the State branch
            Microsoft.Win32.RegistryKey tempKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(sRegMultikeys, true);
            //the keys are 'numbered' Event1 to Eventx
            int iValCount = tempKey.ValueCount;
            string[] sMultikeys = new string[iValCount];
            for (int i = 0; i < iValCount; i++)
            {
                string sEventVal = "ModKey" + (i + 1).ToString();
                sMultikeys[i] = (string)tempKey.GetValue(sEventVal);
            }
            tempKey.Close();
            return sMultikeys;
        }


        /// <summary>
        /// get the active registry key for the keyboard driver mapping
        /// </summary>
        /// <returns>a string with the registry location of the current mapping</returns>
        public static string getRegLocation(bool bUseITEtable)
        {
            // [HKLM]Hardware\DeviceMap\Keybd:CurrentActiveLayoutKey
            Microsoft.Win32.RegistryKey tempKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"Hardware\DeviceMap\Keybd");
            string regKeyb = (string)tempKey.GetValue("CurrentActiveLayoutKey");
            if (bUseITEtable)
            {
                //TE2000LayoutAlias
                Microsoft.Win32.RegistryKey tempKeyITE = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(regKeyb);
                string regKeybITE = (string)tempKeyITE.GetValue("TE2000LayoutAlias");

                regKeyb = regKeybITE;
                tempKeyITE.Close();
            }
            tempKey.Close();
            return regKeyb;
        }
        /// <summary>
        /// get the active registry key for the keyboard driver mapping
        /// </summary>
        /// <returns>a string with the registry location of the current mapping</returns>
        public static string getRegLocation()
        {
            // [HKLM]Hardware\DeviceMap\Keybd:CurrentActiveLayoutKey
            Microsoft.Win32.RegistryKey tempKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"Hardware\DeviceMap\Keybd");
            string regKeyb = (string)tempKey.GetValue("CurrentActiveLayoutKey");
            tempKey.Close();
            return regKeyb;
        }
        public static string getRegLocationITE()
        {
            // [HKLM]Hardware\DeviceMap\Keybd:CurrentActiveLayoutKey
            Microsoft.Win32.RegistryKey tempKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"Hardware\DeviceMap\Keybd");
            string regKeyb = (string)tempKey.GetValue("CurrentActiveLayoutKey");
            //TE2000LayoutAlias
            Microsoft.Win32.RegistryKey tempKeyITE = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(regKeyb);
            string regKeybITE = (string)tempKeyITE.GetValue("TE2000LayoutAlias");
            if (regKeybITE == null)
            {
                tempKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"Hardware\DeviceMap\Keybd");              
                byte[] regKeybITEStruct = (byte[])tempKey.GetValue("TEUSBTranslateTable");
            }
            
            regKeyb = regKeybITE;
            tempKey.Close();
            tempKeyITE.Close();
            return regKeyb;
        }
        /// <summary>
        /// Set the key with scancode in the keyboard plane to be an index into the rotatekey entries
        /// </summary>
        /// <param name="iPlane">keyboard plane</param>
        /// <param name="iScancode">USB HID scancode of key to change</param>
        /// <param name="iRotateIndex">index of the rotate entry</param>
        /// <returns>0 for success, -1 for error</returns>
        public int setKeyRotate(int iPlane, int iScancode, int iRotateIndex)
        {
            //sample; 0x07,0x1F,0x00,0x02,0x40,0x01
            /*
                "RotateKeyX" = hex: aa1,bb1,cc1,sc1, \
                                    aa2,bb2,cc2,sc2, \
                                    …
                                    aaN,bbN,ccN,scN
            */
            try
            {
                if (iRotateIndex > this.getRotateKeyCount())
                    return -1;

                int iIndex = getKeyIndex(iPlane, iScancode);
                _usbKeysAll[iPlane][iIndex].bHID = 0x07; // keyStruct.bHID;
                _usbKeysAll[iPlane][iIndex].bScanKey = (CUsbKeyTypes.HWkeys)iScancode;

                _usbKeysAll[iPlane][iIndex].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;//0x00; // keyStruct.bFlag1;
                _usbKeysAll[iPlane][iIndex].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoRepeat;// 0x02; // Non Repeating
                _usbKeysAll[iPlane][iIndex].bFlagLow = CUsbKeyTypes.usbFlagsLow.RotateKeyIndex;// 0x40; // Rotate Key Index (Key that cycles through a specific set of keys)
                _usbKeysAll[iPlane][iIndex].bIntScan = (byte)iRotateIndex;
            }
            catch (Exception)
            {
                return -1;
            }
            return 0;
        }
        /// <summary>
        /// Set the key with scancode in the active keyboard table 
        /// to be an index into the rotatekey entries
        /// Obsolete: DO NOT USE
        /// </summary>
        /// <param name="iScancode">USB HID scancode of key to change</param>
        /// <param name="iRotateIndex">index of the rotate entry</param>
        /// <returns>0 for success, -1 for error</returns>
        [Obsolete]
        public int setKeyRotate(int iScancode, int iRotateIndex)
        {
            //sample; 0x07,0x1F,0x00,0x02,0x40,0x01
            /*
                "RotateKeyX" = hex: aa1,bb1,cc1,sc1, \
                                    aa2,bb2,cc2,sc2, \
                                    …
                                    aaN,bbN,ccN,scN
            */
            try
            {
                if (iRotateIndex > this.getRotateKeyCount())
                    return -1;

                int iIndex = getKeyIndex(iScancode);
                _usbKeys[iIndex].bHID = 0x07; // keyStruct.bHID;
                _usbKeys[iIndex].bScanKey = (CUsbKeyTypes.HWkeys)iScancode;

                _usbKeys[iIndex].bFlagHigh =CUsbKeyTypes.usbFlagsHigh.NoFlag ; //0x00; // keyStruct.bFlag1;
                _usbKeys[iIndex].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoRepeat;// 0x02; // Non Repeating
                _usbKeys[iIndex].bFlagLow = CUsbKeyTypes.usbFlagsLow.RotateKeyIndex;// 0x40; // Rotate Key Index (Key that cycles through a specific set of keys)
                _usbKeys[iIndex].bIntScan = (byte)iRotateIndex;
            }
            catch (Exception)
            {
                return -1;
            }
            return 0;
        }
        
        /// <summary>
        /// define a key with new values
        /// </summary>
        /// <param name="iPlane">the keyboard plane to change</param>
        /// <param name="iScancode">the USB HID hardware scancode</param>
        /// <param name="keyStruct">an usbKeyStruct with the new values</param>
        /// <returns>-1 if error, 0 for success</returns>
        public int setKey(int iPlane, CUsbKeyTypes.HWkeys iScancode, usbKeyStruct keyStruct)
        {
            int x = (int)iScancode;
            return setKey(iPlane, x, keyStruct);
        }
        /// <summary>
        /// define a key with new values
        /// </summary>
        /// <param name="iPlane">the keyboard plane to change</param>
        /// <param name="iScancode">the USB HID hardware scancode</param>
        /// <param name="keyStruct">an usbKeyStruct with the new values</param>
        /// <returns>-1 if error, 0 for success</returns>
        public int setKey(int iPlane, int iScancode, usbKeyStruct keyStruct)
        {
            try
            {
                int iIndex = getKeyIndex(iPlane, iScancode);
                _usbKeysAll[iPlane][iIndex].bHID = keyStruct.bHID;
                _usbKeysAll[iPlane][iIndex].bScanKey = keyStruct.bScanKey;
                _usbKeysAll[iPlane][iIndex].bFlagHigh = keyStruct.bFlagHigh;
                _usbKeysAll[iPlane][iIndex].bFlagMid = keyStruct.bFlagMid;
                _usbKeysAll[iPlane][iIndex].bFlagLow = keyStruct.bFlagLow;
                _usbKeysAll[iPlane][iIndex].bIntScan = keyStruct.bIntScan;
            }
            catch (Exception)
            {
                return -1;
            }
            return 0;
        }
        public int setKey(cPlanes.plane iPlane, int iScancode, usbKeyStruct keyStruct)
        {
            try
            {
                int iIndex = getKeyIndex((int)iPlane, iScancode);
                _usbKeysAll[(int)iPlane][iIndex].bHID = keyStruct.bHID;
                _usbKeysAll[(int)iPlane][iIndex].bScanKey = keyStruct.bScanKey;
                _usbKeysAll[(int)iPlane][iIndex].bFlagHigh = keyStruct.bFlagHigh;
                _usbKeysAll[(int)iPlane][iIndex].bFlagMid = keyStruct.bFlagMid;
                _usbKeysAll[(int)iPlane][iIndex].bFlagLow = keyStruct.bFlagLow;
                _usbKeysAll[(int)iPlane][iIndex].bIntScan = keyStruct.bIntScan;
            }
            catch (Exception)
            {
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// define a key with new values in the actual plane
        /// Obsolete: DO NOT USE
        /// </summary>
        /// <param name="iScancode">the USB HID hardware scancode</param>
        /// <param name="keyStruct">an usbKeyStruct with the new values</param>
        /// <returns>-1 if error, 0 for success</returns>
        [Obsolete("use setKey(int iPlane, int iScancode, usbKeyStruct keyStruct)")]
        public int setKey(int iScancode, usbKeyStruct keyStruct)
        {
            try
            {
                int iIndex = getKeyIndex(iScancode);
                _usbKeys[iIndex].bHID = keyStruct.bHID;
                _usbKeys[iIndex].bScanKey = keyStruct.bScanKey;
                _usbKeys[iIndex].bFlagHigh = keyStruct.bFlagHigh;
                _usbKeys[iIndex].bFlagMid = keyStruct.bFlagMid;
                _usbKeys[iIndex].bFlagLow = keyStruct.bFlagLow;
                _usbKeys[iIndex].bIntScan = keyStruct.bIntScan;
            }
            catch (Exception)
            {
                return -1;
            }
            return 0;
        }
        /// <summary>
        /// save the current key remapping tables to registry
        /// and inform driver about update
        /// </summary>
        /// <returns>0 for OK, -1 for error</returns>
        public int writeKeyTables()
        {
            int iRet=-99;
            int iPlaneCount = this.getNumPlanes();
            string regKeyb = getRegLocation();
            try
            {
                Microsoft.Win32.RegistryKey tempKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(regKeyb, true);
                for (int p = 0; p < iPlaneCount; p++)
                {
                    byte[] bRaw = RawSerialize(_usbKeysAll[p]);
                    //ShiftPlane 0 to x
                    tempKey.SetValue("ShiftPlane" + p.ToString(), bRaw, Microsoft.Win32.RegistryValueKind.Binary);
                }
                tempKey.Close();
                if (this.updateDriver() == 0)
                    iRet = 0;
                else
                    iRet = -1; //something wrent wrong
            }
            catch (Exception)
            {
                return -2; // we got an exception
            }
            return iRet;
        }
        /// <summary>
        /// save the key remapping table for keyboard plane iPlane to registry
        /// and inform driver about update
        /// Obsolete: DO NOT USE
        /// </summary>
        /// <param name="iPlane">the single keyboard plane to save</param>
        /// <returns>0 for OK, -1 for error</returns>
        [Obsolete("use writeKeyTables instead")]
        public int saveKeyTable(int iPlane)
        {
            if (iPlane > this.getNumPlanes())
                throw new ArgumentOutOfRangeException("plane id exceeds max planes");
            try
            {
                byte[] bRaw = RawSerialize(_usbKeys);
                string regKeyb = getRegLocation();
                Microsoft.Win32.RegistryKey tempKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(regKeyb, true);
                tempKey.SetValue("ShiftPlane"+iPlane.ToString(),bRaw,Microsoft.Win32.RegistryValueKind.Binary);
                tempKey.Close();
                this.updateDriver();
            }
            catch (Exception)
            {
                return -1;                
            }
            return 0;
        }

        /// <summary>
        /// adds the key with the defined keystruct to the keyboard table of the current keyboard table
        /// Obsolete: DO NOT USE
        /// </summary>
        /// <param name="newKey">the new keystruct definition</param>
        [Obsolete("addKey(int iPlane, usbKeyStruct newKey) instead")]
        public void addKey(usbKeyStruct newKey)
        {
            int iCount = _usbKeys.Length;
            usbKeyStruct[] _tempKeys = new usbKeyStruct[iCount + 1];
//            for (int i = 0; i < iCount; i++)
//            {
                _usbKeys.CopyTo(_tempKeys,0);
//            }
            _tempKeys[_tempKeys.Length-1] = newKey;
            _usbKeys = new usbKeyStruct[_tempKeys.Length];
            _usbKeys = _tempKeys;
            
        }
        /// <summary>
        /// adds the key with the defined keystruct to the keyboard table of plane iPlane
        /// </summary>
        /// <param name="iPlane">the keyboard plane</param>
        /// <param name="newKey">the new keystruct definition</param>
        public void addKey(int iPlane, usbKeyStruct newKey)
        {
            int iCount = _usbKeysAll[iPlane].Length;
            usbKeyStruct[] _tempKeys = new usbKeyStruct[iCount + 1];
            //_usbKeys
            _usbKeysAll[iPlane].CopyTo(_tempKeys,0);
            _tempKeys[_tempKeys.Length-1] = newKey;
            _usbKeysAll[iPlane] = new usbKeyStruct[_tempKeys.Length];
            _usbKeysAll[iPlane] = _tempKeys;
            
        }
        public void addKey(cPlanes.plane iPlane, usbKeyStruct newKey)
        {
            int iCount = _usbKeysAll[(int)iPlane].Length;
            usbKeyStruct[] _tempKeys = new usbKeyStruct[iCount + 1];
            //_usbKeys
            _usbKeysAll[(int)iPlane].CopyTo(_tempKeys,0);
            _tempKeys[_tempKeys.Length-1] = newKey;
            _usbKeysAll[(int)iPlane] = new usbKeyStruct[_tempKeys.Length];
            _usbKeysAll[(int)iPlane] = _tempKeys;
            
        }

        /// <summary>
        /// remove the key definition (structure) for the key matching
        /// </summary>
        /// <param name="iPlane">which plane to work on</param>
        /// <param name="rmKey">the keyboard struct to search and remove</param>
        /// <returns>true if OK, false on error</returns>
        public bool removeKey(int iPlane, usbKeyStruct rmKey)
        {
            int iScanCode = (int)rmKey.bScanKey;
            int iIndex = getKeyIndex(iPlane, (CUsbKeyTypes.HWkeys) iScanCode);
            if (iIndex > -1)
            {
                int iCount = _usbKeysAll[iPlane].Length;
                usbKeyStruct[] _tempKeys = new usbKeyStruct[iCount - 1];
                //copy all keys except the one at iIndex
                int y = 0;
                for (int x = 0; x < iCount; x++)
                {
                    if (x != iIndex)
                    {
                        _tempKeys[y] = _usbKeysAll[iPlane][x];
                        y++;
                    }
                }
                _usbKeysAll[iPlane] = new usbKeyStruct[_tempKeys.Length];
                _usbKeysAll[iPlane] = _tempKeys;
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// read the keyboard mapping table as current table
        /// Obsolete: DO NOT USE
        /// </summary>
        /// <param name="iPlane">the keyboard plane to read</param>
        [Obsolete]
        public void readKeyTable(int iPlane)
        {
            if (iPlane > 2)
                throw new ArgumentOutOfRangeException("Actually only planes 0-2 supported");
            string regKeyb = getRegLocation();
            Microsoft.Win32.RegistryKey tempKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(regKeyb, true);
            byte[] bKeys= (byte[])tempKey.GetValue("ShiftPlane"+iPlane.ToString());

            tempKey.Close();
            _usbKeys = RawDeserialize2(bKeys);

#if DEBUG
            //TESTING ONLY
            byte[] bTest = RawSerialize(_usbKeys);
            if (bTest.Length == bKeys.Length)
            {
                for (int j = 0; j < bTest.Length; j++)
                {
                    if (bTest[j] != bKeys[j])
                        throw new ArgumentOutOfRangeException("Upps. Found DIFF at " + j.ToString());
                }
            }
            else
                throw new ArgumentOutOfRangeException("Upps. The byte arrays have different size.");
#endif
        }

#region helpers
        /// <summary>
        /// convert byte array to keystruct
        /// </summary>
        /// <param name="dataIn">byte array as read from registry</param>
        /// <returns>array of keystructs</returns>
        public usbKeyStruct[] BytesToStruct(byte[] dataIn)
        {
            //pinn the mememory block
            GCHandle hDataIn = GCHandle.Alloc(dataIn, GCHandleType.Pinned);
            //create a new byte[] with new size
            usbKeyStruct[] _usbKeys = new usbKeyStruct[dataIn.Length / 6];
            //copy the bytes into the struct
            _usbKeys = (usbKeyStruct[])Marshal.PtrToStructure(hDataIn.AddrOfPinnedObject(), typeof(usbKeyStruct));
            hDataIn.Free();
            return _usbKeys;
        }
        // http://bytes.com/topic/c-sharp/answers/249770-byte-structure
        /// <summary>
        /// convert array of keystruct to byte array to store in registry
        /// </summary>
        /// <param name="structData">the KeyStruct array to convert</param>
        /// <returns>a byte array ready to be saved to registry</returns>
        public byte[] RawSerialize(usbKeyStruct[] structData)
        {
            //size
            int structSize = Marshal.SizeOf( structData[0]);
            int iRawSize = structData.Length * structSize;
            byte[] rawDatas = new byte[iRawSize];
            for (int i = 0; i < structData.Length; i++)
            {
                rawDatas[(i * structSize) + 0] = structData[i].bHID;
                rawDatas[(i * structSize) + 1] = (byte)structData[i].bScanKey;
                rawDatas[(i * structSize) + 2] = (byte)structData[i].bFlagHigh;
                rawDatas[(i * structSize) + 3] = (byte)structData[i].bFlagMid;
                rawDatas[(i * structSize) + 4] = (byte)structData[i].bFlagLow;
                rawDatas[(i * structSize) + 5] = (byte)structData[i].bIntScan;
            }
            return rawDatas;
        }
        private static usbKeyStruct[] RawDeserialize2(byte[] rawData)
        {
            int structSize = 6;
            int iCount = rawData.Length / structSize; //we have 5 bytes per struct
            usbKeyStruct[] _usbKey = new usbKeyStruct[iCount];
            for (int i = 0; i < iCount; i++)
            {
                _usbKey[i].bHID = rawData[i * structSize + 0];
                _usbKey[i].bScanKey = (CUsbKeyTypes.HWkeys)rawData[i * structSize + 1];
                _usbKey[i].bFlagHigh = (CUsbKeyTypes.usbFlagsHigh)rawData[i * structSize + 2];
                _usbKey[i].bFlagMid = (CUsbKeyTypes.usbFlagsMid)rawData[i * structSize + 3];
                _usbKey[i].bFlagLow = (CUsbKeyTypes.usbFlagsLow)rawData[i * structSize + 4];
                _usbKey[i].bIntScan = (byte)rawData[i * structSize + 5];
            }
            return _usbKey;
        }
#endregion
        /*
            Bit 0: 00,00,01: Shift Key Index (Changes Shift planes).
            Bit 1: 00,00,02: Named Event Index (Sets/Resets a Named Event Level based
                                on Key State and Sets a Named Event at every Key State
                                Change)
            Bit 2: 00,00,04: MultiKey Index(Single key press emulates multiple key strokes)
            Bit 3: 00,00,08: Modifier Index (Shift/Control/Alt/Caps/etc.)
            Bit 4: 00,00,10: App Launch (Microsoft Shell Application Launch Key)
            Bit 5: 00,00,20: Function Key Index (Launches a specified function from a DLL)
            Bit 6: 00,00,40: Rotate Key Index (Key that cycles through a specific set of keys)
            Bit 7: 00,00,80: Reserved
            Bit 8: 00,01,00: Extended Key )PC Scan Code is accompanied by the Extended
                                0xE0 Byte)
            Bit 9: 00,02,00: Non Repeating (Key does not Auto Repeat)
            Bit 10: 00,04,00: Silent (No key click).
            Bit 11: 00,08,00: VKEY (No Scan Code-vkey translation)
            Bit 12: 00,10,00: Ignored (NOOP key).
            Bit 13: 00,20,00: Shifted (Sends Shift before key).
            Bit 14: 00,40,00: No Chord (Only for multikeys).
            Bit 15: 00,80,00: Reserved
            Bit 16: 01,00,00: Sticky Once (Key remains pressed until next key press of any
                                key)
            Bit 17: 02,00,00: Sticky Persist (Key remains pressed until next key press of the
                                same key)
            Bit 18: 04,00,00: Sticky Lock (Key pressed twice in a row remains pressed until
                                next key press of same key. Otherwise behaves like Sticky
                                Once.)
            Bit 19: 08,00,00: Reserved
            Bit 20: 10,00,00: LED 1 (Key lights CAPS LOCK LED when depressed)
            Bit 21: 20,00,00: LED 2 (Key lights NUM LOCK LED when depressed)
            Bit 22: 40,00,00: LED 3 (Key lights SCOLL LOCK LED when depressed)
            Bit 23: 80,00,00: Reserved         
         */

        public static usbKeyStructShort controlModKey
        {
            get
            {
                CUSBkeys.usbKeyStructShort uKey = new CUSBkeys.usbKeyStructShort();
                uKey.bFlagHigh = CUsbKeyTypes.usbFlagsHigh.StickyOnce;
                uKey.bFlagMid = CUsbKeyTypes.usbFlagsMid.NoRepeat;
                uKey.bFlagLow = CUsbKeyTypes.usbFlagsLow.ModifierIndex;

                uKey.bIntScan = 0x14;//a PS/2 key CUSBPS2_vals.Cusbps2key.getName(0x14)="Left Control"
                return uKey;
            }
        }
        public static usbKeyStructShort ModKeyLeftControl
        {
            get
            {
                CUSBkeys.usbKeyStructShort uKey = new CUSBkeys.usbKeyStructShort();
                uKey.bFlagHigh = CUsbKeyTypes.usbFlagsHigh.StickyOnce;
                uKey.bFlagMid = CUsbKeyTypes.usbFlagsMid.NoRepeat;
                uKey.bFlagLow = CUsbKeyTypes.usbFlagsLow.ModifierIndex;

                uKey.bIntScan = 0x14;//a PS/2 key CUSBPS2_vals.Cusbps2key.getName(0x14)="Left Control"
                return uKey;
            }
        }
        public static usbKeyStructShort ModKeyRightShift
        {
            get
            {
                CUSBkeys.usbKeyStructShort uKey = new CUSBkeys.usbKeyStructShort();
                uKey.bFlagHigh = CUsbKeyTypes.usbFlagsHigh.StickyOnce;
                uKey.bFlagMid = CUsbKeyTypes.usbFlagsMid.NoRepeat;
                uKey.bFlagLow = CUsbKeyTypes.usbFlagsLow.ModifierIndex;
                uKey.bIntScan = 0x59;//a PS/2 key CUSBPS2_vals.Cusbps2key.getName(0x59)="Right Shift"
                return uKey;
            }
        }
        public static usbKeyStructShort ModKeyLeftAlt
        {
            get
            {
                // reg(hex): 01 02 08 11
                CUSBkeys.usbKeyStructShort uKey = new CUSBkeys.usbKeyStructShort();
                uKey.bFlagHigh = CUsbKeyTypes.usbFlagsHigh.StickyOnce;
                uKey.bFlagMid = CUsbKeyTypes.usbFlagsMid.NoRepeat;
                uKey.bFlagLow = CUsbKeyTypes.usbFlagsLow.ModifierIndex;
                uKey.bIntScan = 0x11;
                // USB HID LeftAlt = 0xE2
                // 0xA4; //VK_MENU 0x12; VK_LMENU 0xA4; 
                // 0x11;//a PS/2 key CUSBPS2_vals.Cusbps2key.getName(0x11)="Left Alt"
                return uKey;
            }
        }
        public static usbKeyStructShort ModKeyCapsLock
        {
            get
            {
                CUSBkeys.usbKeyStructShort uKey = new CUSBkeys.usbKeyStructShort();
                uKey.bFlagHigh = CUsbKeyTypes.usbFlagsHigh.StickyOnce;
                uKey.bFlagMid = CUsbKeyTypes.usbFlagsMid.NoRepeat;
                uKey.bFlagLow = CUsbKeyTypes.usbFlagsLow.ModifierIndex;
                uKey.bIntScan = 0x58;//a PS/2 key CUSBPS2_vals.Cusbps2key.getName(0x58)="Caps Lock"
                return uKey;
            }
        }
    }

}
