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
            int retornoBanco = 0;

            var cliente = _mapper.Map<Cliente>(cmd);

            if (cmd.Id == 0)
            {
                throw new System.Exception("Id de cliente não encontrado");
            }

            if (cmd.Id > 9999)
            {
                retornoBanco = await _rep.InserirRetaguarda(cliente);
            }
            else
            {
                retornoBanco = await _rep.Inserir(cliente);
            }

            return retornoBanco;
        }

    }
}
