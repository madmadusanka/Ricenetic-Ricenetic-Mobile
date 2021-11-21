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

        private ICommand _uploadPhotoCommand = null;
        public ICommand UploadPhotoCommand => _uploadPhotoCommand = new Command(() => DoUploadPhotoCommand());


        private void DoUploadPhotoCommand()
        {
            Application.Current.MainPage = new OptionChooserView();
            Application.Current.MainPage.BindingContext = new OptionChooserViewModel();

        }
    }
}
