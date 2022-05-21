using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modele;
using Interfejsy;

namespace eSklep.Controllers
{
    public class ProducentsController : BaseController
    {
      
        public ProducentsController(BaseProvider baseShopProvider, UserManager<IdentityUser> userManager):base(baseShopProvider,userManager)
        {
          
        }

        // GET: Producents
        public async Task<IActionResult> Index()
        {
            return View(await _shopProvider.ProducentManager.PobierzElementy());
        }

        // GET: Producents/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Producent = await _shopProvider.ProducentManager.PobierzElement(id.Value);
            if (Producent == null)
            {
                return NotFound();
            }

            return View(Producent);
        }

        // GET: Producents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Producents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
              // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nazwa,Id")] Producent Producent)
        {
            if (ModelState.IsValid)
            {
                Producent.Id = Guid.NewGuid();
                await _shopProvider.ProducentManager.Utwórz(Producent);
                return RedirectToAction(nameof(Index));
            }
            return View(Producent);
        }

        // GET: Producents/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Producent = await _shopProvider.ProducentManager.PobierzElement(id.Value);
            if (Producent == null)
            {
                return NotFound();
            }
            return View(Producent);
        }

        // POST: Producents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
              // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Nazwa,Id")] Producent Producent)
        {
            if (id != Producent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _shopProvider.ProducentManager.Edytuj(id,Producent);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ProducentExists(Producent.Id))
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
            return View(Producent);
        }

        // GET: Producents/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Producent = await _shopProvider.ProducentManager.PobierzElement(id.Value);
            if (Producent == null)
            {
                return NotFound();
            }

            return View(Producent);
        }

        // POST: Producents/Delete/5
        [HttpPost, ActionName("Delete")]
              // [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var Producent = await  _shopProvider.ProducentManager.PobierzElement(id);
           await  _shopProvider.ProducentManager.Usuń(Producent);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProducentExists(Guid id)
        {
            return await _shopProvider.ProducentManager.PobierzElement(id) != null;
        }
    }
}
