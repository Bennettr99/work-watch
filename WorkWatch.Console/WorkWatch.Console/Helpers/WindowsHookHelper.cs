using System;
using System.Runtime.InteropServices;

namespace WorkWatch.Console.Helpers
{ // from http://joelabrahamsson.com/detecting-mouse-and-keyboard-input-with-net/
    public class WindowsHookHelper
    {
        public delegate IntPtr HookDelegate(
            int Code, IntPtr wParam, IntPtr lParam);

        [DllImport("User32.dll")]
        public static extern IntPtr CallNextHookEx(
            IntPtr hHook, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("User32.dll")]
        public static extern IntPtr UnhookWindowsHookEx(IntPtr hHook);


        [DllImport("User32.dll")]
        public static extern IntPtr SetWindowsHookEx(
            int idHook, HookDelegate lpfn, IntPtr hmod,
            int dwThreadId);
    }
}