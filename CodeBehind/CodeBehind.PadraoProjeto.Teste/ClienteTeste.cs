using AutoMapper;
using CodeBehind.PadraoProjeto.Application;
using CodeBehind.PadraoProjeto.Repository;
using CodeBehind.PadraoProjeto.Repository.Entity;
using MediatR;
using Moq;

namespace CodeBehind.PadraoProjeto.Teste
{
    public class ClienteTeste
    {

        readonly Mock<IClienteRepository> _repMock;
        private static IMapper _mapper;

        public void IniciarMapper()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MyProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        public ClienteTeste()
        {
            IniciarMapper();
            _repMock = new Mock<IClienteRepository>();            
        }

        [Fact]
        public void Quando_Inserir_Cliente_Retorna_Sucesso()
        {
            var clienteCMD = new ClienteInserirCommand() { Id = 1, Nome = "fulano" };
            var cliente = _mapper.Map<Cliente>(clienteCMD);

            _repMock.Setup<Task<int>>(x => x.Inserir(cliente)).Returns(Task.FromResult(1));
            
            var handler = new ClienteInserirCommandHandler(_repMock.Object, _mapper);

            var retorno = handler.Handle(clienteCMD, new CancellationToken());

            Assert.True(retorno.Result == 1);

        }


        [Fact]
        public void Quando_Inserir_Cliente_Retorna_Falha_Persistir()
        {

            _repMock.Setup<Task<int>>(x => x.Inserir(new Repository.Entity.Cliente() { Id = 1, Nome = "fulano" })).Returns(Task.FromResult(0));

            var cliente = new ClienteInserirCommand() { Id = 1, Nome = "fulano" };
            var handler = new ClienteInserirCommandHandler(_repMock.Object, _mapper);

            var retorno = handler.Handle(cliente, new CancellationToken());

            Assert.True(retorno.Result == 0);

        }
    }
}