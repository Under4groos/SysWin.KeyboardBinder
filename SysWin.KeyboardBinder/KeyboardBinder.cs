
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using SysWin.KeyboardBinder.Enums;
using SysWin.KeyboardBinder.Helper;
using SysWin.KeyboardBinder.Structures;
using static SysWin.KeyboardBinder.Delegates.Interop;

namespace SysWin.KeyboardBinder
{
    public class KeyboardBinder : IDisposable
    {
        private int HOTKEY_ID = 0x0312;
        private Random r = new Random();
        private ObservableCollection<WMKeyBind> wMKeyBinds = new ObservableCollection<WMKeyBind>();
        public ObservableCollection<WMKeyBind> WMKeyBinds
        {
            get
            {
                return wMKeyBinds;
            }
        }
        protected HwndSource? hwndSource;

        public EventKeyDown? WndProcKeyDown;
        public EventKeyBindingChange? KeyBindingChange;
        public KeyboardBinder(Window window)
        {
            // https://learn.microsoft.com/en-us/dotnet/api/system.windows.presentationsource.fromvisual?view=windowsdesktop-9.0
            if (PresentationSource.FromVisual(window) is HwndSource hwnd)
            {
                hwndSource = hwnd;
                hwndSource.AddHook(WndProc);
            }

        }



        #region Key Register
        public bool Register(WMKeys wMKeys, Key keyBind, int id)
        {
            // https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-registerhotkey

            if (hwndSource == null)
                throw new ArithmeticException("Object reference not set to an instance of an object.");

            var newKeyBind = new WMKeyBind()
            {
                handle = hwndSource.Handle,
                wmBindKey = wMKeys,
                binKey = keyBind,
                bindId = (int)id
            };

            if (NativeMethods.RegisterHotKey(hwndSource.Handle, (int)id, wMKeys, (uint)KeyInterop.VirtualKeyFromKey(keyBind)) is bool status)
            {

                wMKeyBinds.Add(newKeyBind);
                KeyBindingChange?.Invoke(newKeyBind);
                return true;
            }
            else
            {
                throw new ArithmeticException($"Hotkey '{newKeyBind}' registered, using MOD_NOREPEAT flag\\n\"");

            }


        }
        public bool Register(WMKeys wMKeys, Key keyBind)
        {
            return Register(wMKeys, keyBind, r.Next(0, 999 ^ 10));
        }
        public bool Register(Key keyBind, WMKeys wMKeys = WMKeys.MOD_ALT)
        {
            return Register(wMKeys, keyBind);
        }
        #endregion

        #region Key UnRegister

        public void UnRegister(nint winHandle, int bindId)
        {
            NativeMethods.UnregisterHotKey(winHandle, bindId);
        }

        public void UnRegister(WMKeyBind wMKeyBind)
        {
            NativeMethods.UnregisterHotKey(wMKeyBind.handle, wMKeyBind.bindId);
        }


        public void UnRegisterAll()
        {
            this.wMKeyBinds.AsParallel().ForAll(bind =>
            {
                NativeMethods.UnregisterHotKey(bind.handle, bind.bindId);
            });
        }
        #endregion


        public virtual nint WndProc(nint hwnd, int msg, nint wParam, nint lParam, ref bool handled)
        {
            if (msg != HOTKEY_ID)
                return IntPtr.Zero;
            if (WndProcKeyDown != null)

                foreach (var bind in wMKeyBinds.AsParallel()
                    .Where(bind => bind.bindId == wParam)
                    .Select(bind => bind))
                {
                    WndProcKeyDown?.Invoke(bind);
                }
            return IntPtr.Zero;
        }

        public void Dispose()
        {
            hwndSource?.Dispose();
            this.UnRegisterAll();
        }
    }

}
