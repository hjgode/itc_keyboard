using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace OrangeStickyOnce
{
    class Program
    {
        static void Main(string[] args)
        {
            OrangeSticky oSticky = new OrangeSticky();
            OrangeSticky.stickyTypeEnum stickyOld = oSticky.getOrangeSticky();
            System.Diagnostics.Debug.WriteLine("Orange Sticky is: " + oSticky.getOrangeSticky().ToString());

            //set sticky once (and save)
            oSticky.stickyType = OrangeSticky.stickyTypeEnum.stickyOnce;

        }
    }
}
