using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using ITC_KEYBOARD;

namespace MapVol2Page
{
    class remapVol_Up_Down_to_Pg_Up_Down
    {
        ITC_KEYBOARD.CUSBkeys _cusbKeys;
        CUsbKeyTypes.HWkeys[] keysToMap;// = new CUsbKeyTypes.HWkeys[] { CUsbKeyTypes.HWkeys.Left_Control };
        Form1 _frm;

        public remapVol_Up_Down_to_Pg_Up_Down(Form1 frm)
        {
            _frm = frm;
            _cusbKeys = new CUSBkeys();
        }

        public void mapVolumeKeys()
        {
            try
            {
                CUSBkeys.usbKeyStruct uKeyVolDn = new CUSBkeys.usbKeyStruct();
                CUSBkeys.usbKeyStruct uKeyVolUp = new CUSBkeys.usbKeyStruct();

                cPlanes.plane plane = cPlanes.plane.green;

                addLog("Reading key mappings");
                _cusbKeys.getKeyStruct((int)plane, CUsbKeyTypes.HWkeys.F7_VOL_DN, ref uKeyVolDn);
                _cusbKeys.getKeyStruct((int)plane, CUsbKeyTypes.HWkeys.F6_VOL_UP, ref uKeyVolUp);
                
                addLog("Changing key structs");
                uKeyVolDn.bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
                uKeyVolDn.bFlagMid = CUsbKeyTypes.usbFlagsMid.VKEY;
                uKeyVolDn.bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
                uKeyVolDn.bIntScan = (byte)ITC_KEYBOARD.VKEY.VK_NEXT;                

                uKeyVolUp.bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
                uKeyVolUp.bFlagMid = CUsbKeyTypes.usbFlagsMid.VKEY;
                uKeyVolUp.bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
                uKeyVolUp.bIntScan = (byte)ITC_KEYBOARD.VKEY.VK_PRIOR;

                int iRes = 0;
                iRes = _cusbKeys.setKey(plane, (int)CUsbKeyTypes.HWkeys.F7_VOL_DN, uKeyVolDn);
                addLog("changing key mapping for VolDown="+iRes);
                iRes = _cusbKeys.setKey(plane, (int)CUsbKeyTypes.HWkeys.F6_VOL_UP, uKeyVolUp);
                addLog("changing key mapping for VolUp=" + iRes);

                iRes = _cusbKeys.writeKeyTables();
                addLog("changes saved=" + iRes);
            }
            catch (Exception ex)
            {
                addLog("Exception: " + ex.Message);
            }
        }
        public void restoreDefaultMappings()
        {
            CUSBkeys cusb = new CUSBkeys();
            int iRes = cusb.resetKeyDefaults();
            addLog("Restored defaults=" + iRes);
        }
        public void addLog(string text)
        {
            _frm.addLog(text);
        }

    }
}
