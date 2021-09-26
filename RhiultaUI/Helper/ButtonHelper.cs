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
    public class ButtonHelper
    {
        #region Icon

        public static readonly DependencyProperty IconAwesomeProperty = DependencyProperty.RegisterAttached("IconAwesome", typeof(PackIconFontAwesomeKind?), typeof(ButtonHelper), new UIPropertyMetadata(null));

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

        public static readonly DependencyProperty IconPlacementProperty = DependencyProperty.RegisterAttached("IconPlacement", typeof(IconPosition), typeof(ButtonHelper), new UIPropertyMetadata(null));

        public static void SetIconPlacement(DependencyObject obj, IconPosition? value)
        {
            obj.SetValue(IconAwesomeProperty, value);
        }

        public static IconPosition GetIconPlacement(DependencyObject obj)
        {
            return (IconPosition)obj.GetValue(IconAwesomeProperty);
        }

        #endregion

    }

    public enum IconPosition
    {
        Left = 0,
        Right = 1,
    }
}
