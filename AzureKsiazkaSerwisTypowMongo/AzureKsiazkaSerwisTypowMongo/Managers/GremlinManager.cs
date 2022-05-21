using Modele;
using Gremlin.Net.Driver;
using Gremlin.Net.Structure.IO.GraphSON;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SerwisTypów.Managers
{
    public class GremlinManager
    {
        IConfiguration konfiguracja;
        public GremlinServer gremlinSerwer;
        public GremlinClient gremlinKlient;
        private  string Host => konfiguracja["Gremlin:Host"];
        private  string PK => konfiguracja["Gremlin:PK"];
        private  string Baza => konfiguracja["Gremlin:Baza"];
        private string Kontener => konfiguracja["Gremlin:Kontener"];
      

        public GremlinManager(IConfiguration configuration)
        {
            var mimeType = "application/vnd.gremlin-v2.0+json";
            konfiguracja = configuration;
            string linkDoKontenera = "/dbs/" + Baza + "/colls/" + Kontener;
            gremlinSerwer = new GremlinServer(Host,8182, true, linkDoKontenera, PK);
            gremlinKlient = new GremlinClient(gremlinSerwer, graphSONReader: new GraphSON2Reader(),graphSONWriter: new GraphSON2Writer(), mimeType:mimeType);
        }
        public async Task<bool> DodajWieżchołek(Produkt p)
        {
            var result = true;
            try
            {
                StringBuilder zapytanieBudowniczy = new StringBuilder();
                zapytanieBudowniczy.Append("g.addV('produkt')");
                Type typ = p.GetType();
                var props = typ.GetProperties();
                zapytanieBudowniczy.Append($".property('id','{p.Id}')");
                foreach (var property in props)
                {
                    if (!property.Name.Equals("ID") && property.GetValue(p) !=null)
                    {
                        zapytanieBudowniczy.Append($".property('{property.Name}', '{property.GetValue(p).ToString()}')");
                    }
                }
                zapytanieBudowniczy.Append($".property('pk', '{p.Id}')");
                await gremlinKlient.SubmitAsync<dynamic>(zapytanieBudowniczy.ToString());
            }
            catch (Exception e)
            {
                result = false;
            }
            return result;

        }
        public async Task<IEnumerable<Produkt>> PobierzPowiazania(Guid id)
        {
            List<Produkt> produkty = new List<Produkt>();
            try
            {

                string zapytanie = $"g.V('{id}').out('kupowanyRazem').hasLabel('produkt')";
                string zapytanieIn = $"g.V('{id}').in('kupowanyRazem').hasLabel('produkt')";
                var wyniki = await gremlinKlient.SubmitAsync<dynamic>(zapytanie);
                var wynikiIn= await gremlinKlient.SubmitAsync<dynamic>(zapytanieIn);
                foreach (var wynik in wyniki)
                {
                    var json = JsonConvert.SerializeObject(wynik);
                    WynikGremlin wynikGremlin = JsonConvert.DeserializeObject<WynikGremlin>(json);
                    produkty.Add(wynikGremlin.MapujNaProdukt());
                }
                foreach (var wynik in wynikiIn)
                {
                    var json = JsonConvert.SerializeObject(wynik);
                    WynikGremlin wynikGremlin = JsonConvert.DeserializeObject<WynikGremlin>(json);
                    produkty.Add(wynikGremlin.MapujNaProdukt());
                }
            }
            catch (Exception e)
            {

            }
            return produkty;
        }

        public async Task<bool> DodajKrawędż(Guid Id, Guid desId)
        {
            var result = true;
            try
            {
                string zapytanie = $"g.V('{Id}').addE('kupowanyRazem').to(g.V('{desId}'))";
                await gremlinKlient.SubmitAsync<dynamic>(zapytanie);
            }
            catch (Exception e)
            {
                result = false;
            }
            return result;
        }
    }
}
