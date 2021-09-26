using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace RhiultaUI
{
    /// <summary>
    /// Converte valores vazios em Visibility.Hidden
    /// </summary>
    public class NullEmptyZeroToHidden : System.Windows.Markup.MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return Visibility.Hidden;
            if (value is string) if ((value as string).IsNullOrWhiteSpace()) return Visibility.Hidden;
            if (value is int) if ((value as int?) == 0 || (value as int?) == null) return Visibility.Hidden;

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

    /// <summary>
    /// Converte valores vazios em Visibility.Visible
    /// </summary>
    public class NullEmptyZeroToVisible : System.Windows.Markup.MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return Visibility.Visible;
            if (value is string) if ((value as string).IsNullOrWhiteSpace()) return Visibility.Visible;
            if (value is int) if ((value as int?) == 0 || (value as int?) == null) return Visibility.Visible;

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

    /// <summary>
    /// Converte valores vazios em Visibility.Collapsed
    /// </summary>
    public class NullEmptyZeroToCollapsed : System.Windows.Markup.MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return Visibility.Collapsed;
            if (value is string) if ((value as string).IsNullOrWhiteSpace()) return Visibility.Collapsed;
            if (value is int) if ((value as int?) == 0 || (value as int?) == null) return Visibility.Collapsed;

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }


    /// <summary>
    /// Check Value Bind
    /// </summary>
    public class ValueBindingVerify : System.Windows.Markup.MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Console.WriteLine(value);
            if (value == null) return true;

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }


    /// <summary>
    /// Converte valores false em Visibility.Collapsed;
    /// </summary>
    public class FalseToCollapsed : System.Windows.Markup.MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return Visibility.Collapsed;
            if ((bool)value == false) return Visibility.Collapsed;

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }


    /// <summary>
    /// Converte valores vazios em Visibility.Hidden
    /// </summary>
    public class NullToImagePlacementPath : System.Windows.Markup.MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return "pack://application:,,,/dropimage.png";

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

}
