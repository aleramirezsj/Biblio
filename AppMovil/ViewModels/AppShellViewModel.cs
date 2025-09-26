using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Service.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace AppMovil.ViewModels
{
    public partial class AppShellViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isLoggedIn;

        [ObservableProperty]
        private bool loginVisible = true;

        [ObservableProperty]
        private bool menuVisible = false;

        [ObservableProperty]
        private bool resetPasswordVisible = false;

        public Usuario? Usuario { get; private set; }

        partial void OnIsLoggedInChanged(bool value)
        {
            LoginVisible = !value;
            MenuVisible = value;
        }

        public IRelayCommand LogoutCommand { get; }

        public AppShellViewModel()
        {
            LogoutCommand = new RelayCommand(OnLogout);
        }

        public void SetLoginState(bool isLoggedIn)
        {
            if (isLoggedIn)
                Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
            else
                Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;

            IsLoggedIn = isLoggedIn;
            if (isLoggedIn)
                Shell.Current.GoToAsync("//MainPage");  // Cambio a MainPage (pantalla de inicio)
            else if(!ResetPasswordVisible)
                Shell.Current.GoToAsync("//LoginPage");
            else
                Shell.Current.GoToAsync("//ResetPassword");
        }

        public void SetUserLogin(Usuario usuario)
        {
            Usuario = usuario;
        }

        private void OnLogout()
        {
            SetLoginState(false);
        }

    }
}