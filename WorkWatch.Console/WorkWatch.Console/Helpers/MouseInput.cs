using System;

namespace WorkWatch.Console.Helpers
{ // from http://joelabrahamsson.com/detecting-mouse-and-keyboard-input-with-net/
    public class MouseInput : IDisposable
    {
        private const int WH_MOUSE_LL = 14;

        private bool disposed;

        private readonly WindowsHookHelper.HookDelegate mouseDelegate;
        private readonly IntPtr mouseHandle;

        public MouseInput()
        {
            mouseDelegate = MouseHookDelegate;
            mouseHandle = WindowsHookHelper.SetWindowsHookEx(WH_MOUSE_LL, mouseDelegate, IntPtr.Zero, 0);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public event EventHandler<EventArgs> MouseMoved;

        private IntPtr MouseHookDelegate(int Code, IntPtr wParam, IntPtr lParam)
        {
            if (Code < 0)
                return WindowsHookHelper.CallNextHookEx(mouseHandle, Code, wParam, lParam);

            if (MouseMoved != null)
                MouseMoved(this, new EventArgs());

            return WindowsHookHelper.CallNextHookEx(mouseHandle, Code, wParam, lParam);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (mouseHandle != IntPtr.Zero)
                    WindowsHookHelper.UnhookWindowsHookEx(mouseHandle);

                disposed = true;
            }
        }

        ~MouseInput()
        {
            Dispose(false);
        }
    }
}