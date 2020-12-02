//***CODE BEHIND - BY RODOLFO.FONSECA***//
using MediatR;

namespace CodeBehind.PadraoProjeto.Application
{
    public class ClienteInserirCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Nome { get; set; }
      
    }
}
