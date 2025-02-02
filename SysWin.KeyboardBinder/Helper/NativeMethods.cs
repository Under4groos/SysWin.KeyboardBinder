using System.Runtime.InteropServices;
using SysWin.KeyboardBinder.Enums;

namespace SysWin.KeyboardBinder.Helper
{
    internal static class NativeMethods
    {
        #region Keyboard
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, WMKeys fsModifiers, uint vk);

        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        #endregion
    }
}
