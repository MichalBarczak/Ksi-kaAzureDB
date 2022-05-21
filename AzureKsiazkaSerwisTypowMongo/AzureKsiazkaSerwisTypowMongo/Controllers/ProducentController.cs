using Interfejsy;
using Modele;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SerwisTypów.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProducentController : ControllerBase
    {
        private IProducentManager _producentManager;
        public ProducentController(IProducentManager producentManager)
        {
            _producentManager = producentManager;
        }
        // GET: <ProducentController>
       [HttpGet]
        public async Task<IEnumerable<Producent>> Get()
        {
            return await _producentManager.PobierzElementy();
        }

        // GET <ProducentController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producent>> Get(Guid id)
        {
            return await _producentManager.PobierzElement(id);
        }

        // POST <ProducentController>
        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody] Producent producent)
        {
            return await _producentManager.Utwórz(producent);
        }

        // PUT <ProducentController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(Guid id, [FromBody] Producent produkt)
        {
            return await _producentManager.Edytuj(id, produkt);
        }

        // DELETE <ProducentController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            var producent = await _producentManager.PobierzElement(id);
            if (producent == null)
            {
                return NotFound();
            }
            return await _producentManager.Usuń(producent);
        }
    }
}
