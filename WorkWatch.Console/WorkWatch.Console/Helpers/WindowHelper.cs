using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace WorkWatch.Console.Helpers
{ //from https://stackoverflow.com/questions/115868/how-do-i-get-the-title-of-the-current-active-window-using-c
    public class WindowHelper
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        public string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }

        public string GetActiveWindowApplication()
        {
            var windowTitle = GetActiveWindowTitle();

            return windowTitle?.Split('-')
                .Select((titleSegment) => titleSegment.Trim())
                .Last();
        }
    }
}