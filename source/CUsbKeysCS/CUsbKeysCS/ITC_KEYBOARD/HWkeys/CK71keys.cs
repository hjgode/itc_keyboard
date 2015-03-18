using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using EventNamespace;

namespace ITC_KEYBOARD
{
    public partial class CUsbHwKeys
    {
        public static class ck70Keys
        {
            public static vkPair[] vkPairs = new vkPair[]{
                // Numeric Keys
                //      Page  Id    Name
                new vkPair(0x1E, "1"),
                new vkPair(0x1F, "2"),
                new vkPair(0x20, "3"),
                new vkPair(0x21, "4"),
                new vkPair(0x22, "5"),
                new vkPair(0x23, "6"),
                new vkPair(0x24, "7"),
                new vkPair(0x25, "8"),
                new vkPair(0x26, "9"),
                new vkPair(0x27, "0"),

                // Alpha Keys
                new vkPair(0x04, "A"),
                new vkPair(0x05, "B"),
                new vkPair(0x06, "C"),
                new vkPair(0x07, "D"),
                new vkPair(0x08, "E"),
                new vkPair(0x09, "F"),
                new vkPair(0x0A, "G"),
                new vkPair(0x0B, "H"),
                new vkPair(0x0C, "I"),
                new vkPair(0x0D, "J"),
                new vkPair(0x0E, "K"),
                new vkPair(0x0F, "L"),
                new vkPair(0x10, "M"),
                new vkPair(0x11, "N"),
                new vkPair(0x12, "O"),
                new vkPair(0x13, "P"),
                new vkPair(0x14, "Q"),
                new vkPair(0x15, "R"),
                new vkPair(0x16, "S"),
                new vkPair(0x17, "T"),
                new vkPair(0x18, "U"),
                new vkPair(0x19, "V"),
                new vkPair(0x1A, "W"),
                new vkPair(0x1B, "X"),
                new vkPair(0x1C, "Y"),
                new vkPair(0x1D, "Z"),

                // Punctuation and special keys
                new vkPair(0x28, "RETURN"),
                new vkPair(0x29, "ESCAPE"),
                new vkPair(0x2A, "BACKSPACE"),
                new vkPair(0x2B, "TAB"),
                new vkPair(0x2C, "SPACE"),
                new vkPair(0x2D, "DASH"),
                new vkPair(0x2E, "EQUAL"),
                new vkPair(0x2F, "LSQRBRACKET"),
                new vkPair(0x30, "RSQRBRACKET"),
                new vkPair(0x31, "BACKSLASH"),
                new vkPair(0x33, "SEMICOLON"),
                new vkPair(0x34, "QUOTE"),
                new vkPair(0x35, "GRAVEACCENT"),
                new vkPair(0x36, "COMMA"),
                new vkPair(0x37, "PERIOD"),
                new vkPair(0x38, "SLASH"),
                new vkPair(0x39, "CAPSLOCK"),
                new vkPair(0x58, "KEYPAD_ENTER"),

                // Function Keys
                new vkPair(0xEA, "F1"),
                new vkPair(0xEB, "F2"),
                new vkPair(0xEC, "F3"),
                new vkPair(0xED, "F4"),
                new vkPair(0xEE, "F5"),
                new vkPair(0xEF, "F6"),
                new vkPair(0xF0, "F7"),
                new vkPair(0xF1, "F8"),
                new vkPair(0xF2, "F9"),
                new vkPair(0xF3, "F10"),
                new vkPair(0xF4, "F11"),
                new vkPair(0xF5, "F12"),

                // Windows Mobile Keys
                new vkPair(0x3A, "TSOFTKEY1"),
                new vkPair(0x3B, "TSOFTKEY2"),
                new vkPair(0x3C, "TALK"),
                new vkPair(0x3D, "TEND"),
                new vkPair(0x3F, "TVOLUMEUP"),
                new vkPair(0x40, "TVOLUMEDOWN"),
                new vkPair(0x41, "TSTAR"),
                new vkPair(0x42, "TPOUND"),
                new vkPair(0x43, "TRECORD"),
                new vkPair(0x44, "SYMBOL"),

                // Navagation and Control
                new vkPair(0x4A, "HOME"),
                new vkPair(0x4B, "PAGEUP"),
                new vkPair(0x4C, "DELETE"),
                new vkPair(0x4D, "END"),
                new vkPair(0x4E, "PAGEDOWN"),
                new vkPair(0x4F, "RIGHTARROW"),
                new vkPair(0x50, "LEFTARROW"),
                new vkPair(0x51, "DOWNARROW"),
                new vkPair(0x52, "UPARROW"),
                new vkPair(0x53, "NUMLOCK"),
                new vkPair(0xE0, "LEFTCONTROL"),
                new vkPair(0xE1, "LEFTSHIFT"),
                new vkPair(0xE2, "LEFTALT"),
                new vkPair(0xE3, "INTERMEC"),
                new vkPair(0xE4, "RIGHTCONTROL"),
                new vkPair(0xE5, "RIGHTSHIFT"),
                new vkPair(0xE6, "RIGHTALT"),
                new vkPair(0xE7, "RIGHTGUI"),

                // Intermec Specific Keys
                new vkPair(0x90, "SCAN"),
                new vkPair(0x91, "LSIDELOW"),
                new vkPair(0xE8, "FLD_EXIT"),
#region extern codes
//end provided by Ruben #################################
                //new vkPair ( 0x04, "a" ), //"VK_A"
                //    new vkPair ( 0x05, "b" ), //"VK_B"
                //    new vkPair ( 0x06, "c" ), //"VK_C"
                //    new vkPair ( 0x07, "d" ), //"VK_D"
                //    new vkPair ( 0x08, "e" ), //"VK_E"
                //    new vkPair ( 0x09, "f" ), //"VK_F"
                //    new vkPair ( 0x0A, "g" ), //"VK_G"
                //    new vkPair ( 0x0B, "h" ), //"VK_H"
                //    new vkPair ( 0x0C, "i" ), //"VK_I"
                //    new vkPair ( 0x0D, "j" ), //"VK_J"
                //    new vkPair ( 0x0E, "k" ), //"VK_K"
                //    new vkPair ( 0x0F, "l" ), //"VK_L"
                //    new vkPair ( 0x10, "m" ), //"VK_M"
                //    new vkPair ( 0x11, "n" ), //"VK_N"
                //    new vkPair ( 0x12, "o" ), //"VK_O"
                //    new vkPair ( 0x13, "p" ), //"VK_P"
                //    new vkPair ( 0x14, "q" ), //"VK_Q"
                //    new vkPair ( 0x15, "r" ), //"VK_R"
                //    new vkPair ( 0x16, "s" ), //"VK_S"
                //    new vkPair ( 0x17, "t" ), //"VK_T"
                //    new vkPair ( 0x18, "u" ), //"VK_U"
                //    new vkPair ( 0x19, "v" ), //"VK_V"
                //    new vkPair ( 0x1A, "w" ), //"VK_W"
                //    new vkPair ( 0x1B, "x" ), //"VK_X"
                //    new vkPair ( 0x1C, "y" ), //"VK_Y"
                //    new vkPair ( 0x1D, "z" ), //"VK_Z"
                //    new vkPair ( 0x1E, "1" ),
                //    new vkPair ( 0x1F, "2" ),
                //    new vkPair ( 0x20, "3" ),
                //    new vkPair ( 0x21, "4" ),
                //    new vkPair ( 0x22, "5" ),
                //    new vkPair ( 0x23, "6" ),
                //    new vkPair ( 0x24, "7" ),
                //    new vkPair ( 0x25, "8" ),
                //    new vkPair ( 0x26, "9" ),
                //    new vkPair ( 0x27, "0" ),
                //    new vkPair ( 0x28, "Enter" ), //"VK_RETURN"
                //    new vkPair ( 0x29, "ESC" ),
                //    new vkPair ( 0x29, "Escape" ), //"VK_ESCAPE"
                //    new vkPair ( 0x2A, "Backspace" ), //"VK_BACK"
                //    new vkPair ( 0x2B, "Tab" ), //"VK_TAB"
                //    new vkPair ( 0x2C, "Space" ), //"VK_SPACE"
                //    new vkPair ( 0x37, "." ),
                //    /*
                //     * Normally, USB HID values are used, but the CK71 uses different values for function keys !!!!!
                //     * 
                //        07,EA,00,00,00,05 'F1' [NoFlag,NoFlag,NormalKey,] 'F1'
                //        07,EB,00,00,00,06 'F2' [NoFlag,NoFlag,NormalKey,] 'F2'
                //        07,EC,00,00,00,04 'F3' [NoFlag,NoFlag,NormalKey,] 'F3'
                //        07,ED,00,00,00,0C 'F4' [NoFlag,NoFlag,NormalKey,] 'F4'
                //        07,EE,00,00,00,03 'F5' [NoFlag,NoFlag,NormalKey,] 'F5'
                //        07,EF,00,00,00,0B 'n/a' [NoFlag,NoFlag,NormalKey,] 'F6'
                //        07,F0,00,00,00,83 'n/a' [NoFlag,NoFlag,NormalKey,] 'F7'
                //        07,F1,00,00,00,0A 'n/a' [NoFlag,NoFlag,NormalKey,] 'F8'
                //        07,F2,00,00,00,01 'n/a' [NoFlag,NoFlag,NormalKey,] 'F9'
                //        07,F3,00,00,00,09 'n/a' [NoFlag,NoFlag,NormalKey,] 'F10'
                //        07,F4,00,00,00,78 'n/a' [NoFlag,NoFlag,NormalKey,] 'F11'
                //        07,F5,00,00,00,07 'n/a' [NoFlag,NoFlag,NormalKey,] 'F12'
                //    */
                //    new vkPair ( 0xEA, "F1 (Soft1)" ), //"VK_F1"
                //    new vkPair ( 0xEB, "F2 (Soft2)" ), //"VK_F2"
                //    new vkPair ( 0xEC, "F3 (Send)" ), //"VK_F3"
                //    new vkPair ( 0xED, "F4 (End)" ), //"VK_F4"
                //    new vkPair ( 0xEE, "F5" ), //"VK_F5"
                //    new vkPair ( 0xEF, "F6 (VolUp)" ), //"VK_F6"
                //    new vkPair ( 0xF0, "F7 (VolDn)" ), //"VK_F7"
                //    new vkPair ( 0xF1, "F8" ), //"VK_F8"
                //    new vkPair ( 0xF2, "F9" ), //"VK_F9"
                //    new vkPair ( 0xF3, "F10" ), //"VK_F10"
                //    new vkPair ( 0xF4, "F11" ), //"VK_F11"
                //    new vkPair ( 0xF5, "F12" ), //"VK_F12"
                    
                //    new vkPair ( 0x3D, "END" ), //F4 added for compatibility
                //    new vkPair ( 0x3F, "upper right side button" ),
                //    new vkPair ( 0x40, "lower right side button" ),
                //    new vkPair ( 0x41, "*" ),
                //    new vkPair ( 0x42, "#" ),
                //    new vkPair ( 0x43, "upper left side button" ),
                //    new vkPair ( 0x51, "CURSOR DOWN" ),
                //    new vkPair ( 0x52, "CURSOR UP" ),
                //    new vkPair ( 0x81, "Power Button?" ), //not verified
                //    new vkPair ( 0x87, "light/zero" ), //"undef 0x9C-light"
                //    new vkPair ( 0x8b, "Green Shift Button" ),
                //    new vkPair ( 0x90, "Front Scan Button" ),
                //    new vkPair ( 0x91, "lower left side button" ),
                //    new vkPair ( 0xE1, "Left Shift" ), //"VK_SHIFT"
#endregion
                new vkPair ( 0xB6, "Aqua Shift Button" ), //not verified
                new vkPair ( 0xE9, "Orange Plane" ) //"undef 0x99"      
            };
        }
    }
}
