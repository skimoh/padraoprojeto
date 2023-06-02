//***CODE BEHIND - BY RODOLFO.FONSECA***//

using CodeBehind.PadraoProjeto.MaquinaSaga.Event;
using CodeBehind.PadraoProjeto.MaquinaSaga.Models;
using MassTransit;
using MassTransit.Transports;
using Microsoft.AspNetCore.Mvc;

namespace CodeBehind.PadraoProjeto.MaquinaSaga.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        public readonly ILogger<PedidoController> _logger;

        readonly IRequestClient<IPedidoEnviado> _client;

        public PedidoController(IRequestClient<IPedidoEnviado> client, ILogger<PedidoController> logger)
        {
            _client = client;
            _logger = logger;
        }

        /// <summary>
        /// Enviar Pedido
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(PedidoRequest req)
        {
            _logger.LogInformation("=>Iniciando a Saga");
            try
            {

                var obj = new
                {
                    CurrelationId = Guid.NewGuid(),
                    Timestamp = DateTime.Now,
                    Info = req.Info
                };

                var resp = _client.Create(obj);

                Response<PedidoRetorno> ret = await resp.GetResponse<PedidoRetorno>();

                if (ret.Message.Status < 1)
                    return BadRequest(ret.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                _logger.LogInformation("=>Finalizando a Saga");
            }

            return Ok();
        }
    }
}
