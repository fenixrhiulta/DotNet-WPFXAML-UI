using RhiultaUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfStyleableWindow.StyleableWindow
{
    public class WindowMaximizeCommand : ICommand
    {

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;


        public void Execute(object parameter)
        {
            var window = parameter as Window;

            if (window != null)
            {
                if (window.ResizeMode == ResizeMode.NoResize) return;

                var windowState = WindowHelper.GetWindowState(window);
                if (windowState == WindowState.Maximized)
                {
                    WindowCore.WindowRestore(window);
                }

                if (windowState == WindowState.Normal)
                {
                    WindowCore.WindowMaximize(window);
                }
            }
        }

    }
}