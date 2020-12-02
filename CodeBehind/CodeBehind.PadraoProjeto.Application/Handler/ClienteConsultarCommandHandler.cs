using AutoMapper;
using mediator.dto;
using mediator.repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.application
{

    public class ClienteConsultarCommandHandler : IRequestHandler<ClienteConsultarCommand, IEnumerable<ClienteDto>>//, IRequestHandler<ClienteInserirCommand, int>
    {

        private readonly IClienteRepository _rep;
        IMapper _mapper;
        public ClienteConsultarCommandHandler(IClienteRepository rep, IMapper mapper)
        {
            _mapper = mapper;

            _rep = rep;
        }

        public async Task<IEnumerable<ClienteDto>> Handle(ClienteConsultarCommand query, CancellationToken cancellationToken)
        {
            var clientes = await _rep.ListarPor(query.Nome);

            return await Task.FromResult(_mapper.Map<List<ClienteDto>>(clientes));
        }
    }

}
