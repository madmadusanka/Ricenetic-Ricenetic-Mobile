using Graycorp.Mobile.Services;
using Graycorp.Mobile.Services.Interfaces;
using Graycorp.Mobile.ViewModel.Base;
using RiceneticMobile.Extension;
using RiceneticMobile.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RiceneticMobile.ViewModels
{
    public class RequestViewModel : BaseViewModel
    {
        private string base64;
        private string url = "http://localhost:5000/predict";
        //private string url = "https://www.google.com/search?q=url+in+json+object&rlz=1C1GCEU_enLK970LK970&sxsrf=AOaemvLe_-L96NpHk9PPSNVhGl56235Z3Q%3A1639839144913&ei=qPW9YdmRN9eO2roPtq2x8Ac&ved=0ahUKEwjZheLtzO30AhVXh1YBHbZWDH4Q4dUDCA4&uact=5&oq=url+in+json+object&gs_lcp=Cgdnd3Mtd2l6EAMyBQgAEIAEMgYIABAWEB4yBggAEBYQHjIGCAAQFhAeMgYIABAWEB4yBggAEBYQHjoHCAAQRxCwA0oECEEYAEoECEYYAFChFliNIGD3I2gEcAJ4AIABsQKIAYQJkgEHMC41LjAuMZgBAKABAcgBCMABAQ&sclient=gws-wiz";
        public RequestViewModel(ImageSource image, string base64)
        {
            this.base64 = base64;
            Image = image;
            httpRequestProviderService = new HttpRequestProviderService();
          
        }
        IHttpRequestProviderService httpRequestProviderService;
        private ImageSource _image;
        public ImageSource Image { get { return _image; } set { SetProperty(ref _image, value); } }

        public override Task OnAppearingAsync(object parameter)
        {
            sendImageAsync();
            return base.OnAppearingAsync(parameter);
        }
        public async Task sendImageAsync()
        {
            InputModel input = new InputModel
            {
                base64 = base64
            };
             httpRequestProviderService.PostAsync(url, input, false);
        }
    }
   

}
