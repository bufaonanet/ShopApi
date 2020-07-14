using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;

namespace ShopApi.Controllers
{
    [Route("v1")]
    public class HomeController : Controller
    {
        [HttpGet]       
        [Route("")]           
        public async Task<ActionResult<dynamic>> Get(
            [FromServices] DataContext context)
        {
            var admin = new Usuario { Login = "admin", Senha = "admin" };
            var usuario = new Usuario { Login = "douglas", Senha = "douglas" };
            var categoria = new Categoria { Titulo = "Informática" };
            var produto = new Produto
            {                
                Titulo = "Mouse",
                Preco = 150.99M,
                Descricao = "mouse gamer",
                Categoria = categoria
            };

            context.Usuarios.Add(admin);
            context.Usuarios.Add(usuario);
            context.Categorias.Add(categoria);
            context.Produtos.Add(produto);

            await context.SaveChangesAsync();
            return Ok(new { message = "Dados configurados" });

        }

    }
}