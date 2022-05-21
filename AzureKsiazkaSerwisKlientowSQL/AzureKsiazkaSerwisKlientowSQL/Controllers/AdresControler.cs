using Interfejsy;
using Modele;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AzureKsiazkaSerwisTypowMongo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdresController : ControllerBase
    {
        private IAdresManager _AdresManager;
        public AdresController(IAdresManager AdresManager)
        {
            _AdresManager = AdresManager;
        }
        // GET: <AdresController>
        [HttpGet]
        public async Task<IEnumerable<Adres>> Get()
        {
            return await _AdresManager.PobierzElementy();
        }

        // GET <AdresController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Adres>> Get(Guid id)
        {
            return await _AdresManager.PobierzElement(id);
        }

        // POST <AdresController>
        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody] Adres Adres)
        {
            return await _AdresManager.Utwórz(Adres);
        }

        // PUT <AdresController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(Guid id, [FromBody] Adres produkt)
        {
            return await _AdresManager.Edytuj(id, produkt);
        }

        // DELETE <AdresController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            var Adres = await _AdresManager.PobierzElement(id);
            if (Adres == null)
            {
                return NotFound();
            }
            return await _AdresManager.Usuń(Adres);
        }
    }
}
