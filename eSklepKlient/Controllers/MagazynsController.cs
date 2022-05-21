using Interfejsy;
using Modele;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSklep.Controllers
{
    public class MagazynsController : BaseController
    {

        public MagazynsController(BaseProvider baseShopProvider, UserManager<IdentityUser> userManager) : base(baseShopProvider, userManager)
        {

        }

        public async Task<IActionResult> Index()
        {
            ViewBag.ListOfProducts = await GetListOfProdukts();
            return View(await _shopProvider.MagazynManager.PobierzElementy());
        }


        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemType = await _shopProvider.MagazynManager.PobierzElement(id.Value);
            if (itemType == null)
            {
                return NotFound();
            }

            return View(itemType);
        }

        // GET: ItemTypes/Create
        public async Task <IActionResult> Create()
        {
            ViewBag.ListOfProducts = await GetListOfProdukts();
            return View();
        }

        // POST: ItemTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProduktu,IlośćProduktu,Id")] Magazyn magazyn)
        {
            var produkt =await _shopProvider.ProduktManager.PobierzElement(magazyn.IdProduktu);
            magazyn.NazwaProduktu= produkt.Nazwa;
            ViewBag.ListOfProducts = await GetListOfProdukts();
                       
                magazyn.Id = Guid.NewGuid();
                await _shopProvider.MagazynManager.Utwórz(magazyn);
                return RedirectToAction(nameof(Index));
        }

        // GET: ItemTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.ListOfProducts = await GetListOfProdukts();
            var itemType = await _shopProvider.MagazynManager.PobierzElement(id.Value);
            if (itemType == null)
            {
                return NotFound();
            }
            return View(itemType);
        }

        // POST: ItemTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("IdProduktu,IlośćProduktu,Id")] Magazyn itemType)
        {
            if (id != itemType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                ViewBag.ListOfProdukts = await GetListOfProdukts();
                try
                {
                    await _shopProvider.MagazynManager.Edytuj(id, itemType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await MagazynIstnieje(itemType.Id))
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
            return View(itemType);
        }

        // GET: ItemTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemType = await _shopProvider.MagazynManager.PobierzElement(id.Value);
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
            var itemType = await _shopProvider.MagazynManager.PobierzElement(id);
            await _shopProvider.MagazynManager.Usuń(itemType);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> MagazynIstnieje(Guid id)
        {
            return await _shopProvider.MagazynManager.PobierzElement(id) != null;
        }

        private async Task<IEnumerable<ElementListy>> GetListOfProdukts()
        {
            var result = new List<ElementListy>();
            var list = await _shopProvider.ProduktManager.PobierzElementy();
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
