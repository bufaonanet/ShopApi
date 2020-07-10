using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Controller
{
    [Route("produtos")]
    public class ProdutoController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Produto>>> Get(
            [FromServices] DataContext context)
        {
            var produtos = await context
            .Produtos
            .Include(x => x.Categoria)
            .AsNoTracking()
            .ToListAsync();
            return Ok(produtos);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Produto>> Get(
            [FromServices] DataContext context, int id)
        {
            var produto = await context
            .Produtos
            .Include(x => x.Categoria)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.CategoriaId == id);
            return Ok(produto);
        }

        [HttpGet]//produtos/categorias/1
        [Route("categorias/{id:int}")]
        public async Task<ActionResult<Produto>> GetByCategoria(
            [FromServices] DataContext context, int id)
        {
            var produto = await context
            .Produtos
            .Include(x => x.Categoria)
            .AsNoTracking()
            .Where(x => x.CategoriaId == id)
            .ToListAsync();
            return Ok(produto);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<List<Produto>>> Post(
            [FromBody] Produto model, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                context.Produtos.Add(model);
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



    }
}