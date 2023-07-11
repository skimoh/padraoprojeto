//***CODE BEHIND - BY RODOLFO.FONSECA***//
using MediatR;
using System;

namespace CodeBehind.PadraoProjeto.CQRSPattern.Commands
{
    public class ClienteInserirCommandHandler : IRequestHandler<ClienteInserirCommand, ClienteInserirResponse>
    {
        public ClienteInserirCommandHandler()
        {
            //injetar repositorio
        }
        public async Task<ClienteInserirResponse> Handle(ClienteInserirCommand request, CancellationToken cancellationToken)
        {
            //persistir contexto de escrita

            return await Task.FromResult(new ClienteInserirResponse() { Sucesso = true });
        }
    }
}
