using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Modele;
using Microsoft.EntityFrameworkCore;

namespace SerwisKlientów.db
{
    public class SQLDBContext: DbContext
    {
        public SQLDBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Klient> Klienci { get; set; }
        public DbSet<Adres> Adresy { get; set; }
    }
}
