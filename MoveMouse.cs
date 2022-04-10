using System.Runtime.InteropServices;

namespace MouseJiggler
{
    internal class MoveMouse
    {
        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        public static void SetCursorPosition(int x, int y)
        {
            SetCursorPos(x, y);
        }
    }
}
