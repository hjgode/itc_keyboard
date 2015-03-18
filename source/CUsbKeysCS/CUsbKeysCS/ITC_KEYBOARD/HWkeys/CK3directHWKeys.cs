using System;
using System.Collections.Generic;
using System.Text;

namespace ITC_KEYBOARD
{
    public partial class CDirectHWKeys
    {
        private static class ck3directHWKeys
        {
            /// <summary>
            /// A list of hardware key indizes (0-7) for words 0 to 7 of the VKey HW table
            /// and the names used for the keys
            /// </summary>
            public static vkPair[] vkPairs = new vkPair[]{
                new vkPair( 0x00, "ndef"),
                new vkPair( 0x01, "ndef"),
                new vkPair( 0x02, "PTT"),
                new vkPair( 0x03, "SCAN"),
                new vkPair( 0x04, "ndef"),
                new vkPair( 0x05, "ndef"),
                new vkPair( 0x06, "ndef"),
                new vkPair( 0x07, "ndef"),
            };
        }
    }
}