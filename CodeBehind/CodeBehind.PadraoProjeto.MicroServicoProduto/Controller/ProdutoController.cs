//***CODE BEHIND - BY RODOLFO.FONSECA***//
using AutoMapper;
using CodeBehind.PadraoProjeto.MicroServicoProduto.Application;
using CodeBehind.PadraoProjeto.MicroServicoProduto.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CodeBehind.PadraoProjeto.MicroServicoProduto.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ILogger<ProdutoController> _logger;
        private readonly IMapper _mapper;
        private readonly IProdutoApp _app;

        public ProdutoController(ILogger<ProdutoController> logger,
            IMapper mapper, IProdutoApp app)
        {
            _logger = logger;
            _mapper = mapper;
            _app = app;
        }

        [HttpPost("/Cadastro")]
        public async Task<ActionResult> Post(ProdutoVM vm)
        {
            _logger.LogInformation($"Iniciando persistencia do produto");
            try
            {
                var entity = _mapper.Map<Produto>(vm);
                await _app.CadastroAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERRO: {ex.Message}");
                return BadRequest(ex.Message);
            }

            return Created();
        }

        [HttpGet("/SelecionarPorId")]
        public async Task<ActionResult> Get(int id)
        {
            _logger.LogInformation($"Consultando um produto {id}");

            try
            {
                var obj = await _app.SelecionarPorIdAsync(id);

                if (obj == null)
                {
                    return NotFound();

                }
                else return Ok(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERRO: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
