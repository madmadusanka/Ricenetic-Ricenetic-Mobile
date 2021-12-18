using Graycorp.Mobile.Services;
using Graycorp.Mobile.Services.Interfaces;
using Graycorp.Mobile.ViewModel.Base;
using RiceneticMobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RiceneticMobile.ViewModels
{
    public class ImageUploadViewModel :BaseViewModel
    {
       
        public ImageUploadViewModel(ImageSource image)
        {
            Image = image;
            
        }

        private ImageSource _image;
        public ImageSource Image { get { return _image; } set { SetProperty(ref _image, value); } }

        private ICommand _uploadPhotoCommand = null;
        public ICommand UploadPhotoCommand => _uploadPhotoCommand = new Command(async () => await DoUploadPhotoCommand());
        async Task DoUploadPhotoCommand()
        {
            Application.Current.MainPage = new RequestView();
            Application.Current.MainPage.BindingContext = new RequestViewModel(Image);
            //httpRequestProviderService.PostAsync();
        }
    }
}
