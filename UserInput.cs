using System;
using System.Runtime.InteropServices;

internal struct LASTINPUTINFO
{
    public uint cbSize;
    public uint dwTime;
}

namespace MouseJiggler
{
    internal static class UserInput
    {
        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        public static uint IdleTime()
        {
            LASTINPUTINFO lastinputinfo = new LASTINPUTINFO();
            lastinputinfo.cbSize = (uint)Marshal.SizeOf(lastinputinfo);
            GetLastInputInfo(ref lastinputinfo);
            return ((uint)Environment.TickCount - lastinputinfo.dwTime);
        }
    }
}