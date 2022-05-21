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
    public class ProduktController : ControllerBase
    {
        private IProduktManager _produktManager;
        public ProduktController(IProduktManager produktManager)
        {
            _produktManager = produktManager;
        }
        // GET: api/<ProduktController>
        [HttpGet]
        public async Task<IEnumerable<Produkt>> Get()
        {
            return await _produktManager.PobierzElementy();
        }

        // GET api/<ProduktController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Produkt>> Get(Guid id)
        {
            return await _produktManager.PobierzElement(id);
        }

        // POST api/<ProduktController>
        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody] Produkt produkt)
        {
            var res = await _produktManager.DodajProdukt(produkt);
           var res2= await _produktManager.Utwórz(produkt);
            return res && res2;
        }

        // PUT api/<ProduktController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(Guid id, [FromBody] Produkt produkt)
        {
            return await _produktManager.Edytuj(id, produkt);
        }

        // DELETE api/<ProduktController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            var produkt = await _produktManager.PobierzElement(id);
            if (produkt == null)
            {
                return NotFound();
            }
            return await _produktManager.Usuń(produkt);
        }
        [HttpGet("PobierzPowiazania/{id}")]
        public async Task<IEnumerable<Produkt>> PobierzPowiazaneProdkty (Guid id)
        {
            return await _produktManager.PobierzPowiazania(id);
        }

        [HttpPost("DodajPowiazanie")]
        public async Task<bool> DodajPowiazanie(Guid Id, Guid p)
        {
            return await _produktManager.DodajPowiazanie(Id, p);
        } 

        [HttpGet("Sortuj/{kolejnoscSortowania?}/Filtr/{filtr?}")]
        public async Task<IEnumerable<Produkt>> Sortuj(string kolejnoscSortowania = null, string filtr = null)
        {
           return await _produktManager.Sortuj(kolejnoscSortowania, filtr);
        }
        [HttpGet("Sortuj/{kolejnoscSortowania?}")]
        public async Task<IEnumerable<Produkt>> Sortuj(string kolejnoscSortowania = null)
        {
            return await _produktManager.Sortuj(kolejnoscSortowania,null);
        }
    }
}
