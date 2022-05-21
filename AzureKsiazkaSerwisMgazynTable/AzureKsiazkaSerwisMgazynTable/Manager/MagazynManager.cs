using Interfejsy;
using Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Azure.Data.Tables;

namespace SerwisMagazynu.Manager
{
    public class MagazynManager : IMagazynManager
    {
        private TableClient _tableClient;
        public MagazynManager(TableClient tableClient)
        {
            _tableClient = tableClient;
        }
        public async Task<bool> Edytuj(Guid id, Magazyn item)
        {
            try
            {
                TableEntity entity = new TableEntity();
                entity.RowKey = id.ToString();
                entity.PartitionKey = item.IdProduktu.ToString();
                entity["IlośćProduktu"] = item.IlośćProduktu;
                entity["NazwaProduktu"] = item.NazwaProduktu;
               _tableClient.UpsertEntity(entity);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public  async Task<Magazyn> PobierzElement(Guid id)
        {
            string filter = ($"RowKey eq '{id.ToString()}'");
            Pageable<TableEntity> entities = _tableClient.Query<TableEntity>(filter);
            return entities.Select(e => MapójTabeleNaMagazyn(e)).FirstOrDefault();
        }

        public async Task<IEnumerable<Magazyn>> PobierzElementy()
        {
            Pageable<TableEntity> tabela =   _tableClient.Query<TableEntity>();
            return tabela.Select(e => MapójTabeleNaMagazyn(e));
        }

        public async Task<bool> Usuń(Magazyn item)
        {
            try
            {
                _tableClient.DeleteEntity(item.IdProduktu.ToString(), item.Id.ToString());
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public async Task<bool> Utwórz(Magazyn item)
        {
            return await Edytuj(item.Id, item);
        }

        public Magazyn MapójTabeleNaMagazyn(TableEntity encjaMagazyn)
        {
            Magazyn magazyn = new Magazyn();
            magazyn.Id = Guid.Parse(encjaMagazyn.RowKey);
            magazyn.IdProduktu = Guid.Parse(encjaMagazyn.PartitionKey);
            magazyn.NazwaProduktu =  encjaMagazyn["NazwaProduktu"].ToString();
            magazyn.IlośćProduktu = Convert.ToInt32(encjaMagazyn["IlośćProduktu"]);
            return magazyn;
        }
    }
}
