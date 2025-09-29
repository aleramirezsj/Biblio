using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using Service.Enums;
using Service.Models;
using Service.Services;

namespace AppMovil.ViewModels
{
    public partial class RegistrarseViewModel : ObservableObject
    {
        AuthService _authService= new();
        UsuarioService _usuarioService= new();
        public IRelayCommand RegistrarseCommand { get; }

        [ObservableProperty]
        private string nombre;

        [ObservableProperty]
        private string mail;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string verifyPassword;

        public RegistrarseViewModel()
        {
            RegistrarseCommand = new RelayCommand(Registrarse);
        }

        private async void Registrarse()
        {
            if (Password != VerifyPassword)
            {
                await Application.Current.MainPage.DisplayAlert("Registrarse", "Las contraseñas ingresadas no coinciden", "Ok");
                return;
            }

            try
            {
                var user = await _authService.CreateUserWithEmailAndPasswordAsync(Mail, Password, Nombre);
                if (user == false)
                {
                    await Application.Current.MainPage.DisplayAlert("Registrarse", "No se pudo crear el usuario", "Ok");
                    return;
                }
                else
                {
                    var newUser = new Usuario { Nombre = Nombre, Email = Mail, TipoRol = TipoRolEnum.Alumno, Dni = "12345678", Password = Password };
                    await _usuarioService.AddAsync(newUser);
                    await Application.Current.MainPage.DisplayAlert("Registrarse", "Cuenta creada!", "Ok");
                    await Shell.Current.GoToAsync("//LoginPage");
                }
            }
            catch (FirebaseAuthException error) // Use alias here 
            {
                await Application.Current.MainPage.DisplayAlert("Registrarse", "Ocurrió un problema:" + error.Reason, "Ok");

            }
            
        }
    }
}
