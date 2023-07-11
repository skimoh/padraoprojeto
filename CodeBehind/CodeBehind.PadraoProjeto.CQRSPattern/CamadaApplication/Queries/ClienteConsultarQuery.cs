//***CODE BEHIND - BY RODOLFO.FONSECA***//
using MediatR;

namespace CodeBehind.PadraoProjeto.CQRSPattern.Queries
{
    public class ClienteConsultarQuery : IRequest<ClienteConsultarResponse>
    {
        public int Id { get; set; }
    }
}
