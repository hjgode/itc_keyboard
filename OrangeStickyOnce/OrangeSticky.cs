using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using ITC_KEYBOARD;

namespace OrangeStickyOnce
{
    class OrangeSticky
    {
        ITC_KEYBOARD.CUSBkeys _cusbKeys;
        public OrangeSticky()
        {
            _cusbKeys = new ITC_KEYBOARD.CUSBkeys();
        }

        public stickyTypeEnum stickyType
        {
            get
            {
                return this.getOrangeSticky();
            }
            set
            {
                setOrangeSticky(value);
            }
        }
        public stickyTypeEnum getOrangeSticky()
        {
            stickyTypeEnum sType = stickyTypeEnum.stickyNone;
            CUSBkeys.usbKeyStruct orangeKey = new CUSBkeys.usbKeyStruct();
            int iIdx = _cusbKeys.getKeyStruct(0, CUsbKeyTypes.HWkeys.Orange_Plane, ref orangeKey);
            if (iIdx != -1)
            {
                if (((int)orangeKey.bFlagHigh & (int)CUsbKeyTypes.usbFlagsHigh.StickyLock) == (int)CUsbKeyTypes.usbFlagsHigh.StickyLock)
                    sType = stickyTypeEnum.stickyLock;
                else if (((int)orangeKey.bFlagHigh & (int)CUsbKeyTypes.usbFlagsHigh.StickyOnce) == (int)CUsbKeyTypes.usbFlagsHigh.StickyOnce)
                    sType = stickyTypeEnum.stickyOnce;
                else if (((int)orangeKey.bFlagHigh & (int)CUsbKeyTypes.usbFlagsHigh.StickyPersist) == (int)CUsbKeyTypes.usbFlagsHigh.StickyPersist)
                    sType = stickyTypeEnum.stickyPersist;
            }
            return sType;
        }
        BOOL setOrangeSticky(stickyTypeEnum sType){
            bool bRet = false;
            CUSBkeys.usbKeyStruct orangeKey = new CUSBkeys.usbKeyStruct();
            //get current key settings
            int iIdx = _cusbKeys.getKeyStruct(0, CUsbKeyTypes.HWkeys.Orange_Plane, ref orangeKey);
            if (iIdx != -1)
            {
                switch (sType)
                {
                    case stickyTypeEnum.stickyNone:
                        //reset bit 16,17,18 of high byte: 11111000
                        orangeKey.bFlagHigh &= (CUsbKeyTypes.usbFlagsHigh)0xF8;
                        break;
                    case stickyTypeEnum.stickyLock:
                        orangeKey.bFlagHigh |= CUsbKeyTypes.usbFlagsHigh.StickyLock;
                        break;
                    case stickyTypeEnum.stickyOnce:
                        orangeKey.bFlagHigh |= CUsbKeyTypes.usbFlagsHigh.StickyOnce;
                        break;
                    case stickyTypeEnum.stickyPersist:
                        orangeKey.bFlagHigh |= CUsbKeyTypes.usbFlagsHigh.StickyPersist;
                        break;
                }
                if (_cusbKeys.setKey(cPlanes.plane.normal, (int)CUsbKeyTypes.HWkeys.Orange_Plane, orangeKey) == 0)
                {
                    _cusbKeys.writeKeyTables();
                    bRet = true;
                }
            }

            return bRet;
        }

        [Flags]
        public enum stickyTypeEnum
        {
            stickyNone = 0,
            stickyOnce = 1,
            stickyPersist = 2,
            stickyLock = 4,
        }
    }
}
