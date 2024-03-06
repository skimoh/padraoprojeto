//***CODE BEHIND - BY RODOLFO.FONSECA***//
using CodeBehind.PadraoProjeto.GRPC;
using Grpc.Core;

namespace CodeBehind.PadraoProjeto.GRPC.Services
{
    public class ControllerService : Contrato.ContratoBase
    {
        private readonly ILogger<ControllerService> _logger;
        public ControllerService(ILogger<ControllerService> logger)
        {
            _logger = logger;
        }

        public override Task<ClienteResponse> SayHello(ClienteRequest request, ServerCallContext context)
        {
            _logger.LogInformation("[ControllerService] Entrou aqui");

            var resp = new ClienteResponse
            {
                Message = "Olá " + request.Name
            };

            return Task.FromResult(resp);
        }
    }
}