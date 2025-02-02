using System.Windows.Input;
using SysWin.KeyboardBinder.Enums;

namespace SysWin.KeyboardBinder.Structures
{
    public struct WMKeyBind
    {
        public nint handle;
        public WMKeys wmBindKey;
        public Key binKey;
        public int bindId;

        public override string ToString()
        {
            return $"Win:{handle}[{bindId}], bind: {wmBindKey.ToString()} + {binKey.ToString()}";
        }
    }
}
