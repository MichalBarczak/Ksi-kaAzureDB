using Interfejsy;
using Modele;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerwisZamówień.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ElementZamowieniaController : Controller
    {
        private IElementZamowieniaManager _elementZamowieniaManager;
        public ElementZamowieniaController(IElementZamowieniaManager elementZamowieniaManager)
        {
            _elementZamowieniaManager = elementZamowieniaManager;
        }
        // GET: api/<ProducentController>
        [HttpGet]
        public async Task<IEnumerable<ElementZamowienia>> Get()
        {
            return await _elementZamowieniaManager.PobierzElementy();
        }

        // GET api/<ProducentController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ElementZamowienia>> Get(Guid id)
        {
            return await _elementZamowieniaManager.PobierzElement(id);
        }

        // POST api/<ProducentController>
        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody] ElementZamowienia element)
        {
            return await _elementZamowieniaManager.Utwórz(element);
        }

        // PUT api/<ProducentController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(Guid id, [FromBody] ElementZamowienia element)
        {
            return await _elementZamowieniaManager.Edytuj(id, element);
        }

        // DELETE api/<ProducentController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            var producent = await _elementZamowieniaManager.PobierzElement(id);
            if (producent == null)
            {
                return NotFound();
            }
            return await _elementZamowieniaManager.Usuń(producent);
        }
    }
}
