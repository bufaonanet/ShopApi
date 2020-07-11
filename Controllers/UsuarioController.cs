using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;
using Shop.Services;

namespace Shop.Controllers
{
    [Route("usuarios")]
    public class UsuarioController : Controller
    {
        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<Usuario>> Post(
            [FromServices] DataContext context,
            [FromBody] Usuario model
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                context.Usuarios.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    message = "Não foi possível criar o usuário",
                    error = e.Message
                });
            }
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Autenticar(
            [FromServices] DataContext context,
            [FromBody] Usuario model
        )
        {
            var usuario = await context
                .Usuarios
                .AsNoTracking()
                .Where(x => x.Senha == model.Senha && x.Login == model.Login)
                .FirstOrDefaultAsync();

            if (usuario == null)
                return NotFound(new { message = "Usuário não encontrado" });

            var token = TokenService.GerarToken(usuario);

            return new
            {
                user = usuario,
                token = token
            };
        }

        [HttpGet]
        [Route("anonimo")]
        [AllowAnonymous]
        public string Anonimo() => "Anonimo";

        [HttpGet]
        [Route("autenticado")]
        [Authorize]
        public string Autenticado() => "Autenticado";
        
        [HttpGet]
        [Route("user")]
        [Authorize(Roles = "user")]
        public string AcessoUsuario() => "Usuario";

        [HttpGet]
        [Route("admin")]
        [Authorize(Roles = "admin")]
        public string AcessoAdmin() => "Administrador";

    }
}