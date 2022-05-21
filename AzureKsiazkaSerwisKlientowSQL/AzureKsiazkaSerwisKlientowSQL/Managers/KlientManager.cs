using Interfejsy;
using Modele;
using SerwisKlientów.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerwisKlientów.Managers
{
    public class KlientManager : BaseDBManager<Klient>, IKlientManager
    {
        public KlientManager (SQLDBContext dbContext) :base(dbContext)
        {

        }
        public override async Task<Klient> PobierzElement(Guid id)
        {
            try
            {
                return  (from klient in _dbcontext.Klienci
                        where klient.Id == id
                        select klient).FirstOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public override async Task<IEnumerable<Klient>> PobierzElementy()
        {
            try
            {
                return _dbcontext.Klienci;
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}
