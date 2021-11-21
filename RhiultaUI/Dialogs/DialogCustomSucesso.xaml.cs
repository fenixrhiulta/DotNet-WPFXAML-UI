using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for DIALOG_ALERT.xaml
    /// </summary>
    public partial class DialogCustomSucesso : Window
    {
        public bool IsOnlyConfirma = false;

        public DialogCustomSucesso()
        {
            InitializeComponent();
            this.DataContext = this;

            BorderBackground.PreviewMouseDown += (s, e) =>
            {
                e.Handled = true;
            };

            this.Loaded += (s, e) =>
            {
                if (Owner.WindowState == WindowState.Maximized)
                {
                    this.Top = 0;
                    this.Left = 0;
                }
                if (Owner.WindowState == WindowState.Normal)
                {
                    this.Top = this.Owner.Top;
                    this.Left = this.Owner.Left;
                }
            };

            btnAffirmative.Focus();

            this.KeyDown += (s, e) =>
            {
                if (e.Key == Key.F2)
                {
                    this.Close();
                }

                if (IsOnlyConfirma == false)
                {
                    if (e.Key == Key.Escape)
                    {
                        this.Close();
                    }

                    if (e.Key == Key.Enter)
                    {
                        this.Close();
                    }
                }

            };

        }

        public ICommand Close => new RelayCommand(async o =>
        {
            this.Close();
        }, o => true);
    }
}
