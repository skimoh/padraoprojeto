//***CODE BEHIND - BY RODOLFO.FONSECA***//
using MediatR;

namespace CodeBehind.PadraoProjeto.CQRSPattern.Commands
{
    public class ClienteInserirCommand : IRequest<ClienteInserirResponse>
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
