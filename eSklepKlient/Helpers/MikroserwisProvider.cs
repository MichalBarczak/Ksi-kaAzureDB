using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfejsy;
using eSklep.Managers;

namespace eSklep.Helpers
{
    public class MikroserwisProvider : BaseProvider
    {
  
        private IKlientManager _KlientManager;
        private IAdresManager _AdresManager;
        private IProduktManager _ProduktManager;
        private ITypProduktuManager _TypProduktuManager;
        private IElementZamowieniaManager _orderItemsManager;
        private IZamowienieManager _ZamowienieManager;
        private IMagazynManager _MagazynManager;
        private IProducentManager _ProducentManager;
        public override IKlientManager KlientManager
        {
            get
            {
                if (_KlientManager == null)
                {
                  _KlientManager = new KlientMicroserviceManager();
                }
                return _KlientManager;
            }
        }
        public override IAdresManager AdresManager
        {
            get
            {
                if (_AdresManager == null)
                {
                    _AdresManager = new AdresMicroserviceManager();
                }
                return _AdresManager;
            }
        }
        public override IProduktManager ProduktManager
        {
            get
            {
                if (_ProduktManager == null)
                {
                    _ProduktManager = new ProduktMicroserviceManager();
                }
                return _ProduktManager;
            }
        }
        public override ITypProduktuManager TypProduktuManager
        {
            get
            {
                if (_TypProduktuManager == null)
                {
                    _TypProduktuManager = new TypProduktuMicroserviceManager();
                }
                return _TypProduktuManager;
            }
        }
        public override IElementZamowieniaManager ElementZamowieniaManager
        {
            get
            {
                if (_orderItemsManager == null)
                {
                    _orderItemsManager = new ElementZamowieniaMicroserviceManager();
                }
                return _orderItemsManager;
            }
        }
        public override IZamowienieManager ZamowienieManager
        {
            get
            {
                if (_ZamowienieManager == null)
                {
                    _ZamowienieManager = new ZamowienieMicroserviceManager();
                }
                return _ZamowienieManager;
            }
        }
        public override IMagazynManager MagazynManager
        {
            get
            {
                if (_MagazynManager == null)
                {
                    _MagazynManager = new MagazynMicroserviceManager();
                }
                return _MagazynManager;
            }
        }
        public override IProducentManager ProducentManager
        {
            get
            {
                if (_ProducentManager == null)
                {
                    _ProducentManager = new ProducentMicroserviceManager();
                }
                return _ProducentManager;
            }
        }
    }
}
