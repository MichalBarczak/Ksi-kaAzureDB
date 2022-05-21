using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using Interfejsy;
using Modele;
using MongoDB.Driver;


namespace SerwisTypów.Managers
{
    public class ProducentManager : IProducentManager
    {
        IMongoCollection<Producent> _producenci;
        public ProducentManager()
        {
            var mongoMnager = new MongoManager();
           _producenci = mongoMnager.Baza.GetCollection<Producent>("Producenci");
            if (_producenci == null)
            {
                     mongoMnager.Baza.CreateCollection("Producenci");
                    _producenci = mongoMnager.Baza.GetCollection<Producent>("Producenci");
            }
        }
        public async Task<bool> Edytuj(Guid id, Producent item)
        {
            try
            {
                await _producenci.ReplaceOneAsync(z => z.Id == item.Id, item);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<Producent> PobierzElement(Guid id)
        {
            try {
                var result = await _producenci.FindAsync(z => z.Id == id);
                return result.Single();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Producent>> PobierzElementy()
        {
            try
            {
                return await _producenci.Find(z => true).ToListAsync();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> Usuń(Producent item)
        {
            try
            {
                await _producenci.DeleteOneAsync(z => z.Id == item.Id);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> Utwórz(Producent item)
        {
            try
            {
                await _producenci.InsertOneAsync(item);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
