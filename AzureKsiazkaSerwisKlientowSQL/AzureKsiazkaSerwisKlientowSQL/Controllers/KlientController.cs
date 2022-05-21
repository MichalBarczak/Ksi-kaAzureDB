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
    public class KlientController : ControllerBase
    {
        private IKlientManager _KlientManager;
        public KlientController(IKlientManager KlientManager)
        {
            _KlientManager = KlientManager;
        }
        // GET: <KlientController>
        [HttpGet]
        public async Task<IEnumerable<Klient>> Get()
        {
            return await _KlientManager.PobierzElementy();
        }

        // GET <KlientController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Klient>> Get(Guid id)
        {
            return await _KlientManager.PobierzElement(id);
        }

        // POST <KlientController>
        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody] Klient Klient)
        {
            return await _KlientManager.Utwórz(Klient);
        }

        // PUT <KlientController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(Guid id, [FromBody] Klient produkt)
        {
            return await _KlientManager.Edytuj(id, produkt);
        }

        // DELETE <KlientController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            var Klient = await _KlientManager.PobierzElement(id);
            if (Klient == null)
            {
                return NotFound();
            }
            return await _KlientManager.Usuń(Klient);
        }
    }
}
