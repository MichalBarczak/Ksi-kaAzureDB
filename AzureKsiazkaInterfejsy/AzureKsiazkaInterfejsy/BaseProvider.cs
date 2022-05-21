using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Interfejsy
{
    public abstract class BaseProvider
    {
    
        public abstract IAdresManager AdresManager { get; }
        public abstract IKlientManager KlientManager { get; }
        public abstract IProduktManager ProduktManager { get; }
        public abstract IProducentManager ProducentManager { get; }
        public abstract ITypProduktuManager TypProduktuManager { get; }
        public abstract IElementZamowieniaManager ElementZamowieniaManager { get; }
        public abstract IZamowienieManager ZamowienieManager { get; }
        public abstract IMagazynManager MagazynManager { get; }
    }
}
