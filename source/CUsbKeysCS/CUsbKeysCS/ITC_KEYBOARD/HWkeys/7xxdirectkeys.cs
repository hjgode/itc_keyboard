using System;
using System.Collections.Generic;
using System.Text;

namespace ITC_KEYBOARD
{
    public partial class CDirectHWKeys
    {
        private static class i700directHWKeys
        {
            /// <summary>
            /// A list of hardware key indizes (0-7) for words 0 to 7 of the VKey HW table
            /// and the names used for the keys
            /// </summary>
            public static vkPair[] vkPairs = new vkPair[]{
                //Scancode Key, "Meaning" 
                new vkPair( 0x00, "Reserved" ),
                new vkPair( 0x01, "I/O Button" ),
                new vkPair( 0x02, "Scanner Trigger" ),
                new vkPair( 0x03, "Scanner Left" ),
                new vkPair( 0x04, "Scanner Right" ),
                new vkPair( 0x05, "." ),
                new vkPair( 0x06, "4" ),
                new vkPair( 0x07, "None" ),
                new vkPair( 0x08, "Left Arrow" ),
                new vkPair( 0x09, "None" ),
                new vkPair( 0x0A, "Backspace" ),
                new vkPair( 0x0B, "Gold Key" ),
                new vkPair( 0x0C, "None" ),
                new vkPair( 0x0D, "ESC" ),
                new vkPair( 0x0E, "Down Arrow" ),
                new vkPair( 0x0F, "1" ),
                new vkPair( 0x10, "7" ),
                new vkPair( 0x11, "Alpha Key" ),
                new vkPair( 0x12, "None" ),
                new vkPair( 0x13, "Up Arrow" ),
                new vkPair( 0x14, "Right Arrow" ),
                new vkPair( 0x15, "2" ),
                new vkPair( 0x16, "8" ),
                new vkPair( 0x17, "0" ),
                new vkPair( 0x18, "5" ),
                new vkPair( 0x19, "None" ),
                new vkPair( 0x1A, "Action Key" ),
                new vkPair( 0x1B, "3" ),
                new vkPair( 0x1C, "9" ),
                new vkPair( 0x1D, "ENTER" ),
                new vkPair( 0x1E, "6" ),
                new vkPair( 0x1F, "None" ),
                new vkPair( 0x20, "None" ),
                new vkPair( 0x21, "None" ),
                new vkPair( 0x22, "None" ),
                new vkPair( 0x23, "None" ),
                new vkPair( 0x24, "None" ),
                new vkPair( 0x25, "None" ),
                new vkPair( 0x26, "None" ),
                new vkPair( 0x27, "None" ),
                new vkPair( 0x28, "None" ),
                new vkPair( 0x29, "None" ),
                new vkPair( 0x2A, "None" ),
                new vkPair( 0x2B, "None" ),
                new vkPair( 0x2C, "None" ),
                new vkPair( 0x2D, "None" ),
                new vkPair( 0x2E, "None" ),
                new vkPair( 0x2F, "None" ),
                new vkPair( 0x30, "None" ),
                new vkPair( 0x31, "None" ),
                new vkPair( 0x32, "None" ),
                new vkPair( 0x33, "None" ),
                new vkPair( 0x34, "None" ),
                new vkPair( 0x35, "None" ),
                new vkPair( 0x36, "None" ),
                new vkPair( 0x37, "None" ),
                new vkPair( 0x38, "None" ),
                new vkPair( 0x39, "None" ),
                new vkPair( 0x3A, "None" ),
                new vkPair( 0x3B, "None" ),
                new vkPair( 0x3C, "None" ),
                new vkPair( 0x3D, "None" ),
                new vkPair( 0x3E, "None" ),
                new vkPair( 0x3F, "None" ),
                new vkPair( 0x40, "None" ),
                new vkPair( 0x41, "Charge Detect" ),
                new vkPair( 0x42, "LCD Frontlight Ambient Light Threshold Crossed" ),
                new vkPair( 0x43, "Headset Detected" ),
                new vkPair( 0x44, "Keypad Backlight Ambient Light Threshold Crossed" ),
            };
        }
    }
}