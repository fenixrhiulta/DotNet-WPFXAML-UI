using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RhiultaUI
{
    public class ControlsHelper
    {
        #region AdvancesByEnterKey
        public static readonly DependencyProperty AdvancesByEnterKeyProperty = DependencyProperty.RegisterAttached("AdvancesByEnterKey", typeof(bool), typeof(ControlsHelper), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits, OnAdvancesByEnterKeyPropertyChanged));

        public static bool GetAdvancesByEnterKey(DependencyObject obj)
        {
            return (bool)obj.GetValue(AdvancesByEnterKeyProperty);
        }

        public static void SetAdvancesByEnterKey(DependencyObject obj, bool value)
        {
            obj.SetValue(AdvancesByEnterKeyProperty, value);
        }

        static void OnAdvancesByEnterKeyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            var element = d as UIElement;
            if (element == null) return;

            if ((bool)e.NewValue) { element.KeyDown += Keydown; } else element.KeyDown -= Keydown;
        }

        static void Keydown(object sender, KeyEventArgs e)
        {
            if (!e.Key.Equals(Key.Enter)) return;

            var element = sender as UIElement;
            if (element != null) element.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }

        #endregion

        public static readonly DependencyProperty IsMonitoringProperty = DependencyProperty.RegisterAttached("IsMonitoring", typeof(bool), typeof(ControlsHelper), new UIPropertyMetadata(false, OnIsMonitoringChanged));
        public static readonly DependencyProperty SelectAllOnFocusProperty = DependencyProperty.RegisterAttached("SelectAllOnFocus", typeof(bool), typeof(ControlsHelper), new FrameworkPropertyMetadata(false));

        #region GET AND SETTERS
        public static void SetIsMonitoring(DependencyObject obj, bool value)
        {
            obj.SetValue(IsMonitoringProperty, value);
        }

        public static void SetSelectAllOnFocus(DependencyObject obj, bool value)
        {
            obj.SetValue(SelectAllOnFocusProperty, value);
        }

        public static bool GetSelectAllOnFocus(DependencyObject obj)
        {
            return (bool)obj.GetValue(SelectAllOnFocusProperty);
        }
        #endregion


        private static void TextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            ControlGotFocus(sender as TextBox, textBox => textBox.SelectAll());
        }

        private static void PasswordBoxGotFocus(object sender, RoutedEventArgs e)
        {
            ControlGotFocus(sender as PasswordBox, passwordBox => passwordBox.SelectAll());
        }


        private static void TxtBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Apps) { e.Handled = true; }
        }


        private static void OnIsMonitoringChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox)
            {
                var txtBox = (TextBox)d;
                if ((bool)e.NewValue)
                {
                    txtBox.GotFocus += TextBoxGotFocus;
                    txtBox.PreviewKeyDown += TxtBox_PreviewKeyDown;
                }
                else
                {
                    txtBox.GotFocus -= TextBoxGotFocus;
                    txtBox.PreviewKeyDown -= TxtBox_PreviewKeyDown;

                }
            }

            if (d is PasswordBox)
            {
                var txtBox = (PasswordBox)d;
                if ((bool)e.NewValue)
                {
                    txtBox.GotFocus += PasswordBoxGotFocus;
                }
                else
                {
                    txtBox.GotFocus -= PasswordBoxGotFocus;
                }
            }
        }

        private static void ControlGotFocus<TDependencyObject>(TDependencyObject sender, Action<TDependencyObject> action) where TDependencyObject : DependencyObject
        {
            if (sender != null)
            {
                if (GetSelectAllOnFocus(sender))
                {
                    sender.Dispatcher.BeginInvoke(action, sender);
                }
            }
        }



    }

}
