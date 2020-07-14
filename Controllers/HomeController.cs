using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;

namespace ShopApi.Controllers
{
    [Route("v1")]
    public class HomeController : Controller
    {
        public async Task<ActionResult<dynamic>> Get(
            [FromBody] DataContext context)
        {
            var admin = new Usuario { Id = 1, Login = "admin", Senha = "admin" };
            var usuario = new Usuario { Id = 2, Login = "douglas", Senha = "douglas" };
            var categoria = new Categoria { Id = 1, Titulo = "Informática" };
            var produto = new Produto
            {
                Id = 1,
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