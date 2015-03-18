using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using ITC_KEYBOARD;

namespace Orange2Ctrl
{
    class mapKeyOrange
    {
        CUSBkeys cusb = new CUSBkeys();
        public int mapKeyOrange2Ctrl(bool bSticky)
        {
            CUSBkeys.usbKeyStruct key=new CUSBkeys.usbKeyStruct();
            int iRes2 = -1;
            int iRes1 = cusb.getKeyStruct((int)cPlanes.plane.normal, CUsbKeyTypes.HWkeys.Orange_Plane, ref key);
            if (iRes1 != -1)
            {
                if (bSticky)
                {
                    //find the index of the Ctrl modifier in the modifier list
                    int iModiCtrlIndex = findCtrlModifier();
                    key.bFlagHigh = CUsbKeyTypes.usbFlagsHigh.StickyOnce | CUsbKeyTypes.usbFlagsHigh.LED2;
                    key.bFlagMid = CUsbKeyTypes.usbFlagsMid.NoRepeat;
                    key.bFlagLow = CUsbKeyTypes.usbFlagsLow.ModifierIndex;
                    key.bIntScan = (byte)iModiCtrlIndex;
                }
                else
                {
                    key.bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
                    key.bFlagMid = CUsbKeyTypes.usbFlagsMid.VKEY;
                    key.bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
                    key.bIntScan = (byte)VKEY.VK_CONTROL;
                }
                iRes2 = cusb.setKey((int)cPlanes.plane.normal, CUsbKeyTypes.HWkeys.Orange_Plane, key);
                if (iRes2 != -1)
                    iRes2 = cusb.writeKeyTables();
                else
                    iRes1 = -1;
            }
            return iRes1;

        }

        public int restoreDefault()
        {
            int iRes = cusb.resetKeyDefaults();
            return iRes;
        }

        /// <summary>
        /// find the Modifiers Entry for the "Control Key"
        /// creates a new Modifier Entry if no entry is found
        /// </summary>
        /// <returns>-1 if not found
        /// pos value is index of found/created entry</returns>
        private int findCtrlModifier()
        {
            //  08,02,01,14 'Left Control'
            //assemble the Ctrl modifier entry: 01,02,08,14[StickyOnce,NoRepeat,ModifierIndex,] 'Left Control'
            CUSBkeys.usbKeyStructShort uKey = CUSBkeys.controlModKey;// new CUSBkeys.usbKeyStructShort();
            int iFound = -1;
            CModifiersKeys cModis = new CModifiersKeys();
            iFound = cModis.findModifierKey(uKey);

            if (iFound == -1)
                iFound = cModis.addModifierKey(uKey);

            return iFound;
        }
    }
}
