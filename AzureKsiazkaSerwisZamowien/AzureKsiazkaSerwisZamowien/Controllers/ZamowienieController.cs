using Interfejsy;
using Modele;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SerwisZamówień.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ZamowienieController : ControllerBase
    {
        private IZamowienieManager _zamowienieManager;
        public ZamowienieController(IZamowienieManager zamowienieManager)
        {
            _zamowienieManager = zamowienieManager;
        }
        // GET: api/<ProducentController>
        [HttpGet]
        public async Task<IEnumerable<Zamowienie>> Get()
        {
            return await _zamowienieManager.PobierzElementy();
        }

        // GET api/<ProducentController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Zamowienie>> Get(Guid id)
        {
            return await _zamowienieManager.PobierzElement(id);
        }

        // POST api/<ProducentController>
        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody] Zamowienie zamowienie)
        {
            return await _zamowienieManager.Utwórz(zamowienie);
        }

        // PUT api/<ProducentController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(Guid id, [FromBody] Zamowienie zamowienie)
        {
            return await _zamowienieManager.Edytuj(id, zamowienie);
        }

        // DELETE api/<ProducentController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            var producent = await _zamowienieManager.PobierzElement(id);
            if (producent == null)
            {
                return NotFound();
            }
            return await _zamowienieManager.Usuń(producent);
        }
    }
}
