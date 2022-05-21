using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Modele;
using Modele.Enums;
using Interfejsy;

namespace eSklep.Controllers
{
    public class KlientsController : BaseController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public KlientsController(BaseProvider baseShopProvider, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager) : base(baseShopProvider, userManager)
        {
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        // GET: Klients
        public async Task<IActionResult> Index()
        {
            return View(await _shopProvider.KlientManager.PobierzElementy());
        }

        // GET: Klients/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Klient = await _shopProvider.KlientManager.PobierzElement(id.Value);
            if (Klient == null)
            {
                return NotFound();
            }

            return View(Klient);
        }

        // GET: Klients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Klients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
              // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Imię,Nazwisko,Email,NumerTelefonu,Hasło")] Klient Klient)
        {
            if (ModelState.IsValid)
            {
                Klient.Id = Guid.NewGuid();
                _userManager.Options.SignIn.RequireConfirmedAccount = false;
                var result = await _userManager.CreateAsync(new IdentityUser
                {
                    Email = Klient.Email,
                    UserName = Klient.Email,
                    Id = Klient.Id.ToString(),
                    EmailConfirmed = true
                }, Klient.Hasło) ;
                var success = result.Succeeded;
                if (success)
                {
                    if (!await _roleManager.RoleExistsAsync(Role.Klient.ToString()))
                    {
                        var role = new IdentityRole
                        {
                            Name = Role.Klient.ToString(),
                            ConcurrencyStamp = Role.Klient.ToString(),
                            NormalizedName = Role.Klient.ToString(),
                            Id = Guid.NewGuid().ToString()
                        };
                        await _roleManager.CreateAsync(role);

                    }
                    var user = await _userManager.FindByIdAsync(Klient.Id.ToString());
                    await _userManager.AddToRoleAsync(user, Role.Klient.ToString());
                    await _signInManager.SignInAsync(user, false);
                    await  _shopProvider.KlientManager.Utwórz(Klient);
                    return RedirectToAction("Create", "Adress", new { KlientId = Klient.Id });
                }
            }
            return View(Klient);
        }

        // GET: Klients/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Klient = await _shopProvider.KlientManager.PobierzElement(id.Value);
            if (Klient == null)
            {
                return NotFound();
            }
            return View(Klient);
        }

        // POST: Klients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
              // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Imię,Nazwisko,Email,NumerTelefonu,Hasło")] Klient Klient)
        {
            if (id != Klient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   await _shopProvider.KlientManager.Edytuj(id,Klient);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await KlientExists(Klient.Id))
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
            return View(Klient);
        }

        // GET: Klients/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Klient = await _shopProvider.KlientManager.PobierzElement(id.Value);
            if (Klient == null)
            {
                return NotFound();
            }

            return View(Klient);
        }

        // POST: Klients/Delete/5
        [HttpPost, ActionName("Delete")]
              // [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var Klient = await _shopProvider.KlientManager.PobierzElement(id);
            await _shopProvider.KlientManager.Usuń(Klient);
            return RedirectToAction(nameof(Index));
        }

        private async Task <bool> KlientExists(Guid id)
        {
            return await _shopProvider.KlientManager.PobierzElement(id) != null;
        }
    }
}
