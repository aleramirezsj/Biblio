using Microsoft.Extensions.Caching.Memory;
using Service.DTOs;
using Service.Interfaces;
using Service.Models;
using Service.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
