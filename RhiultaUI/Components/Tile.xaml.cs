using MahApps.Metro.IconPacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RhiultaUI
{
    /// <summary>
    /// Interação lógica para Tile.xam
    /// </summary>
    public partial class Tile : UserControl
    {

        public Tile()
        {
            this.DataContext = this;
            InitializeComponent();
        }

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register("Header", typeof(string), typeof(Tile));
        public string Header
        {
            get
            {
                return (string)GetValue(HeaderProperty);
            }
            set
            {
                SetValue(HeaderProperty, "");
            }
        }

        public static readonly DependencyProperty IconFontAwesomeProperty = DependencyProperty.Register("IconFontAwesome", typeof(PackIconFontAwesomeKind), typeof(Tile));
        public PackIconFontAwesomeKind IconFontAwesome
        {
            get
            {
                return (PackIconFontAwesomeKind)GetValue(IconFontAwesomeProperty);
            }
            set
            {
                SetValue(IconFontAwesomeProperty, null);
            }
        }

    }
}
