//***CODE BEHIND - BY RODOLFO.FONSECA***//
using CodeBehind.BFF.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Refit;
using System.Threading.Tasks;

namespace CodeBehind.BFF.Controllers
{
    /// <summary>
    /// BFF - Back-end for Front-end
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AppController : ControllerBase
    {
        private readonly ILogger<AppController> _logger;
        private readonly IClienteRefit _clientCliente;
        private readonly IPedidoRefit _clientPedido;
        private readonly IRegiaoRefit _clientRegiao;

        /// <summary>
        /// Controller
        /// </summary>
        /// <param name="logger"></param>
        public AppController(ILogger<AppController> logger)
        {
            _logger = logger;
            _clientCliente = RestService.For<IClienteRefit>("http://localhost:49584/");
            _clientPedido = RestService.For<IPedidoRefit>("http://localhost:49593/");
            _clientRegiao = RestService.For<IRegiaoRefit>("https://viacep.com.br/");
        }

        /// <summary>
        /// Selecionar um cliente pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetCustomer")]
        public Task<Cliente> GetCustomer(int id)
        {
            return _clientCliente.SelecionarAsync(id);
        }

        /// <summary>
        /// Selecionar um pedido pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetOrder")]
        public Task<Pedido> GetOrder(int id)
        {
            var pedido = _clientPedido.SelecionarAsync(id).GetAwaiter().GetResult();

            var regiao = _clientRegiao.SelecionarPorCepAsync(pedido.Cep).GetAwaiter().GetResult();

            pedido.Cidade = regiao.Localidade;

            return Task.FromResult(pedido);
        }
    }
}
