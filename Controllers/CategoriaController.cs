using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Models;

namespace Shop.Controller
{

    //Endpoint = URL
    //hppts://localhost5001/categorias

    [Route("Categorias")]
    public class CategoriaController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Categoria>>> Get()
        {
            return new List<Categoria>();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Categoria>> GetById(int id)
        {
            return new Categoria();
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<List<Categoria>>> Post([FromBody] Categoria model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(model);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<List<Categoria>>> Put([FromBody] Categoria model, int id)
        {
            //valida se o formato do model está correto com o objeto Categoria  
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //verifica se o id na rota é o mesmo do id do modelo
            if (model.Id != id)
                return NotFound(new { mensagem = "categoria não encontrada" });

            return Ok(model);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<List<Categoria>>> Delete()
        {
            return Ok();
        }
    }
}