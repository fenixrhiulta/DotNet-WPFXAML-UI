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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RhiultaUI
{
    /// <summary>
    /// Interação lógica para DrawerItem.xam
    /// </summary>
    public partial class DrawerItem : UserControl
    {
        public DrawerItem()
        {
            this.DataContext = this;
            InitializeComponent();


        }

        public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(
        "CommandClick",
        typeof(ICommand),
        typeof(DrawerItem));

        public ICommand CommandClick
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }

            set
            {
                SetValue(CommandProperty, value);
            }
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(DependencyObject), typeof(DrawerItem));
        public DependencyObject Icon
        {
            get
            {
                return (DependencyObject)GetValue(IconProperty);
            }
            set
            {
                SetValue(IconProperty, null);
            }
        }

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register("Header", typeof(string), typeof(DrawerItem));
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

        private void content_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.Write("Clicked");
            Console.Write(CommandClick);

            Storyboard StoryboardName = this.Resources["MouseDownIn"] as Storyboard;
            StoryboardName.Begin();

            if (CommandClick == null) return;


            CommandClick.Execute(true);
        }

        private void content_MouseEnter(object sender, MouseEventArgs e)
        {
            Storyboard StoryboardName = this.Resources["MouseEnter"] as Storyboard;
            StoryboardName.Begin();
        }

        private void content_MouseLeave(object sender, MouseEventArgs e)
        {
            Storyboard StoryboardName = this.Resources["MouseLeave"] as Storyboard;
            StoryboardName.Begin();

        }
    }
}
