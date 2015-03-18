using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.InteropServices;

namespace ITC_KEYBOARD
{
    public class KdbToolsClass:IDisposable
    {
#region KbdRemap.cpl imports

        public enum KeyUSBPage : byte
        {
            standard = 0x07,
        }
        public enum KeyPlane:byte{
            Normal = 0,   // Represents the normal plane when neither Orange or Green are selected.
            Orange = 1,   // Represents the Orange plane, selected by the Orange key.
            Green = 2,    // Represents the Green plane, selected by the Green key.
        }
        
        /// Key Type.  Used to determine the primary function of the key.
        [Flags]
        public enum KeyType:byte{
            NormalKey = 0x00,     // Key will be a normal key value.
            PlaneShift = 0x01,    // Key will select the key plane, such as Orange or Green.
            NamedEvent = 0x02,    // Key will trigger a predefined Named Event.
            MultiKey = 0x04,      // Key will trigger a predefined key macro.
            Modifier = 0x08,      // Key will act as a modifier key, such as Shift, Control, Alt, Caps, etc. 
            AppLaunch = 0x10,     // Key will act as a Microsoft Shell Application Launch key.
            Rotate = 0x40,        // Key will select one of several predefined key values, such as ABC on a cell phone 2 key.
        }

        /// Key Attribute.  Used to modify the operation of the key.
        [Flags]
        public enum KeyAttribute:byte{
            NoAttrib = 0x00,          // Value parameter will represent a Scan Code value.
            Extended = 0x01,          // Key is preceded by the extended 0xE0 byte.
            NoRepeat = 0x02,          // Key will not auto repeat.
            Silent = 0x04,            // Key press will not produce a key click.
            VKEY = 0x08,              // Value parameter will represent a VKEY value.
            NOOP = 0x10,              // Key will do nothing.  Disables the key.
            Shifted = 0x20,           // Key is preceded by a left shift key before it's value is sent.
            NoChord = 0x40,           // Multikeys will not be chorded when sent. 
        }

        /// Key Modifier.  Used to set the "Sticky" value of the key and control the keyboard LEDs.
        [Flags]
        public enum KeyModifier : byte
        {
            NoModifier = 0x00,     // No Stickyness and no LED.
            StickyOnce = 0x01,     // Key remains pressed until next key press of any key.   Intended for Shift, Cap, Orange, etc.
            StickyPersist = 0x02,  // Key remains pressed until next key press of same key.  Intended for Shift, Cap, Orange, etc.
            StickyLock = 0x04,     // Key pressed twice in a row remains pressed until next key press of same key.  Otherwise like StickyOnce. Intended for Shift, Cap, Orange, etc.
            LED1 = 0x10,           // Key will also light LED 1.
            LED2 = 0x20,           // Key will also light LED 2.
            LED3 = 0x40,           // Key will also light LED 3.
        }

        public struct size_t
        {
            int size;
        }

#region keyValues (VKEY)
        /// <summary>
        /// used to identify a Hardware Key, (USB) page=0x07 and (USB) usage= one of these values
        /// </summary>
        public enum KeyUsage : byte
        {
            ITC_Standard_A_Key = 0x04,
            ITC_Standard_B_Key = 0x05,
            ITC_Standard_C_Key = 0x06,
            ITC_Standard_D_Key = 0x07,
            ITC_Standard_E_Key = 0x08,
            ITC_Standard_F_Key = 0x09,
            ITC_Standard_G_Key = 0x0A,
            ITC_Standard_H_Key = 0x0B,
            ITC_Standard_I_Key = 0x0C,
            ITC_Standard_J_Key = 0x0D,
            ITC_Standard_K_Key = 0x0E,
            ITC_Standard_L_Key = 0x0F,
            ITC_Standard_M_Key = 0x10,
            ITC_Standard_N_Key = 0x11,
            ITC_Standard_O_Key = 0x12,
            ITC_Standard_P_Key = 0x13,
            ITC_Standard_Q_Key = 0x14,
            ITC_Standard_R_Key = 0x15,
            ITC_Standard_S_Key = 0x16,
            ITC_Standard_T_Key = 0x17,
            ITC_Standard_U_Key = 0x18,
            ITC_Standard_V_Key = 0x19,
            ITC_Standard_W_Key = 0x1A,
            ITC_Standard_X_Key = 0x1B,
            ITC_Standard_Y_Key = 0x1C,
            ITC_Standard_Z_Key = 0x1D,
            ITC_Standard_1_Key = 0x1E,
            ITC_Standard_Shift_Key = 0x1E,
            ITC_Standard_2_Key = 0x1F,
            ITC_Standard_3_Key = 0x20,
            ITC_Standard_4_Key = 0x21,
            ITC_Standard_5_Key = 0x22,
            ITC_Standard_6_Key = 0x23,
            ITC_Standard_7_Key = 0x24,
            ITC_Standard_8_Key = 0x25,
            ITC_Standard_9_Key = 0x26,
            ITC_Standard_0_Key = 0x27,
            ITC_Standard_Enter_Key = 0x28,
            ITC_Standard_Escape_Key = 0x29,
            ITC_Standard_Backspace_Key = 0x2A,
            ITC_Standard_Tab_Key = 0x2B,
            ITC_Standard_Space_Key = 0x2C,
            ITC_Standard_Period_Key = 0x37,
            ITC_Standard_F1_Soft1_Key = 0x3A,
            ITC_Standard_F2_Soft2_Key = 0x3B,
            ITC_Standard_F3_Send_Key = 0x3C,
            ITC_Standard_F4_END_Key = 0x3D,
            ITC_Standard_F5_Key = 0x3E,
            ITC_Standard_F6_Key = 0x3F,
            ITC_Standard_UpperRight_Btn = 0x3f,
            ITC_Standard_F7_Key = 0x40,
            ITC_Standard_F8_Asterisk_Key = 0x41,
            ITC_Standard_F9_Star_Key = 0x41,
            ITC_Standard_Hash_Key = 0x42,
            ITC_Standard_F10_Key = 0x43,
            ITC_Standard_F11_Key = 0x44,
            ITC_Standard_F12_Key = 0x45,
            ITC_Standard_Menu_Key = 0x45,
            ITC_Standard_PrtScrn_Key = 0x46,
            ITC_Standard_ScrLck_Key = 0x47,
            ITC_Standard_Break_Key = 0x48,
            ITC_Standard_Insert_Key = 0x49,
            ITC_Standard_Home_Key = 0x4A,
            ITC_Standard_PgDn_Key = 0x4B,
            ITC_Standard_Del_Key = 0x4C,
            ITC_Standard_End_Key = 0x4D,
            ITC_Standard_PgUp_Key = 0x4E,
            ITC_Standard_RArrow_Key = 0x4F,
            ITC_Standard_LArrow_Key = 0x50,
            ITC_Standard_DArrow_Key = 0x51,
            ITC_Standard_UArrow_Key = 0x52,
            ITC_Standard_NumLk_Key = 0x53,
            ITC_Standard_Numpad_Div_Key = 0x54,
            ITC_Standard_Numpad_Mult_Key = 0x55,
            ITC_Standard_Numpad_Minus_Key = 0x56,
            ITC_Standard_Numpad_Add_Key = 0x57,
            ITC_Standard_FieldExit_Key = 0x58,
            ITC_Standard_Numpad_1_Key = 0x59,
            ITC_Standard_Numpad_2_Key = 0x5A,
            ITC_Standard_Numpad_3_Key = 0x5B,
            ITC_Standard_Numpad_4_Key = 0x5C,
            ITC_Standard_Numpad_5_Key = 0x5D,
            ITC_Standard_Numpad_6_Key = 0x5E,
            ITC_Standard_Numpad_7_Key = 0x5F,
            ITC_Standard_Numpad_8_Key = 0x60,
            ITC_Standard_Numpad_9_Key = 0x61,
            ITC_Standard_Numpad_0_Key = 0x62,
            ITC_Standard_Numpad_Dec_Key = 0x63,
            ITC_Standard_App_Key = 0x65,
            ITC_Standard_Backlight_Key = 0x87,
            ITC_Standard_LowerLeft_Btn = 0x91,
            ITC_Standard_LCtrl_Key = 0xE0,
            ITC_Standard_LShift_Key = 0xE1,
            ITC_Standard_LAlt_Key = 0xE2,
            ITC_Standard_Windows_Key = 0xE3,
            ITC_Standard_RCtrl_Key = 0xE4,
            ITC_Standard_RShift_Key = 0xE5,
            ITC_Standard_RAlt_Key = 0xE6,
        }
#endregion
#region keyValues (VKEY)
        /// <summary>
        /// these values can be used to set a key meaning
        /// </summary>
        public enum KeyValues
        {
            KeyValue_DISABLE = 0x0000,
            KeyValue_BACKSPACE = 0x0008,	// <back>,
            KeyValue_TAB = 0x0009,	// <tab>,
            KeyValue_RETURN = 0x000d,	// <return> (Enter),
            KeyValue_SHIFT = 0x0010,	// <Shift>,
            KeyValue_CONTROL = 0x0011,	// <Control>,
            KeyValue_MENU = 0x0012,	// <Menu>,
            KeyValue_ESCAPE = 0x001b,	// <Escape>,
            KeyValue_SPACE = 0x0020,	// <space>,
            KeyValue_PAGEUP = 0x0021,	// <page up>,
            KeyValue_PAGEDOWN = 0x0022,	// <page down>,
            KeyValue_END = 0x0023,	// <end>,
            KeyValue_HOME = 0x0024,	// <Home>,
            KeyValue_LEFT = 0x0025,	// <left arrow>,
            KeyValue_UP = 0x0026,	// <up arrow>,
            KeyValue_RIGHT = 0x0027,	// <right arrow>,
            KeyValue_DOWN = 0x0028,	// <down arrow>,
            KeyValue_PRINT = 0x002a,	// <Print>,
            KeyValue_INSERT = 0x002d,	// <insert>,
            KeyValue_DELETE = 0x002e,	// <delete>,
            KeyValue_HELP = 0x002f,	// <Help>,
            KeyValue_0 = 0x0030,
            KeyValue_1 = 0x0031,
            KeyValue_2 = 0x0032,
            KeyValue_3 = 0x0033,
            KeyValue_4 = 0x0034,
            KeyValue_5 = 0x0035,
            KeyValue_6 = 0x0036,
            KeyValue_7 = 0x0037,
            KeyValue_8 = 0x0038,
            KeyValue_9 = 0x0039,
            KeyValue_a = 0x0041,
            KeyValue_b = 0x0042,
            KeyValue_c = 0x0043,
            KeyValue_d = 0x0044,
            KeyValue_e = 0x0045,
            KeyValue_f = 0x0046,
            KeyValue_g = 0x0047,
            KeyValue_h = 0x0048,
            KeyValue_i = 0x0049,
            KeyValue_j = 0x004a,
            KeyValue_k = 0x004b,
            KeyValue_l = 0x004c,
            KeyValue_m = 0x004d,
            KeyValue_n = 0x004e,
            KeyValue_o = 0x004f,
            KeyValue_p = 0x0050,
            KeyValue_q = 0x0051,
            KeyValue_r = 0x0052,
            KeyValue_s = 0x0053,
            KeyValue_t = 0x0054,
            KeyValue_u = 0x0055,
            KeyValue_v = 0x0056,
            KeyValue_w = 0x0057,
            KeyValue_x = 0x0058,
            KeyValue_y = 0x0059,
            KeyValue_z = 0x005a,
            KeyValue_LWIN = 0x005b,	// <Left Win>,
            KeyValue_RWIN = 0x005c,	// <Right Win>,
            KeyValue_APPS = 0x005d,	// <Apps>,
            KeyValue_F1 = 0x0070,	// <F1>,
            KeyValue_F2 = 0x0071,	// <F2>,
            KeyValue_F3 = 0x0072,	// <F3>,
            KeyValue_F4 = 0x0073,	// <F4>,
            KeyValue_F5 = 0x0074,	// <F5>,
            KeyValue_F6 = 0x0075,	// <F6>,
            KeyValue_F7 = 0x0076,	// <F7>,
            KeyValue_F8 = 0x0077,	// <F8>,
            KeyValue_F9 = 0x0078,	// <F9>,
            KeyValue_F10 = 0x0079,	// <F10>,
            KeyValue_F11 = 0x007a,	// <F11>,
            KeyValue_F12 = 0x007b,	// <F12>,
            KeyValue_F13 = 0x007c,	// <F13>,
            KeyValue_F14 = 0x007d,	// <F14>,
            KeyValue_F15 = 0x007e,	// <F15>,
            KeyValue_F16 = 0x007f,	// <F16>,
            KeyValue_F17 = 0x0080,	// <F17>,
            KeyValue_F18 = 0x0081,	// <F18>,
            KeyValue_F19 = 0x0082,	// <F19>,
            KeyValue_F20 = 0x0083,	// <F20>,
            KeyValue_F21 = 0x0084,	// <F21>,
            KeyValue_F22 = 0x0085,	// <F22>,
            KeyValue_F23 = 0x0086,	// <F23>,
            KeyValue_F24 = 0x0087,	// <F24>,
            KeyValue_NUMLOCK = 0x0090,	// <Num Lock>,
            KeyValue_SCROLL = 0x0091,	// <Scroll>,
            KeyValue_SEMICOLON = 0x00ba,
            KeyValue_EQUAL = 0x00bb,
            KeyValue_COMMA = 0x00bc,
            KeyValue_HYPHEN = 0x00bd,
            KeyValue_PERIOD = 0x00be,
            KeyValue_SLASH = 0x00bf,
            KeyValue_DQUOTE = 0x00c0,
            KeyValue_LBRACKET = 0x00db,
            KeyValue_BACKSLASH = 0x00dc,
            KeyValue_RBRACKET = 0x00dd,
            KeyValue_APOSTROPHE = 0x00de,
            //shifted values
            KeyValue_BACKTAB = 0x0109,	// <back tab>,
            KeyValue_LINEFEED = 0x010d,	// <linefeed>,
            KeyValue_RPAREN = 0x0130,
            KeyValue_EXCLAMATION = 0x0131,
            KeyValue_AT = 0x0132,
            KeyValue_POUND = 0x0133,
            KeyValue_DOLLAR = 0x0134,
            KeyValue_PERCENT = 0x0135,
            KeyValue_CARET = 0x0136,
            KeyValue_AMPERSAND = 0x0137,
            KeyValue_ASTERIX = 0x0138,
            KeyValue_LPAREN = 0x0139,
            KeyValue_A = 0x0141,
            KeyValue_B = 0x0142,
            KeyValue_C = 0x0143,
            KeyValue_D = 0x0144,
            KeyValue_E = 0x0145,
            KeyValue_F = 0x0146,
            KeyValue_G = 0x0147,
            KeyValue_H = 0x0148,
            KeyValue_I = 0x0149,
            KeyValue_J = 0x014a,
            KeyValue_K = 0x014b,
            KeyValue_L = 0x014c,
            KeyValue_M = 0x014d,
            KeyValue_N = 0x014e,
            KeyValue_O = 0x014f,
            KeyValue_P = 0x0150,
            KeyValue_Q = 0x0151,
            KeyValue_R = 0x0152,
            KeyValue_S = 0x0153,
            KeyValue_T = 0x0154,
            KeyValue_U = 0x0155,
            KeyValue_V = 0x0156,
            KeyValue_W = 0x0157,
            KeyValue_X = 0x0158,
            KeyValue_Y = 0x0159,
            KeyValue_Z = 0x015a,
            KeyValue_COLON = 0x01ba,
            KeyValue_PLUS = 0x01bb,
            KeyValue_LESSTHAN = 0x01bc,
            KeyValue_UNDERSCORE = 0x01bd,
            KeyValue_GREATERTHAN = 0x01be,
            KeyValue_QUESTIONMARK = 0x01bf,
            KeyValue_QUOTE = 0x01c0,	// ' (single quote),
            KeyValue_LBRACE = 0x01db,	// { (curly brace)
            KeyValue_VERTBAR = 0x01dc,
            KeyValue_RBRACE = 0x01dd,	// } (curly brace),
            KeyValue_TILDE = 0x01de,
            //end of shifted values
        }
        #endregion

        [DllImport("kbdtools.cpl")]
        public static extern int SetKey(KeyPlane plane, KeyUSBPage page, KeyUsage usage, KeyType keyType, KeyAttribute keyAttribute, KeyModifier keyModifier, KeyValues value);

        [DllImport("kbdtools.cpl")]
        public static extern int SetKey(KeyPlane plane, KeyUSBPage page, KeyUsage usage, KeyType keyType, KeyAttribute keyAttribute, KeyModifier keyModifier, byte value);

        [DllImport("kbdtools.cpl")]
        public static extern int GetKey(KeyPlane plane, KeyUSBPage page, KeyUsage usage, ref KeyType keyType, ref KeyAttribute keyAttribute, ref KeyModifier keyModifier, ref byte value);

        [DllImport("kbdtools.cpl")]
        public static extern int GetDefaultKey(byte plane, byte page, byte usage, ref byte keyType, ref byte keyAttribute, ref byte keyModifier, ref byte value);

        [DllImport("kbdtools.cpl")]
        public static extern int DisableKey(byte plane, byte page, byte usage);

        [DllImport("kbdtools.cpl")]
        public static extern int ResetKey(byte plane, byte page, byte usage);

        [DllImport("kbdtools.cpl")]
        public static extern int ResetAll();

        [DllImport("kbdtools.cpl")]
        public static extern int ExportKeys(string fileName, string str, ref size_t strLen);

        [DllImport("kbdtools.cpl")]
        public static extern int ExportDefaultKeys(string fileName, string str, ref size_t strLen);

        [DllImport("kbdtools.cpl")]
        public static extern int ImportKeys(string str, bool additive);

        [DllImport("kbdtools.cpl")]
        public static extern int GetKeyboardNotify(ref bool enabled);

        [DllImport("kbdtools.cpl")]
        public static extern int SetKeyboardNotify(bool enable);

        [DllImport("kbdtools.cpl")]
        public static extern int GetKeyboardID(string str, ref uint len);

        [DllImport("kbdtools.cpl")]
        private static extern int GetVersion(ref short major, ref short minor, ref short build, ref short revision);

        [DllImport("kbdtools.cpl")]
        public static extern int KeyExists(byte plane, byte page, byte usage, ref bool result);

        [DllImport("kbdtools.cpl")]
        public static extern int KeyDefaultExists(byte plane, byte page, byte usage, ref bool result);

#endregion
        private class KeybdEventHandling:IDisposable
        {
            #region Event Handling
            [DllImport("coredll.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Auto)]
            static extern uint WaitForMultipleObjects(uint nCount, IntPtr[] lpHandles, [MarshalAs(UnmanagedType.Bool)] bool bWaitAll, uint dwMilliseconds);
            //static extern uint WaitForMultipleObjects(uint nCount, IntPtr lpHandles, [MarshalAs(UnmanagedType.Bool)] bool bWaitAll, uint dwMilliseconds);

            [DllImport("coredll.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Auto)]
            static extern uint WaitForSingleObject(IntPtr hEvent, uint dwMilliseconds);

            private const uint WAIT_OBJECT_0 = 0x00;
            private const uint WAIT_ABANDONED_0 = 0x80;
            private const uint WAIT_FAILED = 0xffffffff;
            private const uint INFINITE = 0xffffffff;
            private const uint WAIT_TIMEOUT = 0x00000102;

            private static class WaitOptions
            {
                public static uint WAIT_OBJECT_0 { get { return 0x00000000; } }
                public static uint WAIT_ABANDONED { get { return 0x00000080; } }
                public static uint WAIT_ABANDONED_0 { get { return 0x00000080; } }
                public static uint WAIT_FAILED { get { return 0xffffffff; } }
                public static uint INFINITE { get { return 0xffffffff; } }
                public static uint WAIT_TIMEOUT { get { return 0x00000102; } }
            }
            [DllImport("coredll.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Auto)]
            public static extern IntPtr CreateEvent(IntPtr lpEventAttributes, [In, MarshalAs(UnmanagedType.Bool)] bool bManualReset, [In, MarshalAs(UnmanagedType.Bool)] bool bIntialState, [In, MarshalAs(UnmanagedType.BStr)] string lpName);

            [DllImport("coredll.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Auto)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool CloseHandle(IntPtr hObject);

            [DllImport("coredll.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool EventModify(IntPtr hEvent, [In, MarshalAs(UnmanagedType.U4)] int dEvent);

            /// <summary>
            /// event access types class needed for native OpenEvent etc.
            /// </summary>
            public static class EventAccess
            {
                public static uint SYNCHRONIZE { get { return 0x00100000; } }
                public static uint STANDARD_RIGHTS_REQUIRED { get { return 0x000F0000; } }
                public static uint EVENT_ALL_ACCESS { get { return STANDARD_RIGHTS_REQUIRED | SYNCHRONIZE | 0x3; } }
            }

            private const uint EVENT_SET = 3;

            // HANDLE OpenEvent(DWORD dwDesiredAccess, BOOL bInheritHandle, LPCTSTR lpName);
            [DllImport("coredll.dll")]
            private static extern IntPtr OpenEvent(uint dwDesiredAccess, int bInheritHandle, string lpName);

            /// <summary>
            /// provide the different event flag constants
            /// like PULSE, SET, RESET
            /// </summary>
            public static class EventFlags
            {
                public static int PULSE { get { return 1; } }
                public static int RESET { get { return 2; } }
                public static int SET { get { return 3; } }
                public static int EVENT_SET { get { return 3; } }
            }
            private System.Threading.Thread _thread = null;
            IntPtr _iHandle = IntPtr.Zero;
            IntPtr _iHandleStop = IntPtr.Zero;
            private const string itc_keyboard_event_name = "ITC_KEYBOARD_CHANGE_USB";
            private const string event_name_stop = "stop_event_handling";
            public KeybdEventHandling()
            {
                _iHandle = OpenEvent(EventAccess.EVENT_ALL_ACCESS, 0, itc_keyboard_event_name);
                if (_iHandle != IntPtr.Zero)
                {
                    _iHandleStop=CreateEvent(IntPtr.Zero, true, false, event_name_stop);
                    if(_iHandleStop!=IntPtr.Zero){
                        _thread = new System.Threading.Thread(threadProc);
                        _thread.Start();
                    }
                    else
                        throw new SystemException("Could not create eventhandling thread");
                }
            }
            public bool bRun = true;

            // Track whether Dispose has been called.
            private bool disposed = false;
            public void Dispose(){
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            private void Dispose(bool disposing)
            {
                // Check to see if Dispose has already been called.
                if (!this.disposed)
                {
                    // If disposing equals true, dispose all managed 
                    // and unmanaged resources.
                    if (disposing)
                    {
                        // Dispose managed resources.
                        // component.Dispose();
                    }

                    // Call the appropriate methods to clean up 
                    // unmanaged resources here.
                    // If disposing is false, 
                    // only the following code is executed.
                    bRun = false;
                    SetEvent(_iHandleStop);
                    System.Threading.Thread.Sleep(10);
                    if (_thread != null)
                        _thread.Abort();
                    CloseHandle(_iHandle);
                    CloseHandle(_iHandleStop);
                }
                disposed = true;
            }

            bool SetEvent(IntPtr hEvent)
            {
                return EventModify(hEvent, (int)EventFlags.SET);
            }
            private void threadProc()
            {
                bRun = true;
                IntPtr[] handles = new IntPtr[2];
                handles[0] = _iHandle;
                handles[1] = _iHandleStop;
                try
                {
                    while (bRun)
                    {
                        uint uWaitRes = WaitForMultipleObjects(2, handles, false, 1000);
                        switch (uWaitRes)
                        {
                            case WAIT_OBJECT_0:
                                keyboardHasChanged();
                                break;
                            case WAIT_OBJECT_0+1:
                                bRun=false;                                
                                break;
                            case WAIT_TIMEOUT:
                                break;
                            case WAIT_FAILED:
                                break;
                            case WAIT_ABANDONED_0:
                                break;
                        }
                    }
                }
                catch (System.Threading.ThreadAbortException ex)
                {
                    System.Diagnostics.Debug.WriteLine("EventHandling: Thread aborted exception: " +ex.Message);
                    bRun = false;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("EventHandling: Exception in thread: " +ex.Message);
                    bRun = false;
                }
            }
            public event EventHandler KeyboardChanged;
            delegate void setKeyboardChangedCallback();
            private void keyboardHasChanged()
            {
                OnKeyboardChanged(new EventArgs());
            }
            /// <summary>
            /// another helper to fire attached ImageReady eventhandlers
            /// </summary>
            /// <param name="e"></param>
            protected virtual void OnKeyboardChanged(EventArgs e)
            {
                if (KeyboardChanged != null)
                {
                    KeyboardChanged(this, e);
                }
            }
        }
#endregion

        KeybdEventHandling evHandler = null;
        public event EventHandler KeyboardChanged;
        delegate void setKeyboardChangedCallback();
        private void keyboardHasChanged()
        {
            OnKeyboardChanged(new EventArgs());
        }
        /// <summary>
        /// another helper to fire attached ImageReady eventhandlers
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnKeyboardChanged(EventArgs e)
        {
            if (KeyboardChanged != null)
            {
                KeyboardChanged(this, e);
            }
        }

        //#################################################################################################################
        public KdbToolsClass()
        {
            if (!System.IO.File.Exists("\\Windows\\kbdtools.cpl"))
            {
                throw new MissingMethodException("Missing runtime kbdtools.cpl");
            }
            dbgOut("KbdTools loaded. Version: " + sKeybdVersion);
            evHandler = new KeybdEventHandling();
            evHandler.KeyboardChanged += new EventHandler(evHandler_KeyboardChanged);
            SetKeyboardNotify(true);
#if DEBUG
            KeyType keyT = 0;
            KeyModifier keyM = 0;
            KeyAttribute keyA = 0;
            byte bValue = 0;
            int iRes = GetKey(KeyPlane.Normal, KeyUSBPage.standard, KeyUsage.ITC_Standard_Enter_Key, ref keyT, ref keyA, ref keyM, ref bValue);
            dbgOut("Test getKey returned (type, Attr, Mod, val): " + keyT.ToString() + "," + keyA.ToString() + "," + keyM.ToString() + "," + ((KeyValues)bValue).ToString());
            iRes = SetKey(KeyPlane.Normal, KeyUSBPage.standard, KeyUsage.ITC_Standard_Enter_Key, KeyType.NormalKey, KeyAttribute.VKEY, KeyModifier.NoModifier, KeyValues.KeyValue_RETURN);
            dbgOut("Test setKey returned: " + iRes.ToString());
#endif
        }

        void evHandler_KeyboardChanged(object sender, EventArgs e)
        {
            dbgOut("Keyboard has changed!");
            OnKeyboardChanged(e);
        }
        public bool bEnableKeybdNotify
        {
            get
            {
                bool bEnabled = false;
                if (GetKeyboardNotify(ref bEnabled) == 0)
                    return bEnabled;
                else
                    return false;
            }
            set
            {
                SetKeyboardNotify(value);
            }
        }
        private void dbgOut(string s)
        {
            System.Diagnostics.Debug.WriteLine(s);
        }
        public string sKeybdVersion
        {
            get
            {
                short sMajor = 0, sMinor = 0, sBuild = 0, sRev = 0;
                int iRes = GetVersion(ref sMajor, ref sMinor, ref sBuild, ref sRev);
                if (iRes == 0)
                    return (sMajor.ToString("00") + "." + sMinor.ToString("00") + "." + sBuild.ToString("00") + "." + sRev.ToString("00"));
                else
                    return "n/a";
            }
        }

        public int restoreDefaults()
        {
            return ResetAll();
        }
        
        // Track whether Dispose has been called.
        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed 
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    if (evHandler != null)
                    {
                        evHandler.Dispose();
                    }
                }

                // Call the appropriate methods to clean up 
                // unmanaged resources here.
                // If disposing is false, 
                // only the following code is executed.
                bool bEnabled = false;
                if (GetKeyboardNotify(ref bEnabled) == 0)
                    if (bEnabled)
                        SetKeyboardNotify(false);
            }
            disposed = true;
        }
    }
}
