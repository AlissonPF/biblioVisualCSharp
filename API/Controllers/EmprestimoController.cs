using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;


namespace API.Controllers
{
    [ApiController]
    [Route("api/emprestimo")]
    public class EmprestimoController : ControllerBase
    {
        private readonly AppDataContext _ctx;

        public EmprestimoController(AppDataContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        [Route("listar")]
        public IActionResult Listar()
        {
            try
            {
                List<Emprestimo> emprestimos = _ctx.Emprestimos
                    .Include(x => x.Livro)
                    .Include(x => x.Cliente)
                    .ToList();
                return emprestimos.Count == 0 ? NotFound() : Ok(emprestimos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("cadastrar")]
        public IActionResult Cadastrar([FromBody] Emprestimo emprestimo)
        {
            try
            {
                Livro livro = _ctx.Livros.Find(emprestimo.LivroId);
                Cliente cliente = _ctx.Clientes.Find(emprestimo.ClienteId);

                if (livro == null || cliente == null)
                {
                    return NotFound("Livro ou cliente não encontrados.");
                }

                emprestimo.Livro = livro;
                emprestimo.Cliente = cliente;
                _ctx.Emprestimos.Add(emprestimo);
                _ctx.SaveChanges();
                return Created("", emprestimo);
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
                Emprestimo emprestimoCadastrado = _ctx.Emprestimos
                    .Include(x => x.Livro)
                    .Include(x => x.Cliente)
                    .FirstOrDefault(x => x.EmprestimoId == id);

                return emprestimoCadastrado != null ? Ok(emprestimoCadastrado) : NotFound();
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
                Emprestimo emprestimoCadastrado = _ctx.Emprestimos.Find(id);
                if (emprestimoCadastrado != null)
                {
                    _ctx.Emprestimos.Remove(emprestimoCadastrado);
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
        public IActionResult Alterar([FromRoute] int id, [FromBody] Emprestimo emprestimo)
        {
            try
            {
                Emprestimo emprestimoCadastrado = _ctx.Emprestimos.Find(id);
                if (emprestimoCadastrado != null)
                {
                    Livro livro = _ctx.Livros.Find(emprestimo.LivroId);
                    Cliente cliente = _ctx.Clientes.Find(emprestimo.ClienteId);

                    if (livro == null || cliente == null)
                    {
                        return NotFound("Livro ou cliente não encontrados.");
                    }

                    emprestimoCadastrado.Livro = livro;
                    emprestimoCadastrado.Cliente = cliente;
                    emprestimoCadastrado.DataEmprestimo = emprestimo.DataEmprestimo;
                    emprestimoCadastrado.DataDevolucao = emprestimo.DataDevolucao;

                    _ctx.Emprestimos.Update(emprestimoCadastrado);
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
