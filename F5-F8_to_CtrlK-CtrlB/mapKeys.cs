using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using ITC_KEYBOARD;
using System.Windows.Forms;

namespace KeyMapper
{
    class remapF5_to_F8_to_CtrlKLNB
    {
        ITC_KEYBOARD.CUSBkeys _cusbKeys;
        CUsbKeyTypes.HWkeys[] keysToMap = new CUsbKeyTypes.HWkeys[] { CUsbKeyTypes.HWkeys.Left_Control };
        LogForm.CallBackForm _form;

        public remapF5_to_F8_to_CtrlKLNB(LogForm.CallBackForm f)
        {
            _cusbKeys = new CUSBkeys();
            _form = f;
            _form.addLog("init done");
        }

        /// <summary>
        /// map the function keys:
        /// F4 to Ctrl+n
        /// F5 to Ctrl+g
        /// </summary>
        public void mapKeys()
        {
            _form.addLog("mapKeys():");
            keysToMap = new CUsbKeyTypes.HWkeys[4];
            keysToMap[0] = CUsbKeyTypes.HWkeys.F5_cozumel;
            /*
            keysToMap[1] = CUsbKeyTypes.HWkeys.F1_cozumel;//orange plane F1 == F6
            keysToMap[2] = CUsbKeyTypes.HWkeys.F2_cozumel;//orange plane F2 == F7
            keysToMap[3] = CUsbKeyTypes.HWkeys.F3_cozumel;//orange plane F3 == F8
            */
            keysToMap[1] = CUsbKeyTypes.HWkeys.F6_cozumel;
            keysToMap[2] = CUsbKeyTypes.HWkeys.F7_cozumel;
            keysToMap[3] = CUsbKeyTypes.HWkeys.F8_cozumel;

            for (int i=0; i<keysToMap.Length;i++)
                _form.addLog("prepare mapping for key: "+ keysToMap[i]);
            createMultiKeys();
        }

        /// <summary>
        /// create multikey entries with Ctrl + Letters
        /// Control is 08,02,01,14
        /// </summary>
        public void createMultiKeys()
        {
            _form.addLog("createMultiKeys():");
            /*
            leftCtrl key is 
                07,E0,00,00,08,01 'Left Control' [NoFlag,NoFlag,ModifierIndex,] 'F9' 'ModifierKey'->08,02,01,14 'Left Control' | 
            */
            //F1 key:   = 07 3A 00 00 00 05
            // ########## as multi4 key:        07,3a,00,02,04,04

            //prepare a new multikey entry for two keys
            CUSBkeys.usbKeyStructShort[] uKey = new CUSBkeys.usbKeyStructShort[2];  // we need Ctrl+n
            //map to Ctrl 
            //find Ctrl modifier index
            ITC_KEYBOARD.CModifiersKeys _cModis = new CModifiersKeys();
            int iModiCtrlIndex = findCtrlModifier();

            //create a key struct that points to the ctrl key modifier entry
            uKey[0].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey[0].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoFlag;
            uKey[0].bFlagLow = CUsbKeyTypes.usbFlagsLow.ModifierIndex;
            uKey[0].bIntScan = (byte)iModiCtrlIndex;  //point to modifier index
            //create an entry for k
            uKey[1].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey[1].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoFlag;
            uKey[1].bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
            uKey[1].bIntScan = (byte)ITC_KEYBOARD.PS2KEYS.K;  //point to K key

            //try to find existing MultiKey
            CMultiKeys cmulti = new CMultiKeys();
            int iMax = cmulti.getMultiKeyCount();
            int iFoundCtrlK = cmulti.findMultiKey(uKey);
            if (iFoundCtrlK == -1)//if there was no existing entry, create a new one
            {
                iFoundCtrlK = cmulti.addMultiKey(uKey);
            }
            _form.addLog(string.Format("done Multikey{0}",iFoundCtrlK));

            //create another multikey entry for Ctrl+L
            //create a key struct that points to the ctrl key modifier entry
            uKey[0].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey[0].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoFlag;
            uKey[0].bFlagLow = CUsbKeyTypes.usbFlagsLow.ModifierIndex;
            uKey[0].bIntScan = (byte)iModiCtrlIndex;  //point to modifier index
            //map to g
            uKey[1].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey[1].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoFlag;
            uKey[1].bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
            uKey[1].bIntScan = (byte)ITC_KEYBOARD.PS2KEYS.L;  //point to L key
            //is this already known?
            int iFoundCtrlL = cmulti.findMultiKey(uKey);
            if (iFoundCtrlL == -1)//if there was no existing entry, create a new one
            {
                iFoundCtrlL = cmulti.addMultiKey(uKey);
            }
            _form.addLog(string.Format("done Multikey{0}", iFoundCtrlL));

            //create another multikey entry for Ctrl+N
            //create a key struct that points to the ctrl key modifier entry
            uKey[0].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey[0].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoFlag;
            uKey[0].bFlagLow = CUsbKeyTypes.usbFlagsLow.ModifierIndex;
            uKey[0].bIntScan = (byte)iModiCtrlIndex;  //point to modifier index
            //map to g
            uKey[1].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey[1].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoFlag;
            uKey[1].bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
            uKey[1].bIntScan = (byte)ITC_KEYBOARD.PS2KEYS.N;  //point to N key
            //is this already known?
            int iFoundCtrlN = cmulti.findMultiKey(uKey);
            if (iFoundCtrlN == -1)//if there was no existing entry, create a new one
            {
                iFoundCtrlN = cmulti.addMultiKey(uKey);
            }
            _form.addLog(string.Format("done Multikey{0}", iFoundCtrlN));

            //create another multikey entry for Ctrl+B
            //create a key struct that points to the ctrl key modifier entry
            uKey[0].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey[0].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoFlag;
            uKey[0].bFlagLow = CUsbKeyTypes.usbFlagsLow.ModifierIndex;
            uKey[0].bIntScan = (byte)iModiCtrlIndex;  //point to modifier index
            //map to g
            uKey[1].bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
            uKey[1].bFlagMid = CUsbKeyTypes.usbFlagsMid.NoFlag;
            uKey[1].bFlagLow = CUsbKeyTypes.usbFlagsLow.NormalKey;
            uKey[1].bIntScan = (byte)ITC_KEYBOARD.PS2KEYS.B;  //point to B key
            //is this already known?
            int iFoundCtrlB = cmulti.findMultiKey(uKey);
            if (iFoundCtrlB == -1)//if there was no existing entry, create a new one
            {
                iFoundCtrlB = cmulti.addMultiKey(uKey);
            }
            _form.addLog(string.Format("done Multikey{0}", iFoundCtrlB));

            //now map the key(s) in question to the new/existing MultiKeyEntry in all planes
            //CUSBkeys.usbKeyStruct remapKey = new CUSBkeys.usbKeyStruct();

            //############### map the keys to the new multikeys #####################
            //map F1 key in plane x to new multikey
#if DEBUG
            keysToMap[0] = CUsbKeyTypes.HWkeys.g;
            keysToMap[1] = CUsbKeyTypes.HWkeys.h;
            setKeyToMultiKey((int)keysToMap[0], (byte)(iFoundCtrlN), cPlanes.plane.green);
            setKeyToMultiKey((int)keysToMap[1], (byte)(iFoundCtrlG), cPlanes.plane.green);
#else
            /*
            //find key and plane
            int plane=-1;
            int iRes1 = 0;
            CUSBkeys.usbKeyStruct _ukey=new CUSBkeys.usbKeyStruct();
            CUsbKeyTypes.HWkeys bVKey;
            
            bVKey=CUsbKeyTypes.HWkeys.F5_cozumel;
            iRes1 = _cusbKeys.findHWKey(bVKey, ref plane, ref _ukey);
            if(iRes1==0)
                _form.addLog(string.Format("found plane {0} for {1}, USBHID={2}:0x{3:x}->0x{4:x}", plane, bVKey, _ukey.bHID, _ukey.bScanKey, _ukey.bIntScan));
            else
                _form.addLog(string.Format("no plane found for {0}", bVKey));

            iRes1 = _cusbKeys.findVKey((byte)VKEY.VK_F6, ref plane, ref _ukey);
            if(iRes1==0)
                _form.addLog(string.Format("found plane {0} for {1}, USBHID={2}:0x{3:x}->0x{4:x}", plane, VKEY.VK_F6, _ukey.bHID, _ukey.bScanKey, _ukey.bIntScan));
            else
                _form.addLog(string.Format("no plane found for {0}", bVKey));

            bVKey=CUsbKeyTypes.HWkeys.F6_cozumel;
            iRes1 = _cusbKeys.findHWKey(bVKey, ref plane, ref _ukey);
            if(iRes1==0)
                _form.addLog(string.Format("found plane {0} for {1}, USBHID={2}:0x{3:x}->0x{4:x}", plane, bVKey, _ukey.bHID, _ukey.bScanKey, _ukey.bIntScan));
            else
                _form.addLog(string.Format("no plane found for {0}", bVKey));
            
            bVKey = CUsbKeyTypes.HWkeys.F7_cozumel;
            iRes1 = _cusbKeys.findHWKey(bVKey, ref plane, ref _ukey);
            if(iRes1==0)
                _form.addLog(string.Format("found plane {0} for {1}, USBHID={2}:0x{3:x}->0x{4:x}", plane, bVKey, _ukey.bHID, _ukey.bScanKey, _ukey.bIntScan));
            else
                _form.addLog(string.Format("no plane found for {0}", bVKey));

            bVKey = CUsbKeyTypes.HWkeys.F8_cozumel;
            iRes1 = _cusbKeys.findHWKey(bVKey, ref plane, ref _ukey);
            if(iRes1==0)
                _form.addLog(string.Format("found plane {0} for {1}, USBHID={2}:0x{3:x}->0x{4:x}", plane, bVKey, _ukey.bHID, _ukey.bScanKey, _ukey.bIntScan));
            else
                _form.addLog(string.Format("no plane found for {0}", bVKey));
            */

            //set keys 
            setKeyToMultiKey((int)keysToMap[0], (byte)(iFoundCtrlK), cPlanes.plane.normal);
            setKeyToMultiKey((int)keysToMap[1], (byte)(iFoundCtrlL), cPlanes.plane.normal);
            setKeyToMultiKey((int)keysToMap[2], (byte)(iFoundCtrlN), cPlanes.plane.normal);
            setKeyToMultiKey((int)keysToMap[3], (byte)(iFoundCtrlB), cPlanes.plane.normal);
#endif
            //save and update
            _form.addLog("saving...");
            int iRes = _cusbKeys.writeKeyTables();
            if (iRes != 0)
            {
                MessageBox.Show("Need reboot");
            }
            else
                _form.addLog("No reboot needed");
        }

        /// <summary>
        /// map a key to a multikey index
        /// </summary>
        /// <param name="iKey">the key to map</param>
        /// <param name="iMultiIndex">zero based index to Multi1, Multi2...</param>
        /// <param name="cPlane">keyboard plane</param>
        private void setKeyToMultiKey(int iKey, byte iMultiIndex, cPlanes.plane cPlane)
        {
            CUSBkeys.usbKeyStruct remapKey = new CUSBkeys.usbKeyStruct();

            if (_cusbKeys.getKeyStruct((int)cPlane, iKey, ref remapKey) != -1)
            {   //key exists
                _form.addLog("mapping existing key 0x" + iKey.ToString("x"));
                remapKey.bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
                remapKey.bFlagMid = CUsbKeyTypes.usbFlagsMid.NoRepeat | CUsbKeyTypes.usbFlagsMid.NoChord;
                remapKey.bFlagLow = CUsbKeyTypes.usbFlagsLow.MultiKeyIndex;
                remapKey.bIntScan = (byte)(iMultiIndex + 1); //the idx is zero based! but it is Multi1, Multi2..
                _cusbKeys.setKey((int)cPlane, iKey, remapKey);
            }
            else
            {
                //add key
                _form.addLog("adding new entry for 0x" + iKey.ToString("x"));
                remapKey.bHID = 0x07;
                remapKey.bScanKey = (CUsbKeyTypes.HWkeys)iKey;
                remapKey.bFlagHigh = CUsbKeyTypes.usbFlagsHigh.NoFlag;
                remapKey.bFlagMid = CUsbKeyTypes.usbFlagsMid.NoRepeat | CUsbKeyTypes.usbFlagsMid.NoChord;
                remapKey.bFlagLow = CUsbKeyTypes.usbFlagsLow.MultiKeyIndex;
                remapKey.bIntScan = (byte)iMultiIndex;
                _cusbKeys.addKey(cPlane, remapKey);
            }
            System.Diagnostics.Debug.WriteLine("setKeyToMultiKey: " + _cusbKeys.dumpKey(remapKey));
            _form.addLog("setKeyToMultiKey: " + _cusbKeys.dumpKey(remapKey));
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

        public void restoreDefaultMappings()
        {
            CUSBkeys cusb = new CUSBkeys();
            cusb.resetKeyDefaults();
        }
    }
}
