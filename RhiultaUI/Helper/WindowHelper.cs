using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;

namespace RhiultaUI
{
    public class WindowHelper
    {

        #region HideIsteandOfCloseWindow

        public static readonly DependencyProperty HideWindowCloseProperty = DependencyProperty.RegisterAttached("HideWindowClose", typeof(bool), typeof(WindowHelper), new FrameworkPropertyMetadata(false, HideWindowClosePropertyChanged));

        public static bool GetHideWindowClose(DependencyObject obj)
        {
            return (bool)obj.GetValue(HideWindowCloseProperty);
        }

        public static void SetHideWindowClose(DependencyObject obj, bool value)
        {
            obj.SetValue(HideWindowCloseProperty, value);
        }

        static bool AltDown = false;

        static private void HideWindowClosePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Window element = d as Window;

            if ((bool)e.NewValue) { element.PreviewKeyDown += Window_PreviewKeyDown; element.Closing += Window_Closing; } else element.PreviewKeyDown -= Window_PreviewKeyDown;
        }

        private static void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ((Window)sender).Hide();
            e.Cancel = true;
        }

        static private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.SystemKey == Key.LeftAlt || e.SystemKey == Key.RightAlt)
            {
                AltDown = true;
            }
            else if (e.SystemKey == Key.F4 && AltDown)
            {
                ((Window)sender).Hide();
                e.Handled = true;
            }
        }

        #endregion

        #region MonitoringWindow

        public static readonly DependencyProperty MonitoringWindowProperty = DependencyProperty.RegisterAttached("MonitoringWindow", typeof(bool), typeof(WindowHelper), new FrameworkPropertyMetadata(false, MonitoringWindowPropertyChanged));

        public static bool GetMonitoringWindow(DependencyObject obj)
        {
            return (bool)obj.GetValue(MonitoringWindowProperty);
        }

        public static void SetMonitoringWindow(DependencyObject obj, bool value)
        {
            obj.SetValue(MonitoringWindowProperty, value);
        }


        static private void MonitoringWindowPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Window element = d as Window;
            if ((bool)e.NewValue) {
                element.Initialized += (s, e) =>
                {
                    var windowState = GetWindowState(element);
                    if (windowState == WindowState.Maximized) WindowCore.WindowMaximize(element);
                };
                element.KeyDown += (s, e) =>
                {
                    if (e.Key == Key.Enter && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                    {
                        var windowState = GetWindowState(element);
                        if (windowState == WindowState.Maximized) WindowCore.WindowRestore(element);
                        if (windowState == WindowState.Normal) WindowCore.WindowMaximize(element);
                        e.Handled = true;
                    }
                };
                element.Loaded += Element_Loaded;
            } else { element.Loaded -= Element_Loaded; }
        }

        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private static void Element_Loaded(object sender, RoutedEventArgs e)
        {
            var window = (Window)sender;
            var hwnd = new WindowInteropHelper(window).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        #endregion

        #region CustomWindowStyle
        public static readonly DependencyProperty WindowStyleProperty = DependencyProperty.RegisterAttached("WindowStyle", typeof(WindowStyle), typeof(WindowHelper), new FrameworkPropertyMetadata(null));

        public static WindowStyle GetWindowStyle(DependencyObject obj)
        {
            return (WindowStyle)obj.GetValue(WindowStyleProperty);
        }

        public static void SetWindowStyle(DependencyObject obj, WindowStyle value)
        {
            obj.SetValue(WindowStyleProperty, value);
        }

        #endregion

        #region CustomWindowState
        public static readonly DependencyProperty WindowStateProperty = DependencyProperty.RegisterAttached("WindowState", typeof(WindowState), typeof(WindowHelper), new FrameworkPropertyMetadata(null));

        public static WindowState GetWindowState(DependencyObject obj)
        {
            return (WindowState)obj.GetValue(WindowStateProperty);
        }

        public static void SetWindowState(DependencyObject obj, WindowState value)
        {
            obj.SetValue(WindowStateProperty, value);

        }

        #endregion

        #region TitleBackground

        public static readonly DependencyProperty TitleBackgroundProperty = DependencyProperty.RegisterAttached("TitleBackground", typeof(Brush), typeof(WindowHelper), new FrameworkPropertyMetadata(null));

        public static Brush GetTitleBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(TitleBackgroundProperty);
        }

        public static void SetTitleBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(TitleBackgroundProperty, value);
        }

        #endregion

        #region TitleForeground

        public static readonly DependencyProperty TitleForegroundProperty = DependencyProperty.RegisterAttached("TitleForeground", typeof(Brush), typeof(WindowHelper), new FrameworkPropertyMetadata(null));

        public static Brush GetTitleForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(TitleForegroundProperty);
        }

        public static void SetTitleForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(TitleForegroundProperty, value);
        }

        #endregion

        #region DropShadow

        public static readonly DependencyProperty DropShadowProperty = DependencyProperty.RegisterAttached("DropShadow", typeof(bool), typeof(WindowHelper), new FrameworkPropertyMetadata(null));

        public static bool GetDropShadow(DependencyObject obj)
        {
            return (bool)obj.GetValue(DropShadowProperty);
        }

        public static void SetDropShadow(DependencyObject obj, bool value)
        {
            obj.SetValue(DropShadowProperty, value);
        }

        #endregion

        #region FullScreen

        public static readonly DependencyProperty FullScreenProperty = DependencyProperty.RegisterAttached("FullScreen", typeof(bool), typeof(WindowHelper), new FrameworkPropertyMetadata(false));

        public static bool GetFullScreen(DependencyObject obj)
        {
            return (bool)obj.GetValue(FullScreenProperty);
        }

        public static void SetFullScreen(DependencyObject obj, bool value)
        {
            obj.SetValue(FullScreenProperty, value);
        }

        #endregion

        #region MinimizeButton

        public static readonly DependencyProperty MinimizeButtonProperty = DependencyProperty.RegisterAttached("MinimizeButton", typeof(bool), typeof(WindowHelper), new FrameworkPropertyMetadata(null));

        public static bool GetMinimizeButton(DependencyObject obj)
        {
            return (bool)obj.GetValue(MinimizeButtonProperty);
        }

        public static void SetMinimizeButton(DependencyObject obj, bool value)
        {
            obj.SetValue(MinimizeButtonProperty, value);
        }

        #endregion

        #region RestoreButton

        public static readonly DependencyProperty RestoreButtonProperty = DependencyProperty.RegisterAttached("RestoreButton", typeof(bool), typeof(WindowHelper), new FrameworkPropertyMetadata(null));

        public static bool GetRestoreButton(DependencyObject obj)
        {
            return (bool)obj.GetValue(RestoreButtonProperty);
        }

        public static void SetRestoreButton(DependencyObject obj, bool value)
        {
            obj.SetValue(RestoreButtonProperty, value);
        }

        #endregion

        #region CloseButton

        public static readonly DependencyProperty CloseButtonProperty = DependencyProperty.RegisterAttached("CloseButton", typeof(bool), typeof(WindowHelper), new FrameworkPropertyMetadata(null));

        public static bool GetCloseButton(DependencyObject obj)
        {
            return (bool)obj.GetValue(CloseButtonProperty);
        }

        public static void SetCloseButton(DependencyObject obj, bool value)
        {
            obj.SetValue(CloseButtonProperty, value);
        }

        #endregion

        #region CloseButton

        public static readonly DependencyProperty FixCloseProblemProperty = DependencyProperty.RegisterAttached("FixCloseProblem", typeof(bool), typeof(WindowHelper), new FrameworkPropertyMetadata(FixCloseProblemPropertyChanged));

        public static bool GetFixCloseProblem(DependencyObject obj)
        {
            return (bool)obj.GetValue(FixCloseProblemProperty);
        }

        public static void SetFixCloseProblem(DependencyObject obj, bool value)
        {
            obj.SetValue(FixCloseProblemProperty, value);
        }


        private static void FixCloseProblemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var win = d as Window;
            if ((bool)e.NewValue)
            {
                win.Closed += Win_Closed;
            }
            else
            {
                win.Closed -= Win_Closed;
            }
        }

        private static void Win_Closed(object sender, EventArgs e)
        {
            var win = sender as Window;
            if (win.Owner != null) win.Owner.Activate();
        }

        #endregion

        #region props

        public static readonly DependencyProperty ContentHeaderProperty = DependencyProperty.Register("ContentHeader", typeof(DependencyObject), typeof(WindowHelper));

        public static DependencyObject GetContentHeader(DependencyObject obj)
        {
            return (DependencyObject)obj.GetValue(ContentHeaderProperty);
        }

        public static void SetContentHeader(DependencyObject obj, DependencyObject value)
        {
            obj.SetValue(ContentHeaderProperty, value);
        }
        #endregion



        //public static readonly DependencyProperty WindowStyleProperty = DependencyProperty.RegisterAttached("WindowStyle", typeof(WindowStyle), typeof(WindowHelper), new FrameworkPropertyMetadata(null));

        //public static WindowStyle GetWindowStyle(DependencyObject obj)
        //{
        //    return (WindowStyle)obj.GetValue(WindowStyleProperty);
        //}

        //public static void SetWindowStyle(DependencyObject obj, WindowStyle value)
        //{
        //    obj.SetValue(WindowStyleProperty, value);
        //}

        #region PreventCloseWindow
        public static readonly DependencyProperty PreventCloseWindowProperty = DependencyProperty.RegisterAttached("PreventCloseWindow", typeof(bool), typeof(WindowHelper), new FrameworkPropertyMetadata(OnPreventCloseWindowPropertyChanged));


        public static bool GetPreventCloseWindow(DependencyObject obj)
        {
            return (bool)obj.GetValue(PreventCloseWindowProperty);
        }

        public static void SetPreventCloseWindow(DependencyObject obj, bool value)
        {
            obj.SetValue(PreventCloseWindowProperty, value);
        }

        public static void OnPreventCloseWindowPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Window;
            if (element == null) return;

            if ((bool)e.NewValue)
            {
                element.KeyDown += KeyDown;
                element.PreviewKeyDown += PreviewKeyDown;
            }
            else
            {
                element.KeyDown -= KeyDown;
                element.PreviewKeyDown -= PreviewKeyDown;
            }
        }

        public static void PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt))
            {
                e.Handled = true;
                if (e.SystemKey == Key.F4)
                {
                    e.Handled = true;
                }
            }

            //if (e.Key == Key.F2 && Keyboard.Modifiers.HasFlag(ModifierKeys.Alt))
            //{
            //    MessageBox.Show("TRUE");
            //    e.Handled = true;
            //}
        }

        public static void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.SystemKey == Key.F10)
            {
                e.Handled = true;
            }
        }

        #endregion

        #region StartCenterScreen

        public static readonly DependencyProperty StartCenterScreenProperty = DependencyProperty.RegisterAttached("StartCenterScreen", typeof(bool), typeof(WindowHelper), new FrameworkPropertyMetadata(OnStartCenterScreenPropertyChanged));

        public static bool GetStartCenterScreen(DependencyObject obj)
        {
            return (bool)obj.GetValue(PreventCloseWindowProperty);
        }

        public static void SetStartCenterScreen(DependencyObject obj, bool value)
        {
            obj.SetValue(PreventCloseWindowProperty, value);
        }

        static void OnStartCenterScreenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Window;
            if (element == null) return;

            if ((bool)e.NewValue)
            {
                element.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }
            else
            {
                element.WindowStartupLocation = WindowStartupLocation.Manual;
            }
        }

        #endregion




        #region Corrige Maximized Window com Style none


        public static readonly DependencyProperty WindowMaximizedSolutionProperty = DependencyProperty.RegisterAttached("WindowMaximizedSolution", typeof(bool), typeof(WindowHelper), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits, WindowMaximizedSolutionPropertyChanged));

        public static bool GetWindowMaximizedSolution(DependencyObject obj)
        {
            return (bool)obj.GetValue(WindowMaximizedSolutionProperty);
        }

        public static void SetWindowMaximizedSolution(DependencyObject obj, bool value)
        {
            obj.SetValue(WindowMaximizedSolutionProperty, value);
        }


        static void WindowMaximizedSolutionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            var window = d as Window;
            if (window == null) return;

            if ((bool)e.NewValue)
            {
                window.SourceInitialized += Window_SourceInitialized;
            }
            else window.SourceInitialized -= Window_SourceInitialized;
        }

        public static void Window_SourceInitialized(object sender, EventArgs e)
        {
            IntPtr mWindowHandle = (new WindowInteropHelper((Window)sender)).Handle;
            HwndSource.FromHwnd(mWindowHandle).AddHook(new HwndSourceHook(WindowProc));
        }


        private static System.IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case 0x0024:
                    WmGetMinMaxInfo(hwnd, lParam);
                    break;
            }

            return IntPtr.Zero;
        }


        private static void WmGetMinMaxInfo(System.IntPtr hwnd, System.IntPtr lParam)
        {
            POINT lMousePosition;
            GetCursorPos(out lMousePosition);

            IntPtr lPrimaryScreen = MonitorFromPoint(new POINT(0, 0), MonitorOptions.MONITOR_DEFAULTTOPRIMARY);
            MONITORINFO lPrimaryScreenInfo = new MONITORINFO();
            if (GetMonitorInfo(lPrimaryScreen, lPrimaryScreenInfo) == false)
            {
                return;
            }

            IntPtr lCurrentScreen = MonitorFromPoint(lMousePosition, MonitorOptions.MONITOR_DEFAULTTONEAREST);

            MINMAXINFO lMmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));

            if (lPrimaryScreen.Equals(lCurrentScreen) == true)
            {
                lMmi.ptMaxPosition.X = lPrimaryScreenInfo.rcWork.Left;
                lMmi.ptMaxPosition.Y = lPrimaryScreenInfo.rcWork.Top;
                lMmi.ptMaxSize.X = lPrimaryScreenInfo.rcWork.Right - lPrimaryScreenInfo.rcWork.Left;
                lMmi.ptMaxSize.Y = lPrimaryScreenInfo.rcWork.Bottom - lPrimaryScreenInfo.rcWork.Top;
            }
            else
            {
                lMmi.ptMaxPosition.X = lPrimaryScreenInfo.rcMonitor.Left;
                lMmi.ptMaxPosition.Y = lPrimaryScreenInfo.rcMonitor.Top;
                lMmi.ptMaxSize.X = lPrimaryScreenInfo.rcMonitor.Right - lPrimaryScreenInfo.rcMonitor.Left;
                lMmi.ptMaxSize.Y = lPrimaryScreenInfo.rcMonitor.Bottom - lPrimaryScreenInfo.rcMonitor.Top;
            }

            Marshal.StructureToPtr(lMmi, lParam, true);
        }


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCursorPos(out POINT lpPoint);


        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr MonitorFromPoint(POINT pt, MonitorOptions dwFlags);

        enum MonitorOptions : uint
        {
            MONITOR_DEFAULTTONULL = 0x00000000,
            MONITOR_DEFAULTTOPRIMARY = 0x00000001,
            MONITOR_DEFAULTTONEAREST = 0x00000002
        }


        [DllImport("user32.dll")]
        static extern bool GetMonitorInfo(IntPtr hMonitor, MONITORINFO lpmi);


        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        };


        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class MONITORINFO
        {
            public int cbSize = Marshal.SizeOf(typeof(MONITORINFO));
            public RECT rcMonitor = new RECT();
            public RECT rcWork = new RECT();
            public int dwFlags = 0;
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left, Top, Right, Bottom;

            public RECT(int left, int top, int right, int bottom)
            {
                this.Left = left;
                this.Top = top;
                this.Right = right;
                this.Bottom = bottom;
            }
        }

        #endregion
    }
}
