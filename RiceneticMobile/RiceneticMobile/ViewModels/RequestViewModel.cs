using Graycorp.Mobile.Services;
using Graycorp.Mobile.Services.Interfaces;
using Graycorp.Mobile.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RiceneticMobile.ViewModels
{
    public class RequestViewModel : BaseViewModel
    {
        public RequestViewModel(ImageSource image)
        {
            Image = image;
            httpRequestProviderService = new HttpRequestProviderService();
        }
        IHttpRequestProviderService httpRequestProviderService;
        private ImageSource _image;
        public ImageSource Image { get { return _image; } set { SetProperty(ref _image, value); } }

        public override Task OnAppearingAsync(object parameter)
        {
            httpRequestProviderService.PostAsync()
            return base.OnAppearingAsync(parameter);
        }
    }
}
