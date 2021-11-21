using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Graycorp.Mobile.ViewModel.Base
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region PropertyChangedNotification bits

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            RaisePropertyChangedExplicit(propertyName);
        }

        protected virtual void RaisePropertyChangedExplicit(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T property, T newValue, [CallerMemberName] string propertyName = null)
        {
            bool isSuccessful = false;

            if (!Equals(property, newValue))
            {
                property = newValue;
                RaisePropertyChangedExplicit(propertyName);
                isSuccessful = true;
            }

            return isSuccessful;
        }

        #endregion PropertyChangedNotification bits

        #region Constructors & initialization
        //public BaseViewModel(INavigationService pNavigationService, IErrorHandlingService pErrorHandlingService)
        //{
        //    navigationService = pNavigationService;
        //    errorHandlingService = pErrorHandlingService;
        //}
        public BaseViewModel()
        {
            //navigationService = DependencyService.Get<INavigationService>();
            //errorHandlingService = DependencyService.Get<IErrorHandlingService>();
        }
        #endregion

        #region Services
        //protected readonly INavigationService navigationService;
        //protected readonly IErrorHandlingService errorHandlingService;
        #endregion

        #region Properties

        internal object InitializationParameter { get; set; }

        private bool _isConnectionAvailable =
            Connectivity.NetworkAccess == NetworkAccess.Internet
            || Connectivity.NetworkAccess == NetworkAccess.ConstrainedInternet;

        public bool IsConnectionAvailable { get => _isConnectionAvailable; set => SetProperty(ref _isConnectionAvailable, value); }

        private bool _isBusy;
        public bool IsBusy { get => _isBusy; set => SetProperty(ref _isBusy, value); }

        private bool _isLoading = false;
        public bool IsLoading { get => _isLoading; set => SetProperty(ref _isLoading, value); }

        private double _viewOpacity = 1;
        public double ViewOpacity { get => _viewOpacity; set => SetProperty(ref _viewOpacity, value); }

        private bool _isViewEnabled = true;
        public bool IsViewEnabled { get => _isViewEnabled; set => SetProperty(ref _isViewEnabled, value); }

        private string _userFirstName;
        public string UserFirstName { get => _userFirstName; set => SetProperty(ref _userFirstName, value); }

        #endregion

        #region Command

   
        #endregion

        #region Methods

        private void SubscribeToConnectivityChanges()
        {
            IsConnectionAvailable =
                Connectivity.NetworkAccess == NetworkAccess.Internet
                || Connectivity.NetworkAccess == NetworkAccess.ConstrainedInternet;

            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        private void UnsubscribeFromConnectivityChanges()
        {
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        }
        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            IsConnectionAvailable =
                Connectivity.NetworkAccess == NetworkAccess.Internet
                || Connectivity.NetworkAccess == NetworkAccess.ConstrainedInternet;

            if (IsConnectionAvailable)
            {
                //LoadData().SafeFireAndForget(false);
            }
        }

        private void SubscribeToMessages()
        {
            //MessagingCenter.Subscribe<AuthenticationMessage>(
            //    this,
            //    Constants.AuthenticationMessage,
            //    (authMessage) =>
            //    {
            //        if (authMessage.IsAuthenticated)
            //        {
            //            LoadData().SafeFireAndForget(false);
            //        }
            //    });
        }

        private void UnsubscribeFromMessages()
        {
            //MessagingCenter.Unsubscribe<AuthenticationMessage>(
            //    this,
            //    Constants.AuthenticationMessage);
        }


        public async Task SetBusyAsync(Func<Task> func, string loadingMessage = null)
        {
            if (loadingMessage == null)
            {
                loadingMessage = $"Loading...";
            }

            //await navigationService.NavigateToPopupAsync<BusyIndicatorViewModel>(loadingMessage);
            IsBusy = true;

            try
            {
                await func();
            }
            finally
            {
                //await navigationService.PopPopupAsync();
                MessagingCenter.Send<BaseViewModel, string>(this, "NotBusy", "Y");
                IsBusy = false;
            }
        }

        #endregion

        #region Virtual Methods
        public virtual void InitializeWith(object parameter)
        {
        }

        public virtual void NavigatedBack(object parameter)
        {
        }

        public virtual Task OnAppearingAsync(object parameter)
        {
            SubscribeToConnectivityChanges();
            SubscribeToMessages();
            return Task.CompletedTask;
        }

        public virtual Task OnDisappearingAsync()
        {
            UnsubscribeFromConnectivityChanges();
            UnsubscribeFromMessages();
            return Task.CompletedTask;
        }

        public virtual Task LoadData()
        {
            return Task.CompletedTask;
        }
        #endregion
    }
}
