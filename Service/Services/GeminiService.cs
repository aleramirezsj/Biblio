using Microsoft.Extensions.Configuration;
using Service.Interfaces;
using Service.Models;
using Service.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service.Services
{
    public class GeminiService : IGeminiService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient = new HttpClient();
        protected readonly JsonSerializerOptions _options;
        public static string? jwtToken = string.Empty;

        public GeminiService(IConfiguration configuration)
        {
            _configuration = configuration;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            if (!string.IsNullOrEmpty(GenericService<object>.jwtToken))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GenericService<object>.jwtToken);
            else
                throw new ArgumentException("Error Token no definido", nameof(GenericService<object>.jwtToken));

        }
        public async Task<string?> GetPrompt(string textPrompt)
        {
            if (string.IsNullOrEmpty(textPrompt))
            {
                throw new ArgumentException("El texto del prompt no puede estar vacío.", nameof(textPrompt));
            }
            try
            {
                var urlApi = _configuration["UrlApi"];
                var endpointGemini = ApiEndpoints.GetEndpoint("Gemini");
                
                var response = await _httpClient.GetAsync($"{urlApi}{endpointGemini}/prompt/{textPrompt}");
                if(response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                else
                {
                    throw new Exception($"Error en la respuesta de la API: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el prompt de Gemini: " + ex.Message);
            }




        }

        public async Task<Libro?> GetLibroFromPortada(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                throw new ArgumentException("La url de la imagen no puede estar vacía.", nameof(imageUrl));
            }
            try
            {
                var urlApi = _configuration["UrlApi"];
                var endpointGemini = ApiEndpoints.GetEndpoint("Gemini");
                // limpio la url
                imageUrl = Uri.EscapeDataString(imageUrl);
                var response = await _httpClient.GetAsync($"{urlApi}{endpointGemini}/ocr-portada?imageUrl={imageUrl}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var libro = JsonSerializer.Deserialize<Libro>(result,_options);
                    if (libro != null)
                    {
                        return libro;
                    }
                    else
                    {
                        return null;
                    }

                }
                var errorContent = await response.Content.ReadAsStringAsync();
                var error2=$"Error en la respuesta de la API: {response.StatusCode} - {response.ReasonPhrase}";
                return null;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error en la respuesta de la API: {ex.Message}");
            }
        }
    }
}
