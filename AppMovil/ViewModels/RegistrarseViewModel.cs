using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using Service.Enums;
using Service.ExtentionMethods;
using Service.Services;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Linq;
using Service.Models;
using Service.DTOs;

namespace AppMovil.ViewModels
{
    public partial class RegistrarseViewModel : ObservableObject
    {
        private readonly AuthService _authService = new();
        private readonly UsuarioService _usuarioService = new();
        private readonly InstitutoAppService _institutoAppService = new();
        private readonly CarreraService _carreraService = new();

        public IRelayCommand RegistrarseCommand { get; }
        public IRelayCommand VolverCommand { get; }
        public IRelayCommand ObtenerDatosAppInstitutoCommand { get; }
        public IRelayCommand<int> ToggleCarreraCommand { get; }

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RegistrarseCommand))]
        private string nombre = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ObtenerDatosAppInstitutoCommand))]
        [NotifyCanExecuteChangedFor(nameof(RegistrarseCommand))]
        private string mail = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RegistrarseCommand))]
        private string dni = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RegistrarseCommand))]
        private string domicilio = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RegistrarseCommand))]
        private string telefono = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RegistrarseCommand))]
        private TipoRolEnum tipoRol = TipoRolEnum.Alumno;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RegistrarseCommand))]
        private string password = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RegistrarseCommand))]
        private string verifyPassword = string.Empty;

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private ObservableCollection<Carrera> carreras = new();

        // Colección de carreras disponibles (Backend principal)
        [ObservableProperty]
        private ObservableCollection<Carrera> carrerasDisponibles = new();

        // Carreras seleccionadas por el usuario (relaciones)
        [ObservableProperty]
        private ObservableCollection<UsuarioCarrera> carrerasSeleccionadas = new();

        public RegistrarseViewModel()
        {
            RegistrarseCommand = new RelayCommand(Registrarse, CanRegistrarse);
            VolverCommand = new AsyncRelayCommand(OnVolver);
            ObtenerDatosAppInstitutoCommand = new AsyncRelayCommand(ObtenerDatosAppInstituto, CanObtenerDatosAppInstituto);
            ToggleCarreraCommand = new RelayCommand<int>(ToggleCarrera);
            _ = CargarCarrerasAsync();
        }

        private bool CanRegistrarse()
        {
            if (!IsBusy)
            {
                return !string.IsNullOrEmpty(Nombre) &&
                       !string.IsNullOrEmpty(Mail) &&
                       !string.IsNullOrEmpty(Password) &&
                       !string.IsNullOrEmpty(VerifyPassword) &&
                       Password.Length >= 6 &&
                       !string.IsNullOrEmpty(Dni) &&
                       !string.IsNullOrEmpty(Domicilio) &&
                       !string.IsNullOrEmpty(Telefono);
            }
            return false;
        }

        private bool CanObtenerDatosAppInstituto()
        {
            if (string.IsNullOrEmpty(Mail)) return false;
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
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
                    Dni = institutoAppData?.Alumno?.Dni ?? string.Empty;
                    Domicilio = institutoAppData?.Alumno?.Direccion ?? string.Empty;
                    Telefono = institutoAppData?.Alumno?.Telefono ?? string.Empty;

                    if (institutoAppData?.Docente != null)
                    {
                        TipoRol = TipoRolEnum.Docente;
                    }
                    else
                    {
                        TipoRol = TipoRolEnum.Alumno;
                        // Mapear inscripciones del instituto a modelo backend Carrera
                        if (institutoAppData?.Alumno?.InscripcionesACarreras is not null)
                        {
                            foreach (var ins in institutoAppData.Alumno.InscripcionesACarreras)
                            {
                                if (ins.Carrera != null && !CarrerasSeleccionadas.Any(c => c.CarreraId == ins.Carrera.Id))
                                {
                                    ToggleCarrera(ins.Carrera.Id);
                                }
                            }
                        }
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Obtener Datos", "No se encontraron datos asociados a ese correo.", "Ok");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Obtener Datos", "Problema: " + ex.Message, "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task CargarCarrerasAsync()
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                var lista = await _carreraService.GetAllAsync();
                Carreras.Clear();
                if (lista != null)
                {
                    foreach (var c in lista.Where(c => !c.IsDeleted))
                    {
                        Carreras.Add(c);
                    }
                }
                CarrerasDisponibles = new ObservableCollection<Carrera>(Carreras.OrderBy(c=>c.Nombre));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Carreras", "Error al cargar carreras: " + ex.Message, "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void ToggleCarrera(int carreraId)
        {
            var carrera = CarrerasDisponibles.FirstOrDefault(c => c.Id == carreraId);
            if (carrera != null)
                carrerasDisponibles.Remove(carrera);
            else
            {
                var carreraAVolverADisponibles = Carreras.FirstOrDefault(c => c.Id == carreraId);
                if (carreraAVolverADisponibles != null)
                {
                    CarrerasDisponibles.Add(carreraAVolverADisponibles);
                    CarrerasDisponibles= new ObservableCollection<Carrera>(CarrerasDisponibles.OrderBy(c => c.Nombre));
                }
            }

            var carreraSeleccionada = Carreras.FirstOrDefault(c => c.Id == carreraId);

            var existente = CarrerasSeleccionadas.FirstOrDefault(x => x.CarreraId == carreraId);
            if (existente != null)
            {
                CarrerasSeleccionadas.Remove(existente);
            }
            else
            {
                CarrerasSeleccionadas.Add(new UsuarioCarrera
                {
                    CarreraId = carreraSeleccionada.Id,
                    Carrera = new Carrera { Id = carreraSeleccionada.Id, Nombre = carreraSeleccionada.Nombre }
                });
                CarrerasSeleccionadas= new ObservableCollection<UsuarioCarrera>(CarrerasSeleccionadas.OrderBy(c => c.Carrera.Nombre));
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
                IsBusy = false;
                return;
            }

            try
            {
                var userOk = await _authService.CreateUserWithEmailAndPasswordAsync(Mail, Password, Nombre);
                if (!userOk)
                {
                    await Application.Current.MainPage.DisplayAlert("Registrarse", "No se pudo crear el usuario", "Ok");
                    return;
                }

                var nuevoUsuario = new Usuario
                {
                    Nombre = Nombre,
                    Email = Mail,
                    TipoRol = TipoRol,
                    Dni = Dni,
                    Password = Password.GetHashSha256(),
                    Domicilio = Domicilio,
                    Telefono = Telefono,
                    CarrerasInscriptas = CarrerasSeleccionadas.Select(cs => new UsuarioCarrera
                    {
                        CarreraId = cs.CarreraId,
                        Carrera = cs.Carrera
                    }).ToList()
                };

                await _usuarioService.AddAsync(nuevoUsuario);
                await Application.Current.MainPage.DisplayAlert("Registrarse", "Cuenta creada!", "Ok");
                if (Application.Current?.MainPage is AppShell shell)
                {
                    await shell.GoToAsync("//LoginPage");
                }
            }
            catch (FirebaseAuthException error)
            {
                await Application.Current.MainPage.DisplayAlert("Registrarse", "Problema: " + error.Reason, "Ok");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Registrarse", "Error: " + ex.Message, "Ok");
                await _authService.DeleteUser(new LoginDTO() { Password=Password, Username=Mail});
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
