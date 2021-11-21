using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RhiultaUI
{
    public static class MessageBoxCustom
    {

        public static void Alerta(Window owner, string alerta = "", string desc = "", bool IsOnlyConfirma = false)
        {
            DialogCustomAlerta win = new DialogCustomAlerta();
            win.info.Text = alerta;
            win.infosub.Text = desc;
            win.IsOnlyConfirma = IsOnlyConfirma;
            win.Owner = owner;
            win.WindowState = WindowState.Normal;
            win.Height = owner.ActualHeight;
            win.Width = owner.ActualWidth;
            win.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            win.ShowDialog();
        }


        //public static void AlertaTimer(Window owner, string alerta = "", string desc = "")
        //{
        //    Dialogs.DialogCustomAlerta win = new Dialogs.DialogCustomAlerta();
        //    win.info.Text = alerta;
        //    win.infosub.Text = desc;
        //    win.Owner = owner;
        //    win.WindowState = WindowState.Normal;
        //    win.Height = owner.ActualHeight;
        //    win.Width = owner.ActualWidth;
        //    win.WindowStartupLocation = WindowStartupLocation.CenterOwner;

        //    win.ShowDialog();
        //}

        public static void Sucesso(Window owner, string msg = "", string desc = "")
        {
            DialogCustomSucesso win = new DialogCustomSucesso();
            win.info.Text = msg;
            win.infosub.Text = desc;
            win.Owner = owner;
            win.WindowState = WindowState.Normal;
            win.Height = owner.ActualHeight;
            win.Width = owner.ActualWidth;
            win.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            win.ShowDialog();
        }


        public static LoadingController ShowLoadingOwner(Window win = null, string msg = "Por favor, aguarde carregar as informações...")
        {
            DialogLoading loading = new DialogLoading();
            loading.TxtMsg.Text = msg;
            loading.Owner = win;
            loading.WindowState = WindowState.Normal;
            loading.Height = win.ActualHeight;
            loading.Width = win.ActualWidth;
            loading.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            return new LoadingController(loading);

            //var fenix = Application.Current.Dispatcher.Invoke(new Func<DialogLoading>(() =>
            //{

            //    //Grid grid = win.FindChildren<Grid>().FirstOrDefault();

            //    //grid.CoverArea("#FFF", 0.8);
            //    //foreach (UIElement c in grid.Children)
            //    //{
            //    //    c.IsEnabled = false;
            //    //}

            //    //DialogLoading loading = new DialogLoading();
            //    //loading.TxtMsg.Text = msg;

            //    //Panel.SetZIndex(loading, 9999);
            //    //Grid.SetRowSpan(loading, 9999);
            //    //grid.Children.Add(loading);

            //    return loading;
            //}));

            //return new LoadingController((DialogLoading)fenix);
        }



        public class LoadingController
        {
            private DialogLoading dialogLoading = null;

            public LoadingController(RhiultaUI.DialogLoading value)
            {
                dialogLoading = value;
            }

            public void ShowAsync()
            {
                if (dialogLoading != null) dialogLoading.ShowMessage(dialogLoading.Owner);
            }

            public void SetMessage(string msg)
            {
                Application.Current.Dispatcher.Invoke(new Action(() => { dialogLoading.TxtMsg.Text = msg; }));
            }

            public void Close()
            {
                dialogLoading.CloseAsync();
                //Application.Current.Dispatcher.Invoke(new Action(() => { Grid parente = (Grid)dialogLoading.Parent;
                //    parente.Children.Remove(dialogLoading);
                //    foreach (UIElement c in parente.Children)
                //    {
                //        c.IsEnabled = true;
                //    }
                //    parente.RemoveCoverArea();  }));
            }
        }


    }

}
