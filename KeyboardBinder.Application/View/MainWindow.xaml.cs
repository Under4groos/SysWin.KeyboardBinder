using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using SysWin.KeyboardBinder.Enums;
using SysWin.KeyboardBinder.Structures;

namespace KeyboardBinder.Application.View
{
    public partial class MainWindow : Window
    {
        Random random = new Random();
        SysWin.KeyboardBinder.KeyboardBinder? keyboardBinder;
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            keyboardBinder = new SysWin.KeyboardBinder.KeyboardBinder(this);
            keyboardBinder.WndProcKeyDown += EventProcKeyDown;
            keyboardBinder.KeyBindingChange += EventKeyBindingChange;

            keyboardBinder.Register(WMKeys.MOD_ALT, Key.B);
            // or 
            // keyboardBinder.Register(WMKeys.MOD_ALT, Key.B , 999);
            keyboardBinder.Register(WMKeys.MOD_ALT, Key.N);
        }
        protected override void OnClosed(EventArgs e)
        {
            keyboardBinder?.UnRegisterAll();
        }
        void EventKeyBindingChange(WMKeyBind keyPress)
        {
            Debug.WriteLine(keyPress);
        }

        void EventProcKeyDown(WMKeyBind keyPress)
        {
            Debug.WriteLine(keyPress);
            if (keyPress.binKey == Key.B)
                this.Background = new SolidColorBrush(
                    Color.FromRgb(
                        (byte)random.Next(100, 255),
                        (byte)random.Next(100, 255),
                        (byte)random.Next(100, 255)));
        }
    }
}