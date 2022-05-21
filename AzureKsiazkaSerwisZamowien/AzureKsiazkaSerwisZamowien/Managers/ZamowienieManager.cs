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
    public class ZamowienieManager : BaseCassandraManager, IZamowienieManager
    {
       
        public ZamowienieManager(IConfiguration configuartion):base(configuartion)
        {
          
        }
        public async Task<bool> Edytuj(Guid id, Zamowienie item)
        {
            try
            {
                await mapper.UpdateAsync(item);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public async Task<Zamowienie> PobierzElement(Guid id)
        {
            Zamowienie result = null; 
            try
            {
                result=await mapper.FirstOrDefaultAsync< Zamowienie >("Select * from ksiazka.Zamowienia where Id = ? ", id);
                return result;
            }
            catch (Exception e)
            {
                return result;
            }
        }

        public async Task<IEnumerable<Zamowienie>> PobierzElementy()
        {
            IEnumerable<Zamowienie> result = new List<Zamowienie>();
            try
            {
                
                result = await mapper.FetchAsync<Zamowienie>("Select * from ksiazka.Zamowienia");
                return result;
            }
            catch (Exception e)
            {
                return result;
            }
        }

        public async Task<bool> Usuń(Zamowienie item)
        {
            try
            {
                
                var id = item.Id;
                var deleteStatement = new SimpleStatement("DELETE FROM ksiazka.Zamowienia WHERE id = ? ", id);
                await  sesja.ExecuteAsync(deleteStatement);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> Utwórz(Zamowienie item)
        {
            try
            {
                var query = $@"Insert into ksiazka.Zamowienia (Id, IdKlienta, IdAdresu, Upust, Cena, CenaDostawy, StatusZamowienia, SposobDostawy, SposobPLatnosci) values ('{item.Id}','{item.IdKlienta}','{item.IdAdresu}',{item.Upust},{item.Cena},{item.CenaDostawy},'{item.StatusZamowienia}','{item.SposobDostawy}','{item.SposobPlatnosci}')";
                var insertQuer=new SimpleStatement(query);
                await sesja.ExecuteAsync(insertQuer);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        protected override void UtwórzSesję()
        {
            sesja = klaster.ConnectAsync("ksiazka").Result;
            if (MappingConfiguration.Global.Get<Zamowienie>()==null)
            {
                MappingConfiguration.Global.Define(new Map<Zamowienie>().TableName("Zamowienia").PartitionKey(u => u.Id));
            }
            var tabela = sesja.Execute(new SimpleStatement("CREATE TABLE IF NOT EXISTS ksiazka.Zamowienia (Id UUID PRIMARY KEY, IdKlienta UUID, IdAdresu UUID, Upust double, Cena double, CenaDostawy double, StatusZamowienia text, SposobDostawy text, SposobPLatnosci text); "));
            mapper = new Mapper(sesja);
        }
    }   
}
