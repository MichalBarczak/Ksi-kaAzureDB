using Interfejsy;
using Modele;
using Cassandra;
using Cassandra.Mapping;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SerwisZamówień.Managers
{
    public class ElementZamowieniaManager : BaseCassandraManager, IElementZamowieniaManager
    {       
        public ElementZamowieniaManager(IConfiguration configuartion) :base(configuartion)
        {
                      
        }
        public async Task<bool> Edytuj(Guid id, ElementZamowienia item)
        {
            try
            {
                await mapper.UpdateAsync(item);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<ElementZamowienia> PobierzElement(Guid id)
        {
            ElementZamowienia result = null;
            try
            {
                result = await mapper.FirstOrDefaultAsync<ElementZamowienia>("Select * from ksiazka.ElementyZamowienia where Id = ? ", id);
                return result;
            }
            catch (Exception e)
            {
                return result;
            }
        }

        public async Task<IEnumerable<ElementZamowienia>> PobierzElementy()
        {
            IEnumerable<ElementZamowienia> result = new List<ElementZamowienia>();
            try
            {
                result = await mapper.FetchAsync<ElementZamowienia>("Select * from ksiazka.ElementyZamowienia");
                return result;
            }
            catch (Exception e)
            {
                return result;
            }
        }

        public async Task<bool> Usuń(ElementZamowienia item)
        {
            try
            {

                var id = item.Id;
                await mapper.DeleteAsync<ElementZamowienia>("Delete from ksiazka.ElementyZamowienia where Id = ? ", id);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> Utwórz(ElementZamowienia item)
        {
            try
            {
                await mapper.InsertAsync<ElementZamowienia>(item);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        protected override void UtwórzSesję()
        {
            sesja = klaster.ConnectAsync().Result;
            var insert = sesja.Execute(new SimpleStatement("CREATE TABLE IF NOT EXISTS ksiazka.ElementyZamowienia (Id UUID PRIMARY KEY, IdZamowienia UUID, IdProduktu UUID, IloscProduktu int); "));
            mapper = new Mapper(sesja);
        }
    }
}
