using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Modele;
using Interfejsy;

namespace SerwisTypów.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TypProduktuController : ControllerBase
    {
        private ITypProduktuManager _typProduktuManager;
        public TypProduktuController(ITypProduktuManager typProduktuManager)
        {
            _typProduktuManager = typProduktuManager;
        }
        [HttpGet]
        // GET: TypProduktuController
        public async Task<IEnumerable<TypProduktu>> Get()
        {
            return await _typProduktuManager.PobierzElementy();
        }
        [HttpGet("{id}")]
        // GET: TypProduktuController/Details/5
        public async Task<ActionResult<TypProduktu>> Get(Guid id)
        {
            return await _typProduktuManager.PobierzElement(id);
        }

     
        // POST: TypProduktuController/Create
        [HttpPost]
        public async Task<ActionResult<bool>>  Post([FromBody]  TypProduktu typProduktu)
        {
            return await _typProduktuManager.Utwórz(typProduktu);
        }


        // POST: TypProduktuController/Edit/5
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>>  Put(Guid id, [FromBody] TypProduktu typProduktu)
        {
            return await _typProduktuManager.Edytuj(id,typProduktu);
        }

        // POST: TypProduktuController/Delete/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            var typProduktu= await _typProduktuManager.PobierzElement(id);
            if (typProduktu == null)
            {
                return NotFound();
            }

            return await _typProduktuManager.Usuń(typProduktu);
        }
    }
}
