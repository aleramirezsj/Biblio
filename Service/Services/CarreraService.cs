using Microsoft.Extensions.Caching.Memory;
using Service.Models;
using System.Text.Json;

namespace Service.Services
{
    public class CarreraService : GenericService<Carrera>
    {
        public CarreraService(HttpClient? httpClient = null, IMemoryCache? memoryCache = null) : base(httpClient, memoryCache)
        {

        }

        public override async Task<List<Carrera>?> GetAllAsync(string? filtro = "")
        {
            var response = await _httpClient.GetAsync($"{_endpoint}?filtro={filtro}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Carrera>>(content, _options);
            }
            else
            {
                throw new Exception("Error al obtener los datos");
            }
        }

    }
}
