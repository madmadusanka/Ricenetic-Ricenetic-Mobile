using RiceneticMobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RiceneticMobile.ViewModels
{
    public class HomeViewModel
    {

        private ICommand openSelecctionViewCommand = null;
        public ICommand OpenSelecctionViewCommand => openSelecctionViewCommand = new Command(() => DoOpenSelecctionViewCommand());


        private void DoOpenSelecctionViewCommand()
        {
            Application.Current.MainPage = new OptionChooserView();
            Application.Current.MainPage.BindingContext = new OptionChooserViewModel();

        }
    }
}
