using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Modele;

namespace Interfejsy
{
    public interface IAdresManager : IBaseManager<Adres>
    {
        public  Task<bool> JestAdresesmDomyślnym(Guid idKlienta);
    }
}
