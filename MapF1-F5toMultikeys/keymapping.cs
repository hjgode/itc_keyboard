using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using ITC_KEYBOARD;

namespace MapF1_F5toMultikeys
{
    class keymapping
    {
        CUSBkeys.usbKeyStruct[] hwKeys = new CUSBkeys.usbKeyStruct[10]{
            new CUSBkeys.cusbKeyStruct(0x07, CUsbKeyTypes.HWkeys.F1_cozumel, CUsbKeyTypes.usbFlagsHigh.NoFlag, CUsbKeyTypes.usbFlagsMid.NoFlag, CUsbKeyTypes.usbFlagsLow.MultiKeyIndex, 0x00).usbKeyStruct,
            new CUSBkeys.cusbKeyStruct(0x07, CUsbKeyTypes.HWkeys.F2_cozumel, CUsbKeyTypes.usbFlagsHigh.NoFlag, CUsbKeyTypes.usbFlagsMid.NoFlag, CUsbKeyTypes.usbFlagsLow.MultiKeyIndex, 0x00).usbKeyStruct,
            new CUSBkeys.cusbKeyStruct(0x07, CUsbKeyTypes.HWkeys.F3_cozumel, CUsbKeyTypes.usbFlagsHigh.NoFlag, CUsbKeyTypes.usbFlagsMid.NoFlag, CUsbKeyTypes.usbFlagsLow.MultiKeyIndex, 0x00).usbKeyStruct,
            new CUSBkeys.cusbKeyStruct(0x07, CUsbKeyTypes.HWkeys.F4_cozumel, CUsbKeyTypes.usbFlagsHigh.NoFlag, CUsbKeyTypes.usbFlagsMid.NoFlag, CUsbKeyTypes.usbFlagsLow.MultiKeyIndex, 0x00).usbKeyStruct,
            new CUSBkeys.cusbKeyStruct(0x07, CUsbKeyTypes.HWkeys.F5_cozumel, CUsbKeyTypes.usbFlagsHigh.NoFlag, CUsbKeyTypes.usbFlagsMid.NoFlag, CUsbKeyTypes.usbFlagsLow.MultiKeyIndex, 0x00).usbKeyStruct,
            new CUSBkeys.cusbKeyStruct(0x07, CUsbKeyTypes.HWkeys.F6_cozumel, CUsbKeyTypes.usbFlagsHigh.NoFlag, CUsbKeyTypes.usbFlagsMid.NoFlag, CUsbKeyTypes.usbFlagsLow.MultiKeyIndex, 0x00).usbKeyStruct,
            new CUSBkeys.cusbKeyStruct(0x07, CUsbKeyTypes.HWkeys.F7_cozumel, CUsbKeyTypes.usbFlagsHigh.NoFlag, CUsbKeyTypes.usbFlagsMid.NoFlag, CUsbKeyTypes.usbFlagsLow.MultiKeyIndex, 0x00).usbKeyStruct,
            new CUSBkeys.cusbKeyStruct(0x07, CUsbKeyTypes.HWkeys.F8_cozumel, CUsbKeyTypes.usbFlagsHigh.NoFlag, CUsbKeyTypes.usbFlagsMid.NoFlag, CUsbKeyTypes.usbFlagsLow.MultiKeyIndex, 0x00).usbKeyStruct,
            new CUSBkeys.cusbKeyStruct(0x07, CUsbKeyTypes.HWkeys.F9_cozumel, CUsbKeyTypes.usbFlagsHigh.NoFlag, CUsbKeyTypes.usbFlagsMid.NoFlag, CUsbKeyTypes.usbFlagsLow.MultiKeyIndex, 0x00).usbKeyStruct,
            new CUSBkeys.cusbKeyStruct(0x07, CUsbKeyTypes.HWkeys.F10_cozumel, CUsbKeyTypes.usbFlagsHigh.NoFlag, CUsbKeyTypes.usbFlagsMid.NoFlag, CUsbKeyTypes.usbFlagsLow.MultiKeyIndex, 0x00).usbKeyStruct,
        };


        CUSBkeys.usbKeyStructShort defaultKeyStructShort = new CUSBkeys.usbKeyStructShort();
        CUSBkeys.usbKeyStructShort[][] charSequences= new CUSBkeys.usbKeyStructShort[10][]{
            new CUSBkeys.usbKeyStructShort[]{
            new CUSBkeys.cusbKeyStructShort(CUsbKeyTypes.usbFlagsHigh.NoFlag, CUsbKeyTypes.usbFlagsMid.VKEY,CUsbKeyTypes.usbFlagsLow.NormalKey, 0x00).shortStruct,
            },
            new CUSBkeys.usbKeyStructShort[]{
            new CUSBkeys.cusbKeyStructShort(CUsbKeyTypes.usbFlagsHigh.NoFlag, CUsbKeyTypes.usbFlagsMid.VKEY,CUsbKeyTypes.usbFlagsLow.NormalKey, 0x00).shortStruct,
            },
            new CUSBkeys.usbKeyStructShort[]{
            new CUSBkeys.cusbKeyStructShort(CUsbKeyTypes.usbFlagsHigh.NoFlag, CUsbKeyTypes.usbFlagsMid.VKEY,CUsbKeyTypes.usbFlagsLow.NormalKey, 0x00).shortStruct,
            },
            new CUSBkeys.usbKeyStructShort[]{
            new CUSBkeys.cusbKeyStructShort(CUsbKeyTypes.usbFlagsHigh.NoFlag, CUsbKeyTypes.usbFlagsMid.VKEY,CUsbKeyTypes.usbFlagsLow.NormalKey, 0x00).shortStruct,
            },
            new CUSBkeys.usbKeyStructShort[]{
            new CUSBkeys.cusbKeyStructShort(CUsbKeyTypes.usbFlagsHigh.NoFlag, CUsbKeyTypes.usbFlagsMid.VKEY,CUsbKeyTypes.usbFlagsLow.NormalKey, 0x00).shortStruct,
            },
            new CUSBkeys.usbKeyStructShort[]{
            new CUSBkeys.cusbKeyStructShort(CUsbKeyTypes.usbFlagsHigh.NoFlag, CUsbKeyTypes.usbFlagsMid.VKEY,CUsbKeyTypes.usbFlagsLow.NormalKey, 0x00).shortStruct,
            },
            new CUSBkeys.usbKeyStructShort[]{
            new CUSBkeys.cusbKeyStructShort(CUsbKeyTypes.usbFlagsHigh.NoFlag, CUsbKeyTypes.usbFlagsMid.VKEY,CUsbKeyTypes.usbFlagsLow.NormalKey, 0x00).shortStruct,
            },
            new CUSBkeys.usbKeyStructShort[]{
            new CUSBkeys.cusbKeyStructShort(CUsbKeyTypes.usbFlagsHigh.NoFlag, CUsbKeyTypes.usbFlagsMid.VKEY,CUsbKeyTypes.usbFlagsLow.NormalKey, 0x00).shortStruct,
            },
            new CUSBkeys.usbKeyStructShort[]{
            new CUSBkeys.cusbKeyStructShort(CUsbKeyTypes.usbFlagsHigh.NoFlag, CUsbKeyTypes.usbFlagsMid.VKEY,CUsbKeyTypes.usbFlagsLow.NormalKey, 0x00).shortStruct,
            },
            new CUSBkeys.usbKeyStructShort[]{
            new CUSBkeys.cusbKeyStructShort(CUsbKeyTypes.usbFlagsHigh.NoFlag, CUsbKeyTypes.usbFlagsMid.VKEY,CUsbKeyTypes.usbFlagsLow.NormalKey, 0x00).shortStruct,
            },

        };

        string[] chrSequences = new string[10];
        public keymapping()
        {
            chrSequences[0] = "26D15000";
            chrSequences[1] = "26D15000";
            chrSequences[2] = "26D15000";
            chrSequences[3] = "26D15000";
            chrSequences[4] = "26D15000"; //f5
            chrSequences[5] = "26D2999";
            chrSequences[6] = "26D2999";
            chrSequences[7] = "26D2999";
            chrSequences[8] = "26D2999";
            chrSequences[9] = "26D2999";
        }
    }
}
