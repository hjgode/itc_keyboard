using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using EventNamespace;

namespace ITC_KEYBOARD
{
    /// <summary>
    /// class to manage event key entries
    /// </summary>
	public class CkeybNamedEvents
	{
//		[DllImport("CN3kbdRemap.cpl", CharSet = CharSet.Unicode)]
//		private static extern int ITC_ResetKeyDefaults();
		/// <summary>
		/// named events are a EventName (Event1 to EventX)
		/// and a eventValue (ie 'StateLeftScan')
		/// </summary>
        public struct eventPair
        {
            /// <summary>
            /// name of the event
            /// </summary>
            public string eventName;
            /// <summary>
            /// contents of the event
            /// </summary>
            public string eventValue;
        }
        //use directKey or usbKey tables
        private bool _bDirectKey = false;
		/// <summary>
		/// delta events list
		/// </summary>
        private eventPair[] _eventPairsDelta;
        /// <summary>
        /// state events list
        /// </summary>
        private eventPair[] _eventPairsState;
		/// <summary>
		/// Create a new instance of the named events class
		/// </summary>
		public CkeybNamedEvents (bool bDirectKey)
		{
            this._bDirectKey = bDirectKey;
			readReg();
		}
        /// <summary>
        /// get the list of state events
        /// </summary>
        /// <returns>an eventPair list</returns>
        public eventPair[] getStateEvents()
        {
            return _eventPairsState;
        }
        /// <summary>
        /// get the list of delta events
        /// </summary>
        /// <returns>an eventPait list</returns>
        public eventPair[] getDeltaEvents()
        {
            return _eventPairsDelta;
        }
        /// <summary>
        /// get the full platform name of the current device
        /// </summary>
        /// <returns>a string with the platform name</returns>
        private string getPlatformModel()
        {
            string sTemp = "";
            try
            {
                Microsoft.Win32.RegistryKey tempKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"\Platform");
                sTemp = (string)tempKey.GetValue("Model", "");
            }
            catch (Exception)
            {
                
            }
            return sTemp;
        }
		/// <summary>
		/// read the registry and fill the internal tables
		/// </summary>
        private void readReg()
        {
			if(this._eventPairsDelta!=null){
				this._eventPairsDelta=null;
				this._eventPairsState=null;
			}
            string sRegKey;
            if (getPlatformModel() != "CN50")
                sRegKey = getLocation();
            else
                sRegKey = @"Hardware\DeviceMap\Keybd";
            Microsoft.Win32.RegistryKey tempKey;// = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(sRegKey);
/*            //byte[] defBytes = new byte[16] = new byte[16] { 0x00, 0x00, 0x00, 0x00, 0xc5, 0x07, 0x01, 0x05, 0x75, 0x00, 0x76, 0x00, 0x00, 0x00, 0x00, 0x00 };
            tempKey.OpenSubKey(sRegKey);
            int iCount = tempKey.ValueCount;
            _bDirectKeys = (byte[])tempKey.GetValue("VKey");
            tempKey.Close();
            _bKeyPairs = this.RawDeserialize2(_bDirectKeys);
			 */
            tempKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(sRegKey+@"\Events\Delta");
            string[] deltaKeys = tempKey.GetValueNames();
            _eventPairsDelta = new eventPair[deltaKeys.Length];
            for (int i = 0; i < deltaKeys.Length; i++)
            {
                _eventPairsDelta[i].eventName = deltaKeys[i];
                _eventPairsDelta[i].eventValue = (string)tempKey.GetValue(deltaKeys[i]);
            }
            tempKey.Close();
            tempKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(sRegKey+@"\Events\State");
            string[] stateKeys = tempKey.GetValueNames();
            _eventPairsState = new eventPair[stateKeys.Length];
            for (int i = 0; i < stateKeys.Length; i++)
            {
                _eventPairsState[i].eventName = stateKeys[i];
                _eventPairsState[i].eventValue = (string)tempKey.GetValue(stateKeys[i]);
            }
            tempKey.Close();
        }
		/// <summary>
		/// used only for debugging, emulation and documentation
		/// </summary>
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
        private string getLocation()
        {
#if EMULATE
            return @"Hardware\DeviceMap\Keybd\0000";
#else
            /*
            [HKEY_LOCAL_MACHINE\HARDWARE\DEVICEMAP\KEYBD]
            "ActiveConfig"="0000"*/
            Microsoft.Win32.RegistryKey tempKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"Hardware\DeviceMap\Keybd");
            string regKeyb="";
            if(this._bDirectKey){
                regKeyb = (string)tempKey.GetValue("ActiveConfig");
                tempKey.Close();
                return @"Hardware\DeviceMap\Keybd\"+regKeyb;
            }
            else{
                regKeyb = (string)tempKey.GetValue("CurrentActiveLayoutKey");
                tempKey.Close();
                return regKeyb;
            }
#endif
        }
        /// <summary>
        /// get delta event for event with name
        /// </summary>
        /// <param name="sEvent"></param>
        /// <returns></returns>
        public string getDeltaEvent(string sEvent)
        {
            for (int i = 0; i < this._eventPairsDelta.Length; i++)
            {
                if (sEvent == this._eventPairsDelta[i].eventName)
                    return this._eventPairsDelta[i].eventValue;
            }
            return "";
        }
        /// <summary>
        /// get state event for event with name
        /// </summary>
        /// <param name="sEvent"></param>
        /// <returns></returns>
        public string getStateEvent(string sEvent)
        {
            for (int i = 0; i < this._eventPairsState.Length; i++)
            {
                if (sEvent == this._eventPairsState[i].eventName)
                    return this._eventPairsState[i].eventValue;
            }
            return "";
        }
		
        /// <summary>
        /// get delta event for event with index
        /// </summary>
        /// <param name="iIndex"></param>
        /// <returns></returns>
        public string getDeltaEvent(int iIndex)
        {
			//find event name with ending iIndex
			string searchEventName="Event"+iIndex.ToString();
			for(int i=0; i<this._eventPairsDelta.Length; i++){
				if(searchEventName==this._eventPairsDelta[i].eventName)
					return this._eventPairsDelta[i].eventValue;
			}
			return "";
		}

        /// <summary>
        /// get state event for event with index
        /// </summary>
        /// <param name="iIndex"></param>
        /// <returns></returns>
		public string getStateEvent(int iIndex)
        {
			//find event name with ending iIndex
			string searchEventName="Event"+iIndex.ToString();
			for(int i=0; i<this._eventPairsState.Length; i++){
                if (searchEventName == this._eventPairsState[i].eventName)
                    return this._eventPairsState[i].eventValue;
			}
			return "";
		}
        /// <summary>
        /// get the list of all delta event names
        /// </summary>
        /// <returns></returns>
		public string[] getDeltaEventNames()
        {
			string [] _deltaEvents=new string[this._eventPairsDelta.Length];
			for(int i=0; i<this._eventPairsDelta.Length; i++){
                _deltaEvents[i] = this._eventPairsDelta[i].eventName;
			}
			return _deltaEvents;
		}
        /// <summary>
        /// get list of all state event names
        /// </summary>
        /// <returns></returns>
		public string[] getStateEventNames()
        {            
			string [] _stateEvents=new string[this._eventPairsState.Length];
            for (int i = 0; i < _eventPairsState.Length; i++)
            {
				_stateEvents[i]=this._eventPairsState[i].eventName;
			}
			return _stateEvents;
/*			
            string sRegEventsState = getRegLocation() + @"\Events\State";
            //the events are below this in branches called Events\State and Events\Delta
            //open the State branch
            Microsoft.Win32.RegistryKey tempKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(sRegEventsState, true);
            //the keys are 'numbered' Event1 to Eventx
            
            string[] sStateEvents = new string[tempKey.ValueCount];
            for (int i = 0; i < tempKey.ValueCount; i++)
            {
                string sEventVal = "Event" + (i + 1).ToString();
                sStateEvents[i] = (string)tempKey.GetValue(sEventVal);
            }
            tempKey.Close();
            return sStateEvents;
*/
        }
        private string getRegLocation()
        {
            return getLocation();
        }

		/// <summary>
		/// add a named event at end of table
		/// </summary>
		/// <param name="sState">
		/// the name of the state event
		/// </param>
		/// <param name="sDelta">
		/// the name of the delta event
		/// </param>
		/// <returns>
		/// idx of new entry on success
		/// -1 on failure
		/// </returns>
        public int addNamedEvent(string sState, string sDelta)
        {
            bool isCN50 = false;
            try
            {
                string sPlatformModel = "";
                Microsoft.Win32.RegistryKey regPlatform = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Platform");
                sPlatformModel = (string)regPlatform.GetValue("Model");
                if (sPlatformModel == "CN50")
                    isCN50 = true;
            }
            catch
            {
            }

            string sRegEventsState;
            string sRegEventsDelta;
            if (!isCN50)
            {
                sRegEventsState = getRegLocation() + @"\Events\State";
                sRegEventsDelta = getRegLocation() + @"\Events\Delta";
            }
            else
            {
                sRegEventsState = @"HARDWARE\DEVICEMAP\KEYBD" + @"\Events\State";
                sRegEventsDelta = @"HARDWARE\DEVICEMAP\KEYBD" + @"\Events\Delta";
            }

            //the events are below this in branches called Events\State and Events\Delta
            int iNewEventCount = this.getEventCount() + 1;
            string sNewEventValueName = "Event" + iNewEventCount.ToString();
            try
            {
                Microsoft.Win32.RegistryKey regTemp = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(sRegEventsState, true);
                regTemp.SetValue(sNewEventValueName, sState,Microsoft.Win32.RegistryValueKind.String);
                regTemp.Close();
                regTemp = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(sRegEventsDelta, true);
                regTemp.SetValue(sNewEventValueName, sDelta, Microsoft.Win32.RegistryValueKind.String);
                regTemp.Close();
				//read tables
				this.readReg();
                return iNewEventCount-1;
            }
            catch (Exception)
            {
                return -1;                
            }
        }

        /// <summary>
        /// get the index of a NamedEvent
        /// </summary>
        /// <returns>
        /// the number (EventXX) of the NamedEvents or -1 if missing
        /// NOTE: idx starts at 0 but registry uses 1 as start
        /// </returns>
        public int findEventIndex(string sNamedEvent)
        {
            int iRet = -1;
            string sRegEventsState = getRegLocation() + @"\Events\State";
            string sRegEventsDelta = getRegLocation() + @"\Events\Delta";

            //the events are below this in branches called Events\State and Events\Delta
            int iEventCount = this.getEventCount();
            int idx = 0;
            do 
            {
                if (this._eventPairsDelta[idx].eventValue == sNamedEvent)
                {
                    iRet = Convert.ToInt16(this._eventPairsDelta[idx].eventName.Substring("Event".Length));
                }
                if (this._eventPairsState[idx].eventValue == sNamedEvent)
                    iRet = Convert.ToInt16(this._eventPairsDelta[idx].eventName.Substring("Event".Length)); 

                idx++;
            }while (idx<iEventCount && iRet==-1);
            return iRet;
        }
		/// <summary>
		/// get the number og known named events
		/// </summary>
		/// <returns>
		/// the number of named events
		/// </returns>
        private int getEventCount()
        {
			//or simply
			return this._eventPairsDelta.Length;
/*			
			//dynamic
            string sRegEventsState = getRegLocation() + @"\Events\State";
            Microsoft.Win32.RegistryKey tempKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(sRegEventsState, true);
            //the keys are 'numbered' Event1 to Eventx
            int iValCount = tempKey.ValueCount;
            tempKey.Close();
            return iValCount;
*/
        }

        /// <summary>
        /// for simplicity only the last entry can be removed
        /// </summary>
        /// <returns>0 if success
        /// -1 on failure</returns>
        public int removeNamedEvent(string sEventName)
        {
            string sRegEventsState = getRegLocation() + @"\Events\State";
            string sRegEventsDelta = getRegLocation() + @"\Events\Delta";

            //the events are below this in branches called Events\State and Events\Delta
            string sDelEventValueName = sEventName;
            try
            {
                Microsoft.Win32.RegistryKey regTemp = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(sRegEventsState, true);
                regTemp.DeleteValue(sDelEventValueName);
                regTemp.Close();
                regTemp = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(sRegEventsDelta, true);
                regTemp.DeleteValue(sDelEventValueName);
                regTemp.Close();
				//update tables
				this.readReg();
                return 0;
            }
            catch (Exception)
            {
                return -1;                
            }
        }
        private void updateDriver()
        {
            EventHandling.setNamedEvent("ITC_KEYBOARD_CHANGE");
        }
        //public void resetKeyDefaults()
        //{
        //    try
        //    {
        //        ITC_ResetKeyDefaults();

        //    }
        //    catch (Exception)
        //    {
        //        System.Windows.Forms.MessageBox.Show("Did you install/copy the keymap applet?");
        //    }
        //}
	}
}
