using Modele;
using SerwisKlientów.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerwisKlientów.Managers
{
    public abstract class BaseDBManager<T> where T : BaseModel
    {
        protected internal SQLDBContext _dbcontext;
        public BaseDBManager(SQLDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<bool> Edytuj(Guid id, T item)
        {
            try
            {
                _dbcontext.Update(item);
                _dbcontext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
          
        public async Task<bool> Usuń(T item)
        {
            try
            {
                _dbcontext.Remove(item);
                _dbcontext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public async Task<bool> Utwórz(T item)
        {
            try
            {
                _dbcontext.Add(item);
                _dbcontext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public abstract Task<T> PobierzElement(Guid id);

        public abstract Task<IEnumerable<T>> PobierzElementy();
    }
}
