//***CODE BEHIND - BY RODOLFO.FONSECA***//
using AutoMapper;
using CodeBehind.PadraoProjeto.Repository;
using CodeBehind.PadraoProjeto.Repository.Entity;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CodeBehind.PadraoProjeto.Application
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
