using System.Net;
using Transferencias.Web.Api.Models;

namespace Transferencias.Web.Api.Interfaces
{
    public class ClientesService
    {
        static readonly HttpClient _client = new HttpClient();

        public ClientesService() { }


        public async Task<bool> Exists(string cuit)
        {
            string clientesApiUrl = GetFromConfig<string>("Clientes:Url");

            HttpResponseMessage result = await _client.GetAsync($"{clientesApiUrl}/cuil/{cuit}");
            
            if (result.IsSuccessStatusCode) 
                return true;
            else 
                return false;
        }


        public async Task<ClienteServiceResponse> GetByCuit(string cuit)
        {
            return null;
        }


        private static T GetFromConfig<T>(string clave, T defaultValue = default)
        {
            using IHost host = Host.CreateDefaultBuilder().Build();
            IConfiguration config = host.Services.GetRequiredService<IConfiguration>();
            return config.GetValue<T>(clave, defaultValue);
        }
    }
}
