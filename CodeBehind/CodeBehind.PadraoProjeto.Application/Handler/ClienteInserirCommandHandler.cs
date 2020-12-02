using AutoMapper;
using mediator.dto;
using mediator.repository;
using mediator.repository.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.application
{
    public class ClienteInserirCommandHandler : IRequestHandler<ClienteInserirCommand, int>
    {
        private readonly IClienteRepository _rep;
        IMapper _mapper;
        public ClienteInserirCommandHandler(IClienteRepository rep, IMapper mapper)
        {
            _mapper = mapper;
            _rep = rep;
        }

        public async Task<int> Handle(ClienteInserirCommand cmd, CancellationToken cancellationToken)
        {
            var cliente = new Cliente { Id = cmd.Id, Nome = cmd.Nome };

            return await _rep.Inserir(cliente);
        }

    }
}
