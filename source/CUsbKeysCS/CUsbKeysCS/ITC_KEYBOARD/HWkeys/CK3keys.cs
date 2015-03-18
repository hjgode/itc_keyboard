
namespace ITC_KEYBOARD
{
    public partial class CUsbHwKeys
    {
        private static class ck3Keys
        {
            //public struct vkPair
            //{
            //    public int iVal;
            //    public string sKey;
            //    public vkPair(int i, string s)
            //    {
            //        this.iVal = i;
            //        this.sKey = s;
            //    }
            //    public override string ToString()
            //    {
            //        return this.sKey;
            //    }
            //    public string ToString(byte b)
            //    {
            //        //find name for key value
            //        foreach (vkPair vkPar in vkPairs)
            //        {
            //            if (vkPar.iVal == b)
            //                return vkPar.sKey;
            //        }
            //        return "undef";
            //    }
            //}
            public static vkPair[] vkPairs = new vkPair[]{
                new vkPair( 0x04, "A"),
                new vkPair( 0x05, "B"),
                new vkPair( 0x06, "C"),
                new vkPair( 0x07, "D"),
                new vkPair( 0x08, "E"),
                new vkPair( 0x09, "F"),
                new vkPair( 0x0A, "G"),
                new vkPair( 0x0B, "H"),
                new vkPair( 0x0C, "I"),
                new vkPair( 0x0D, "J"),
                new vkPair( 0x0E, "K"),
                new vkPair( 0x0F, "L"),
                new vkPair( 0x10, "M"),
                new vkPair( 0x11, "N"),
                new vkPair( 0x12, "O"),
                new vkPair( 0x13, "P"),
                new vkPair( 0x14, "Q"),
                new vkPair( 0x15, "R"),
                new vkPair( 0x16, "S"),
                new vkPair( 0x17, "T"),
                new vkPair( 0x18, "U"),
                new vkPair( 0x19, "V"),
                new vkPair( 0x1A, "W"),
                new vkPair( 0x1B, "X"),
                new vkPair( 0x1C, "Y"),
                new vkPair( 0x1D, "Z"),
                new vkPair( 0x1E, "1"),
                new vkPair( 0x1F, "2"),
                new vkPair( 0x20, "3"),
                new vkPair( 0x21, "4"),
                new vkPair( 0x22, "5"),
                new vkPair( 0x23, "6"),
                new vkPair( 0x24, "7"),
                new vkPair( 0x25, "8"),
                new vkPair( 0x26, "9"),
                new vkPair( 0x27, "0"),
                new vkPair( 0x28, "Enter"),
                new vkPair( 0x29, "Escape"),
                new vkPair( 0x2A, "Backspace"),
                new vkPair( 0x2B, "Tab"),
                new vkPair( 0x37, "Period"),
                new vkPair( 0x4F, "Right Arrow"),
                new vkPair( 0x50, "Left Arrow"),
                new vkPair( 0x52, "Up Arrow"),
                new vkPair( 0x51, "Down Arrow"),
                new vkPair( 0x58, "FldExit"),
                new vkPair( 0x87, "Backlight"),
                new vkPair( 0x3A, "F1"),
                new vkPair( 0x3B, "F2"),
                new vkPair( 0x3C, "F3"),
                new vkPair( 0x3D, "F4"),
                new vkPair( 0x3E, "F5"),
                new vkPair( 0x3F, "F6"),
                new vkPair( 0x40, "F7"),
                new vkPair( 0x41, "F8"),
                new vkPair( 0x42, "F9"),
                new vkPair( 0x43, "F10"),
                new vkPair( 0x44, "F11"),
                new vkPair( 0x45, "F12"),

                new vkPair ( 0x8a,  "Action" ), //not verified

                new vkPair ( 0x8b,  "Green Shift Button" ),
                new vkPair ( 0x90,  "Front Scan Button" ),
                new vkPair ( 0xE9,  "Orange Shift Button" ),
                new vkPair ( 0xB6,  "Aqua Shift Button" ), //not verified
                new vkPair ( 0x81,  "Power Button" ) //not verified
             };

            //private enum ck3_HardwareKeys{
            //const int ITC_Standard_A_Key = 0x04;            // A
            //const int ITC_Standard_B_Key = 0x05;            // B
            //const int ITC_Standard_C_Key = 0x06;            // C
            //const int ITC_Standard_D_Key = 0x07;            // D
            //const int ITC_Standard_E_Key = 0x08;            // E
            //const int ITC_Standard_F_Key = 0x09;            // F
            //const int ITC_Standard_G_Key = 0x0A;            // G
            //const int ITC_Standard_H_Key = 0x0B;            // H
            //const int ITC_Standard_I_Key = 0x0C;            // I
            //const int ITC_Standard_J_Key = 0x0D;            // J
            //const int ITC_Standard_K_Key = 0x0E;            // K
            //const int ITC_Standard_L_Key = 0x0F;            // L
            //const int ITC_Standard_M_Key = 0x10;            // M
            //const int ITC_Standard_N_Key = 0x11;            // N
            //const int ITC_Standard_O_Key = 0x12;            // O
            //const int ITC_Standard_P_Key = 0x13;            // P
            //const int ITC_Standard_Q_Key = 0x14;            // Q
            //const int ITC_Standard_R_Key = 0x15;            // R
            //const int ITC_Standard_S_Key = 0x16;            // S
            //const int ITC_Standard_T_Key = 0x17;            // T
            //const int ITC_Standard_U_Key = 0x18;            // U
            //const int ITC_Standard_V_Key = 0x19;            // V
            //const int ITC_Standard_W_Key = 0x1A;            // W
            //const int ITC_Standard_X_Key = 0x1B;            // X
            //const int ITC_Standard_Y_Key = 0x1C;            // Y
            //const int ITC_Standard_Z_Key = 0x1D;            // Z
            //const int ITC_Standard_1_Key = 0x1E;            // 1
            //const int ITC_Standard_2_Key = 0x1F;            // 2
            //const int ITC_Standard_3_Key = 0x20;            // 3
            //const int ITC_Standard_4_Key = 0x21;            // 4
            //const int ITC_Standard_5_Key = 0x22;            // 5
            //const int ITC_Standard_6_Key = 0x23;            // 6
            //const int ITC_Standard_7_Key = 0x24;            // 7
            //const int ITC_Standard_8_Key = 0x25;            // 8
            //const int ITC_Standard_9_Key = 0x26;            // 9
            //const int ITC_Standard_0_Key = 0x27;            // 0
            //const int ITC_Standard_Enter_Key = 0x28;    // Enter
            //const int ITC_Standard_Escape_Key = 0x29;    // Escape
            //const int ITC_Standard_Backspace_Key = 0x2A;    // Backspace
            //const int ITC_Standard_Tab_Key = 0x2B;    // Tab
            //const int ITC_Standard_Period_Key = 0x37;    // Period
            //const int ITC_Standard_RArrow_Key = 0x4F;    // Right Arrow
            //const int ITC_Standard_LArrow_Key = 0x50;    // Left Arrow
            //const int ITC_Standard_UArrow_Key = 0x52;    // Up Arrow
            //const int ITC_Standard_DArrow_Key = 0x51;    // Down Arrow
            //const int ITC_Standard_FieldExit_Key = 0x58;    // FldExit
            //const int ITC_Standard_Backlight_Key = 0x87;    // Backlight
            //const int ITC_Standard_F1_Key = 0x3A;   // F1
            //const int ITC_Standard_F2_Key = 0x3B;   // F2
            //const int ITC_Standard_F3_Key = 0x3C;   // F3
            //const int ITC_Standard_F4_Key = 0x3D;   // F4
            //const int ITC_Standard_F5_Key = 0x3E;   // F5
            //const int ITC_Standard_F6_Key = 0x3F;   // F6
            //const int ITC_Standard_F7_Key = 0x40;   // F7
            //const int ITC_Standard_F8_Key = 0x41;   // F8
            //const int ITC_Standard_F9_Key = 0x42;   // F9
            //const int ITC_Standard_F10_Key = 0x43;   // F10
            //const int ITC_Standard_F11_Key = 0x44;   // F11
            //const int ITC_Standard_F12_Key = 0x45;   // F12
            //}
        }
    }
}