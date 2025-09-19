using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Service.Models;
using Service.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace AppMovil.ViewModels
{
    public partial class BuscarLibrosViewModel : ObservableObject
    {
        GenericService<Libro> _libroService = new GenericService<Libro>();
        [ObservableProperty]
        //[NotifyCanExecuteChangedFor(nameof(BuscarCommand))]
        private string searchText = string.Empty;

        [ObservableProperty]
        private bool isBusy;

        private List<string> _todosLosLibros = new List<string>();

        [ObservableProperty]
        private ObservableCollection<Libro> libros= new();
        public IRelayCommand BuscarCommand { get; }

        public BuscarLibrosViewModel()
        {
            // Simulación de datos iniciales
            BuscarCommand = new RelayCommand(OnBuscar);

            // Cargar todos los libros inicialmente de forma asíncrona
            _ = InicializarAsync();
        }

        private async Task InicializarAsync()
        {
            OnBuscar();
        }

        partial void OnSearchTextChanged(string value)
        {
            if(string.IsNullOrEmpty(value)) OnBuscar();
        }

        private async void OnBuscar()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                var libros = await _libroService.GetAllAsync(SearchText);
                Libros= new ObservableCollection<Libro>(libros ?? new List<Libro>());
            }
            finally
            {
                IsBusy = false;
            }
        }




    }
}
