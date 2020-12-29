//***CODE BEHIND - BY RODOLFO.FONSECA***//
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeBehind.MicroServico.Cliente.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private static readonly List<Cliente> clientes = new List<Cliente>
        {
            new Cliente(){ IdCliente = 1, Idade = 18,  Nome = "Fulano", SobreNome = "da Silva"},
            new Cliente(){ IdCliente = 2, Idade = 28,  Nome = "Ciclano", SobreNome = "de Souza"},
            new Cliente(){ IdCliente = 3, Idade = 38,  Nome = "Beltrano", SobreNome = "Oliveira"},
        };

        private readonly ILogger<ClienteController> _logger;

        public ClienteController(ILogger<ClienteController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Cliente Get(int id)
        {
            return clientes.FirstOrDefault(x => x.IdCliente == id);
        }
    }
}
