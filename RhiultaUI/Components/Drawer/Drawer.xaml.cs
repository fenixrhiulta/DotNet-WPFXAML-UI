using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for Drawer.xaml
    /// </summary>
    public partial class Drawer : UserControl
    {
        public Drawer()
        {
            this.DataContext = this;
            InitializeComponent();
        }

        #region props

        public static readonly DependencyProperty ContentHeaderProperty = DependencyProperty.Register("ContentHeader", typeof(DependencyObject), typeof(Drawer));
        public DependencyObject ContentHeader
        {
            get
            {
                return (DependencyObject)GetValue(ContentHeaderProperty);
            }
            set
            {
                SetValue(ContentHeaderProperty, null);
            }
        }


        public static readonly DependencyProperty ContentListProperty = DependencyProperty.Register("ContentList", typeof(DependencyObject), typeof(Drawer));
        public DependencyObject ContentList
        {
            get
            {
                return (DependencyObject)GetValue(ContentListProperty);
            }
            set
            {
                SetValue(ContentListProperty, null);
            }
        }


        public static readonly DependencyProperty DrawerOpenProperty = DependencyProperty.Register("DrawerOpen", typeof(bool), typeof(Drawer));

        public bool DrawerOpen
        {
            get
            {
                return (bool)GetValue(DrawerOpenProperty);
            }
            set
            {
                SetValue(DrawerOpenProperty, value);
            }
        }

        #endregion

        public ICommand Open => new RelayCommand(async o =>
        {
            this.SetCurrentValue(DrawerOpenProperty, true);
        });
        public ICommand Close => new RelayCommand(async o =>
        {
            this.SetCurrentValue(DrawerOpenProperty, false);
        });
    }
}
