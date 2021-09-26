using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace RhiultaUI
{
    public partial class WindowMessage : Window
    {
        private AsyncAutoResetEvent _ReadyToStop = new AsyncAutoResetEvent();

        private void Owner_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (this.OwnedWindows.Count > 0)
            {
                this.OwnedWindows.Cast<Window>().FirstOrDefault().Activate();
            }
            else
            {
                this.Activate();
            }
        }

        KeyEventHandler DisableKeyDown = (a, e) => { e.Handled = true; };

        public void CloseAsync()
        {
            _ReadyToStop.Set();
        }

        public async void ShowMessage(Window owner)
        {
            this.Width = owner.Width;
            this.Height = owner.Height;
            this.Owner = owner;
            this.ShowInTaskbar = false;
            owner.PreviewKeyDown += DisableKeyDown;
            owner.PreviewMouseDown += Owner_PreviewMouseDown; ;
            this.KeyDown += (a, e) =>
            {
                if (e.Key == Key.System && e.SystemKey == Key.F4)
                {
                    e.Handled = true;
                }
            };

            this.Show();

            await _ReadyToStop.WaitAsync();
            owner.PreviewKeyDown -= DisableKeyDown;
            owner.PreviewMouseDown -= Owner_PreviewMouseDown; ;

            this.Close();
        }
    }

}
