namespace ITC_KEYBOARD
{
    public static class cn3Keys
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
                new vkPair(    0x04,    "A"),
                new vkPair(    0x05,    "B"),
                new vkPair(    0x06,    "C"),
                new vkPair(    0x07,    "D"),
                new vkPair(    0x08,    "E"),
                new vkPair(    0x09,    "F"),
                new vkPair(    0x0A,    "G"),
                new vkPair(    0x0B,    "H"),
                new vkPair(    0x0C,    "I"),
                new vkPair(    0x0D,    "J"),
                new vkPair(    0x0E,    "K"),
                new vkPair(    0x0F,    "L"),
                new vkPair(    0x10,    "M"),
                new vkPair(    0x11,    "N"),
                new vkPair(    0x12,    "O"),
                new vkPair(    0x13,    "P"),
                new vkPair(    0x14,    "Q"),
                new vkPair(    0x15,    "R"),
                new vkPair(    0x16,    "S"),
                new vkPair(    0x17,    "T"),
                new vkPair(    0x18,    "U"),
                new vkPair(    0x19,    "V"),
                new vkPair(    0x1A,    "W"),
                new vkPair(    0x1B,    "X"),
                new vkPair(    0x1C,    "Y"),
                new vkPair(    0x1D,    "Z"),
                new vkPair(    0x1E,    "1"),
                new vkPair(    0x1F,    "2"),
                new vkPair(    0x20,    "3"),
                new vkPair(    0x21,    "4"),
                new vkPair(    0x22,    "5"),
                new vkPair(    0x23,    "6"),
                new vkPair(    0x24,    "7"),
                new vkPair(    0x25,    "8"),
                new vkPair(    0x26,    "9"),
                new vkPair(    0x27,    "0"),
                new vkPair(    0x28,    "Enter"),
                new vkPair(    0x29,    "Escape"),
                new vkPair(    0x2A,    "Backspace"),
                new vkPair(    0x2B,    "Tab"),
                new vkPair(    0x2C,    "Space Bar"),
                new vkPair(    0x37,    "Period"),
                new vkPair(    0x41,    "*"),
                new vkPair(    0x42,    "#"),
                new vkPair(    0x4F,    "Right Arrow"),
                new vkPair(    0x50,    "Left Arrow"),
                new vkPair(    0xE0,    "Left Control"),
                new vkPair(    0xE1,    "Left Shift"),
                new vkPair(    0xE3,    "Windows"),
                new vkPair ( 0x8b,  "Green Shift Button" ),
                new vkPair ( 0x90,  "Front Scan Button" ),
                new vkPair ( 0xE9,  "Orange Shift Button" ),
                new vkPair ( 0xB6,  "Aqua Shift Button" ), //not verified
                new vkPair ( 0x81,  "Power Button" ) //not verified
                 };

        //private enum cn3_HardwareKeys{
        //    ITC_Standard_A_Key = 0x04,            // A
        //    ITC_Standard_B_Key = 0x05,            // B
        //    ITC_Standard_C_Key = 0x06,            // C
        //    ITC_Standard_D_Key = 0x07,            // D
        //    ITC_Standard_E_Key = 0x08,            // E
        //    ITC_Standard_F_Key = 0x09,            // F
        //    ITC_Standard_G_Key = 0x0A,            // G
        //    ITC_Standard_H_Key = 0x0B,            // H
        //    ITC_Standard_I_Key = 0x0C,            // I
        //    ITC_Standard_J_Key = 0x0D,            // J
        //    ITC_Standard_K_Key = 0x0E,            // K
        //    ITC_Standard_L_Key = 0x0F,            // L
        //    ITC_Standard_M_Key = 0x10,            // M
        //    ITC_Standard_N_Key = 0x11,            // N
        //    ITC_Standard_O_Key = 0x12,            // O
        //    ITC_Standard_P_Key = 0x13,            // P
        //    ITC_Standard_Q_Key = 0x14,            // Q
        //    ITC_Standard_R_Key = 0x15,            // R
        //    ITC_Standard_S_Key = 0x16,            // S
        //    ITC_Standard_T_Key = 0x17,            // T
        //    ITC_Standard_U_Key = 0x18,            // U
        //    ITC_Standard_V_Key = 0x19,            // V
        //    ITC_Standard_W_Key = 0x1A,            // W
        //    ITC_Standard_X_Key = 0x1B,            // X
        //    ITC_Standard_Y_Key = 0x1C,            // Y
        //    ITC_Standard_Z_Key = 0x1D,            // Z
        //    ITC_Standard_1_Key = 0x1E,            // 1
        //    ITC_Standard_2_Key = 0x1F,            // 2
        //    ITC_Standard_3_Key = 0x20,            // 3
        //    ITC_Standard_4_Key = 0x21,            // 4
        //    ITC_Standard_5_Key = 0x22,            // 5
        //    ITC_Standard_6_Key = 0x23,            // 6
        //    ITC_Standard_7_Key = 0x24,            // 7
        //    ITC_Standard_8_Key = 0x25,            // 8
        //    ITC_Standard_9_Key = 0x26,            // 9
        //    ITC_Standard_0_Key = 0x27,            // 0
        //    ITC_Standard_Enter_Key     = 0x28,    // Enter
        //    ITC_Standard_Escape_Key    = 0x29,    // Escape
        //    ITC_Standard_Backspace_Key = 0x2A,    // Backspace
        //    ITC_Standard_Tab_Key       = 0x2B,    // Tab
        //    ITC_Standard_Space_Key     = 0x2C,    // Space Bar
        //    ITC_Standard_Period_Key    = 0x37,    // Period
        //    ITC_Standard_Astrix_Key    = 0x41,    // *
        //    ITC_STandard_Hash_Key      = 0x42,    // #
        //    ITC_Standard_RArrow_Key    = 0x4F,    // Right Arrow
        //    ITC_Standard_LArrow_Key    = 0x50,    // Left Arrow
        //    ITC_Standard_LCtrl_Key     = 0xE0,    // Left Control
        //    ITC_Standard_LShift_Key    = 0xE1,    // Left Shift
        //    ITC_Standard_Windows_Key   = 0xE3,    // Windows
        //}
    }
}
