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
    public class MagazynController : ControllerBase
    {
        private IMagazynManager _MagazynManager;
        public MagazynController(IMagazynManager MagazynManager)
        {
            _MagazynManager = MagazynManager;
        }
        // GET: <MagazynController>
        [HttpGet]
        public async Task<IEnumerable<Magazyn>> Get()
        {
            return await _MagazynManager.PobierzElementy();
        }

        // GET <MagazynController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Magazyn>> Get(Guid id)
        {
            return await _MagazynManager.PobierzElement(id);
        }

        // POST <MagazynController>
        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody] Magazyn Magazyn)
        {
            return await _MagazynManager.Utwórz(Magazyn);
        }

        // PUT <MagazynController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(Guid id, [FromBody] Magazyn produkt)
        {
            return await _MagazynManager.Edytuj(id, produkt);
        }

        // DELETE <MagazynController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            var Magazyn = await _MagazynManager.PobierzElement(id);
            if (Magazyn == null)
            {
                return NotFound();
            }
            return await _MagazynManager.Usuń(Magazyn);
        }
    }
}
