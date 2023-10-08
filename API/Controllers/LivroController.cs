using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;


namespace API.Controllers
{
    [ApiController]
    [Route("api/livro")]
    public class LivroController : ControllerBase
    {
        private readonly AppDataContext _ctx;

        public LivroController(AppDataContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        [Route("listar")]
        public IActionResult Listar()
        {
            try
            {
                List<Livro> livros = _ctx.Livros.ToList();
                return livros.Count == 0 ? NotFound() : Ok(livros);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("cadastrar")]
        public IActionResult Cadastrar([FromBody] Livro livro)
        {
            try
            {
                _ctx.Livros.Add(livro);
                _ctx.SaveChanges();
                return Created("", livro);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("buscar/{titulo}")]
        public IActionResult Buscar([FromRoute] string titulo)
        {
            try
            {
                Livro livroCadastrado = _ctx.Livros.FirstOrDefault(x => x.Titulo == titulo);
                return livroCadastrado != null ? Ok(livroCadastrado) : NotFound();
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
                Livro livroCadastrado = _ctx.Livros.Find(id);
                if (livroCadastrado != null)
                {
                    _ctx.Livros.Remove(livroCadastrado);
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
        public IActionResult Alterar([FromRoute] int id, [FromBody] Livro livro)
        {
            try
            {
                Livro livroCadastrado = _ctx.Livros.FirstOrDefault(x => x.LivroId == id);
                if (livroCadastrado != null)
                {
                    livroCadastrado.Titulo = livro.Titulo;
                    livroCadastrado.Autor = livro.Autor;
                    livroCadastrado.Editora = livro.Editora;
                    livroCadastrado.DataPublicacao = livro.DataPublicacao;

                    _ctx.Livros.Update(livroCadastrado);
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
