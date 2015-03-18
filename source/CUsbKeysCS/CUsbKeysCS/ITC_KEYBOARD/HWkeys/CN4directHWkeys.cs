using System;
using System.Collections.Generic;
using System.Text;

namespace ITC_KEYBOARD
{
    public partial class CDirectHWKeys
    {
        private static class cn4directHWKeys
        {
            /// <summary>
            /// A list of hardware key indizes (0-7) for words 0 to 7 of the VKey HW table
            /// and the names used for the keys
            /// </summary>
            public static vkPair[] vkPairs = new vkPair[]{
                new vkPair( 0x00, "ndef"),
                new vkPair( 0x01, "ndef"),
                new vkPair( 0x02, "upper Left"), //upper left side
                new vkPair( 0x03, "lower Left"),//lower left side
                new vkPair( 0x04, "upper Right"),//upper right side
                new vkPair( 0x05, "lower Right"),//lower right side
                new vkPair( 0x06, "ndef"),
                new vkPair( 0x07, "ndef"),
            };
        }
    }
}