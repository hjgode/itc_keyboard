using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace ITC_KEYBOARD
{
    /// <summary>
    /// class to manage rotatekeys
    /// </summary>
    public class CFunctionkeys
    {
        //  there are two subitems to manage
        //  FunctionKeyX
        //  FunctionKeyXFlags
        /* from Reprog. USB keypad
            HKLM\Drivers\HID\ClientDrivers\ ITCKeyboard\Layout\ <CurrentActiveLayoutId>\FunctionKeys
            Entries under this key will launch the named function from the named DLL and pass the included parameter if
            available.
            "FunctionKeyX" = multi_sz: sz1, sz2
            "FunctionKeyXFlags" = dword:parm
            Where:
            sz1 = DLL Name
            sz2 = DLL Function
            parm = Function Parameter
         */

        //[Serializable]
        //[StructLayout(LayoutKind.Sequential)]
        //public struct RotateKeyStruct
        //{
        //    public byte bFlag1;
        //    public byte bFlag2;
        //    public byte bFlag3;
        //    public byte bIntScan;
        //}


        /// <summary>
        /// dump functionkeyFlags
        /// </summary>
        /// <param name="iIdx"></param>
        /// <returns>a string representing the functionkey flags</returns>
        private string dumpFunctionkeyFlags(int iIdx)
        {
            functionkeyStruct fks = new functionkeyStruct();
            fks = _FunctionkeyStructs[iIdx - 1];
            //usbKeyStructShort[] rs = RawDeserialize2(bs);
            string s = "dwFlag: ";
            s += fks.functionKeyFlag.ToString();
            return s;
        }

        /// <summary>
        /// dump Functionkey
        /// </summary>
        /// <param name="iIdx"></param>
        /// <returns>a string with DLL and function name</returns>
        public string dumpFunctionkey(int iIdx)
        {
            string s = "->";
            s+="'"+_FunctionkeyStructs[iIdx - 1].DllName+"'";
            s += "/'" + _FunctionkeyStructs[iIdx - 1].DllFunction + "', ";
            s+=this.dumpFunctionkey(iIdx);
            return s;
        }

        /// <summary>
        /// structure to hold the functionkey data
        /// </summary>
        public struct functionkeyStruct
        {
            public string DllName; /*!< the DLL name the function key is linked to*/
            public string DllFunction; /*!< the function inside the DLL the functionkey is linked to*/
            public UInt32 functionKeyFlag; /*!< additional optional DWORD flag for the DLL function linked to*/
        }

        /// <summary>
        /// array of FunctionKey entries
        /// </summary>
        private functionkeyStruct[] _FunctionkeyStructs;

        /// <summary>
        /// a list of known function keys according to the registry
        /// </summary>
        private List<functionkeyStruct> _functionkeyList=new List<functionkeyStruct>();

        /// <summary>
        /// the number of functionkeys defined according to the registry
        /// </summary>
        private int _functionkeyCount = 0;

        /// <summary>
        /// init the rotatekeys list and array
        /// </summary>
        public CFunctionkeys()
        {
            //read number of entries
            int iCount = this.getfunctionkeyCount();
            if (iCount == 0)
            {
                return;
                //throw new ArgumentNullException("Sorry, no RotateKeys supported");
            }
            //create new arrays
            this.readAll();
        }

        /// <summary>
        /// get the number of defined rotatekeys
        /// </summary>
        /// <returns></returns>
        public int getfunctionkeyCount()
        {
            if (this._functionkeyCount != 0)
                return this._functionkeyCount;
            string regKeyb = CUSBkeys.getRegLocation() + @"\FunctionKeys";
            Microsoft.Win32.RegistryKey tempKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(regKeyb, true);
            int i = 1;
            object o = null;
            do
            {
                try
                {
                    o = tempKey.GetValue("FunctionKey" + i.ToString());
                }
                catch (Exception)
                {
                    o = null;
                    break;
                }
                if (o != null)
                    i++;
            } while (o != null);
            if (i != 0)
                return i - 1;
            else
                return 0;
        }

        /// <summary>
        /// read all functionkey entries of registry
        /// </summary>
        private void readAll()
        {
            _functionkeyList.Clear();
            int iCount = this.getfunctionkeyCount();
            if (iCount == 0)
                return;
            //read all RotateKey entries
            _FunctionkeyStructs = new  functionkeyStruct[iCount];

            string regKeyb = CUSBkeys.getRegLocation() + @"\FunctionKeys";
            Microsoft.Win32.RegistryKey tempKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(regKeyb, true);


            for (int i = 1; i <= iCount; i++)
            {
                string[] sFunctionkey = (string[])tempKey.GetValue("FunctionKey" + i.ToString());
                //_RotateKeyStructs = RawDeserialize2(bRotateKeys);
                _FunctionkeyStructs[i - 1].DllName = sFunctionkey[0];
                _FunctionkeyStructs[i - 1].DllFunction = sFunctionkey[1];
            }
            for (int i = 1; i <= iCount; i++)
            {
                object oDWORD = tempKey.GetValue("FunctionKey" + i.ToString() + "Flags");
                UInt32 u32=0;
                try
                {
                    u32 = Convert.ToUInt32(oDWORD);
                    //u32 = (UInt32)tempKey.GetValue("FunctionKey" + i.ToString() + "Flags");   //casting gives exception
                    _FunctionkeyStructs[i - 1].functionKeyFlag = u32;
                    _functionkeyList.Add(_FunctionkeyStructs[i - 1]);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("FunctionKeys:ReadAll() Exception: " + ex.Message + " for FunctionKey" + i.ToString() + "Flags");
                }
            }

            tempKey.Close();
            System.Diagnostics.Debug.WriteLine("FunctionKey readall finished");
        }
        /// <summary>
        /// a test function to fill the registry with a functionkey entry
        /// </summary>
        public void writeTestFunctionkeys(){
            string regKeyb = CUSBkeys.getRegLocation() + @"\FunctionKeys";
            Microsoft.Win32.RegistryKey tempKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(regKeyb, true);
            if (tempKey == null)
            {
                tempKey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(regKeyb);
                tempKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(regKeyb, true);
            }
            string[] sFk = new string[] { "ITC50.DLL", "ITCGetSerialNumber" };
            UInt16 uFlag = 0x00;
            string sValue = "FunctionKey1";
            tempKey.SetValue(sValue, sFk, Microsoft.Win32.RegistryValueKind.MultiString);
            sValue = "FunctionKey1Flags";
            tempKey.SetValue(sValue, uFlag, Microsoft.Win32.RegistryValueKind.DWord);
            tempKey.Close();
        }
        /// <summary>
        /// return the list of known function keys
        /// </summary>
        /// <returns>a list of function key structs</returns>
        public List<functionkeyStruct> functionKeys()
        {
            return this._functionkeyList;
        }
    }
}
