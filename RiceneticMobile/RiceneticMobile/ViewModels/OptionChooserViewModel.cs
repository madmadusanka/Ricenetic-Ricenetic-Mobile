using Graycorp.Mobile.Services;
using Graycorp.Mobile.Services.Interfaces;
using Graycorp.Mobile.ViewModel.Base;
using RiceneticMobile.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RiceneticMobile.ViewModels
{
    public class OptionChooserViewModel : BaseViewModel
    {
        private string base64;
        private ICommand _openCameraCommand = null;
        public ICommand OpenCameraCommand => _openCameraCommand = new Command(async () => await DoOpenCameraCommandAsync());

        private ICommand _pickPhotoCommand = null;
        public ICommand PickPhotoCommand => _pickPhotoCommand = new Command(async () => await DoPickPhotoCommand());



        private ImageSource _image;
        public ImageSource Image { get { return _image; } set { SetProperty(ref _image, value);} }

        IHttpRequestProviderService httpRequestProviderService;
        public OptionChooserViewModel()
        {
            httpRequestProviderService = new HttpRequestProviderService();
        }

        private async Task DoOpenCameraCommandAsync()
        {
            await TakePhotoAsync();

        }



        async Task TakePhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                await LoadPhotoAsync(photo);
                Console.WriteLine($"CapturePhotoAsync COMPLETED");
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature is not supported on the device
            }
            catch (PermissionException pEx)
            {
                // Permissions not granted
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }


        async Task DoPickPhotoCommand()
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                await LoadPhotoAsync(photo);
                Console.WriteLine($"CapturePhotoAsync COMPLETED");
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature is not supported on the device
            }
            catch (PermissionException pEx)
            {
                // Permissions not granted
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }

     
        async Task LoadPhotoAsync(FileResult photo)
        {
            
            // canceled
            if (photo == null)
            {
                //PhotoPath = null;
                return;
            }

            // save the file into local storage
            var stream = await photo.OpenReadAsync();
            var stream2 = await photo.OpenReadAsync();
            Image = ImageSource.FromStream(() => stream2);
            using (MemoryStream memory = new MemoryStream())
            {

                stream.CopyTo(memory);
                byte[] bytes = memory.ToArray();
                base64 = System.Convert.ToBase64String(bytes);
            }




            Application.Current.MainPage = new ImageUploadView();
            Application.Current.MainPage.BindingContext = new ImageUploadViewModel(Image, base64);
            //PhotoPath = newFile;
        }

        //async Task<FileResult> PickAndShow(PickOptions options)
        //{
        //    try
        //    {
        //        var result = await FilePicker.PickAsync(options);
        //        if (result != null)
        //        {
        //            if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
        //                result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
        //            {
        //                var stream = await result.OpenReadAsync();
        //                Image = ImageSource.FromStream(() => stream);
        //            }
        //        }

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        // The user canceled or something went wrong
        //    }

        //    return null;
        //}

    }
}
