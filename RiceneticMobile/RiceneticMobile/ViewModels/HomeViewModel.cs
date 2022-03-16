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

        private ICommand _openInstructionCommand = null;
        public ICommand OpenInstructionCommand => _openInstructionCommand = new Command(async () => DoOpenInstructionCommand());

        private ICommand _openAboutCommand = null;
        public ICommand OpenAboutCommand => _openAboutCommand = new Command(async () => DoOpenAboutCommand());


        private void DoOpenAboutCommand()
        {
            Application.Current.MainPage = new AboutView();
        }
        private void DoOpenInstructionCommand()
        {
            Application.Current.MainPage = new InstructionView();
        }
        private void DoOpenSelecctionViewCommand()
        {
            Application.Current.MainPage = new OptionChooserView();
            Application.Current.MainPage.BindingContext = new OptionChooserViewModel();

        }
    }
}
