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
    public class ElementZamowieniaMicroserviceManager : IElementZamowieniaManager
    {
        private readonly string _clientApiURl = "http://localhost:53673/";
        public async Task<bool> Utwórz(ElementZamowienia item)
        {
            var result = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_clientApiURl);
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(item);
                //HTTP GET
                var responseTask = client.PostAsJsonAsync<ElementZamowienia>("ElementZamowienia/", item);
                responseTask.Wait();
                var res = responseTask.Result;
                if (res.IsSuccessStatusCode)
                {
                    result = true;

                }
            }
            return result;
        }

        public async Task<bool> Usuń(ElementZamowienia item)
        {
            var result = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_clientApiURl);
                //HTTP GET
                var responseTask = client.DeleteAsync($"ElementZamowienia/{item.Id}");
                responseTask.Wait();
                var res = responseTask.Result;
                if (res.IsSuccessStatusCode)
                {
                    result = true;

                }
            }
            return result;
        }

        public async Task<ElementZamowienia> PobierzElement(Guid id)
        {
           ElementZamowienia result = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_clientApiURl);
                //HTTP GET
                var responseTask = client.GetAsync($"ElementZamowienia/{id}");
                responseTask.Wait();
                var res = responseTask.Result;
                if (res.IsSuccessStatusCode)
                {
                    var clientsJson = res.Content.ReadAsStringAsync().Result;

                    result = JsonConvert.DeserializeObject<ElementZamowienia>(clientsJson);

                }
            }
            return result;
        }

        public async Task <IEnumerable<ElementZamowienia>> PobierzElementy()
        {
            var result = new List<ElementZamowienia>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_clientApiURl);
                //HTTP GET
                var responseTask = client.GetAsync("ElementZamowienia");
                responseTask.Wait();
                var res = responseTask.Result;
                if (res.IsSuccessStatusCode)
                {
                    var clientsJson = res.Content.ReadAsStringAsync().Result;

                    var clients = JsonConvert.DeserializeObject<ElementZamowienia[]>(clientsJson);
                    result = clients.ToList();
                }
            }
            return result;
        }

        public async Task<bool> Edytuj(Guid id,ElementZamowienia item)
        {
            var result = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_clientApiURl);
                //HTTP GET
                var responseTask = client.PutAsJsonAsync<ElementZamowienia>($"ElementZamowienia/{item.Id}", item);
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
