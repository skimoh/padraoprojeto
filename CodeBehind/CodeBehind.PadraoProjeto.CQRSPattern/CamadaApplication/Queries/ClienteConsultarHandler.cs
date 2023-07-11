//***CODE BEHIND - BY RODOLFO.FONSECA***//
using CodeBehind.PadraoProjeto.CQRSPattern.Entity;
using MediatR;
using System;

namespace CodeBehind.PadraoProjeto.CQRSPattern.Queries
{
    public class GetProdutoPorIdQueryHandler : IRequestHandler<ClienteConsultarQuery, ClienteConsultarResponse>
    {
        public GetProdutoPorIdQueryHandler()
        {
        }
        public async Task<ClienteConsultarResponse> Handle(ClienteConsultarQuery request, CancellationToken cancellationToken)
        {
            //consultar no contexto de leitura
            return await Task.FromResult(new ClienteConsultarResponse() { Cliente = new Cliente() { Id = 1, Nome = "Joao" } });
        }
    }
}
