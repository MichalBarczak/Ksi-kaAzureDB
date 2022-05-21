using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfejsy;
using Modele;
using MongoDB.Driver;

namespace SerwisTypów.Managers
{
    public class TypProduktuManager : ITypProduktuManager
    {
        IMongoCollection<TypProduktu> _typyProduktow;
        public TypProduktuManager()
        {
            var mongoMnager = new MongoManager();
            _typyProduktow = mongoMnager.Baza.GetCollection<TypProduktu>("TypyProduktow");
            if (_typyProduktow == null)
            {
                mongoMnager.Baza.CreateCollection("TypyProduktow");
                _typyProduktow = mongoMnager.Baza.GetCollection<TypProduktu>("TypyProduktow");
            }
        }
        public async Task<bool> Edytuj(Guid id, TypProduktu item)
        {
            try
            {
                await _typyProduktow.ReplaceOneAsync(z => z.Id == id, item);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<TypProduktu> PobierzElement(Guid id)
        {
            try
            {
                var result = await _typyProduktow.FindAsync(z => z.Id == id);
                return result.Single();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<IEnumerable<TypProduktu>> PobierzElementy()
        {
            try
            {
                return await _typyProduktow.Find(z => true).ToListAsync();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> Usuń(TypProduktu item)
        {
            try
            {
                await _typyProduktow.DeleteOneAsync(z => z.Id == item.Id);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public async Task<bool> Utwórz(TypProduktu item)
        {
            try
            {
                await _typyProduktow.InsertOneAsync(item);
                return true;
            }
            catch (Exception e)
            {                
                return false;
            }
        }
    }
}
