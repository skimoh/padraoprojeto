//***CODE BEHIND - BY RODOLFO.FONSECA***//
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CodeBehind.PadraoProjeto.SegurancaJWT.Controller
{
    [ApiController]    
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        public readonly ILogger<ClienteController> _logger;

        public ClienteController(ILogger<ClienteController> logger)
        {

            _logger = logger;
        }

        /// <summary>
        /// Consultar cliente
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("=>ACESSO OK");

            var nome = User.FindFirstValue(ClaimTypes.Name);
            var email = User.FindFirstValue(ClaimTypes.Email);

            return Ok(new { nome, email });
        }


       /// <summary>
       /// Consultar cliente pela role
       /// </summary>
       /// <returns></returns>
        [HttpGet]
        [Route("Visitante")]
        [Authorize(Roles = "visitante")]
        public async Task<IActionResult> GetVisitante()
        {
            _logger.LogInformation("=>ACESSO VISITANTE OK");

            var nome = User.FindFirstValue(ClaimTypes.Name);
            var email = User.FindFirstValue(ClaimTypes.Email);

            return Ok(new { nome, email });
        }
    }
}
