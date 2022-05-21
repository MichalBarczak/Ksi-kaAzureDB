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
    public class ProduktMicroserviceManager : IProduktManager
    {

        private readonly string _clientApiURl = "https://localhost:44355/";
        public async Task<bool> Utwórz(Produkt Produkt)
        {
            var result = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_clientApiURl);
                //HTTP GET
                var responseTask = client.PostAsJsonAsync<Produkt>("Produkt/", Produkt);
                responseTask.Wait();
                var res = responseTask.Result;
                if (res.IsSuccessStatusCode)
                {
                    result = true;

                }
            }
            return result;
        }

        public async Task<bool> Usuń(Produkt Produkt)
        {
            var result = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_clientApiURl);
                //HTTP GET
                var responseTask = client.DeleteAsync($"Produkt/{Produkt.Id}");
                responseTask.Wait();
                var res = responseTask.Result;
                if (res.IsSuccessStatusCode)
                {
                    result = true;

                }
            }
            return result;
        }


        public async Task<Produkt> PobierzElement(Guid id)
        {
            Produkt result = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_clientApiURl);
                //HTTP GET
                var responseTask = client.GetAsync($"Produkt/{id}");
                responseTask.Wait();
                var res = responseTask.Result;
                if (res.IsSuccessStatusCode)
                {
                    var clientsJson = res.Content.ReadAsStringAsync().Result;

                   result = JsonConvert.DeserializeObject<Produkt>(clientsJson);
                   
                }
            }
            return result;
        }

        public async Task <IEnumerable<Produkt>> PobierzElementy()
        {
            var result = new List<Produkt>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_clientApiURl);
                //HTTP GET
                var responseTask = client.GetAsync("Produkt");
                responseTask.Wait();
                var res = responseTask.Result;
                if (res.IsSuccessStatusCode)
                {
                    var clientsJson = res.Content.ReadAsStringAsync().Result;

                    var clients = JsonConvert.DeserializeObject<Produkt[]>(clientsJson);
                    result = clients.ToList();
                }
            }
            return result;
        }

        public async Task<IEnumerable<Produkt>> Sortuj(string SortOrder, string filter)
        {
            List<Produkt> result = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_clientApiURl);
                //HTTP GET
                var responseTask = client.GetAsync($"Produkt/Sortuj/{SortOrder}/Filtr/{filter}");
                responseTask.Wait();
                var res = responseTask.Result;
                if (res.IsSuccessStatusCode)
                {
                    var clientsJson = res.Content.ReadAsStringAsync().Result;

                    result = JsonConvert.DeserializeObject<List<Produkt>>(clientsJson);

                }
            }
            return result;
        }

        public async Task<bool> Edytuj(Guid id,Produkt Produkt)
        {
            var result = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_clientApiURl);
                //HTTP GET
                var responseTask = client.PutAsJsonAsync<Produkt>($"Produkt/{Produkt.Id}", Produkt);
                responseTask.Wait();
                var res = responseTask.Result;
                if (res.IsSuccessStatusCode)
                {
                    result = true;

                }
            }
            return result;
        }


        public async Task<IEnumerable<Produkt>> PobierzPowiazania(Guid id)
        {
            var result = new List<Produkt>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_clientApiURl);
                //HTTP GET
                var responseTask = client.GetAsync($"PobierzPowiązania/{id}");
                responseTask.Wait();
                var res = responseTask.Result;
                if (res.IsSuccessStatusCode)
                {
                    var clientsJson = res.Content.ReadAsStringAsync().Result;

                    var clients = JsonConvert.DeserializeObject<Produkt[]>(clientsJson);
                    result = clients.ToList();
                }
            }
            return result;
        }

        public async Task<bool> DodajPowiazanie(Guid Id, Guid p)
        {
            var result = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_clientApiURl);
                //HTTP GET
                var content = new FormUrlEncodedContent(new[]
               {
                new KeyValuePair<string, string>("Id", Id.ToString()),
                new KeyValuePair<string, string>("type", p.ToString())

            });
                var responseTask = client.PostAsync("DodajPowiazanie", content);
                responseTask.Wait();
                var res = responseTask.Result;
                if (res.IsSuccessStatusCode)
                {
                    result = true;

                }
            }
            return result;
        }

        public async Task<bool> DodajProdukt(Produkt p)
        {
            var result = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_clientApiURl);
                //HTTP GET
                var responseTask = client.PostAsJsonAsync<Produkt>("Produkt/", p);
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
