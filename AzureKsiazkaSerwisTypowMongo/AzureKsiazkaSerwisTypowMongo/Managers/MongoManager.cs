using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
namespace SerwisTypów.Managers
{
    public class MongoManager
    {
       private readonly IMongoClient _klient;
        private readonly IMongoDatabase _baza;
        public MongoManager()
        {
            var nazwaSerwera = KonfiguracjaManager.PobierzWartoscZKonfiguracji("MongoDB", "NazwaSerwera");
            var nazwaBazy = KonfiguracjaManager.PobierzWartoscZKonfiguracji("MongoDB", "NazwaBazy");
           _klient = new MongoClient(nazwaSerwera);
            _baza = _klient.GetDatabase(nazwaBazy);
         }
        public IMongoClient Klient
        {
            get { return _klient; }
        }

        public IMongoDatabase Baza
        {
            get { return _baza; }
        }
    }
}
