//***CODE BEHIND - BY RODOLFO.FONSECA***//
using CodeBehind.PadraoProjeto.CQRSPattern.Commands;
using CodeBehind.PadraoProjeto.CQRSPattern.Entity;
using CodeBehind.PadraoProjeto.CQRSPattern.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CodeBehind.PadraoProjeto.CQRSPattern.Controller
{

    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        public readonly ILogger<ClienteController> _logger;
        private readonly IMediator _mediator;
        public ClienteController(ILogger<ClienteController> logger, IMediator mediator)
        {            
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {            
            var command = new ClienteConsultarQuery();
            await _mediator.Send(command);

            return Ok("Get OK");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClienteModel model)
        {            
            var command = new ClienteInserirCommand() { Id = model.Id, Nome = model.Nome };
            await _mediator.Send(command);

            return Ok("Post OK");
        }
    }
}
