using System;
using System.Collections.Generic;
using System.Text;

namespace ITC_KEYBOARD
{
    /// <summary>
    /// class to provide lists of directKey hardware key codes
    /// </summary>
    public partial class CDirectHWKeys
    {
        /// <summary>
        /// list of vkPair structures 
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
        /// init a list of possible DirectKeys values 
        /// </summary>
        public CDirectHWKeys()
        {
            string sPlatform = ITC_Tools.getPlatformModel();
            //the keys
            if (sPlatform == "CN3")
            {
                _vkPairs = cn3directHWKeys.vkPairs;
                //the planes
            }
            else if (sPlatform == "CN4") //no direct HW keys supported!
            {
                _vkPairs = cn4directHWKeys.vkPairs;
            }
            else if (sPlatform == "CN50") //no direct HW keys supported!
            {
                _vkPairs = null;
            }
            else if (sPlatform == "CK3")
            {
                string s = ITC_Tools.getModelNumber();
                _vkPairs=ck3directHWKeys.vkPairs;
            }
            else if (sPlatform.StartsWith("7"))
            {
                _vkPairs=i700directHWKeys.vkPairs;
            }
        }
    }
}
