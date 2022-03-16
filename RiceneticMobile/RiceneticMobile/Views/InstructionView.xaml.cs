using RiceneticMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RiceneticMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InstructionView : ContentPage
    {
        public InstructionView()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new HomeView();
            Application.Current.MainPage.BindingContext = new HomeViewModel();
        }
    }
}