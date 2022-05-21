using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerwisTypów.Managers
{
    public static class KonfiguracjaManager
    {
        private static IConfiguration _konfiguracja;
        public static void WgrajKonfiguracje(IConfiguration konfiguracja)
        {
            _konfiguracja = konfiguracja;
        }

        public static string PobierzWartoscZKonfiguracji( string sekcja,string klucz)
        {
            return _konfiguracja.GetSection(sekcja).GetSection(klucz).Value;
        }
    }
}
