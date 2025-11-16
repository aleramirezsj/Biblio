using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Service.DTOs;
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
        LibroService _libroService = new ();

        [ObservableProperty]
        private string searchText = string.Empty;

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private ObservableCollection<Libro> libros = new();

        // Propiedades para los filtros
        [ObservableProperty]
        private bool filtrarPorTitulo = true;

        [ObservableProperty]
        private bool filtrarPorAutor = false;

        [ObservableProperty]
        private bool filtrarPorEditorial = false;

        [ObservableProperty]
        private bool filtrarPorGenero = false;

        [ObservableProperty]
        private bool filtrarPorSinopsis = false;

        [ObservableProperty]
        private bool mostrarFiltros = false;

        private List<Libro> _todosLosLibros = new();

        public IRelayCommand BuscarCommand { get; }
        public IRelayCommand LimpiarCommand { get; }
        public IRelayCommand ToggleFiltrosCommand { get; }

        public BuscarLibrosViewModel()
        {
            BuscarCommand = new RelayCommand(OnBuscar);
            LimpiarCommand = new RelayCommand(OnLimpiar);
            ToggleFiltrosCommand = new RelayCommand(OnToggleFiltros);
            _ = InicializarAsync();
        }

        private async Task InicializarAsync()
        {
            OnBuscar();
        }

        partial void OnSearchTextChanged(string value)
        {
            //if (string.IsNullOrEmpty(value)) OnBuscar();
        }

        // Los cambios en filtros también disparan nueva búsqueda
        partial void OnFiltrarPorTituloChanged(bool value) => ActivarDesactivarFiltrosSegunTitulo();

        private void ActivarDesactivarFiltrosSegunTitulo()
        {
            if (FiltrarPorTitulo)
            {
                FiltrarPorSinopsis = false;
            }
        }
        private void ActivarDesactivarFiltrosSegunGenero()
        {
            if (FiltrarPorGenero)
            {
                FiltrarPorSinopsis = false;
            }
        }
        private void ActivarDesactivarFiltrosSegunAutor()
        {
            if (FiltrarPorAutor)
            {
                FiltrarPorSinopsis = false;
            }
        }
        private void ActivarDesactivarFiltrosSegunEditorial()
        {
            if (FiltrarPorEditorial)
            {
                FiltrarPorSinopsis = false;
            }
        }

        partial void OnFiltrarPorAutorChanged(bool value) => ActivarDesactivarFiltrosSegunAutor();
        partial void OnFiltrarPorEditorialChanged(bool value) => ActivarDesactivarFiltrosSegunEditorial();
        partial void OnFiltrarPorGeneroChanged(bool value) => ActivarDesactivarFiltrosSegunGenero();
        partial void OnFiltrarPorSinopsisChanged(bool value) => ActivarDesactivarFiltrarSegunSinopsis();

        private void ActivarDesactivarFiltrarSegunSinopsis()
        {
            if (FiltrarPorSinopsis)
            {
                FiltrarPorTitulo = false;
                FiltrarPorAutor = false;
                FiltrarPorEditorial = false;
                FiltrarPorGenero = false;
            }
            
        }

        private async void OnBuscar()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;

                FilterLibroDTO filtro = new()
                {
                    SearchText = this.SearchText,
                    ForTitulo = this.FiltrarPorTitulo,
                    ForAutor = this.FiltrarPorAutor,
                    ForEditorial = this.FiltrarPorEditorial,
                    ForGenero = this.FiltrarPorGenero,
                    ForSinopsis = this.FiltrarPorSinopsis

                };
                // Obtener todos los libros si no los tenemos
                 var librosFiltrados= await _libroService.GetWithFilterAsync(filtro);

                Libros = librosFiltrados!=null ? 
                        new ObservableCollection<Libro>(librosFiltrados)
                        : new ObservableCollection<Libro>();
            }
            finally
            {
                IsBusy = false;
            }
        }

        

        private void OnLimpiar()
        {
            SearchText = string.Empty;
            // Mantener los filtros pero ejecutar búsqueda limpia
            OnBuscar();
        }

        private void OnToggleFiltros()
        {
            MostrarFiltros = !MostrarFiltros;
        }
    }
}
