using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Interfejsy;
using Modele;
using System.Threading.Tasks;

namespace eSklep.Managers
{
    public class TypProduktuMicroserviceManager : ITypProduktuManager
    {
        private readonly string _clientApiURl = "https://localhost:44355/";

        public async Task <bool> Utwórz(TypProduktu item)
        {
            var result = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_clientApiURl);
                //HTTP GET
                var responseTask = client.PostAsJsonAsync<TypProduktu>("TypProduktu/", item);
                responseTask.Wait();
                var res = responseTask.Result;
                if (res.IsSuccessStatusCode)
                {
                    result = true;

                }
            }
            return result;
        }

        public async Task<bool> Usuń(TypProduktu item)
        {
            var result = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_clientApiURl);
                //HTTP GET
                var responseTask = client.DeleteAsync($"TypProduktu/{item.Id}");
                responseTask.Wait();
                var res = responseTask.Result;
                if (res.IsSuccessStatusCode)
                {
                    result = true;

                }
            }
            return result;
        }

        public async Task <TypProduktu> PobierzElement(Guid id)
        {
            TypProduktu result = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_clientApiURl);
                //HTTP GET
                var responseTask = client.GetAsync($"TypProduktu/{id}");
                responseTask.Wait();
                var res = responseTask.Result;
                if (res.IsSuccessStatusCode)
                {
                    var clientsJson = res.Content.ReadAsStringAsync().Result;

                    result = JsonConvert.DeserializeObject<TypProduktu>(clientsJson);

                }
            }
            return result;
        }

        public async Task <IEnumerable<TypProduktu>> PobierzElementy()
        {
            var result = new List<TypProduktu>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_clientApiURl);
                //HTTP GET
                var responseTask = client.GetAsync("TypProduktu");
                responseTask.Wait();
                var res = responseTask.Result;
                if (res.IsSuccessStatusCode)
                {
                    var clientsJson = res.Content.ReadAsStringAsync().Result;

                    var clients = JsonConvert.DeserializeObject<TypProduktu[]>(clientsJson);
                    result = clients.ToList();
                }
            }
            return result;
        }

        public async Task<bool> Edytuj(Guid id,TypProduktu item)
        {
            var result = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_clientApiURl);
                //HTTP GET
                var responseTask = client.PutAsJsonAsync<TypProduktu>($"TypProduktu/{item.Id}", item);
                responseTask.Wait();
                var res = responseTask.Result;
                if (res.IsSuccessStatusCode)
                {
                    result = true;

                }
            }
            return result;
        }
    }
}
