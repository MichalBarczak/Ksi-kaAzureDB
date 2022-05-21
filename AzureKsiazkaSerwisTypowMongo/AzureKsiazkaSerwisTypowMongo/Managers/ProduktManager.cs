using Interfejsy;
using Modele;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerwisTypów.Managers
{
    public class ProduktManager : IProduktManager
    {
        IMongoCollection<Produkt> _produkty;
        private GremlinManager _gremlinManager;
        //private ITypProduktuManager _typProduktuManager;
        //private IProducentManager _producentManager;
        public ProduktManager(IConfiguration configuration)
        {
            _gremlinManager = new GremlinManager(configuration);
            var mongoMnager = new MongoManager();
             _produkty = mongoMnager.Baza.GetCollection<Produkt>("Produkty");
            if (_produkty == null)
            {
                mongoMnager.Baza.CreateCollection("Produkty");
                _produkty = mongoMnager.Baza.GetCollection<Produkt>("Produkty");
            }
        }

        public async Task<bool> DodajPowiazanie(Guid Id, Guid p)
        {
            return await _gremlinManager.DodajKrawędż(Id,p);
        }

        public async Task<bool> DodajProdukt(Produkt p)
        {
            return await _gremlinManager.DodajWieżchołek(p);
        }

        public async Task<bool> Edytuj(Guid id,Produkt item)
        {
            try
            {
                await _produkty.ReplaceOneAsync(z=>z.Id==item.Id,item);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<Produkt> PobierzElement(Guid id)
        {
            try
            {
                var produkt = await _produkty.Find(z => z.Id==id).FirstOrDefaultAsync();
                //var produkt = (from prod in await _produkty.Find(z => z.Id == id).ToListAsync()
                //               join producenci in await _producentManager.PobierzElementy()
                //               on prod.IdProducenta equals producenci.Id
                //               join typy in await _typProduktuManager.PobierzElementy()
                //               on prod.IdTypu equals typy.Id
                //               select new Produkt
                //               {
                //                   Cena = prod.Cena,
                //                   Id = prod.Id,
                //                   IdProducenta = prod.IdProducenta,
                //                   IdTypu = prod.IdTypu,
                //                   Nazwa = prod.Nazwa,
                //                   NazwaProducenta = producenci.Nazwa,
                //                   NazwaTypu = typy.Nazwa,
                //                   Opis = prod.Opis
                //               }
                //              ).First();
                //var result = await _produkty.FindAsync(z => z.Id == id);
                //var produkt= result.Single();
                //var producent = await _producentManager.PobierzElement(produkt.IdProducenta);
                //produkt.NazwaProducenta = producent.Nazwa;
                //var typProd = await _typProduktuManager.PobierzElement(produkt.IdTypu);
                //produkt.NazwaTypu = typProd.Nazwa;
                return produkt;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Produkt>> PobierzElementy()
        {

            try
            {
                var produkty = await _produkty.Find(z => true).ToListAsync();
                return produkty;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Task<IEnumerable<Produkt>> PobierzPowiazania(Guid id)
        {
            try
            {
                return _gremlinManager.PobierzPowiazania(id);
            }
            catch (Exception e)
            {
                return null;
            }
        
        }

        public async Task<IEnumerable<Produkt>> Sortuj(string kierunkeSortowania, string nazwa)
        {
            var produkty = new List<Produkt>();
            try
            {
               
                var sortowanie = Builders<Produkt>.Sort.Ascending(a => a.Nazwa);
                switch (kierunkeSortowania)
                {
                    case "Nazwa_desc":                        
                        sortowanie = Builders<Produkt>.Sort.Descending(a => a.Nazwa);
                        break;
                    case "Opis":
                        sortowanie = Builders<Produkt>.Sort.Ascending(a => a.Opis);
                        break;
                    case "Opis_desc":
                        sortowanie = Builders<Produkt>.Sort.Descending(a => a.Opis);
                        break;
                    case "Cena":
                        sortowanie = Builders<Produkt>.Sort.Ascending(a => a.Cena);
                        break;
                    case "Cena_desc":
                        sortowanie = Builders<Produkt>.Sort.Descending(a => a.Cena);
                        break;
                    case "Typ":
                        sortowanie = Builders<Produkt>.Sort.Ascending(a => a.NazwaTypu);
                        break;
                    case "Typ_desc":
                        sortowanie = Builders<Produkt>.Sort.Descending(a => a.NazwaTypu);
                        break;
                    case "Producent":
                        sortowanie = Builders<Produkt>.Sort.Ascending(a => a.NazwaProducenta);
                        break;
                    case "Producent_desc":
                        sortowanie = Builders<Produkt>.Sort.Descending(a => a.NazwaProducenta);
                        break;
                    default:
                        sortowanie = Builders<Produkt>.Sort.Ascending(a => a.Nazwa);
                        break;
                }
                if (string.IsNullOrEmpty(nazwa))
                {
                    produkty= await _produkty.Find(bson=>true).Sort(sortowanie).ToListAsync();
                }
                else
                {
                     var filter = Builders<Produkt>.Filter.Eq("Nazwa", nazwa);
                   produkty= await _produkty.Find(filter).Sort(sortowanie).ToListAsync();
                }
            }
            catch (Exception e)
            {
               produkty= _produkty.Find(z => true).ToList();
            }
            return produkty;
        }

        public async Task<bool> Usuń(Produkt item)
        {
            try
            {
                await _produkty.DeleteOneAsync(z=>z.Id==item.Id);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public async Task<bool> Utwórz(Produkt item)
        {
            try
            {
                await _produkty.InsertOneAsync(item);
                
                return true && await DodajProdukt(item);
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
