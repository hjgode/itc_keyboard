using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace KeyAquaAutoFallback
{
    class CUSBkeys2
    {
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct usbKeyPartial
        {
            //public byte bHID;
            //public byte bScanKey;
            public byte bFlag1;
            public byte bFlag2;
            public byte bFlag3;
            public byte bIntScan;
        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct usbKey 
        {
            public byte bHID;
            public byte bScanKey;
            //public usbKeyPartial usbFlagScan;
			public byte bFlag1;
            public byte bFlag2;
            public byte bFlag3;
            public byte bIntScan;
        }
        
        public class usbKeys
        {
            private usbKey[] _usbKeysPlane;
			private usbKey[][] _usbKeysAll;
            //private List<usbKey[]> _usbKeys;
			
            public usbKeys()
            {
                //this.readKeyTable(0);
                //read in all ShifPlanes
				this.readKeyTables();
            }
            //public usbKeys(int iPlane)
            //{
            //    this.readKeyTable(iPlane);
            //}
            public int getNumPlanes()
            {
                string regKeybd = getRegLocation();
                Microsoft.Win32.RegistryKey tempKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(regKeybd, true);
                //try and error
                string[] s = tempKey.GetValueNames();
                int iCountPlanes = -1;
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i].StartsWith("ShiftPlane"))
                        iCountPlanes++;
                }
                tempKey.Close();
                return iCountPlanes + 1;
            }
            private void readKeyTables()
            {
				int iCount=getNumPlanes();
                string regKeyb = getRegLocation();
				_usbKeysAll=new CUSBkeys2.usbKey[iCount][];
                Microsoft.Win32.RegistryKey tempKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(regKeyb, true);
				for (int x=0; x<iCount; x++){
	                byte[] bKeys = (byte[])tempKey.GetValue("ShiftPlane" + x.ToString());
	
					usbKey[] _usbKeyTemp = RawDeserialize2(bKeys);
	                
                    //_usbKeysAll=new CUSBkeys2.usbKey[x][];
                    //_usbKeysAll[x] = new usbKey[_usbKeyTemp.Length];
                    _usbKeysAll[x] = _usbKeyTemp;
					
				}
                tempKey.Close();
			}
			public void readKeyTable(int iPlane)
            {
                if (iPlane > 2)
                    throw new ArgumentOutOfRangeException("Actually only planes 0-2 supported");
                string regKeyb = getRegLocation();
                Microsoft.Win32.RegistryKey tempKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(regKeyb, true);
                byte[] bKeys = (byte[])tempKey.GetValue("ShiftPlane" + iPlane.ToString());

                tempKey.Close();
                _usbKeysPlane = RawDeserialize2(bKeys);
				int iCount=getNumPlanes();
				_usbKeysAll=new CUSBkeys2.usbKey[iCount][];
				_usbKeysAll[0]=_usbKeysPlane;

#if DEBUG
                //TESTING ONLY
                byte[] bTest = RawSerialize(_usbKeysPlane);
                if (bTest.Length == bKeys.Length)
                {
                    for (int j = 0; j < bTest.Length; j++)
                    {
                        if (bTest[j] != bKeys[j])
                            throw new ArgumentOutOfRangeException("Upps. Found DIFF at " + j.ToString());
                    }
                }
                else
                    throw new ArgumentOutOfRangeException("Upps. The byte arrays have different size.");
#endif
            }
            public byte[] RawSerialize(usbKey[] structData)
            {
                //size
                int structSize = Marshal.SizeOf(structData[0]);
                int iRawSize = structData.Length * structSize;
                byte[] rawDatas = new byte[iRawSize];
                for (int i = 0; i < structData.Length; i++)
                {
                    rawDatas[(i * structSize) + 0] = structData[i].bHID;
                    rawDatas[(i * structSize) + 1] = structData[i].bScanKey;
                    rawDatas[(i * structSize) + 2] = structData[i].bFlag1;
                    rawDatas[(i * structSize) + 3] = structData[i].bFlag2;
                    rawDatas[(i * structSize) + 4] = structData[i].bFlag3;
                    rawDatas[(i * structSize) + 5] = structData[i].bIntScan;
                }
                return rawDatas;
            }
            private static usbKey[] RawDeserialize2(byte[] rawData)
            {
                int structSize = 6;
                int iCount = rawData.Length / structSize; //we have 6 bytes per struct
                usbKey[] _usbKey = new usbKey[iCount];
                for (int i = 0; i < iCount; i++)
                {
                    _usbKey[i] = new usbKey();
                    _usbKey[i].bHID = rawData[i * structSize + 0];
                    _usbKey[i].bScanKey = rawData[i * structSize + 1];
                    _usbKey[i].bFlag1 = rawData[i * structSize + 2];
                    _usbKey[i].bFlag2 = rawData[i * structSize + 3];
                    _usbKey[i].bFlag3 = rawData[i * structSize + 4];
                    _usbKey[i].bIntScan = rawData[i * structSize + 5];
                }
                return _usbKey;
            }

            public static string getRegLocation()
            {
                // [HKLM]Hardware\DeviceMap\Keybd:CurrentActiveLayoutKey
                Microsoft.Win32.RegistryKey tempKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"Hardware\DeviceMap\Keybd");
                string regKeyb = (string)tempKey.GetValue("CurrentActiveLayoutKey");
                tempKey.Close();
                return regKeyb;
            }

        }
    }
}
