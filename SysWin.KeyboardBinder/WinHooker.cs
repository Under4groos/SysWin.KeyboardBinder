using System.Windows;
using System.Windows.Interop;
using static SysWin.KeyboardBinder.Delegates.Interop;

namespace SysWin.KeyboardBinder
{

    public class WinHooker : IDisposable
    {
        protected HwndSource? hwndSource;

        public event EventWidowProc? Wndproc;

        public WinHooker(Window window)
        {

            // https://learn.microsoft.com/en-us/dotnet/api/system.windows.presentationsource.fromvisual?view=windowsdesktop-9.0
            if (PresentationSource.FromVisual(window) is HwndSource hwndSource && hwndSource != null)
                hwndSource.AddHook(WndProc);
        }

        public virtual IntPtr WndProc(nint hwnd, int msg, nint wParam, nint lParam, ref bool handled)
        {
            if (Wndproc != null)
                return Wndproc?.Invoke(hwnd, msg, wParam, lParam, handled) ?? IntPtr.Zero;
            return IntPtr.Zero;
        }
        public virtual void Dispose()
        {
            hwndSource?.Dispose();
        }
    }
}
