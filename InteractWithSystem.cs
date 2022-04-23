using System;
using System.Runtime.InteropServices;

namespace MouseJiggler
{

    internal class InteractWithSystem
    {

        [FlagsAttribute]
        public enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS        = 0x80000000,
            ES_DISPLAY_REQUIRED  = 0x00000002,
            ES_SYSTEM_REQUIRED   = 0x00000001
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        public static void ResetTimer()
        {
            EXECUTION_STATE r = SetThreadExecutionState(EXECUTION_STATE.ES_DISPLAY_REQUIRED | EXECUTION_STATE.ES_SYSTEM_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS);
            Console.WriteLine("Response from SetThreadExecutionState call {0:S}", r.ToString());
        }
    
    }
}
