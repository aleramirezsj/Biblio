using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Service.DTOs;
using Service.Services;

namespace AppMovil.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        AuthService _authService = new AuthService();
        UsuarioService _usuarioService = new UsuarioService();

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        private string username = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        private string password = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        private bool _isBusy;

        [ObservableProperty]
        private string _errorMessage = string.Empty;

        public IRelayCommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(OnLogin, CanLogin);
        }

        private bool CanLogin()
        {
            return !IsBusy && 
                   !string.IsNullOrWhiteSpace(Username) && 
                   !string.IsNullOrWhiteSpace(Password);
        }

        private async void OnLogin()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                ErrorMessage = string.Empty;

                var response= await _authService.Login(new LoginDTO
                {
                    Username = this.Username,
                    Password = this.Password
                });

                if(string.IsNullOrEmpty(response))
                {
                    ErrorMessage = "Usuario o contrase�a incorrectos.";
                    return;
                }
                var usuario = await _usuarioService.GetByEmailAsync(username);
                if(usuario==null)
                {
                    ErrorMessage = "No se pudo obtener la informaci�n del usuario.";
                    return;
                }
                // PERMITE CUALQUIER USUARIO/CONTRASE�A durante desarrollo
                // Solo requiere que no est�n vac�os
                if (Application.Current?.MainPage is AppShell shell)
                {
                    shell.SetLoginState(true);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al iniciar sesi�n: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
