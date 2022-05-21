using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Modele;
using Interfejsy;

namespace eSklep.Controllers
{
    public class TypProduktusController : BaseController
    { 
        public TypProduktusController(BaseProvider baseShopProvider, UserManager<IdentityUser> userManager):base(baseShopProvider,userManager)
        {
          
        }
        public async Task<IActionResult> Index()
        {
            return View(await _shopProvider.TypProduktuManager.PobierzElementy());
        }
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var TypProduktu = await _shopProvider.TypProduktuManager.PobierzElement(id.Value);
            if (TypProduktu == null)
            {
                return NotFound();
            }

            return View(TypProduktu);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Nazwa,Id")] TypProduktu typProduktu)
        {
            if (ModelState.IsValid)
            {
                typProduktu.Id = Guid.NewGuid();
               await _shopProvider.TypProduktuManager.Utwórz(typProduktu);
                return RedirectToAction(nameof(Index));
            }
            return View(typProduktu);
        }
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var TypProduktu=await _shopProvider.TypProduktuManager.PobierzElement(id.Value);
            if (TypProduktu == null)
            {
                return NotFound();
            }
            return View(TypProduktu);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, [Bind("Nazwa,Id")] TypProduktu typProduktu)
        {
            if (id != typProduktu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _shopProvider.TypProduktuManager.Edytuj(id, typProduktu);
                return RedirectToAction(nameof(Index));
            }
            return View(typProduktu);
        }
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var TypProduktu = await _shopProvider.TypProduktuManager.PobierzElement(id.Value);
            if (TypProduktu == null)
            {
                return NotFound();
            }

            return View(TypProduktu);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var TypProduktu = await _shopProvider.TypProduktuManager.PobierzElement(id);
            await _shopProvider.TypProduktuManager.Usuń(TypProduktu);
            return RedirectToAction(nameof(Index));
        }
        private async Task<bool> TypProduktuExists(Guid id)
        {
            return await _shopProvider.TypProduktuManager.PobierzElement(id) != null;
        }
    }
}
