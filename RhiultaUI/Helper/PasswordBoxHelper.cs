using MahApps.Metro.IconPacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RhiultaUI
{
    public class PasswordBoxHelper
    {
        #region Icon

        public static readonly DependencyProperty IconAwesomeProperty = DependencyProperty.RegisterAttached("IconAwesome", typeof(PackIconFontAwesomeKind?), typeof(PasswordBoxHelper), new UIPropertyMetadata(null));

        public static void SetIconAwesome(DependencyObject obj, PackIconFontAwesomeKind? value)
        {
            obj.SetValue(IconAwesomeProperty, value);
        }

        public static PackIconFontAwesomeKind? GetIconAwesome(DependencyObject obj)
        {
            return (PackIconFontAwesomeKind?)obj.GetValue(IconAwesomeProperty);
        }

        #endregion

        #region IconPlacement

        public static readonly DependencyProperty IconPlacementProperty = DependencyProperty.RegisterAttached("IconPlacement", typeof(IconPosition), typeof(PasswordBoxHelper), new UIPropertyMetadata(null));

        public static void SetIconPlacement(DependencyObject obj, IconPosition? value)
        {
            obj.SetValue(IconAwesomeProperty, value);
        }

        public static IconPosition GetIconPlacement(DependencyObject obj)
        {
            return (IconPosition)obj.GetValue(IconAwesomeProperty);
        }

        #endregion

        #region Placeholder

        public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.RegisterAttached("Placeholder", typeof(string), typeof(PasswordBoxHelper), new UIPropertyMetadata(null));

        public static void SetPlaceholder(DependencyObject obj, string value)
        {
            obj.SetValue(PlaceholderProperty, value);
        }

        public static string GetPlaceholder(DependencyObject obj)
        {
            return (string)obj.GetValue(PlaceholderProperty);
        }

        #endregion


        #region VisualStateProperty

        public static readonly DependencyProperty VisualStateProperty = DependencyProperty.RegisterAttached("VisualState", typeof(bool), typeof(PasswordBoxHelper), new FrameworkPropertyMetadata(VisualStateChanged));

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
            if (!(d is PasswordBox)) return;

            var txtBox = (PasswordBox)d;

            bool IsNullEnabled = false;
            bool IsNotNullEnabled = false;

            var TextChangedEventHandlerValue = new RoutedEventHandler((s1, e1) =>
            {
                var a = txtBox.GetValue(PasswordBoxHelper.PlaceholderProperty);
                if (a == null) return;
                if (txtBox.Password.IsNullOrWhiteSpace())
                {
                    if (IsNullEnabled) return;
                    IsNullEnabled = VisualStateManager.GoToState(txtBox, "IsNull", false);
                    IsNotNullEnabled = false;
                }
                else
                {
                    if (IsNotNullEnabled) return;
                    IsNotNullEnabled = VisualStateManager.GoToState(txtBox, "IsNotNull", false);
                    IsNullEnabled = false;
                }
            });

            var RoutedEventHandler = new RoutedEventHandler((s1, e1) =>
            {
                var a = txtBox.GetValue(PasswordBoxHelper.PlaceholderProperty);
                if (a == null) return;
                if (txtBox.Password.IsNullOrWhiteSpace())
                {
                    if (IsNullEnabled) return;
                    VisualStateManager.GoToState(txtBox, "IsNull", false);
                    IsNotNullEnabled = false;

                }
                else
                {
                    if (IsNotNullEnabled) return;
                    VisualStateManager.GoToState(txtBox, "IsNotNull", false);
                    IsNullEnabled = false;

                }
            });

            if ((bool)e.NewValue)
            {
                txtBox.PasswordChanged += TextChangedEventHandlerValue;
                txtBox.Loaded += RoutedEventHandler;

            }
            else
            {
                txtBox.PasswordChanged -= TextChangedEventHandlerValue;
                txtBox.Loaded -= RoutedEventHandler;

            }
        }

        #endregion

    }
}
