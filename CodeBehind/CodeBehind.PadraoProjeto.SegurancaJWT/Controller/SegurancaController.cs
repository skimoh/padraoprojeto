//***CODE BEHIND - BY RODOLFO.FONSECA***//
using CodeBehind.PadraoProjeto.SegurancaJWT.Models;
using CodeBehind.PadraoProjeto.SegurancaJWT.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeBehind.PadraoProjeto.SegurancaJWT.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class SegurancaController : ControllerBase
    {
        public readonly ILogger<SegurancaController> _logger;
        private readonly IUsuarioRepository _rep;
        public SegurancaController(ILogger<SegurancaController> logger, IUsuarioRepository rep)
        {
            _rep = rep;
            _logger = logger;
        }

        /// <summary>
        /// Logar
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]        
        public async Task<IActionResult> Post(LoginRequest login)
        {
            var usuario = _rep.Get(login.Usuario, login.Senha);

            if (usuario is not null)
            {
                _logger.LogInformation($"Sucesso na autenticação do usuário");
                
                return Ok(TokenService.GenerateToken(usuario));
            }
            else
            {
                _logger.LogError($"Falha na autenticação do usuário");

                return BadRequest("Falha na autenticação do usuário");
            }
        }
    }
}
