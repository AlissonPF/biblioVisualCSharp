using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("api/cliente")]
    public class ClienteController : ControllerBase
    {
        private readonly AppDataContext _ctx;

        public ClienteController(AppDataContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        [Route("listar")]
        public IActionResult Listar()
        {
            try
            {
                List<Cliente> clientes = _ctx.Clientes.ToList();
                return clientes.Count == 0 ? NotFound() : Ok(clientes);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("cadastrar")]
        public IActionResult Cadastrar([FromBody] Cliente cliente)
        {
            try
            {
                _ctx.Clientes.Add(cliente);
                _ctx.SaveChanges();
                return Created("", cliente);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("buscar/{id}")]
        public IActionResult Buscar([FromRoute] int id)
        {
            try
            {
                Cliente clienteCadastrado = _ctx.Clientes.Find(id);
                return clienteCadastrado != null ? Ok(clienteCadastrado) : NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("deletar/{id}")]
        public IActionResult Deletar([FromRoute] int id)
        {
            try
            {
                Cliente clienteCadastrado = _ctx.Clientes.Find(id);
                if (clienteCadastrado != null)
                {
                    _ctx.Clientes.Remove(clienteCadastrado);
                    _ctx.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("alterar/{id}")]
        public IActionResult Alterar([FromRoute] int id, [FromBody] Cliente cliente)
        {
            try
            {
                Cliente clienteCadastrado = _ctx.Clientes.Find(id);
                if (clienteCadastrado != null)
                {
                    clienteCadastrado.Nome = cliente.Nome;
                    clienteCadastrado.DataNascimento = cliente.DataNascimento;
                    clienteCadastrado.Email = cliente.Email;

                    _ctx.Clientes.Update(clienteCadastrado);
                    _ctx.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
