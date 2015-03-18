using System;
using System.Collections.Generic;
using System.Text;
using EventNamespace;
using System.Runtime.InteropServices;

namespace ITC_KEYBOARD
{
    /// <summary>
    /// Class to access the direct keyboard driver tables
    /// </summary>
    public partial class CDirectKeys 
    {
        /*
        0x00 Virtual key (VK_)
        0x01 SHIFT + virtual key (VK_)
        0x03 Multi key
        0x05 Named event
        0x07 Application button
        */
        /// <summary>
        /// a list of possible key types
        /// </summary>
        public enum directKeyType : byte
        {
            kTypeVirtualKey = 0x00,
            kTypeShiftdVirtualKey = 0x01,
            kTypeMultiKey = 0x03,
            kTypeNamedEvent = 0x05,
            kTypeAppButton = 0x07
        }
        //public const int kTypeVirtualKey = 0x00;
        //public const int kTypeShiftdVirtualKey = 0x01;
        //public const int kTypeMultiKey = 0x03;
        //public const int kTypeNamedEvent = 0x05;
        //public const int kTypeAppButton = 0x07;

        /// <summary>
        /// list of possible windows app keys
        /// </summary>
        public enum appButtons : byte
        {
                VK_APP1 = 0xC1,
                VK_APP2 = 0xC2,
                VK_APP3 = 0xC3,
                VK_APP4 = 0xC4,
                VK_APP5 = 0xC5,
                VK_APP6 = 0xC6
        }

        /// <summary>
        /// definition of a key definition
        /// </summary>
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct directKeyStruct
        {
            /// <summary>
            /// the virtual windows keycode (see winuser.h and winuserm.h)
            /// </summary>
            public VKEY keyVal;
            /// <summary>
            /// the directKeyType of the key mapping
            /// </summary>
            public directKeyType keyType;
        }
        /// <summary>
        /// internal array of actual key definitions as byte[]
        /// </summary>
        private byte[] _bDirectKeys;
        /// <summary>
        /// internal array of actual key definitions as key pairs
        /// </summary>
        private directKeyStruct[] _bKeyPairs;
        /// <summary>
        /// an event has a event name and a value
        /// </summary>
        public struct eventPair
        {
            /// <summary>
            /// the name of the event
            /// normally event1 to eventx
            /// </summary>
            public string eventName;
            /// <summary>
            /// the contents of the event
            /// </summary>
            public string eventValue;
        }
        /// <summary>
        /// a list of Delta events
        /// </summary>
        private eventPair[] _eventPairsDelta;
        /// <summary>
        /// a list of state event pairs
        /// </summary>
        private eventPair[] _eventPairsState;
        /// <summary>
        /// multikeys are made of a name and a key sequence
        /// </summary>
        public struct multiDirectKeyStruct
        {
            /// <summary>
            /// the name of a multikey, normally 
            /// Multi1 to MultiX
            /// </summary>
            public string sName;
            /// <summary>
            /// the content of the Multikey
            /// </summary>
            public directKeyStruct[] bValues;
        }
        /// <summary>
        /// internal list of multi key events
        /// </summary>
        private multiDirectKeyStruct[] _multiEvents;
        /// <summary>
        /// the constructor of the class
        /// reads the registry and fills the internal tables
        /// </summary>
        public CDirectKeys()
        {
#if EMULATE
            writeReg();
#endif
            readReg();
        }
        /// <summary>
        /// read the number of key pairs
        /// </summary>
        /// <returns>count of key pairs</returns>
        public int getKeyCount()
        {
            if (this._bDirectKeys == null)
                return 0;
            return _bKeyPairs.Length;
        }
        /// <summary>
        /// the mutlikeys list
        /// </summary>
        /// <returns>a list of multikeys</returns>
        public multiDirectKeyStruct[] getMultiKeys()
        {
            return _multiEvents;
        }
        /// <summary>
        /// Provide a string for a keypair
        /// </summary>
        /// <param name="iIndex"></param>
        /// <returns>the name of the key at position iIndex of the key list</returns>
        public string getKeyString(int iIndex)
        {
            if (iIndex > _bKeyPairs.Length)
                throw new ArgumentOutOfRangeException("index outside values");
            string s = string.Format("0x{0:x}, 0x{1:x}", _bKeyPairs[iIndex].keyVal, _bKeyPairs[iIndex].keyType);
            return s;
        }
        /// <summary>
        /// get a key pair
        /// </summary>
        /// <param name="iIndex"></param>
        /// <returns>the key pair at iIndex</returns>
        public directKeyStruct getKey(int iIndex)
        {
            if (iIndex > _bKeyPairs.Length)
                throw new ArgumentOutOfRangeException("index outside values");
            return _bKeyPairs[iIndex];
        }
        /// <summary>
        /// Change a direct key to a VKEY
        /// </summary>
        /// <param name="iIndex">index of the key to change. Note: Not all index's point to a used key</param>
        /// <param name="kVal">the new virtual key value</param>
        /// <param name="bType">the new key type</param>
        public void setKey(int iIndex, VKEY kVal, directKeyType bType)
        {
            if (iIndex > _bKeyPairs.Length)
                throw new ArgumentOutOfRangeException("index outside values");
            _bKeyPairs[iIndex].keyVal = kVal;
            _bKeyPairs[iIndex].keyType = bType;
            //save to reg
            saveKeyTable();
            //inform the driver
            this.updateDriver();
        }
        /// <summary>
        /// Change a direct key to point to an index of MultiKey or EventKey table
        /// </summary>
        /// <param name="iIndex">index of the key to change. Note: Not all index's point to a used key</param>
        /// <param name="iTableIndex">the index to point to</param>
        /// <param name="bType">the key type (MultiKey or EventKey)</param>
        public void setKey(int iIndex, int iTableIndex, directKeyType bType)
        {
            if (iIndex > _bKeyPairs.Length)
                throw new ArgumentOutOfRangeException("index outside values");
            _bKeyPairs[iIndex].keyVal = (VKEY)iTableIndex;
            _bKeyPairs[iIndex].keyType = bType;
            //save to reg
            saveKeyTable();
            //inform the driver
            this.updateDriver();
        }
        /// <summary>
        /// Access the State events list
        /// </summary>
        /// <returns>a list of known state events</returns>
        public eventPair[] getStateEvents()
        {
            return _eventPairsState;
        }
        /// <summary>
        /// Access the Delta events list
        /// </summary>
        /// <returns>a list of known delta events</returns>
        public eventPair[] getDeltaEvents()
        {
            return _eventPairsDelta;
        }
        /// <summary>
        /// internal function to fill MultiKey list from registry
        /// </summary>
        private void readMultiKeys()
        {
            string sRegKey = getLocation();
            Microsoft.Win32.RegistryKey tempKey;// = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(sRegKey);
            tempKey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(sRegKey + @"\Multikey");
            string[] multiKeys = tempKey.GetValueNames();
            _multiEvents = new multiDirectKeyStruct[multiKeys.Length];
            for (int i = 0; i < multiKeys.Length; i++)
            {
                _multiEvents[i].sName = multiKeys[i];
                byte[] bytes = (byte[])tempKey.GetValue(multiKeys[i]);
                _multiEvents[i].bValues = RawDeserialize2(bytes);
            }
            tempKey.Close();
        }
        /// <summary>
        /// internal function to read the registry to the internal vars
        /// </summary>
        private void readReg()
        {
            string sRegKey = getLocation();
            Microsoft.Win32.RegistryKey tempKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(sRegKey);
            //byte[] defBytes = new byte[16] = new byte[16] { 0x00, 0x00, 0x00, 0x00, 0xc5, 0x07, 0x01, 0x05, 0x75, 0x00, 0x76, 0x00, 0x00, 0x00, 0x00, 0x00 };
            tempKey.OpenSubKey(sRegKey);
            int iCount = tempKey.ValueCount;
            _bDirectKeys = (byte[])tempKey.GetValue("VKey");
            tempKey.Close();
            _bKeyPairs = this.RawDeserialize2(_bDirectKeys);

            tempKey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(sRegKey + @"\Events\Delta");
            string[] deltaKeys = tempKey.GetValueNames();
            _eventPairsDelta = new eventPair[deltaKeys.Length];
            for (int i = 0; i < deltaKeys.Length; i++)
            {
                _eventPairsDelta[i].eventName = deltaKeys[i];
                _eventPairsDelta[i].eventValue = (string)tempKey.GetValue(deltaKeys[i]);
            }
            tempKey.Close();
            tempKey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(sRegKey + @"\Events\State");
            string[] stateKeys = tempKey.GetValueNames();
            _eventPairsState = new eventPair[stateKeys.Length];
            for (int i = 0; i < stateKeys.Length; i++)
            {
                _eventPairsState[i].eventName = stateKeys[i];
                _eventPairsState[i].eventValue = (string)tempKey.GetValue(stateKeys[i]);
            }
            tempKey.Close();
            readMultiKeys();
        }
        /// <summary>
        /// converts raw byte[] to directKeysStruct[]
        /// </summary>
        /// <param name="rawData">the input byte[]s</param>
        /// <returns>directKeysStruct[]</returns>
        private directKeyStruct[] RawDeserialize2(byte[] rawData)
        {
            if (rawData == null)
                return null;
            int structSize = 2;
            int iCount = rawData.Length / structSize; //we have 5 bytes per struct
            directKeyStruct[] _directKey = new directKeyStruct[iCount];
            for (int i = 0; i < iCount; i++)
            {
                _directKey[i].keyVal = (VKEY) rawData[i * structSize + 0];
                _directKey[i].keyType = (directKeyType)rawData[i * structSize + 1];
            }
            return _directKey;
        }
        /// <summary>
        /// convert keyStruct[] to raw bytes[] for save to registry
        /// </summary>
        /// <param name="structData">the input directKeyStruct[]</param>
        /// <returns>the output as byte[]</returns>
        public byte[] RawSerialize(directKeyStruct[] structData)
        {
            //size
            int structSize = System.Runtime.InteropServices.Marshal.SizeOf(structData[0]);
            int iRawSize = structData.Length * structSize;
            byte[] rawDatas = new byte[iRawSize];
            for (int i = 0; i < structData.Length; i++)
            {
                rawDatas[(i * structSize) + 0] = (byte)structData[i].keyVal;
                rawDatas[(i * structSize) + 1] = (byte)structData[i].keyType;
            }
            return rawDatas;
        }
        /// <summary>
        /// save the current internal tables to the registry
        /// and update the driver
        /// </summary>
        public void saveKeyTable()
        {
            string sRegKey = getLocation();
            Microsoft.Win32.RegistryKey tempKey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(sRegKey);
            byte[] tByte = RawSerialize(this._bKeyPairs);
            tempKey.SetValue("VKey", tByte, Microsoft.Win32.RegistryValueKind.Binary);
            tempKey.Close();
            updateDriver();
        }
        /// <summary>
        /// let the driver know, that the tables have to be reloaded
        /// </summary>
        private void updateDriver()
        {
            EventHandling.setNamedEvent("ITC_KEYBOARD_CHANGE");
            uint iRes = 99;
            try
            {
                iRes = EventNamespace.EventHandling.WaitForDirectLoadComplete();
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Exception in WaitForDirectLoadComplete");
            }
            System.Diagnostics.Debug.WriteLine("WaitForDirectLoadComplete = "+iRes.ToString());
        }

#if EMULATE
        private void writeReg()
        {
            /*
                [HKEY_LOCAL_MACHINE\HARDWARE\DEVICEMAP\KEYBD\0000]
                "ConfigFlags"=dword:00000002
                "DeviceConfig"=dword:000003C2
                "DeviceDebounce"=dword:00000050
                "DKeyMask"=dword:0000007C
                "KeyMapFile"=""
                "ResumeMask"=dword:00000007
                "TaskBarIcons"=dword:00000000
                "VFlags"=hex(3):00,00,00,00,00,00,00,00
                "VKey"=hex(3):00,00,00,00,c5,07,01,05,75,00,76,00,00,00,00,00            
            */
            string sRegKey=getLocation();
            Microsoft.Win32.RegistryKey tempKey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(sRegKey);
            byte[] defBytes = new byte[16] { 0x00,0x00,0x00,0x00,0xc5,0x07,0x01,0x05,0x75,0x00,0x76,0x00,0x00,0x00,0x00,0x00 };
            tempKey.SetValue("VKey", defBytes, Microsoft.Win32.RegistryValueKind.Binary);
            tempKey.Close();
            /*
            [HKEY_LOCAL_MACHINE\HARDWARE\DEVICEMAP\KEYBD\0000\EVENTS\DELTA]
            "Event1"="DeltaLeftScan"
            "Event2"="DeltaRightScan"
            "Event3"="DeltaCenterScan"
            "Event4"="ITC_BACKLIGHT_KEY_DELTA"
            "Event5"="ITC_VOLUME_UP_DELTA"
            "Event6"="ITC_VOLUME_DOWN_DELTA"

            [HKEY_LOCAL_MACHINE\HARDWARE\DEVICEMAP\KEYBD\0000\EVENTS\STATE]
            "Event1"="StateLeftScan"
            "Event2"="StateRightScan"
            "Event3"="StateCenterScan"
            "Event4"="ITC_BACKLIGHT_KEY_STATE"
            "Event5"="ITC_VOLUME_UP_STATE"
            "Event6"="ITC_VOLUME_DOWN_STATE"
             */
            string[] DefEventNames = new string[] { "Event1", "Event2","Event3", "Event4", "Event5", "Event6" };
            string[] DefStateEvents = new string[] { 
                "DeltaLeftScan", 
                "DeltaRightScan", 
                "DeltaCenterScan", 
                "ITC_BACKLIGHT_KEY_DELTA",
                "ITC_VOLUME_UP_DELTA",
                "ITC_VOLUME_DOWN_DELTA"
            };
            string[] DefDeltaEvents = new string[] { 
            "StateLeftScan",
            "StateRightScan",
            "StateCenterScan",
            "ITC_BACKLIGHT_KEY_STAT",
            "ITC_VOLUME_UP_STATE",
            "ITC_VOLUME_DOWN_STATE"};
            tempKey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"Hardware\DeviceMap\Keybd\Events\Delta");
            for (int i = 0; i < DefEventNames.Length; i++)
            {
                tempKey.SetValue(DefEventNames[i], DefDeltaEvents[i], Microsoft.Win32.RegistryValueKind.String);
            }
            tempKey.Close();
            tempKey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"Hardware\DeviceMap\Keybd\Events\State");
            for (int i = 0; i < DefEventNames.Length; i++)
            {
                tempKey.SetValue(DefEventNames[i], DefStateEvents[i], Microsoft.Win32.RegistryValueKind.String);
            }
            tempKey.Close();
        }
#endif
        /// <summary>
        /// get the current registry location of the tables
        /// </summary>
        /// <returns></returns>
        private string getLocation()
        {
#if EMULATE
            return @"Hardware\DeviceMap\Keybd\0000";
#else
            /*
            [HKEY_LOCAL_MACHINE\HARDWARE\DEVICEMAP\KEYBD]
            "ActiveConfig"="0000"*/
            Microsoft.Win32.RegistryKey tempKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"Hardware\DeviceMap\Keybd");
            string regKeyb = (string)tempKey.GetValue("ActiveConfig");
            tempKey.Close();
            return @"Hardware\DeviceMap\Keybd\"+regKeyb;
#endif
        }
    }
}
