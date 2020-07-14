using System;
using System.Collections.Generic;
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
    [Route("v1/usuarios")]
    public class UsuarioController : Controller
    {
        [HttpGet]
        [Route("")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<List<Usuario>>> Get([FromServices] DataContext context)
        {
            var usuarios = await context
            .Usuarios
            .AsNoTracking()
            .ToListAsync();
            return usuarios;
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Usuario>> Get(
            [FromServices] DataContext context, int id)
        {
            var usuario = await context
            .Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
            return usuario;
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<Usuario>> Post(
            [FromServices] DataContext context,
            [FromBody] Usuario model
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                //só faz inserção de usuários de nível user
                model.NivelAcesso = "user";

                context.Usuarios.Add(model);
                await context.SaveChangesAsync();

                //não retornar senha no model
                model.Senha = "";
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível criar o usuário", });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<Usuario>> Put(
            [FromServices] DataContext context,
            [FromBody] Usuario model,
            int id)
        {
            //Verifica se os dados são válidos
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //verifica se o id na rota é o mesmo do id do modelo
            if (model.Id != id)
                return NotFound(new { mensagem = "usuário não encontrado" });

            try
            {
                context.Entry<Usuario>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return model;
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível atualizar o ususario" });
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

            //não retornar senha no model
            usuario.Senha = "";

            return new
            {
                user = usuario,
                token = token
            };
        }
    }
}