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
using Interfejsy;

namespace eSklep.Controllers
{
    public class ElementZamowieniasController : BaseController
    {
       

        public ElementZamowieniasController( BaseProvider baseShopProvider, UserManager<IdentityUser> userManager) : base(baseShopProvider,userManager)
        {
           
        }

        // GET: ElementZamowienias
        public async Task<IActionResult> Index()
        {
            return View(await _shopProvider.ElementZamowieniaManager.PobierzElementy());
        }

        // GET: ElementZamowienias/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ElementZamowienias = await _shopProvider.ElementZamowieniaManager.PobierzElement(id.Value);
            if (ElementZamowienias == null)
            {
                return NotFound();
            }

            return View(ElementZamowienias);
        }

        // GET: ElementZamowienias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ElementZamowienias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
              // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdZamowienia,IdProduktu,IlośćProduktu")] ElementZamowienia ElementZamowienias)
        {
            if (ModelState.IsValid)
            {
                ElementZamowienias.Id = Guid.NewGuid();
                await _shopProvider.ElementZamowieniaManager.Utwórz(ElementZamowienias);
                return RedirectToAction(nameof(Index));
            }
            return View(ElementZamowienias);
        }

        // GET: ElementZamowienias/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ElementZamowienias = await _shopProvider.ElementZamowieniaManager.PobierzElement(id.Value);
            if (ElementZamowienias == null)
            {
                return NotFound();
            }
            return View(ElementZamowienias);
        }

        // POST: ElementZamowienias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
              // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,IdZamowienia,IdProduktu,IlośćProduktu")] ElementZamowienia ElementZamowienias)
        {
            if (id != ElementZamowienias.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _shopProvider.ElementZamowieniaManager.Edytuj(id,ElementZamowienias);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ElementZamowieniasExists(ElementZamowienias.Id))
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
            return View(ElementZamowienias);
        }

        // GET: ElementZamowienias/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ElementZamowienias = await _shopProvider.ElementZamowieniaManager.PobierzElement(id.Value);
            if (ElementZamowienias == null)
            {
                return NotFound();
            }

            return View(ElementZamowienias);
        }

        // POST: ElementZamowienias/Delete/5
        [HttpPost, ActionName("Delete")]
              // [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var ElementZamowienias =  await _shopProvider.ElementZamowieniaManager.PobierzElement(id);
            await _shopProvider.ElementZamowieniaManager.Usuń(ElementZamowienias);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ElementZamowieniasExists(Guid id)
        {
            return await _shopProvider.ElementZamowieniaManager.PobierzElement(id) != null;
        }
    }
}
