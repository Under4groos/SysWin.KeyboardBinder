using System.Diagnostics;
using System.Windows;
using SysWin.KeyboardBinder;

namespace WinHook.Application.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WinHooker winHooker;
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            winHooker = new WinHooker(this);
            winHooker.Wndproc += WinHooker_Wndproc;
        }

        private nint WinHooker_Wndproc(nint hwnd, int msg, nint wParam, nint lParam, bool handled)
        {
            Debug.WriteLine(msg);

            return IntPtr.Zero;
        }
    }
}