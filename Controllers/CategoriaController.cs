using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Controller
{

    //Endpoint = URL
    //hppts://localhost5001/categorias

    [Route("categorias")]
    public class CategoriaController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Categoria>>> Get(
            [FromServices] DataContext context
        )
        {
            var categorias = await context.Categorias.AsNoTracking().ToListAsync();
            return Ok(categorias);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Categoria>> GetById(
            int id,
            [FromServices] DataContext context)
        {
            var categoria = await context.Categorias.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
            return Ok(categoria);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<List<Categoria>>> Post(
            [FromBody] Categoria model,
            [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Categorias.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    message = "Não foi possível gravar a categoria",
                    error = e.Message
                });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<List<Categoria>>> Put(
            [FromBody] Categoria model,
            [FromServices] DataContext context,
            int id)
        {
            //valida se o formato do model está correto com o objeto Categoria  
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //verifica se o id na rota é o mesmo do id do modelo
            if (model.Id != id)
                return NotFound(new { mensagem = "categoria não encontrada" });

            try
            {
                context.Entry<Categoria>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Este registro está sendo utilizado" });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível atualizar a categoria" });
            }

            return Ok(model);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<List<Categoria>>> Delete(
            int id,
            [FromServices] DataContext context
        )
        {
            var categoria = await context.Categorias.FirstOrDefaultAsync(x => x.Id == id);
            if (categoria == null)
                return BadRequest(new { message = "Categoria não encontrada" });

            try
            {
                context.Categorias.Remove(categoria);
                await context.SaveChangesAsync();
                return Ok(new { message = "Categoria deletada com sucesso" });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Erro ao deletar categoria", error = e.Message });
            }
        }
    }
}