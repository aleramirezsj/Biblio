using Service.Interfaces;
using Service.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service.Services
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;
        protected readonly JsonSerializerOptions _options;


        public GenericService(HttpClient? httpClient=null)
        {
            _httpClient = httpClient??new HttpClient();
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _endpoint = Properties.Resources.UrlApiLocal+ApiEndpoints.GetEndpoint(typeof(T).Name);

        }

        public Task<T?> AddAsync(T? entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>?> GetAllAsync(string? filtro="")
        {
            var response= await _httpClient.GetAsync($"{_endpoint}?filtro={filtro}");
            if (response.IsSuccessStatusCode)
            {
                var content= await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<T>>(content,_options);
            }
            else
            {
                throw new Exception("Error al obtener los datos");
            }
        }

        public Task<List<T>?> GetAllDeletedsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RestoreAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(T? entity)
        {
            throw new NotImplementedException();
        }
    }
}
