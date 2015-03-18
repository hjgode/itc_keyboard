using System;
using System.Collections.Generic;
using System.Text;

namespace ITC_KEYBOARD
{
    /// <summary>
    /// class to manage USB HID hardware keys and key lists
    /// </summary>
    public partial class CUsbHwKeys
    {
        /// <summary>
        /// list of vkPair values
        /// </summary>
        private vkPair[] _vkPairs;
        /// <summary>
        /// get the list of defined USB HID hardware keys
        /// </summary>
        public vkPair[] vkPairs
        {
            get { return _vkPairs; }
        }
        
        /// <summary>
        /// constructor to init the USB HID hardware key lists
        /// </summary>
        public CUsbHwKeys()
        {
            string sPlatform = ITC_Tools.getPlatformModel();
            //the keys
            if (sPlatform == "CN3" || sPlatform == "CN4")
            {
                _vkPairs = cn3Keys.vkPairs;
                //the planes
            }
            else if (sPlatform == "CN50")
            {
                _vkPairs = cn50Keys.vkPairs;
            }
            else if (sPlatform.StartsWith("CK3"))
            {
                string s = ITC_Tools.getModelNumber();
                if (s.StartsWith("CK3B3"))
                    _vkPairs = ck3KeysRetail.vkPairs;
                else
                    _vkPairs = ck3Keys.vkPairs;
            }
            else if (sPlatform.StartsWith("CK71"))
            {
                string s = ITC_Tools.getModelNumber();
                _vkPairs = ck70Keys.vkPairs;
            }
            else
            { //use generic table
                _vkPairs = cn3Keys.vkPairs;
            }
        }
    }
}
