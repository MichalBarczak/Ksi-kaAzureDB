using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Modele;
using Modele.Enums;
using Interfejsy;

namespace eSklep.Controllers
{
    [Authorize]
    public class ProduktsController : BaseController
    {

        private ICacheManager _cacheManager;
        public ProduktsController( BaseProvider baseShopProvider, UserManager<IdentityUser> userManager, ICacheManager cacheManager) : base( baseShopProvider,userManager)
        {
            _cacheManager=cacheManager;
        }
        [AllowAnonymous]
        // GET: Produkts
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString,int? pageNumber)
        {
            if (TempData["Result"] != null)
            {
                ViewBag.Result = TempData["Result"].ToString();
            }
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Nazwa_desc" : "";
            ViewData["DescriptionSortParm"] = sortOrder == "Description" ? "Opis_desc" : "Opis";
            ViewData["PrizeSortParm"] = sortOrder == "Prize" ? "Cena_desc" : "Cena";
            ViewData["ProducerSortParm"] = sortOrder == "Producer" ? "Producent_desc" : "Producent";
            ViewData["TypeSortParm"] = sortOrder == "Type" ? "Typ_desc" : "Typ";
            sortOrder=sortOrder== null ? "Nazwa_desc" : sortOrder;
            ViewData["CurrentFilter"] = searchString;
            ViewBag.ListOfProducers =await GetListOfProducers();
            ViewBag.ListOfTypes =await GetListOfProduktTypes();
            var Produkts =await _shopProvider.ProduktManager.Sortuj(sortOrder,searchString);
            int pageSize = 10;
            return View(ListaStronicowana<Produkt>.Create(Produkts.AsQueryable<Produkt>(), pageNumber ?? 1, pageSize));
        }
        [AllowAnonymous]
        // GET: Produkts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Produkt =await _shopProvider.ProduktManager.PobierzElement(id.Value);
            if (Produkt == null)
            {
                return NotFound();
            }

            return View(Produkt);
        }
        [AllowAnonymous]
        public async Task<IActionResult> DodajProduktDoZamowienia (Guid? id)
        {
            try
            {
                var magazyn = await _shopProvider.MagazynManager.PobierzElementy();
                var stock=magazyn.Where(z => z.IdProduktu == id);
                if (stock.Any(z => z.IlośćProduktu > 0)){
                    var ProduktId = await _cacheManager.PobierzWartość();
                     if (ProduktId == null)
                    {
                        ProduktId = id.ToString();
                    }
                    else
                    {
                        ProduktId = ProduktId + ";" + id.ToString();
                    }
                    await _cacheManager.ZapiszWartość(ProduktId);
                    TempData["Result"] = "Produkt dodany";
                }
                else
                {
                    TempData["Result"] = "Produkt nie dostępny w magazynie";
                }
            }
            catch (Exception e)
            {

            }
            return RedirectToAction("Index");
        }
        // GET: Produkts/Create
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create()
        {
            ViewBag.ListOfProducers = await GetListOfProducers();
            ViewBag.ListOfTypes = await GetListOfProduktTypes ();
            return View();
        }
        public IActionResult Order()
        {
            var ProduktId = _cacheManager.PobierzWartość();
            if (ProduktId == null)
            {
                TempData["Result"] = "Prosze dodać produkty do zamóienia";
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Create", "Zamowienies");
            }
        }

        // POST: Produkts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       //       // [ValidateAntiForgeryToken]
       //[Authorize(Roles = "Admin")]
       [AllowAnonymous]
        public async Task<IActionResult> Create([Bind("Id,Nazwa,Opis,Cena,IdTypu,IdProducenta")] Produkt Produkt)
        {

            if (ModelState.IsValid)
            {
                Produkt.Id = Guid.NewGuid();
                Produkt.Producent=await _shopProvider.ProducentManager.PobierzElement(Produkt.IdProducenta);
                Produkt.TypProduktu=await _shopProvider.TypProduktuManager.PobierzElement(Produkt.IdTypu);
                Produkt.NazwaProducenta=Produkt.Producent.Nazwa;
                Produkt.NazwaTypu=Produkt.TypProduktu.Nazwa;
                await _shopProvider.ProduktManager.Utwórz(Produkt);
                return RedirectToAction(nameof(Index));
            }
            return View(Produkt);
        }

        // GET: Produkts/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            ViewBag.ListOfProducers =await GetListOfProducers();
            ViewBag.ListOfTypes =await GetListOfProduktTypes();
            if (id == null)
            {
                return NotFound();
            }

            var Produkt = await _shopProvider.ProduktManager.PobierzElement(id.Value);
            if (Produkt == null)
            {
                return NotFound();
            }
            return View(Produkt);
        }

        // POST: Produkts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
              // [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nazwa,Opis,Cena,IdTypu,IdProducenta")] Produkt Produkt)
        {
            if (id != Produkt.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Produkt.Producent=await _shopProvider.ProducentManager.PobierzElement(Produkt.IdProducenta);
                    Produkt.TypProduktu=await _shopProvider.TypProduktuManager.PobierzElement(Produkt.IdTypu);
                    Produkt.NazwaProducenta=Produkt.Producent.Nazwa;
                    Produkt.NazwaTypu=Produkt.TypProduktu.Nazwa;
                    await _shopProvider.ProduktManager.Edytuj(id,Produkt);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ProduktExists(Produkt.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Produkt);
        }

        // GET: Produkts/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Produkt = await _shopProvider.ProduktManager.PobierzElement(id.Value);
            if (Produkt == null)
            {
                return NotFound();
            }

            return View(Produkt);
        }

        // POST: Produkts/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
              // [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var Produkt = await _shopProvider.ProduktManager.PobierzElement(id);
            await _shopProvider.ProduktManager.Usuń(Produkt);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProduktExists(Guid id)
        {
            return await _shopProvider.ProduktManager.PobierzElement(id) != null;
        }

        private async Task<IEnumerable<ElementListy>> GetListOfProducers()
        {
            var result = new List<ElementListy>();
            var list = await _shopProvider.ProducentManager.PobierzElementy();
            foreach (var Produkt in list)
            {
                result.Add(new ElementListy
                {
                    Id=Produkt.Id,
                    Wartość=Produkt.Nazwa
                });
            }
            return result;
        }
        private async Task<IEnumerable<ElementListy>> GetListOfProduktTypes()
        {
            var result = new List<ElementListy>();
            var list = await _shopProvider.TypProduktuManager.PobierzElementy();
            foreach (var Produkt in list)
            {
                result.Add(new ElementListy
                {
                    Id = Produkt.Id,
                    Wartość = Produkt.Nazwa
                });
            }
            return result;
        }
    }
}
