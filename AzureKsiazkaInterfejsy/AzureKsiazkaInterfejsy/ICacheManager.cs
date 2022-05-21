using Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfejsy
{
    public interface ICacheManager
    {
        Task<string> PobierzWartość();
        Task<bool> ZapiszWartość(string wartość);
    }
}
