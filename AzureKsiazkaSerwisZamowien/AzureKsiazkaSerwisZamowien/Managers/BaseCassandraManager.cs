using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Cassandra;
using Cassandra.Mapping;
using Microsoft.Extensions.Configuration;
namespace SerwisZamówień.Managers
{
    public abstract class BaseCassandraManager
    {
       
        
        internal protected ISession sesja = null;
        internal protected IMapper mapper = null;
        internal protected Cluster klaster = null;
        public BaseCassandraManager(IConfiguration konfiguracja)
        {
            var opcje = new Cassandra.SSLOptions(SslProtocols.Tls12,true,ZweryfikujCertyfikat);
            opcje.SetHostNameResolver((adres) => konfiguracja["Cassandra:ContactPoint"]);
            klaster = Cluster
               .Builder()
               .WithCredentials(konfiguracja["Cassandra:Username"], konfiguracja["Cassandra:Password"])
               .WithPort(10350)
               .AddContactPoint(konfiguracja["Cassandra:ContactPoint"])
               .WithSSL(opcje)
               .Build();
            klaster.Connect("ksiazka");
            UtwórzSesję();
        }
        protected abstract void UtwórzSesję();
        public static bool ZweryfikujCertyfikat(object wysyłający, X509Certificate certyfikat, X509Chain łańcuchCertyfikatów, SslPolicyErrors politykaSSL)
        {
            if (politykaSSL == SslPolicyErrors.None)
                return true;
            return false;
        }
    }
  }
