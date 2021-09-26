using MahApps.Metro.IconPacks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace RhiultaUI
{
    public class TextBoxHelper
    {
        #region Icon

        public static readonly DependencyProperty IconAwesomeProperty = DependencyProperty.RegisterAttached("IconAwesome", typeof(PackIconFontAwesomeKind?), typeof(TextBoxHelper), new UIPropertyMetadata(null));

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

        public static readonly DependencyProperty IconPlacementProperty = DependencyProperty.RegisterAttached("IconPlacement", typeof(IconPosition), typeof(TextBoxHelper), new UIPropertyMetadata(null));

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

        public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.RegisterAttached("Placeholder", typeof(string), typeof(TextBoxHelper), new UIPropertyMetadata(null));

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

        public static readonly DependencyProperty VisualStateProperty = DependencyProperty.RegisterAttached("VisualState", typeof(bool), typeof(TextBoxHelper), new FrameworkPropertyMetadata(VisualStateChanged));

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
            if (!(d is TextBox)) return;

            var txtBox = (TextBox)d;

            bool IsNullEnabled = false;
            bool IsNotNullEnabled = false;

            Action<TextBox> ValidationMethod = async (textBox) =>
            {
                //Validation
                await Task.Delay(50);

                bool hasValidationError = Validation.GetHasError(txtBox);

                if (hasValidationError)
                {
                    VisualStateManager.GoToState(txtBox, "ValidationFail", false);
                }
                else
                {
                    VisualStateManager.GoToState(txtBox, "ValidationSuccess", false);
                }

            };
            Action<TextBox> PlaceholderMethod = async (textBox) =>
            {
                //Placeholder
                var a = txtBox.GetValue(TextBoxHelper.PlaceholderProperty);
                if (a != null)
                {
                    if (txtBox.Text.IsNullOrWhiteSpace())
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
                }
            };

            var TextChangedEventHandlerValue = new TextChangedEventHandler(async (s1, e1) =>
            {
                PlaceholderMethod(txtBox);
                ValidationMethod(txtBox);
            });

            var RoutedEventHandler = new RoutedEventHandler((s1, e1) =>
            {
                PlaceholderMethod(txtBox);
                ValidationMethod(txtBox);
            });

            if ((bool)e.NewValue)
            {
                txtBox.TextChanged += TextChangedEventHandlerValue;
                txtBox.Loaded += RoutedEventHandler;
            }
            else
            {
                txtBox.TextChanged -= TextChangedEventHandlerValue;
                txtBox.Loaded -= RoutedEventHandler;
            }
        }

        #endregion

        #region Tips

        public static readonly DependencyProperty TipsProperty = DependencyProperty.Register("Tips", typeof(DependencyObject), typeof(TextBoxHelper));

        public static void SetTips(DependencyObject obj, string value)
        {
            obj.SetValue(TipsProperty, value);
        }

        public static string GetTips(DependencyObject obj)
        {
            return (string)obj.GetValue(TipsProperty);
        }

        #endregion

    }
}
