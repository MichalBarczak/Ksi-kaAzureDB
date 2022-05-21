using Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfejsy
{
    public interface IBaseManager <T> where T:BaseModel { 
        Task<T> PobierzElement(Guid id);
        Task<IEnumerable<T>> PobierzElementy();
        Task<bool> Edytuj(Guid id,T item);
        Task<bool> Utwórz(T item);
        Task<bool> Usuń(T item);
     }
}
