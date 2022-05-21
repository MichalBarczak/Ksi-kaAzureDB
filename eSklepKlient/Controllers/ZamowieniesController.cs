using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Modele;
using Microsoft.AspNetCore.Http;
using Modele.Enums;
using Interfejsy;

namespace eSklep.Controllers
{
    public class ZamowieniesController : BaseController
    {
        private ICacheManager _cacheManager;
        public ZamowieniesController(BaseProvider baseShopProvider, UserManager<IdentityUser> userManager, ICacheManager cacheManager) : base(baseShopProvider,userManager)
        {
            _cacheManager=cacheManager;
        }

        // GET: Zamowienies
        public async Task<IActionResult> Index()
        {
            return View(await _shopProvider.ZamowienieManager.PobierzElementy());
        }
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemType = await _shopProvider.ZamowienieManager.PobierzElement(id.Value);
            if (itemType == null)
            {
                return NotFound();
            }

            return View(itemType);
        }

        // POST: ItemTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var itemType = await _shopProvider.ZamowienieManager.PobierzElement(id);
            await _shopProvider.ZamowienieManager.Usuń(itemType);
            return RedirectToAction(nameof(Index));
        }
        // GET: Zamowienies/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Zamowienie = await _shopProvider.ZamowienieManager.PobierzElement(id.Value);
            if (Zamowienie == null)
            {
                return NotFound();
            }

            return View(Zamowienie);
        }

        // GET: Zamowienies/Create
        public async Task<IActionResult> Create()
        {
            var Zamowienie = new Zamowienie();
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            var userId= Guid.Parse(user.Id);
            Zamowienie.IdKlienta = userId;
            Zamowienie.Cena = 0;
            Zamowienie.CenaDostawy = 10;
            Zamowienie.Upust = 10;
            Zamowienie.StatusZamowienia = StatusyZamówienia.Zarezrwowane.ToString();
            var IdProduktus = await _cacheManager.PobierzWartość();
            foreach (var IdProduktu in IdProduktus.Split(";"))
            {
                var Item = await _shopProvider.ProduktManager.PobierzElement(Guid.Parse(IdProduktu));
                Zamowienie.Cena = Zamowienie.Cena + Item.Cena;
            }
            Zamowienie.Cena = Zamowienie.Cena + Zamowienie.CenaDostawy;
            Zamowienie.Cena = ((100 - Zamowienie.Upust) / 100) * Zamowienie.Cena;
            var adresy = await _shopProvider.AdresManager.PobierzElementy();
            var address= adresy.FirstOrDefault(z => z.IdKlienta == userId && z.Glowny);
            Zamowienie.AdresDostawy = address;
            return View(Zamowienie);
        }

        // POST: Zamowienies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
      //        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SposobDostawy,SposobPlatnosci,Cena,CenaDostawy,Upust,StatusZamowienia,IdAdresu")] Zamowienie Zamowienie)
        {
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            var userId = Guid.Parse(user.Id);
            Zamowienie.IdKlienta = userId;
            Zamowienie.Cena = 0;
            Zamowienie.CenaDostawy = 10;
            Zamowienie.Upust = 10;
            Zamowienie.StatusZamowienia = StatusyZamówienia.Zarezrwowane.ToString();
            var IdProduktus = await _cacheManager.PobierzWartość();
            var ElementZamowienia = new List<ElementZamowienia>();
            foreach (var IdProduktu in IdProduktus.Split(";"))
            {
                var Item = await _shopProvider.ProduktManager.PobierzElement(Guid.Parse(IdProduktu));
                ElementZamowienia ZamowienieItem = new ElementZamowienia
                {
                    IdProduktu = Item.Id,
                    IlośćProduktu = 1,
                    IdZamowienia = Zamowienie.Id
                };
                ElementZamowienia.Add(ZamowienieItem);
                Zamowienie.Cena = Zamowienie.Cena + Item.Cena;
            }
            Zamowienie.Cena = Zamowienie.Cena + Zamowienie.CenaDostawy;
            Zamowienie.Cena = ((100 - Zamowienie.Upust) / 100) * Zamowienie.Cena;
            var adresy = await _shopProvider.AdresManager.PobierzElementy();
            var address= adresy.FirstOrDefault(z => z.IdKlienta == userId && z.Glowny);
            Zamowienie.AdresDostawy = address;
            Zamowienie.Id = Guid.NewGuid();
            Zamowienie.IdKlienta = Guid.Parse(user.Id);
           await _shopProvider.ZamowienieManager.Utwórz(Zamowienie);

            foreach (var oi in ElementZamowienia)
            {
                oi.IdZamowienia = Zamowienie.Id;               
                await _shopProvider.ElementZamowieniaManager.Utwórz(oi);
            }
            return RedirectToAction(nameof(Index));


        }
        private async Task<bool> ZamowienieExists(Guid id)
        {
            return await _shopProvider.ZamowienieManager.PobierzElement(id) != null;
        }
    }
}
