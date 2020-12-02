//***CODE BEHIND - BY RODOLFO.FONSECA***//
using System.Threading.Tasks;
using CodeBehind.PadraoProjeto.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CodeBehind.PadraoProjeto.Api.Mediator.Controller
{
    /// <summary>
    /// API DE CLIENTES
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private IMediator _mediator;        

        /// <summary>
        /// Construtor da classe cliente
        /// </summary>
        /// <param name="mediator"></param>
        public ClienteController(IMediator mediator)
        {
            _mediator = mediator;            
        }       

        /// <summary>
        /// CONSULTAR CLIENTE PELO NOME
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ClienteConsultarCommand filter)
        {
            try
            {
                return Ok(await _mediator.Send(filter));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// PERSISTIR CLIENTE
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(ClienteInserirCommand command)
        {
            try
            {
                return Ok(await _mediator.Send(command));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
