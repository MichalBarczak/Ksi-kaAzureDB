using Interfejsy;
using Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SerwisKlientów.db;

namespace SerwisKlientów.Managers
{
    public class AdresManager : BaseDBManager<Adres>, IAdresManager
    {
        public AdresManager(SQLDBContext dbContext):base(dbContext)
        {

        }
        public  async Task<bool> JestAdresesmDomyślnym(Guid idKlienta)
        {
            try
            {
                return _dbcontext.Adresy.FirstOrDefault(z => z.IdKlienta == idKlienta).Glowny;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async override Task<Adres> PobierzElement(Guid id)
        {
            try
            {
                return _dbcontext.Adresy.FirstOrDefault(z => z.Id == id);
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public async override Task<IEnumerable<Adres>> PobierzElementy()
        {
            try
            {
                var result = _dbcontext.Adresy.AsEnumerable();
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
