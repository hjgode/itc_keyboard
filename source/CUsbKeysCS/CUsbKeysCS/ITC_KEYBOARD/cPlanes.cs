using System;
using System.Collections.Generic;
using System.Text;

namespace ITC_KEYBOARD
{
    /// <summary>
    /// class to access keyboard planes
    /// </summary>
    public static class cPlanes
    {
        /// <summary>
        /// the possible plane names and there int values
        /// </summary>
        public enum plane : int
        {
            normal = 0x00,
            orange = 0x01,
            aqua   = 0x02,
            green  = 0x02
        }
        public static int ToInt(plane p)
        {
            return (int)p;
        }
    }
}
