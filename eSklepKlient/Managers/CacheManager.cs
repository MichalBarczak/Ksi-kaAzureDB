using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Modele;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using Interfejsy;

namespace eSklep.Managers
{
    public class CacheManager : ICacheManager
    {
        public CacheManager(IConfiguration configuration)
        {
            Konfiguracja = configuration;
        }

        public static ConnectionMultiplexer Połączenia
        {
            get
            {
                string cacheConnection = Konfiguracja.GetSection("config").GetValue<string>("CacheConnection");
                return ConnectionMultiplexer.Connect(cacheConnection);
            }
        }
        private static IConfiguration Konfiguracja { get; set; }
        public async Task<string> PobierzWartość()
        {
            try
            {
                IDatabase cache = Połączenia.GetDatabase();
                var zamowienie = cache.StringGet("Zamowienie").ToString();
                 return zamowienie;
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }

        public async Task<bool> ZapiszWartość(string zamowiecie)
        {
            try
            {
                IDatabase cache = Połączenia.GetDatabase();
                cache.StringSet("Zamowienie", zamowiecie);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
 }
}
