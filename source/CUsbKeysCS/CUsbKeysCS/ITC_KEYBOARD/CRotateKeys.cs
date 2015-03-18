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
        public class CRotateKeys
        {
            //  there are three subitems to manage
            //  RotateCharsX
            //  RotateKeyX
            //  RotateShiftedCharsX
            /* from CN3
                [HKEY_LOCAL_MACHINE\Drivers\HID\ClientDrivers\ITCKeyboard\Layout.ORIG\CN3\0002\RotateKeys]
                "RotateChars1"=hex(7):61,00,00,00,62,00,00,00,63,00,00,00,00,00
                "RotateChars2"=hex(7):64,00,00,00,65,00,00,00,66,00,00,00,00,00
                "RotateChars3"=hex(7):67,00,00,00,68,00,00,00,69,00,00,00,00,00
                "RotateChars4"=hex(7):6a,00,00,00,6b,00,00,00,6c,00,00,00,00,00
                "RotateChars5"=hex(7):6d,00,00,00,6e,00,00,00,6f,00,00,00,00,00
                "RotateChars6"=hex(7):70,00,00,00,71,00,00,00,72,00,00,00,73,00,00,00,00,00
                "RotateChars7"=hex(7):74,00,00,00,75,00,00,00,76,00,00,00,00,00
                "RotateChars8"=hex(7):77,00,00,00,78,00,00,00,79,00,00,00,7a,00,00,00,00,00
                "RotateKey1"=hex(3):00,00,00,1c,00,00,00,32,00,00,00,21
                "RotateKey2"=hex(3):00,00,00,23,00,00,00,24,00,00,00,2b
                "RotateKey3"=hex(3):00,00,00,34,00,00,00,33,00,00,00,43
                "RotateKey4"=hex(3):00,00,00,3b,00,00,00,42,00,00,00,4b
                "RotateKey5"=hex(3):00,00,00,3a,00,00,00,31,00,00,00,44
                "RotateKey6"=hex(3):00,00,00,4d,00,00,00,15,00,00,00,2d,00,00,00,1b
                "RotateKey7"=hex(3):00,00,00,2c,00,00,00,3c,00,00,00,2a
                "RotateKey8"=hex(3):00,00,00,1d,00,00,00,22,00,00,00,35,00,00,00,1a
                "RotateShiftedChars1"=hex(7):41,00,00,00,42,00,00,00,43,00,00,00,00,00
                "RotateShiftedChars2"=hex(7):44,00,00,00,45,00,00,00,46,00,00,00,00,00
                "RotateShiftedChars3"=hex(7):47,00,00,00,48,00,00,00,49,00,00,00,00,00
                "RotateShiftedChars4"=hex(7):4a,00,00,00,4b,00,00,00,4c,00,00,00,00,00
                "RotateShiftedChars5"=hex(7):4d,00,00,00,4e,00,00,00,4f,00,00,00,00,00
                "RotateShiftedChars6"=hex(7):50,00,00,00,51,00,00,00,52,00,00,00,53,00,00,00,00,00
                "RotateShiftedChars7"=hex(7):54,00,00,00,55,00,00,00,56,00,00,00,00,00
                "RotateShiftedChars8"=hex(7):57,00,00,00,58,00,00,00,59,00,00,00,5a,00,00,00,00,00
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
            /// dump contents of rotatekeys struct
            /// </summary>
            /// <param name="_theBytes"></param>
            /// <returns></returns>
            public string dumpRotateKey(CUSBkeys.usbKeyStructShort _theBytes)
            {
                byte b;
                b = (byte)_theBytes.bFlagHigh;
                string s = b.ToString();// _theBytes.bFlag3.ToString("X02");

                b = (byte)_theBytes.bFlagMid;
                s += "," + b.ToString();// _theBytes.bFlag2.ToString("X02");

                b = (byte)_theBytes.bFlagLow;
                s += "," + b.ToString();// _theBytes.bFlag1.ToString("X02");

                s += "," + _theBytes.bIntScan.ToString("X02");

                s += " '" + CUSBPS2_vals.Cusbps2key.getName(_theBytes.bIntScan) + "'";
                s += " | ";
                return s;
            }
            /// <summary>
            /// dump rotatkeys chars
            /// </summary>
            /// <param name="iIdx"></param>
            /// <returns></returns>
            public string dumpRotateChars(int iIdx)
            {
                string[] bs = _RotateCharsList[iIdx - 1];
                //usbKeyStructShort[] rs = RawDeserialize2(bs);
                string s = "ch: ";
                for (int i = 0; i < bs.Length; i++)
                {
                    s += bs[i];
                }
                return s;
            }

            /// <summary>
            /// dump rotatekey shifted chars
            /// </summary>
            /// <param name="iIdx"></param>
            /// <returns></returns>
            public string dumpRotateShiftedChars(int iIdx)
            {
                string[] bs = _RotateShiftedCharsList[iIdx - 1];
                //usbKeyStructShort[] rs = RawDeserialize2(bs);
                string s = "sc: ";
                for (int i = 0; i < bs.Length; i++)
                {
                    s += bs[i];
                }
                return s;
            }
            /// <summary>
            /// dump rotatekey
            /// </summary>
            /// <param name="iIdx"></param>
            /// <returns></returns>
            public string dumpRotateKey(int iIdx)
            {
                byte[] bs = _RotateKeysList[iIdx - 1];
                CUSBkeys.usbKeyStructShort[] rs = RawDeserialize2(bs);
                string s = "->";
                for (int i = 0; i < rs.Length; i++)
                {
                    s += dumpRotateKey(rs[i]);
                }
                return s;
            }

            /// <summary>
            /// array of rotateKeysStructs
            /// </summary>
            private CUSBkeys.usbKeyStructShort[] _RotateKeyStructs;
            /// <summary>
            /// list of rotateKeysStructs
            /// </summary>
            private List<byte[]> _RotateKeysList;

            private String[] _RotateCharsStructs;
            private List<string[]> _RotateCharsList;

            private String[] _RotateShiftedCharsStructs;
            private List<string[]> _RotateShiftedCharsList;

            private int _rotateKeyCount = 0;

            /// <summary>
            /// init the rotatekeys list and array
            /// </summary>
            public CRotateKeys()
            {
                //read number of entries
                int iCount = this.getRotateKeyCount();
                if (iCount == 0)
                {
                    return;
                    //throw new ArgumentNullException("Sorry, no RotateKeys supported");
                }
                //create new arrays
                _RotateKeysList = new List<byte[]>(iCount + 1);
                _RotateCharsList = new List<string[]>(iCount + 1);
                _RotateShiftedCharsList = new List<string[]>(iCount + 1);
                this.readAll();
            }

            private byte[] RawSerialize(CUSBkeys.usbKeyStructShort[] structData)
            {
                //size
                int structSize = Marshal.SizeOf(structData[0]);
                int iRawSize = structData.Length * structSize;
                byte[] rawDatas = new byte[iRawSize];
                for (int i = 0; i < structData.Length; i++)
                {
                    rawDatas[(i * structSize) + 0] = (byte)structData[i].bFlagHigh;
                    rawDatas[(i * structSize) + 1] = (byte)structData[i].bFlagMid;
                    rawDatas[(i * structSize) + 2] = (byte)structData[i].bFlagLow;
                    rawDatas[(i * structSize) + 3] = (byte)structData[i].bIntScan;
                }
                return rawDatas;
            }
            private static CUSBkeys.usbKeyStructShort[] RawDeserialize2(byte[] rawData)
            {
                int structSize = 4;
                int iCount = rawData.Length / structSize; //we have 4 bytes per struct
                CUSBkeys.usbKeyStructShort[] _rotateStruct = new CUSBkeys.usbKeyStructShort[iCount];
                for (int i = 0; i < iCount; i++)
                {
                    _rotateStruct[i].bFlagHigh = (CUsbKeyTypes.usbFlagsHigh) rawData[i * structSize + 0];
                    _rotateStruct[i].bFlagMid = (CUsbKeyTypes.usbFlagsMid)rawData[i * structSize + 1];
                    _rotateStruct[i].bFlagLow = (CUsbKeyTypes.usbFlagsLow)rawData[i * structSize + 2];
                    _rotateStruct[i].bIntScan = rawData[i * structSize + 3];
                }
                return _rotateStruct;
            }
            /// <summary>
            /// get the number of defined rotatekeys
            /// </summary>
            /// <returns></returns>
            public int getRotateKeyCount()
            {
                if (this._rotateKeyCount != 0)
                    return this._rotateKeyCount;
                string regKeyb = CUSBkeys.getRegLocation() + @"\RotateKeys";
                Microsoft.Win32.RegistryKey tempKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(regKeyb, true);
                int i = 1;
                object o = null;
                do
                {
                    try
                    {
                        o = tempKey.GetValue("RotateKey" + i.ToString());
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
            /// rotatekey types
            /// </summary>
            public enum RotateKeyType
            {
                RotateKey,
                RotateChars,
                RotateShiftedChars
            }

            private void readAll()
            {
                int iCount = this.getRotateKeyCount();
                if (iCount == 0)
                    return;
                //read all RotateKey entries
                _RotateKeyStructs = new CUSBkeys.usbKeyStructShort[iCount];

                string regKeyb = CUSBkeys.getRegLocation() + @"\RotateKeys";
                Microsoft.Win32.RegistryKey tempKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(regKeyb, true);


                for (int i = 1; i <= iCount; i++)
                {
                    byte[] bRotateKeys = (byte[])tempKey.GetValue("RotateKey" + i.ToString());
                    _RotateKeyStructs = RawDeserialize2(bRotateKeys);
                    _RotateKeysList.Add(bRotateKeys);
                }
                for (int i = 1; i <= iCount; i++)
                {
                    string[] s = (string[])tempKey.GetValue("RotateChars" + i.ToString());
                    _RotateCharsStructs = s;
                    _RotateCharsList.Add(s);
                }
                for (int i = 1; i <= iCount; i++)
                {
                    string[] s = (string[])tempKey.GetValue("RotateShiftedChars" + i.ToString());
                    _RotateShiftedCharsStructs = s;
                    _RotateShiftedCharsList.Add(s);
                }
                tempKey.Close();
                System.Diagnostics.Debug.WriteLine("RotateKey readall finished");
            }
            //TEST
            /// <summary>
            /// writes a default rotate key table to the registry
            /// </summary>
            /// <returns></returns>
            public static bool writeDefaultRotateSets()
            {
                string regKeyb = CUSBkeys.getRegLocation() + @"\RotateKeys";
                Microsoft.Win32.RegistryKey tempKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(regKeyb, true);
                if (tempKey == null)
                {
                    tempKey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(regKeyb);
                    tempKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(regKeyb, true);
                }

                //RotateShiftedChars
                string[][] rsc = new string[][] {
                new string[]{ "A" , "B", "C" },
                new string[]{ "D" , "E", "F" },
                new string[]{ "G", "H" , "I"},
                new string[]{ "J", "K" , "L"},
                new string[]{ "M", "N", "O" },
                new string[]{ "P", "Q", "R", "S" },
                new string[]{ "T", "U", "V" },
                new string[]{ "W", "X", "Y", "Z" }
            };
                //RotateChars
                string[][] rc = new string[][] {
                new string[]{ "a" , "b", "c" },
                new string[]{ "d" , "e", "f" },
                new string[]{ "g", "h" , "i"},
                new string[]{ "j", "k" , "l"},
                new string[]{ "m", "n", "o" },
                new string[]{ "p", "q", "r", "s" },
                new string[]{ "t", "u", "v" },
                new string[]{ "w", "x", "y", "z" }
            };
                //RotateKeys
                byte[][] rk = new byte[][]{
                //"RotateKey1"=hex:\
                new byte[]{0x00,0x00,0x00,0x1C,0x00,0x00,0x00,0x32,0x00,0x00,0x00,0x21},
                //"RotateKey2"=hex:\
                new byte[]{0x00,0x00,0x00,0x23,0x00,0x00,0x00,0x24,0x00,0x00,0x00,0x2B},
                //"RotateKey3"=hex:\
                new byte[]{0x00,0x00,0x00,0x34,0x00,0x00,0x00,0x33,0x00,0x00,0x00,0x43},
                //"RotateKey4"=hex:\
                new byte[]{0x00,0x00,0x00,0x3B,0x00,0x00,0x00,0x42,0x00,0x00,0x00,0x4B},
                //"RotateKey5"=hex:\
                new byte[]{0x00,0x00,0x00,0x3A,0x00,0x00,0x00,0x31,0x00,0x00,0x00,0x44},
                //"RotateKey6"=hex:\
                new byte[]{0x00,0x00,0x00,0x4D,0x00,0x00,0x00,0x15,0x00,0x00,0x00,0x2D,0x00,0x00,0x00,0x1B},
                //"RotateKey7"=hex:\
                new byte[]{0x00,0x00,0x00,0x2C,0x00,0x00,0x00,0x3C,0x00,0x00,0x00,0x2A},
                //"RotateKey8"=hex:\
                new byte[]{0x00,0x00,0x00,0x1D,0x00,0x00,0x00,0x22,0x00,0x00,0x00,0x35,0x00,0x00,0x00,0x1A}
            };

                for (int x = 0; x < rk.Length; x++)
                {
                    int ix = x + 1;
                    string sValue = "RotateKey" + ix.ToString();
                    tempKey.SetValue(sValue, rk[x], Microsoft.Win32.RegistryValueKind.Binary);
                    sValue = "RotateChars" + ix.ToString();
                    tempKey.SetValue(sValue, rc[x], Microsoft.Win32.RegistryValueKind.MultiString);
                    sValue = "RotateShiftedChars" + ix.ToString();
                    tempKey.SetValue(sValue, rsc[x], Microsoft.Win32.RegistryValueKind.MultiString);
                }

                //change the number keys from plane 1 to RotateKeys
                /*
                    07,1F,40,02,00,01 'RotateIndex'->00,00,00,1C 'a' | 00,00,00,32 'b' | 00,00,00,21 'c' | 
                    07,20,40,02,00,02 'RotateIndex'->00,00,00,23 'd' | 00,00,00,24 'e' | 00,00,00,2B 'f' | 
                    07,21,40,02,00,03 'RotateIndex'->00,00,00,34 'g' | 00,00,00,33 'h' | 00,00,00,43 'i' | 
                    07,22,40,02,00,04 'RotateIndex'->00,00,00,3B 'j' | 00,00,00,42 'k' | 00,00,00,4B 'l' | 
                    07,23,40,02,00,05 'RotateIndex'->00,00,00,3A 'm' | 00,00,00,31 'n' | 00,00,00,44 'o' | 
                    07,24,40,02,00,06 'RotateIndex'->00,00,00,4D 'p' | 00,00,00,15 'q' | 00,00,00,2D 'r' | 00,00,00,1B 's' | 
                    07,25,40,02,00,07 'RotateIndex'->00,00,00,2C 't' | 00,00,00,3C 'u' | 00,00,00,2A 'v' | 
                    07,26,40,02,00,08 'RotateIndex'->00,00,00,1D 'w' | 00,00,00,22 'x' | 00,00,00,35 'y' | 00,00,00,1A 'z' | 
                 */

                CUSBkeys.usbKeyStruct _theKey = new CUSBkeys.usbKeyStruct();
                _theKey.bHID = 0x07;
                _theKey.bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;// UsbKeyFlags3.NoFlag;
                _theKey.bFlagMid = CUsbKeyTypes.usbFlagsMid.NoRepeat;// UsbKeyFlags2.NoRepeat;
                _theKey.bFlagLow = CUsbKeyTypes.usbFlagsLow.RotateKeyIndex;// UsbKeyFlags3.RotateKeyIndex;

                CUSBkeys _cusb = new CUSBkeys();

                //ShiftPlane 2 ####################################################

                for (int iScanCode = 0x1f; iScanCode < 0x27; iScanCode++)
                {
                    _theKey.bScanKey = (CUsbKeyTypes.HWkeys)iScanCode;
                    _theKey.bIntScan = (byte)(iScanCode - 0x1e); //from 1 to 8
                    if (_cusb.setKey(2, iScanCode, _theKey) != 0)
                        _cusb.addKey(2, _theKey);
                }

                //CAPS lock on "1"
                // 'ModKey2' = 00,02,08,58
                _theKey.bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;// UsbKeyFlags3.NoFlag;
                _theKey.bFlagMid = CUsbKeyTypes.usbFlagsMid.NoRepeat;// UsbKeyFlags2.NoRepeat;
                _theKey.bFlagLow = CUsbKeyTypes.usbFlagsLow.ModifierIndex;// UsbKeyFlags3.ModifierIndex;
                _theKey.bIntScan = 0x02; //ModKey index at 2
                _theKey.bScanKey = (CUsbKeyTypes.HWkeys)0x27;
                if (_cusb.setKey(2, _theKey.bScanKey, _theKey) != 0)
                    _cusb.addKey(2, _theKey);

                //space on "0"
                _theKey.bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;// UsbKeyFlags3.NoFlag;
                _theKey.bFlagMid = CUsbKeyTypes.usbFlagsMid.VKEY;// UsbKeyFlags2.VKEY;
                _theKey.bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;// UsbKeyFlags3.NormalKey;
                _theKey.bIntScan = 0x20;
                _theKey.bScanKey = (CUsbKeyTypes.HWkeys)0x27;
                if (_cusb.setKey(2, _theKey.bScanKey, _theKey) != 0)
                    _cusb.addKey(2, _theKey);

                /*
                //remap (OK) to "@" (Shifted-Space)
                _theKey.bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;// UsbKeyFlags3.NoFlag;
                _theKey.bFlagMid = CUsbKeyTypes.usbFlagsMid.VKEY | CUsbKeyTypes.usbFlagsMid.Shifted;// UsbKeyFlags2.Shifted;
                _theKey.bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;// UsbKeyFlags3.NormalKey;
                _theKey.bIntScan = 0x32;
                _theKey.bScanKey = CUsbKeyTypes.HWkeys.Right_GUI;
                if (_cusb.setKey(2, _theKey.bScanKey, _theKey) != 0)
                    _cusb.addKey(2, _theKey);
                */

                //ShiftPlane 1 ####################################################
                //VKLWIN='Left GUI' on "0", 07,27,00,01,00,1F (07,E3,00,01,00,1F)
                _theKey.bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;// UsbKeyFlags3.NoFlag;
                _theKey.bFlagMid = CUsbKeyTypes.usbFlagsMid.Extended;// UsbKeyFlags2.Extended;
                _theKey.bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;// UsbKeyFlags3.NormalKey;
                _theKey.bIntScan = 0x1F;
                _theKey.bScanKey = (CUsbKeyTypes.HWkeys)0x27;
                if (_cusb.setKey(1, _theKey.bScanKey, _theKey) != 0)
                    _cusb.addKey(1, _theKey);

                //change the top row of keys to F1 to F4
                _theKey.bHID = 0x07;
                _theKey.bScanKey = CUsbKeyTypes.HWkeys.Left_GUI;
                _theKey.bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;// UsbKeyFlags3.NoFlag;
                _theKey.bFlagMid = CUsbKeyTypes.usbFlagsMid.VKEY;// UsbKeyFlags2.VKEY;
                _theKey.bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;// UsbKeyFlags3.NormalKey;
                _theKey.bIntScan = 0x70; //F1
                if (_cusb.setKey(0, _theKey.bScanKey, _theKey) != 0)
                    _cusb.addKey(0, _theKey);

                _theKey.bScanKey = CUsbKeyTypes.HWkeys.F1;
                _theKey.bIntScan = (byte)VKEY.VK_PRIOR;//PageUp // 0x71; //F2
                if (_cusb.setKey(0, _theKey.bScanKey, _theKey) != 0)
                    _cusb.addKey(0, _theKey);

                _theKey.bScanKey = CUsbKeyTypes.HWkeys.F2;
                _theKey.bIntScan = (byte)VKEY.VK_NEXT;//PgDwn 0x72; //F3
                if (_cusb.setKey(0, _theKey.bScanKey, _theKey) != 0)
                    _cusb.addKey(0, _theKey);

                _theKey.bScanKey = CUsbKeyTypes.HWkeys.Right_GUI;
                _theKey.bIntScan = 0x73; //F3
                if (_cusb.setKey(0, _theKey.bScanKey, _theKey) != 0)
                    _cusb.addKey(0, _theKey);

                //_cusb.saveKeyTable(0);
                _cusb.writeKeyTables(); //save the changes

                //remap PTT to @
                CDirectKeys _directKeys = new CDirectKeys();
                _directKeys.setKey(0x02, VKEY.VK_SPACE, CDirectKeys.directKeyType.kTypeShiftdVirtualKey);
                _directKeys.saveKeyTable();
                _directKeys = null;

                return true;
            }
        }
    }
//}