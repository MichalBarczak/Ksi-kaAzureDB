using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Modele;

namespace Interfejsy
{
    public interface IProduktManager:IBaseManager<Produkt>
    {
        public Task <IEnumerable<Produkt>> Sortuj(string RodzajSortowania, string filter);
        public Task<IEnumerable<Produkt>> PobierzPowiazania(Guid id);
        public Task<bool> DodajPowiazanie(Guid Id, Guid p);
        public Task<bool> DodajProdukt(Produkt p);
    }
}
