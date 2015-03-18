using System;
using System.Collections.Generic;
using System.Text;

/*! \file UsbKeyFlags.cs
    class source file for CUsbKeyTypes
*/
namespace ITC_KEYBOARD
{
    /// <summary>
    /// class to manage high, mid, low flags of key entries
    /// and a list of USB HID hardware key scan codes
    /// </summary>
    public static class CUsbKeyTypes
    {
#region NOTE
        /* 
        NOTE:
        in code we count this
        left byte(16-23) = flag1, 
        mid byte = flag2, 
        right byte (0-7) = flag3
        */
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

        Bit 8: 00,01,00: Extended Key )PC Scan Code is accompanied by the Extended 0xE0 Byte)
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

        /// <summary>
        /// a Normal, Shifted, Event, Modifier, Function, Multi, Rotate key
        /// </summary>
        [Flags]
	/*! \namespace ITC_KEYBOARD
	    \class CUsbKeyTypes
	    \enum usbFlagsLow
	    \brief Bit flags controlling the main type of the key mapping
	*/
        public enum usbFlagsLow : byte
        {
            NormalKey = 0x00,
            ShiftKeyIndex = 0x01, 	//!< Bit 0: 00,00,01: Shift Key Index (Changes Shift planes).
            NamedEventIndex = 0x02, 	/*!< Bit 1: 00,00,02: Named Event Index (Sets/Resets a Named Event Level based
                         		on Key State and Sets a Named Event at every Key State
                         		Change)*/
            MultiKeyIndex = 0x04, 	/*!< Bit 2: 00,00,04: MultiKey Index(Single key press emulates multiple key strokes)*/
            ModifierIndex = 0x08, 	/*!< Bit 3: 00,00,08: Modifier Index (Shift/Control/Alt/Caps/etc.)*/
            AppLaunchKey = 0x10,	/*!< Bit 4: 00,00,10: App Launch (Microsoft Shell Application Launch Key)*/
            FunctionKeyIndex = 0x20, 	/*!< Bit 5: 00,00,20: Function Key Index (Launches a specified function from a DLL)*/
            RotateKeyIndex = 0x40, 	/*!< Bit 6: 00,00,40: Rotate Key Index (Key that cycles through a specific set of keys)*/
            Reserved = 0x80 		/*!< Bit 7: 00,00,40: Reserved */
        }
        [Flags]
	/*! \namespace ITC_KEYBOARD
	    \class CUsbKeyTypes
	    \enum usbFlagsMid
	    \brief Bit flags controlling the additional attributes of the key mapping
	*/
        public enum usbFlagsMid : byte
        {
            NoFlag = 0x00,
            Extended = 0x01, 		/*!< Bit 8: 00,01,00: Extended Key )PC Scan Code is accompanied by the Extended 0xE0 Byte)*/
            NoRepeat = 0x02, 		/*!< Bit 9: 00,02,00: Non Repeating (Key does not Auto Repeat)*/
            Silent = 0x04,		/*!< Bit 10: 00,04,00: Silent (No key click).*/
            VKEY = 0x08,		/*!< Bit 11: 00,08,00: VKEY (No Scan Code-vkey translation)*/
            NOOP = 0x10,		/*!< Bit 12: 00,10,00: Ignored (NOOP key).*/
            Shifted = 0x20,		/*!< Bit 13: 00,20,00: Shifted (Sends Shift before key).*/
            NoChord = 0x40,		/*!< Bit 14: 00,40,00: No Chord (Only for multikeys).*/
            Reserved = 0x80		/*!< Bit 15: 00,80,00: Reserved*/
        }
        [Flags]
	/*! \namespace ITC_KEYBOARD
	    \class CUsbKeyTypes
	    \enum usbFlagsHigh
	    \brief Bit flags controlling the extra attriutes of the key mapping
	*/
        public enum usbFlagsHigh : byte
        {
            NoFlag = 0x00,
            StickyOnce = 0x01,		/*!< Bit 16: 01,00,00: Sticky Once (Key remains pressed until next key press of any*/
            StickyPersist = 0x02,	/*!< Bit 17: 02,00,00: Sticky Persist (Key remains pressed until next key press of same key)*/
            StickyLock = 0x04,		/*!< Bit 18: 04,00,00: Sticky Lock (Key pressed twice in a row remains pressed until 
					next key press of same key. Otherwise behaves like Sticky Once.)*/
            Reserved1 = 0x08,		/*!< Bit 19: 08,00,00: Reserved*/
            LED1 = 0x10,		/*!< Bit 20: 10,00,00: LED 1 (Key lights CAPS LOCK LED when depressed)*/
            LED2 = 0x20,		/*!< Bit 21: 20,00,00: LED 2 (Key lights NUM LOCK LED when depressed)*/
            LED3 = 0x40,		/*!< Bit 22: 40,00,00: LED 3 (Key lights SCOLL LOCK LED when depressed)*/
            Reserved = 0x80		/*!< Bit 23: 80,00,00: Reserved*/
        }
        /// <summary>
        /// this class holds the possible high byte flags
        /// </summary>
        public static class FlagsHigh
        {
            public const byte NoFlag = 0x00;
            public const byte StickyOnce = 0x01;
            public const byte StickyPersist = 0x02;
            public const byte StickyLock = 0x04;
            public const byte Reserved1 = 0x08;
            public const byte LED1 = 0x10;
            public const byte LED2 = 0x20;
            public const byte LED3 = 0x40;
            public const byte Reserved = 0x80;

            public static bool isNormalkey(usbFlagsHigh b)
            {
                byte _b = (byte)(b & usbFlagsHigh.NoFlag);
                if (_b == (byte)usbFlagsHigh.NoFlag)
                    return true;
                else
                    return false;
            }
            public static bool isStickyOnce(usbFlagsHigh b)
            {
                if ((b & usbFlagsHigh.StickyOnce)== usbFlagsHigh.StickyOnce)
                    return true;
                else
                    return false;
            }
            public static bool isStickyPersist(usbFlagsHigh b)
            {
                if ((b & usbFlagsHigh.StickyPersist) == usbFlagsHigh.StickyPersist)
                    return true;
                else
                    return false;
            }
            public static bool isStickyLock(usbFlagsHigh b)
            {
                if ((b & usbFlagsHigh.StickyLock) == usbFlagsHigh.StickyLock)
                    return true;
                else
                    return false;
            }
            public static bool isReserved1(usbFlagsHigh b)
            {
                if ((b & usbFlagsHigh.Reserved1) == usbFlagsHigh.Reserved1)
                    return true;
                else
                    return false;
            }
            public static bool isLED1(usbFlagsHigh b)
            {
                if ((b & usbFlagsHigh.LED1) == usbFlagsHigh.LED1)
                    return true;
                else
                    return false;
            }
            public static bool isLED2(usbFlagsHigh b)
            {
                if ((b & usbFlagsHigh.LED2)== usbFlagsHigh.LED2)
                    return true;
                else
                    return false;
            }
            public static bool isLED3(usbFlagsHigh b)
            {
                if ((b  & usbFlagsHigh.LED3) == usbFlagsHigh.LED3)
                    return true;
                else
                    return false;
            }
            public static bool isReserved(usbFlagsHigh b)
            {
                if ((b & usbFlagsHigh.Reserved)== usbFlagsHigh.Reserved)
                    return true;
                else
                    return false;
            }

        }
        /// <summary>
        /// this class holds the possible mid byte flags
        /// </summary>
        public static class FlagsMid
        {
            public const byte NoFlag = 0x00;
            public const byte Extended = 0x01;
            public const byte NoRepeat = 0x02;
            public const byte Silent = 0x04;
            public const byte VKEY = 0x08;
            public const byte NOOP = 0x10;
            public const byte Shifted = 0x20;
            public const byte NoChord = 0x40;
            public const byte Reserved = 0x80;

            public static bool isNormalkey(usbFlagsMid b) // byte b)
            {
                byte _b = (byte)(b & usbFlagsMid.NoFlag);
                if (_b == (byte)usbFlagsMid.NoFlag)
                    return true;
                else
                    return false;
            }
            public static bool isExtendedKey(usbFlagsMid b)
            {
                if ((b & usbFlagsMid.Extended) == usbFlagsMid.Extended)
                    return true;
                else
                    return false;
            }
            public static bool isNoRepeat(usbFlagsMid b)
            {
                if (b == usbFlagsMid.NoRepeat)
                    return true;
                else
                    return false;
            }
            public static bool isSilentKey(usbFlagsMid b)
            {
                if (b == usbFlagsMid.Silent)
                    return true;
                else
                    return false;
            }
            public static bool isVkey(usbFlagsMid b)
            {
                if ((b & usbFlagsMid.VKEY) == usbFlagsMid.VKEY)
                    return true;
                else
                    return false;
            }
            public static bool isNoop(usbFlagsMid b)
            {
                if ((b & usbFlagsMid.NOOP) == usbFlagsMid.NOOP)
                    return true;
                else
                    return false;
            }
            public static bool isShifted(usbFlagsMid b)
            {
                if ((b & usbFlagsMid.Shifted)  == usbFlagsMid.Shifted)
                    return true;
                else
                    return false;
            }
            public static bool isNoChord(usbFlagsMid b)
            {
                if ((b & usbFlagsMid.NoChord) == usbFlagsMid.NoChord)
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// this class holds the possible low byte flags
        /// </summary>
        public static class FlagsLow
        {
            public const byte NormalKey = 0x00;
            public const byte ShiftKeyIndex = 0x01;
            public const byte NamedEventIndex = 0x02;
            public const byte MultiKeyIndex = 0x04;
            public const byte ModifierIndex = 0x08;
            public const byte AppLaunchKey = 0x10;
            public const byte FunctionKeyIndex = 0x20;
            public const byte RotateKeyIndex = 0x40;
            public const byte Reserved = 0x80;

            public static bool isNormalkey(CUsbKeyTypes.usbFlagsLow b)//( byte b)
            {
                byte _b = (byte)(b & usbFlagsLow.NormalKey);
                if (_b == (byte)usbFlagsLow.NormalKey)// usbFlags1.NormalKey)
                    return true;
                else
                    return false;
            }
            public static bool isShiftKey(CUsbKeyTypes.usbFlagsLow b)
            {
                if ((b & usbFlagsLow.ShiftKeyIndex) == usbFlagsLow.ShiftKeyIndex)
                    return true;
                else
                    return false;
            }
            public static bool isNamedEventKey(CUsbKeyTypes.usbFlagsLow b)
            {
                if ((b & usbFlagsLow.NamedEventIndex) == usbFlagsLow.NamedEventIndex)
                    return true;
                else
                    return false;
            }
            public static bool isMultiKey(CUsbKeyTypes.usbFlagsLow b)
            {
                if ((b & usbFlagsLow.MultiKeyIndex) == usbFlagsLow.MultiKeyIndex)
                    return true;
                else
                    return false;
            }
            public static bool isModifierKey(CUsbKeyTypes.usbFlagsLow b)
            {
                if ((b & usbFlagsLow.ModifierIndex) == usbFlagsLow.ModifierIndex)
                    return true;
                else
                    return false;
            }
            public static bool isAppLaunchKey(CUsbKeyTypes.usbFlagsLow b)
            {
                if ((b & usbFlagsLow.AppLaunchKey) == usbFlagsLow.AppLaunchKey)
                    return true;
                else
                    return false;
            }
            public static bool isFunctionKey(CUsbKeyTypes.usbFlagsLow b)
            {
                if ((b & usbFlagsLow.FunctionKeyIndex) == usbFlagsLow.FunctionKeyIndex)
                    return true;
                else
                    return false;
            }
            public static bool isRotateKey(CUsbKeyTypes.usbFlagsLow b)
            {
                if ((b & usbFlagsLow.RotateKeyIndex) == usbFlagsLow.RotateKeyIndex)
                    return true;
                else
                    return false;
            }
        }
        /*! \namespace ITC_KEYBOARD
            \class CUsbKeyTypes#HWkeys
            \enum HWkeys
            \brief This list of byte values provides the known USB hardware scancodes
         * see HUT (Hardware USB Usage Table) Hut1_12v2.pdf
        */
        public enum HWkeys : byte
        {
            System_Power = 0x81,
            System_Sleep = 0x82,
            System_Wake = 0x83,
            No_Event = 0x00,
            Overrun_Error = 0x01,
            POST_Fail = 0x02,
            Error_Undefined = 0x03,
            a = 0x04,
            b = 0x05,
            c = 0x06,
            d = 0x07,
            e = 0x08,
            f = 0x09,
            g = 0x0A,
            h = 0x0B,
            i = 0x0C,
            j = 0x0D,
            k = 0x0E,
            l = 0x0F,
            m = 0x10,
            n = 0x11,
            o = 0x12,
            p = 0x13,
            q = 0x14,
            r = 0x15,
            s = 0x16,
            t = 0x17,
            u = 0x18,
            v = 0x19,
            w = 0x1A,
            x = 0x1B,
            y = 0x1C,
            z = 0x1D,
            one = 0x1E,
            two = 0x1F,
            three = 0x20,
            four = 0x21,
            five = 0x22,
            six = 0x23,
            seven = 0x24,
            eight = 0x25,
            nine = 0x26,
            zero = 0x27,
            Return = 0x28,
            Escape = 0x29,
            Backspace = 0x2A,
            Tab = 0x2B,
            Space = 0x2C,
            Europe_1 = 0x32,
            A = 0x04,
            B = 0x05,
            C = 0x06,
            D = 0x07,
            E = 0x08,
            F = 0x09,
            G = 0x0A,
            H = 0x0B,
            I = 0x0C,
            J = 0x0D,
            K = 0x0E,
            L = 0x0F,
            M = 0x10,
            N = 0x11,
            O = 0x12,
            P = 0x13,
            Q = 0x14,
            R = 0x15,
            S = 0x16,
            T = 0x17,
            U = 0x18,
            V = 0x19,
            W = 0x1A,
            X = 0x1B,
            Y = 0x1C,
            Z = 0x1D,
            exclamation = 0x1E,
            at = 0x1F,
            hash = 0x20,
            dollar = 0x21,
            percent = 0x22,
            accentgrave = 0x23,
            ampersand = 0x24,
            asterisk = 0x25,
            leftbracket = 0x26,
            rightbracket = 0x27,
            minus = 0x2D,
            equal = 0x2E,
            leftsquarebracket = 0x2F,
            rightsquarebracket = 0x30,
            backslash = 0x31,
            semicolon = 0x33,
            backapostrophe = 0x34,
            leftsinglequote = 0x35,
            comma = 0x36,
            dot = 0x37,
            slash = 0x38,
            underline = 0x2D,
            plus = 0x2E,
            leftcurlybrace = 0x2F,
            rightcurlybrace = 0x30,
            verticalbar = 0x31,
            colon = 0x33,
            doublequote = 0x34,
            tilde = 0x35,
            less = 0x36,
            greater = 0x37,
            question = 0x38,
            Caps_Lock = 0x39,
            F1 = 0x3A,			/*!< F1 is equal to SoftKey1 on SmartPhone (see winuserm.h)!*/
            F2 = 0x3B,			/*!< F2 is equal to SoftKey2 on SmartPhone (see winuserm.h)!*/
            F3 = 0x3C,			/*!< F3 is equal to the Phone key (green key, start phone call) on SmartPhone (see winuserm.h)!*/
            F4 = 0x3D,			/*!< F4 is equal to Phone End key (red key, hangup) on SmartPhone (see winuserm.h)!*/
            F5 = 0x3E,			
            F6_VOL_UP = 0x3F,	/*!< F6 is equal to Volume UP key on SmartPhone (see winuserm.h)!*/
            F7_VOL_DN = 0x40,	/*!< F7 is equal to Volume DOWN key on SmartPhone (see winuserm.h)!*/		
            F8 = 0x41,
            F9 = 0x42,
            F10 = 0x43,
            F11 = 0x44,
            F12 = 0x45,
            Print_Screen = 0x46,
            Scroll_Lock = 0x47,
            Break = 0x48,
            Insert = 0x49,
            Home = 0x4A,
            Page_Up = 0x4B,
            Delete = 0x4C,
            End = 0x4D,
            Page_Down = 0x4E,
            Right_Arrow = 0x4F,
            Left_Arrow = 0x50,
            Down_Arrow = 0x51,
            Up_Arrow = 0x52,
            Keypad_Slash = 0x54,
            Keypad_Asterisk = 0x55,
            Keypad_Minus = 0x56,
            Pause = 0x48,
            Num_Lock = 0x53,
            Keypad_Plus = 0x57,
            Keypad_Enter = 0x58,
            Keypad_1 = 0x59,
            Keypad_2 = 0x5A,
            Keypad_3 = 0x5B,
            Keypad_4 = 0x5C,
            Keypad_5 = 0x5D,
            Keypad_6 = 0x5E,
            Keypad_7 = 0x5F,
            Keypad_8 = 0x60,
            Keypad_9 = 0x61,
            Keypad_0 = 0x62,
            Keypad_Dot = 0x63,
            Europe_2 = 0x64,
            App = 0x65,
            Keyboard_Power = 0x66,
            Keypad_Equal = 0x67,
            F13 = 0x68,
            F14 = 0x69,
            F15 = 0x6A,
            F16 = 0x6B,
            F17 = 0x6C,
            F18 = 0x6D,
            F19 = 0x6E,
            F20 = 0x6F,
            F21 = 0x70,
            F22 = 0x71,
            F23 = 0x72,
            F24 = 0x73,
            Keyboard_Execute = 0x74,
            Keyboard_Help = 0x75,
            Keyboard_Menu = 0x76,
            Keyboard_Select = 0x77,
            Keyboard_Stop = 0x78,
            Keyboard_Again = 0x79,
            Keyboard_Undo = 0x7A,
            Keyboard_Cut = 0x7B,
            Keyboard_Copy = 0x7C,
            Keyboard_Paste = 0x7D,
            Keyboard_Find = 0x7E,
            Keyboard_Mute = 0x7F,
            Keyboard_Volume_Up = 0x80,
            Keyboard_Volume_Down = 0x81,
            Keyboard_Locking_Caps_Lock = 0x82,
            Keyboard_Locking_Num_Lock = 0x83,
            Keyboard_Locking_Scroll_Lock = 0x84,
            Keypad_Comma = 0x85,
            Keyboard_Equal_Sign = 0x86,
            Keyboard_Intl_1 = 0x87,
            Keyboard_Intl_2 = 0x88,
            Keyboard_Intl_3 = 0x89,
            Action_OK = 0x8A,
            Keyboard_Intl_5 = 0x8B,
            Keyboard_Intl_6 = 0x8C,
            Keyboard_Intl_7 = 0x8D,
            Keyboard_Intl_8 = 0x8E,
            Keyboard_Intl_9 = 0x8F,
            SCAN_Button_KeyLang1 = 0x90,     // 'Keyboard_Lang_1'  used as Scan Button identifier
            Keyboard_Lang_2 = 0x91,
            Keyboard_Lang_3 = 0x92,
            Keyboard_Lang_4 = 0x93,
            Keyboard_Lang_5 = 0x94,
            Keyboard_Lang_6 = 0x95,
            Keyboard_Lang_7 = 0x96,
            Keyboard_Lang_8 = 0x97,
            Keyboard_Lang_9 = 0x98,
            Keyboard_Alternate_Erase = 0x99,
            Keyboard_SysReqAttention = 0x9A,
            Keyboard_Cancel = 0x9B,
            Keyboard_Clear = 0x9C,
            Keyboard_Prior = 0x9D,
            Keyboard_Return = 0x9E,
            Keyboard_Separator = 0x9F,
            Keyboard_Out = 0xA0,
            Keyboard_Oper = 0xA1,
            Keyboard_ClearAgain = 0xA2,
            Keyboard_CrSelProps = 0xA3,
            Keyboard_ExSel = 0xA4,
            RESERVED5A = 0xA5,
            RESERVEDDF = 0xDF,
            Left_Control = 0xE0,
            Left_Shift = 0xE1,
            Left_Alt = 0xE2,
            Left_GUI = 0xE3,
            Right_Control = 0xE4,
            Right_Shift = 0xE5,
            Right_Alt = 0xE6,
            Right_GUI = 0xE7,
            /*
            07,EA,00,00,00,05 'F1' [NoFlag,NoFlag,NormalKey,] 'F1'
            07,EB,00,00,00,06 'F2' [NoFlag,NoFlag,NormalKey,] 'F2'
            07,EC,00,00,00,04 'F3' [NoFlag,NoFlag,NormalKey,] 'F3'
            07,ED,00,00,00,0C 'F4' [NoFlag,NoFlag,NormalKey,] 'F4'
            07,EE,00,00,00,03 'F5' [NoFlag,NoFlag,NormalKey,] 'F5'
            07,EF,00,00,00,0B 'n/a' [NoFlag,NoFlag,NormalKey,] 'F6'
            07,F0,00,00,00,83 'n/a' [NoFlag,NoFlag,NormalKey,] 'F7'
            07,F1,00,00,00,0A 'n/a' [NoFlag,NoFlag,NormalKey,] 'F8'
            07,F2,00,00,00,01 'n/a' [NoFlag,NoFlag,NormalKey,] 'F9'
            07,F3,00,00,00,09 'n/a' [NoFlag,NoFlag,NormalKey,] 'F10'
            07,F4,00,00,00,78 'n/a' [NoFlag,NoFlag,NormalKey,] 'F11'
            07,F5,00,00,00,07 'n/a' [NoFlag,NoFlag,NormalKey,] 'F12'
            */    
            F1_cozumel = 0xEA,
            F2_cozumel = 0xEB,
            F3_cozumel = 0xEC,
            F4_cozumel = 0xED,
            F5_cozumel = 0xEE,
            F6_cozumel = 0xEF,
            F7_cozumel = 0xF0,
            F8_cozumel = 0xF1,
            F9_cozumel = 0xF2,
            F10_cozumel = 0xF3,
            F11_cozumel = 0xF4,
            F12_cozumel = 0xF5,

            Aqua_Plane = 0x8B,		/*!< changes the keyboard plane. Aqua and green can be exchanged*/
            Green_Plane = 0xB6,		/*!< changes the keyboard plane to the green/aqua one*/
            Orange_Plane = 0xE9,	/*!< changes the keyboard plane to the orange one*/
            RESERVEDE8 = 0xE8 //,
            //RESERVEDC000 = 0xc000
        }

    }
}
