using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Service.DTOs;
using Service.Services;
using System.Text;
using System.Text.Json;

namespace BiblioTestProject
{
    public class UnitTestGemini
    {
        [Fact]
        public async Task TestObtenerResumenLibroIA()
        {
            //leemos la clave de la api desde appsettings.json
            await LoginTest();

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var apiKey = configuration["ApiKeyGemini"];
            var url = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key= " + apiKey;

            var prompt = $"Me puedes dar un resumen de 100 palabras como máximo de libro Mi Planta de Naranja lima";

            var payload = new
            {
                contents = new[]
                {
                new
                {
                    parts = new[]
                    {
                        new { text = prompt }
                    }
                }
            }
            };

            var json = JsonSerializer.Serialize(payload);
            using var client = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);
            var result = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(result);
            var texto = doc.RootElement
               .GetProperty("candidates")[0]
               .GetProperty("content")
               .GetProperty("parts")[0]
               .GetProperty("text")
               .GetString();

            Console.WriteLine($"Respuesta de IA: {texto}");
            Assert.True(response.IsSuccessStatusCode);




        }

        private async Task LoginTest()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var serviceAuth = new AuthService(configuration);
            var token = await serviceAuth.Login(new LoginDTO { Username = "aleramirezsj@gmail.com", Password = "123456" });
            Console.WriteLine($">>>>>>>>>>>>>>>>>>>>>>>>>>Token: {token}");
            GeminiService.jwtToken = token;
        }

        [Fact]
        public async Task TestServiceGeminiGetPrompt()
        {
            await LoginTest();
            //leemos la api key desde appsettings.json
            var configuration = new ConfigurationBuilder()
                  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                  .AddEnvironmentVariables()
                  .Build();
            var prompt = $"Me puedes dar un resumen de 100 palabras como máximo del libro Sin Red: Nadal, Federer y la historia detrás del duelo que cambió el tenis";
            var servicio = new GeminiService(configuration);
            var resultado = await servicio.GetPrompt(prompt);
            Console.WriteLine($"Respuesta de IA desde servicio: {resultado}");
            Assert.NotNull(resultado);


        }
    }
}