using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using Service.Enums;
using Service.ExtentionMethods;
using Service.Models;
using Service.Models.InstitutoApp;
using Service.Services;
using System.Text.RegularExpressions;

namespace AppMovil.ViewModels
{
    public partial class RegistrarseViewModel : ObservableObject
    {
        AuthService _authService= new();
        UsuarioService _usuarioService= new();
        InstitutoAppService _institutoAppService= new();
        public IRelayCommand RegistrarseCommand { get; }
        public IRelayCommand VolverCommand { get; }
        public IRelayCommand ObtenerDatosAppInstitutoCommand { get; }


        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RegistrarseCommand))]
        private string nombre;
        
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ObtenerDatosAppInstitutoCommand))]
        [NotifyCanExecuteChangedFor(nameof(RegistrarseCommand))]
        private string mail;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RegistrarseCommand))]
        private string dni;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RegistrarseCommand))]
        private string domicilio;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RegistrarseCommand))]
        private string telefono;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RegistrarseCommand))]
        private TipoRolEnum tipoRol=TipoRolEnum.Alumno;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RegistrarseCommand))]
        private string password;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RegistrarseCommand))]
        private string verifyPassword;

        [ObservableProperty]
        private bool isBusy;

        public RegistrarseViewModel()
        {
            RegistrarseCommand = new RelayCommand(Registrarse,CanRegistrarse);
            VolverCommand = new AsyncRelayCommand(OnVolver);
            ObtenerDatosAppInstitutoCommand = new AsyncRelayCommand(ObtenerDatosAppInstituto,CanObtenerDatosAppInstituto);
        }

        private bool CanRegistrarse()
        {
            if (!IsBusy)
            {
                return !string.IsNullOrEmpty(Nombre) &&
                   !string.IsNullOrEmpty(Mail) &&
                   !string.IsNullOrEmpty(Password) &&
                   !string.IsNullOrEmpty(VerifyPassword) &&
                   (Password.Length >= 6) &&
                   !string.IsNullOrEmpty(Dni) &&
                   !string.IsNullOrEmpty(Domicilio) &&
                   !string.IsNullOrEmpty(Telefono);
            } 
            return false;
        }

        private bool CanObtenerDatosAppInstituto()
        {
            // comprobamos con una expresión regular que el mail tenga formato válido
            if (string.IsNullOrEmpty(Mail)) return false;
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            //explico con lujo de detalle la composición del patrón:

            return Regex.IsMatch(Mail, emailPattern);
        }

        private async Task ObtenerDatosAppInstituto()
        {

            IsBusy = true;
            try
            {
                var institutoAppData = await _institutoAppService.GetUsuarioByEmailAsync(Mail);
                if (institutoAppData is not null)
                {
                    Nombre = institutoAppData.Nombre;
                    Dni = institutoAppData?.Alumno?.Dni ?? "";
                    Domicilio = institutoAppData?.Alumno?.Direccion ?? "";
                    Telefono = institutoAppData?.Alumno?.Telefono ?? "";

                    if (institutoAppData?.Docente != null)
                    {
                        TipoRol = TipoRolEnum.Docente;
                    }
                    else
                    {
                        TipoRol = TipoRolEnum.Alumno;
                        //foreach (var carreraInscripto in institutoAppData?.Alumno?.InscripcionesACarreras)
                        //{
                        //    if (carreraInscripto.Carrera != null)
                        //    {
                        //        usuario.CarrerasInscriptas.Add(new UsuarioCarrera()
                        //        {
                        //            CarreraId = carreraInscripto.Carrera.Id,
                        //            Carrera = new Carrera
                        //            {
                        //                Nombre = carreraInscripto.Carrera.Nombre
                        //            }
                        //        });
                        //    }
                        //}
                    }


                }
                else
                {
                    //mostramos con DisplayAlert que no se encontraron datos
                    await Application.Current.MainPage.DisplayAlert("Obtener Datos", "No se encontraron datos asociados a ese correo en la base de datos del instituto.", "Ok");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Obtener Datos", "Ocurrió un problema al obtener los datos: " + ex.Message, "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task OnVolver()
        {
            if (Application.Current?.MainPage is AppShell shell)
            {
                await shell.GoToAsync("//LoginPage");
            }   
        }

        private async void Registrarse()
        {
            if (IsBusy) return;
            IsBusy = true;
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
                    var newUser = new Service.Models.Usuario { Nombre = Nombre, Email = Mail, TipoRol = TipoRol, Dni = Dni, Password = Password.GetHashSha256(), Domicilio= Domicilio, Telefono= Telefono };
                    await _usuarioService.AddAsync(newUser);
                    await Application.Current.MainPage.DisplayAlert("Registrarse", "Cuenta creada!", "Ok");
                    if (Application.Current?.MainPage is AppShell shell)
                    {
                        await shell.GoToAsync("//LoginPage");
                    }
                }
            }
            catch (FirebaseAuthException error) // Use alias here 
            {
                await Application.Current.MainPage.DisplayAlert("Registrarse", "Ocurrió un problema:" + error.Reason, "Ok");

            }
            finally
            {
                IsBusy = false;
            }

        }
    }
}
