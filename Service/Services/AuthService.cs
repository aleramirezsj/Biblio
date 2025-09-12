using Microsoft.Extensions.Configuration;
using Service.DTOs;
using Service.Interfaces;
using Service.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AuthService : IAuthService
    {
        public AuthService()
        {

        }
        //si no recibo el objeto IConfiguration en el constructor, creo un constructor vacio que instancie uno y lea el archivo appsettings.json


        public async Task<string?> Login(LoginDTO? login)
        {
            if (login==null)
            {
                throw new ArgumentException("El objeto login no llego.");
            }
            try
            {
                var urlApi = Properties.Resources.UrlApi;
                var endpointAuth = ApiEndpoints.GetEndpoint("Login");
                var client = new HttpClient();
                var response = await client.PostAsJsonAsync($"{urlApi}{endpointAuth}/login/",login);
                if(response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al loguearse->: " + ex.Message);
            }




        }
    }
}
