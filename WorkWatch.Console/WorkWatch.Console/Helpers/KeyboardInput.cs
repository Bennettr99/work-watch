using System;

namespace WorkWatch.Console.Helpers
{ // from http://joelabrahamsson.com/detecting-mouse-and-keyboard-input-with-net/
    public class KeyboardInput : IDisposable
    {
        private const int WH_KEYBOARD_LL = 13;
        private bool disposed;

        private readonly WindowsHookHelper.HookDelegate keyBoardDelegate;
        private readonly IntPtr keyBoardHandle;

        public KeyboardInput()
        {
            keyBoardDelegate = KeyboardHookDelegate;
            keyBoardHandle = WindowsHookHelper.SetWindowsHookEx(
                WH_KEYBOARD_LL, keyBoardDelegate, IntPtr.Zero, 0);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public event EventHandler<EventArgs> KeyBoardKeyPressed;

        private IntPtr KeyboardHookDelegate(
            int Code, IntPtr wParam, IntPtr lParam)
        {
            if (Code < 0)
                return WindowsHookHelper.CallNextHookEx(
                    keyBoardHandle, Code, wParam, lParam);

            if (KeyBoardKeyPressed != null)
                KeyBoardKeyPressed(this, new EventArgs());

            return WindowsHookHelper.CallNextHookEx(
                keyBoardHandle, Code, wParam, lParam);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (keyBoardHandle != IntPtr.Zero)
                    WindowsHookHelper.UnhookWindowsHookEx(
                        keyBoardHandle);

                disposed = true;
            }
        }

        ~KeyboardInput()
        {
            Dispose(false);
        }
    }
}