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
    public class AdresMicroserviceManager : IAdresManager
    {
        private readonly string _clientApiURl = "http://localhost:57124/";
        public async Task <bool> Utwórz(Adres item)
        {
            var result = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_clientApiURl);
                //HTTP GET
                var responseTask = client.PostAsJsonAsync<Adres>("Adres/", item);
                responseTask.Wait();
                var res = responseTask.Result;
                if (res.IsSuccessStatusCode)
                {
                    result = true;

                }
            }
            return result;
        }

       

        public async Task<Adres> PobierzElement(Guid id)
        {
            Adres result = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_clientApiURl);
                //HTTP GET
                var responseTask = client.GetAsync($"Adres/{id}");
                responseTask.Wait();
                var res = responseTask.Result;
                if (res.IsSuccessStatusCode)
                {
                    var clientsJson = res.Content.ReadAsStringAsync().Result;

                    var clients = JsonConvert.DeserializeObject<Adres>(clientsJson);
                    result = clients;
                }
            }
            return result;
        }

        public async Task <IEnumerable<Adres>> PobierzElementy()
        {
            var result = new List<Adres>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_clientApiURl);
                //HTTP GET
                var responseTask = client.GetAsync("Adres");
                responseTask.Wait();
                var res = responseTask.Result;
                if (res.IsSuccessStatusCode)
                {
                    var clientsJson = res.Content.ReadAsStringAsync().Result;

                    var clients = JsonConvert.DeserializeObject<Adres[]>(clientsJson);
                    result = clients.ToList();
                }
            }
            return result;
        }

        public async Task <bool> JestAdresesmDomyślnym(Guid clientId)
        {
            var Adres = await PobierzElement(clientId);
            return Adres.Glowny;
        }

        public async Task<bool> Edytuj(Guid id,Adres item)
        {
            var result = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_clientApiURl);
                //HTTP GET
                var responseTask = client.PutAsJsonAsync<Adres>($"Adres/{item.Id}", item);
                responseTask.Wait();
                var res = responseTask.Result;
                if (res.IsSuccessStatusCode)
                {
                    result = true;

                }
            }
            return result;
        }

          


        public async Task<bool> Usuń(Adres item)
        {
            var result = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_clientApiURl);
                //HTTP GET
                var responseTask = client.DeleteAsync($"Adres/{item.Id}");
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
