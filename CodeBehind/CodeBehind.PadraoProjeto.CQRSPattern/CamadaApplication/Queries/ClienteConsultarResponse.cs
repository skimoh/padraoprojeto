//***CODE BEHIND - BY RODOLFO.FONSECA***//
using CodeBehind.PadraoProjeto.CQRSPattern.Entity;
using MediatR;

namespace CodeBehind.PadraoProjeto.CQRSPattern.Queries
{
    public class ClienteConsultarResponse
    {
        public Cliente Cliente { get; set; }
    }
}
