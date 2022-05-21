using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Interfejsy;
using Modele;

namespace eSklep.Controllers
{
    public class AdressController :BaseController
    {
    
        public AdressController(BaseProvider baseShopProvider, UserManager<IdentityUser> userManager) :base (baseShopProvider,userManager)
        {
          
        }

        // GET: adreses
        public async Task<IActionResult> Index()
        {
            return View(await _shopProvider.AdresManager.PobierzElementy());
        }

        // GET: adreses/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adres =await _shopProvider.AdresManager.PobierzElement(id.Value);
            if (adres == null)
            {
                return NotFound();
            }

            return View(adres);
        }

        // GET: adreses/Create
        public IActionResult Create(Guid clientId)
        {
            var adres = new Adres();
            adres.IdKlienta = clientId;
            return View(adres);
        }

        // POST: adreses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
              // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdKlienta,Id,Ulica,NumerDomu,NumerMieszkania,Miasto,KodPocztowy,Kraj")] Adres adres)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                adres.Id = Guid.NewGuid();
                adres.IdKlienta =Guid.Parse(user.Id);
                adres.Glowny = await _shopProvider.AdresManager.JestAdresesmDomyślnym(adres.IdKlienta);
                await _shopProvider.AdresManager.Utwórz(adres);
                return RedirectToAction("Index","Items");
            }
            return View(adres);
        }

        // GET: adreses/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adres = await _shopProvider.AdresManager.PobierzElement(id.Value);
            if (adres == null)
            {
                return NotFound();
            }
            return View(adres);
        }

        // POST: adreses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
              // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("IdKlienta,Id,Ulica,NumerDomu,NumerMieszkania,Miasto,KodPocztowy,Kraj")] Adres adres)
        {
            if (id != adres.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _shopProvider.AdresManager.Edytuj(id,adres);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await adresExists(adres.Id))
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
            return View(adres);
        }

        // GET: adreses/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adres = await _shopProvider.AdresManager.PobierzElement(id.Value);
            if (adres == null)
            {
                return NotFound();
            }

            return View(adres);
        }

        // POST: adreses/Delete/5
        [HttpPost, ActionName("Delete")]
              // [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var adres = await _shopProvider.AdresManager.PobierzElement(id);
            await _shopProvider.AdresManager.Usuń(adres);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> adresExists(Guid id)
        {
            return await _shopProvider.AdresManager.PobierzElement(id) != null; 
        }
    }
}
