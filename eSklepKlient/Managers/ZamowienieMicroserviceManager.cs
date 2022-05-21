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
    public class ZamowienieMicroserviceManager : IZamowienieManager
    {
        private readonly string _clientApiURl = "http://localhost:53673/";
        public async Task<bool> Utwórz(Zamowienie item)
        {
            var result = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_clientApiURl);
                //HTTP GET
                var json=Newtonsoft.Json.JsonConvert.SerializeObject(item);
                var responseTask = client.PostAsJsonAsync<Zamowienie>("Zamowienie", item);
                responseTask.Wait();
                var res = responseTask.Result;
                if (res.IsSuccessStatusCode)
                {
                    result = true;

                }
            }
            return result;
        }

        public async Task<bool> Usuń(Zamowienie item)
        {
            var result = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_clientApiURl);
                //HTTP GET
                var responseTask = client.DeleteAsync($"Zamowienie/{item.Id}");
                responseTask.Wait();
                var res = responseTask.Result;
                if (res.IsSuccessStatusCode)
                {
                    result = true;

                }
            }
            return result;
        }

        public async Task<Zamowienie> PobierzElement(Guid id)
        {
            Zamowienie result = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_clientApiURl);
                //HTTP GET
                var responseTask = client.GetAsync($"Zamowienie/{id}");
                responseTask.Wait();
                var res = responseTask.Result;
                if (res.IsSuccessStatusCode)
                {
                    var clientsJson = res.Content.ReadAsStringAsync().Result;

                    result = JsonConvert.DeserializeObject<Zamowienie>(clientsJson);

                }
            }
            return result;
        }

        public async Task <IEnumerable<Zamowienie>> PobierzElementy()
        {
            var result = new List<Zamowienie>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_clientApiURl);
                //HTTP GET
                var responseTask = client.GetAsync("Zamowienie");
                responseTask.Wait();
                var res = responseTask.Result;
                if (res.IsSuccessStatusCode)
                {
                    var clientsJson = res.Content.ReadAsStringAsync().Result;

                    var clients = JsonConvert.DeserializeObject<Zamowienie[]>(clientsJson);
                    result = clients.ToList();
                }
            }
            return result;
        }

        public async Task<bool> Edytuj(Guid id,Zamowienie item)
        {
            var result = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_clientApiURl);
                //HTTP GET
                var responseTask = client.PutAsJsonAsync<Zamowienie>($"Zamowienie/{item.Id}", item);
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
