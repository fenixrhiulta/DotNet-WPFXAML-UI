using MahApps.Metro.IconPacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RhiultaUI
{
    public class ComboBoxHelper
    {

        #region Placeholder

        public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.RegisterAttached("Placeholder", typeof(string), typeof(ComboBoxHelper), new UIPropertyMetadata(null));

        public static void SetPlaceholder(DependencyObject obj, string value)
        {
            obj.SetValue(PlaceholderProperty, value);
        }

        public static string GetPlaceholder(DependencyObject obj)
        {
            return (string)obj.GetValue(PlaceholderProperty);
        }

        #endregion

        #region VisualState

        public static readonly DependencyProperty VisualStateProperty = DependencyProperty.RegisterAttached("VisualState", typeof(bool), typeof(ComboBoxHelper), new FrameworkPropertyMetadata(VisualStateChanged));

        public static void SetVisualState(DependencyObject obj, bool value)
        {
            obj.SetValue(VisualStateProperty, value);
        }

        public static bool GetVisualState(DependencyObject obj)
        {
            return (bool)obj.GetValue(VisualStateProperty);
        }

        private static void VisualStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (ComboBox)d;
            if (control == null) return;

            bool IsNullEnabled = false;
            bool IsNotNullEnabled = false;

            var EventHandlerValue = new SelectionChangedEventHandler((s1, e1) => {
                if (control.SelectedItem == null)
                {
                    VisualStateManager.GoToState(control, "IsNull", false);
                }
                else
                {
                    VisualStateManager.GoToState(control, "IsNotNull", false);

                }
            });

            var RoutedEventHandler = new RoutedEventHandler((s1, e1) => {
                if (control.SelectedItem == null)
                {
                    VisualStateManager.GoToState(control, "IsNull", false);
                }
                else
                {
                    VisualStateManager.GoToState(control, "IsNotNull", false);

                }
            });

            if ((bool)e.NewValue)
            {
                control.SelectionChanged += EventHandlerValue;
                control.Loaded += RoutedEventHandler;

            }
            else
            {
                control.SelectionChanged -= EventHandlerValue;
                control.Loaded -= RoutedEventHandler;

            }
        }

        #endregion

    }
}
