using mediator.dto;
using MediatR;
using System.Collections.Generic;

namespace mediator.application
{
    public class ClienteConsultarCommand : IRequest<IEnumerable<ClienteDto>>
    {
        public string Nome { get; set; }

    }
}
