using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace EventNamespace
{
    // "ITC_KEYBOARD_CHANGE" or ITC_KEYBOARD_CHANGE_USB
    /*
        Events monitored by the Keypad Driver:
        "ITC_KEYBOARD_CHANGE_USB"
        This event is to be set by the application whenever the USB keyboard map has been changed in the
        registry and results in an unload / load of the keyboard overlay.
        ITC_KEYCLICK_CHANGE"
        This event signifies that the key click setting may have changed
        "ITC_TE2000_KEYBOARD_ACTIVE"
        This event is set by TE2000 to notify the keypad driver that TE2000 is has keyboard focus and any
        modifications to special Terminal Emulation Keys are now in effect (e.g. Shift applied to Function Keys).     

        Notifications set by the Keypad Driver:
        "ITC_USB_KEYBOARD_LOAD_COMPLETE"
        This event signifies that USB keyboard overlay map has just been loaded by the driver
        "ITC_USB_KEYBOARD_UNLOAD_BEGIN"
        This event signifies that USB keyboard is beginning unload of current keyboard overlay map
        "_KeyPress"
        This event is set every keypress.
    */
    /// <summary>
    /// class to handle or fire ITC keyboard driver events
    /// ==================================================
    /// Events monitored by the Keypad Driver:
    /// "ITC_KEYBOARD_CHANGE_USB"
    /// This event is to be set by the application whenever the USB keyboard map has been changed in the
    /// registry and results in an unload / load of the keyboard overlay.
    /// ITC_KEYCLICK_CHANGE"
    /// This event signifies that the key click setting may have changed
    /// "ITC_TE2000_KEYBOARD_ACTIVE"
    /// This event is set by TE2000 to notify the keypad driver that TE2000 is has keyboard focus and any
    /// modifications to special Terminal Emulation Keys are now in effect (e.g. Shift applied to Function Keys).     
    /// Notifications set by the Keypad Driver:
    /// "ITC_USB_KEYBOARD_LOAD_COMPLETE"
    /// This event signifies that USB keyboard overlay map has just been loaded by the driver
    /// "ITC_USB_KEYBOARD_UNLOAD_BEGIN"
    /// This event signifies that USB keyboard is beginning unload of current keyboard overlay map
    /// "_KeyPress"
    /// This event is set every keypress.
/// </summary>
    static class EventHandling
    {
        [DllImport("coredll.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Auto)]
        public static extern IntPtr CreateEvent(IntPtr lpEventAttributes, [In, MarshalAs(UnmanagedType.Bool)] bool bManualReset, [In, MarshalAs(UnmanagedType.Bool)] bool bIntialState, [In, MarshalAs(UnmanagedType.BStr)] string lpName);

        [DllImport("coredll.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("coredll.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EventModify(IntPtr hEvent, [In, MarshalAs(UnmanagedType.U4)] int dEvent);

        /// <summary>
        /// event access types class needed for native OpenEvent etc.
        /// </summary>
        public static class EventAccess
        {
            public static uint SYNCHRONIZE { get { return 0x00100000; } }
            public static uint STANDARD_RIGHTS_REQUIRED { get { return 0x000F0000; } }
            public static uint EVENT_ALL_ACCESS { get { return STANDARD_RIGHTS_REQUIRED | SYNCHRONIZE | 0x3; } }
        }

        private const uint EVENT_SET = 3;

        // HANDLE OpenEvent(DWORD dwDesiredAccess, BOOL bInheritHandle, LPCTSTR lpName);
        [DllImport("coredll.dll")]
        private static extern IntPtr OpenEvent(uint dwDesiredAccess, int bInheritHandle, string lpName);

        /// <summary>
        /// provide the different event flag constants
        /// like PULSE, SET, RESET
        /// </summary>
        public static class EventFlags
        {
            public static int PULSE { get { return 1; } }
            public static int RESET { get { return 2; } }
            public static int SET { get { return  3; } }
            public static int EVENT_SET { get { return  3; } }
        }
        public static bool SetEvent(IntPtr hEvent)
        {
            return EventModify(hEvent, (int)EventFlags.SET);
        }
        public static bool ResetEvent(IntPtr hEvent)
        {
            return EventModify(hEvent, (int)EventFlags.RESET);
        }
        public static bool pulseNamedEvent(string name)
        {
            IntPtr p = CreateEvent(IntPtr.Zero, false, true, name);
            if (SetEvent(p))
                return ResetEvent(p);
            else
                return false;
        }
        public static bool setNamedEvent(string name)
        {
            IntPtr p = CreateEvent(IntPtr.Zero, false, true, name);
            if (p!=IntPtr.Zero)
                return SetEvent(p);
            else
                return false;
        }

        public static bool resetNamedEvent(string name)
        {
            IntPtr p = CreateEvent(IntPtr.Zero, false, true, name);
            if (p!=IntPtr.Zero)
                return ResetEvent(p);
            else
                return false;
        }
        public static IntPtr getEventHandle(string eventName)
        {
            IntPtr hEvent = IntPtr.Zero;
            //hEvent = CreateEvent(IntPtr.Zero, false, false, eventName);
            // Open named event that keyboard driver monitors
            hEvent = OpenEvent(EventAccess.EVENT_ALL_ACCESS, 0/*FALSE*/, eventName);
            if (hEvent != IntPtr.Zero)
                return hEvent;
            else
                return IntPtr.Zero;
        }

        public static IntPtr createNamedEvent(string eventName)
        {
            return CreateEvent(IntPtr.Zero, false, false, eventName);
        }
        public static IntPtr createNamedEvent(string eventName, bool bManualReset)
        {
            return CreateEvent(IntPtr.Zero, bManualReset, false, eventName);
        }
        private static bool NotifyEvent(string eventName)
        {
            IntPtr hEvent = IntPtr.Zero;
            bool result = false;

            // Open named event that keyboard driver monitors
            hEvent = OpenEvent(EventAccess.EVENT_ALL_ACCESS, 0/*FALSE*/, eventName);
            if (hEvent != IntPtr.Zero)
            {
                // Set named event to notify keyboard driver of changes to mapping
                if (EventModify(hEvent, EventFlags.EVENT_SET))
                {
                    System.Threading.Thread.Sleep(0); // yield to keyboard driver thread
                    result = true;
                }
                CloseHandle(hEvent);
            }
            return result;
        }
#region WaitObject
        [DllImport("coredll.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Auto)]
        static extern uint WaitForMultipleObjects(uint nCount, IntPtr[] lpHandles, [MarshalAs(UnmanagedType.Bool)] bool bWaitAll, uint dwMilliseconds);
        //static extern uint WaitForMultipleObjects(uint nCount, IntPtr lpHandles, [MarshalAs(UnmanagedType.Bool)] bool bWaitAll, uint dwMilliseconds);

        [DllImport("coredll.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Auto)]
        static extern uint WaitForSingleObject(IntPtr hEvent, uint dwMilliseconds);

        private const uint WAIT_OBJECT_0 = 0x00;
        private const uint WAIT_ABANDONED_0 = 0x80;
        private const uint WAIT_FAILED = 0xffffffff;
        private const uint INFINITE = 0xffffffff;
        private const uint WAIT_TIMEOUT = 0x00000102;

        private static class WaitOptions
        {
            public static uint WAIT_OBJECT_0 { get { return 0x00000000; } }
            public static uint WAIT_ABANDONED { get { return 0x00000080; } }
            public static uint WAIT_ABANDONED_0 { get { return 0x00000080; } }
            public static uint WAIT_FAILED { get { return 0xffffffff; } }
            public static uint INFINITE { get { return 0xffffffff; } }
            public static uint WAIT_TIMEOUT { get { return 0x00000102; } }
        }

        private static class waitAll {
            public static bool waitForAll
            {
                get{ return true; } //TRUE
            }
            public static bool waitForOne
            {
                get { return false; }
            }
        }
        /// <summary>
        /// Waits for named event that indicates the
        /// keyboard has reloaded the USB tables
        /// </summary>
        /// <returns>0 for success
        /// 1 for Abort Thread requested
        /// 2 for wait has timed out</returns>
        /// <throws>ArgumentOutOfRangeException for failures</throws>
        public static uint WaitForUsbLoadComplete()
        {
            uint iState = 99;
            IntPtr evtAbort = EventNamespace.EventHandling.createNamedEvent("USBKeyWaitAbort");
            if (evtAbort == IntPtr.Zero)
                throw new ArgumentOutOfRangeException("getEventHandle: Could not create AbortEvent handle");

            IntPtr evtDelta = EventNamespace.EventHandling.createNamedEvent("ITC_USB_KEYBOARD_LOAD_COMPLETE");
            if (evtDelta == IntPtr.Zero)
                throw new ArgumentOutOfRangeException("getEventHandle: Could not open DeltaEvent handle");
            
            IntPtr[] lpHandles2 = new IntPtr[2];
            lpHandles2[0] = evtDelta;
            lpHandles2[1] = evtAbort;

            uint uRes = WaitForMultipleObjects((uint)lpHandles2.Length, lpHandles2, waitAll.waitForOne, 3000);
            switch (uRes)
            {
                case WAIT_OBJECT_0:
                    //the delta event has been signaled
                    //key has been released
                    iState = 0;
                    break;
                case (WAIT_OBJECT_0 + 1):
                    //Abort requested
                    iState = 1;
                    break;
                case (WAIT_TIMEOUT):
                    iState = 2; //timed out
                    break;
                default:
                    break;
            }//switch2
            return iState;
        }
        /// <summary>
        /// Waits for named event that indicates the
        /// keyboard has reloaded the DIRECT tables
        /// </summary>
        /// <returns>0 for success
        /// 1 for Abort Thread requested
        /// 2 for wait has timed out</returns>
        public static uint WaitForDirectLoadComplete()
        {
            uint iState = 99;
            IntPtr evtAbort = EventNamespace.EventHandling.createNamedEvent("USBKeyWaitAbort");
            if (evtAbort == IntPtr.Zero)
                throw new ArgumentOutOfRangeException("getEventHandle: Could not create AbortEvent handle");

            IntPtr evtDelta = EventNamespace.EventHandling.createNamedEvent("ITC_KEYBOARD_LOAD_COMPLETE");
            if (evtDelta == IntPtr.Zero)
                throw new ArgumentOutOfRangeException("getEventHandle: Could not open DeltaEvent handle");
            
            IntPtr[] lpHandles2 = new IntPtr[2];
            lpHandles2[0] = evtDelta;
            lpHandles2[1] = evtAbort;

            uint uRes = WaitForMultipleObjects((uint)lpHandles2.Length, lpHandles2, waitAll.waitForOne, 3000);
            switch (uRes)
            {
                case WAIT_OBJECT_0:
                    //the delta event has been signaled
                    //key has been released
                    iState = 0;
                    break;
                case (WAIT_OBJECT_0 + 1):
                    //Abort requested
                    iState = 1;
                    break;
                case (WAIT_TIMEOUT):
                    iState = 2; //timed out
                    break;
                default:
                    break;
            }//switch2
            return iState;
        }
#endregion
    }
}
