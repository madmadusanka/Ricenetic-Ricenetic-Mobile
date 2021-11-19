using RiceneticMobile.ViewModels;
using RiceneticMobile.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RiceneticMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new HomeView();
            MainPage.BindingContext = new HomeViewModel();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
