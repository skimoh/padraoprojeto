//***CODE BEHIND - BY RODOLFO.FONSECA***//
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeBehind.MicroServico.Pedido.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PedidoController : ControllerBase
    {
        private static readonly List<Pedido> Pedidos = new List<Pedido>
        {
            new Pedido(){ IdPedido = 1, IdCliente = 1, DataCompra = DateTime.Now, Desconto = 0, Frete = 0, SubValor = 5, Valor= 5, Cep = "37130031" },
            new Pedido(){ IdPedido = 2, IdCliente = 1, DataCompra = DateTime.Now, Desconto = 1, Frete = 0, SubValor = 20, Valor= 19, Cep="37130310" },
            new Pedido(){ IdPedido = 3, IdCliente = 2, DataCompra = DateTime.Now, Desconto = 0, Frete = 10, SubValor = 10, Valor= 20, Cep = "37135164" }
        };

        private readonly ILogger<PedidoController> _logger;

        public PedidoController(ILogger<PedidoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Pedido Get(int id)
        {
            return Pedidos.FirstOrDefault(x => x.IdPedido == id);
        }
    }
}
