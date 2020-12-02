//***CODE BEHIND - BY RODOLFO.FONSECA***//
using CodeBehind.PadraoProjeto.Dto;
using MediatR;
using System.Collections.Generic;

namespace CodeBehind.PadraoProjeto.Application
{
    public class ClienteConsultarCommand : IRequest<IEnumerable<ClienteDto>>
    {
        public string Nome { get; set; }

    }
}
