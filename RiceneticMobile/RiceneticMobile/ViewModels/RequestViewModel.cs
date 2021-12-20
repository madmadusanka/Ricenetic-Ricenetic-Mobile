using Graycorp.Mobile.Services;
using Graycorp.Mobile.Services.Interfaces;
using Graycorp.Mobile.ViewModel.Base;
using RiceneticMobile.Extension;
using RiceneticMobile.Models;
using RiceneticMobile.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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

        private ICommand _repickCommand = null;
        public ICommand RepickCommand => _repickCommand = new Command(async () =>  DoRepickCommand());

        private ICommand _okCommand = null;
        public ICommand OkCommand => _okCommand = new Command(async () =>  DoOkCommand());


        private void DoRepickCommand()
        {
            Application.Current.MainPage = new OptionChooserView();
            Application.Current.MainPage.BindingContext = new OptionChooserViewModel();
        }

        private void DoOkCommand()
        {
            Application.Current.MainPage = new HomeView();
            Application.Current.MainPage.BindingContext = new HomeViewModel();
        }

        


        IHttpRequestProviderService httpRequestProviderService;
        private ImageSource _image;
        public ImageSource Image { get { return _image; } set { SetProperty(ref _image, value); } }

        private string _result;
        public string Result { get { return _result; } set { SetProperty(ref _result, value); } }

        private bool _isSuccess;
        public bool IsSuccess { get { return _isSuccess; } set { SetProperty(ref _isSuccess, value); } }
        private bool _notSuccess;
        public bool NotSuccess { get { return _notSuccess; } set { SetProperty(ref _notSuccess, value); } }
        public override Task OnAppearingAsync(object parameter)
        {
            IsBusy = true;
            try
            {
                sendImageAsync();
            }
            catch(Exception EX)
            {
                NotSuccess = true;
                Application.Current.MainPage.DisplayAlert("Alert", EX.Message, "Ok");
            }
            finally
            {
                IsBusy = false;
            }
            return base.OnAppearingAsync(parameter);
        }
        public async Task sendImageAsync()
        {
            try
            {
                InputModel input = new InputModel
                {
                    base64 = base64
                };
                var result = await httpRequestProviderService.PostAsync(url, input, false);
                if (!String.IsNullOrEmpty(result.Prediction))
                {
                    Result = result.Prediction;
                    IsSuccess = true;
                }
                else
                {
                    NotSuccess = true;
                }
            }
            catch
            {
                NotSuccess = true;
            }


        }
    }
   

}
