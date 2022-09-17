//***CODE BEHIND - BY RODOLFO.FONSECA***//
using CodeBehind.PadraoProjeto.UnidadeTrabalho.Repositorio;
using CodeBehind.PadraoProjeto.UnidadeTrabalho.Repositorio.Entidade;
using Microsoft.AspNetCore.Mvc;

namespace CodeBehind.PadraoProjeto.UnidadeTrabalho.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublicadorController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public PublicadorController(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }

        [HttpGet("postagens")]
        public async Task<IActionResult> ListarPostagens()
        {
            var users = await _uow.RPostagem.Listar();
            return Ok(users);
        }

        [HttpGet("postagem/{id}")]
        public async Task<IActionResult> SelecionarPostagem(Guid id)
        {
            var item = await _uow.RPostagem.SelecionarPorId(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpGet("usuario/{id}")]
        public async Task<IActionResult> SelecionarUsuario(Guid id)
        {
            var item = await _uow.RUsuario.SelecionarPorId(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost("postar")]
        public async Task<IActionResult> Postar([FromBody] string conteudo, Guid idUsuario)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _uow.RUsuario.SelecionarPorId(idUsuario);
                var post = new Postagem() { Id = Guid.NewGuid(), Conteudo = conteudo, IdUsuario = idUsuario, DataPostagem = DateTime.Now, Usuario = usuario };
                await _uow.RPostagem.Inserir(post);
                await _uow.CompleteAsync();

                return CreatedAtAction("Postar", new { post.Id }, post);
            }

            return new JsonResult("Erro") { StatusCode = 500 };
        }

        [HttpPost("usuario")]
        public async Task<IActionResult> NovoUsuario([FromBody] string nome)
        {
            if (ModelState.IsValid)
            {
                var usr = new Usuario() { Id = Guid.NewGuid(), Nome = nome };
                await _uow.RUsuario.Inserir(usr);
                await _uow.CompleteAsync();

                return CreatedAtAction("Postar", new { usr.Id }, usr);
            }

            return new JsonResult("Erro") { StatusCode = 500 };
        }

        [HttpDelete("postagem/excluir/{id}")]
        public async Task<IActionResult> ExcluirPostagem(Guid id)
        {
            var item = await _uow.RPostagem.SelecionarPorId(id);

            if (item == null)
                return BadRequest();

            await _uow.RPostagem.Excluir(id);
            await _uow.CompleteAsync();

            return Ok(item);
        }
    }
}