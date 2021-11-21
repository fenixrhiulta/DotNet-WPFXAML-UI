using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RhiultaUI
{
    /// <summary>
    /// Interaction logic for UserControlContent.xaml
    /// </summary>
    public partial class UserControlContent : Window
    {
        public UserControlContent()
        {
            InitializeComponent();
            this.WindowStyle = WindowStyle.None;
            this.ResizeMode = ResizeMode.NoResize;
            this.Loaded += (s, e) =>
            {
                //if (Owner.WindowState == WindowState.Maximized)
                //{
                //    this.Top = 0;
                //    this.Left = 0;
                //}
                //if (Owner.WindowState == WindowState.Normal)
                //{
                //    this.Top = this.Owner.Top;
                //    this.Left = this.Owner.Left;
                //}
            };
        }
    }

    public static class UserControlDialog
    {
        public static void Show(Window owner, UserControl userControl)
        {
            var win = new UserControlContent();
            win.Height = owner.ActualHeight;
            win.Width = owner.ActualWidth;
            win.Owner = owner;
            win.Content = userControl;
            win.Focusable = false;
            win.ShowInTaskbar = false;

            var IsFullScreen = RhiultaUI.WindowHelper.GetFullScreen(App.Current.MainWindow);
            var winState = RhiultaUI.WindowHelper.GetWindowState(App.Current.MainWindow);

            if(IsFullScreen) RhiultaUI.WindowHelper.SetFullScreen(win, IsFullScreen);

            if (winState == WindowState.Maximized) WindowCore.WindowMaximize(win);
            if (winState == WindowState.Normal) WindowCore.WindowRestore(win);

            //win.Loaded += (s, e) =>
            //{
            //    userControl.Focus();
            //};

            win.ShowDialog();
            userControl = null;
            GC.Collect();
        }

        public static bool? ShowAsync(Window owner, UserControl userControl)
        {
            var win = new UserControlContent();
            win.Height = owner.ActualHeight;
            win.Width = owner.ActualWidth;
            win.Owner = owner;
            win.Content = userControl;
            win.Focusable = false;
            win.ShowInTaskbar = false;

            var IsFullScreen = RhiultaUI.WindowHelper.GetFullScreen(App.Current.MainWindow);
            var winState = RhiultaUI.WindowHelper.GetWindowState(App.Current.MainWindow);

            if (IsFullScreen) RhiultaUI.WindowHelper.SetFullScreen(win, IsFullScreen);

            if (winState == WindowState.Maximized) WindowCore.WindowMaximize(win);
            if (winState == WindowState.Normal) WindowCore.WindowRestore(win);


            //win.Loaded += (s, e) =>
            //{
            //    userControl.Focus();
            //};

            win.ShowDialog();
            userControl = null;
            GC.Collect();
            return win.DialogResult;
        }
    }
}
