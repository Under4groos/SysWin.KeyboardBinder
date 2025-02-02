using SysWin.KeyboardBinder.Structures;

namespace SysWin.KeyboardBinder.Delegates
{
    public class Interop
    {
        public delegate nint EventWidowProc(nint hwnd, int msg, nint wParam, nint lParam, bool handled);
        public delegate void EventKeyDown(WMKeyBind keyPress);
        public delegate void EventKeyBindingChange(WMKeyBind keyBind);
    }
}
