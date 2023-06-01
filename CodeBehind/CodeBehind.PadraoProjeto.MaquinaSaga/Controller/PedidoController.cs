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

        readonly IRequestClient<IPedidoEnviado> _client;

        public PedidoController(IRequestClient<IPedidoEnviado> client)
        {
            _client = client;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var obj = new
            {
                CurrelationId = Guid.NewGuid(),
                Timestamp = DateTime.Now
            };

            var req = _client.Create(obj);

            Response<PedidoRetorno> res = await req.GetResponse<PedidoRetorno>();

            if (res.Message.Status < 1)
                return BadRequest(res.Message);

            return Ok();
        }
    }
}
