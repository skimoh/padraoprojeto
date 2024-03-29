using CodeBehind.PadraoProjeto.MicroServicoProduto.Application;
using CodeBehind.PadraoProjeto.MicroServicoProduto.Infrastructure;
using CodeBehind.PadraoProjeto.MicroServicoProduto.Infrastructure.Service;
using Microsoft.Extensions.Configuration;
using Moq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using Newtonsoft.Json;
using Confluent.Kafka;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace CodeBehind.PadraoProjeto.MicroServicoProduto.Test
{
    public class ProdutoTest
    {
        private Mock<IProdutoApp> _mockApp;
        private Mock<IProdutoRepository> _mockRepository;
        private Mock<IMensageriaService> _mockMensageria;
        private Mock<IConfiguration> _mockConfig;


        [SetUp]
        public void Setup()
        {
            _mockApp = new Mock<IProdutoApp>();
            _mockRepository = new Mock<IProdutoRepository>();
            _mockConfig = new Mock<IConfiguration>();
            _mockMensageria = new Mock<IMensageriaService>();
        }

        [Test]
        public void Quando_Informado_Quantidade_Invalida_Retornar_Erro()
        {
            //Arrange
            _mockApp.Setup<Task<int>>(x => x.CadastroAsync(ProdutoMock._produtoQuantidadeInvalida)).Returns(Task.FromResult(1));

            //Act
            ProdutoApp app = new ProdutoApp(_mockMensageria.Object, _mockRepository.Object);
            var myException = Assert.Throws<Exception>(() => app.CadastroAsync(ProdutoMock._produtoQuantidadeInvalida).GetAwaiter().GetResult());

            //Assert
            Assert.That(myException.Message, Is.EqualTo("Quantidade inválida"));
        }

        [Test]
        public void Quando_Informado_Nome_Invalido_Retornar_Erro()
        {
            //Arrange
            _mockApp.Setup<Task<int>>(x => x.CadastroAsync(ProdutoMock._produtoNomeAusente)).Returns(Task.FromResult(1));

            //Act
            ProdutoApp app = new ProdutoApp(_mockMensageria.Object, _mockRepository.Object);
            var myException = Assert.Throws<Exception>(() => app.CadastroAsync(ProdutoMock._produtoNomeAusente).GetAwaiter().GetResult());

            //Assert
            Assert.That(myException.Message, Is.EqualTo("Nome inválido"));
        }



        [Test]
        public void Quando_Informado_Produto_Corretamente_Cadastra_Sucesso()
        {
            //Arrange
            _mockApp.Setup<Task<int>>(x => x.CadastroAsync(ProdutoMock._produtoSucesso)).Returns(Task.FromResult(1));
            _mockRepository.Setup<Task<int>>(x => x.CadastroAsync(ProdutoMock._produtoSucesso)).Returns(Task.FromResult(1));

            var objJson = JsonConvert.SerializeObject(ProdutoMock._produtoSucesso);
            _mockMensageria.Setup<Task<bool>>(x => x.ProduceAsync("Estoque", objJson)).Returns(Task.FromResult(true));

            //Act
            ProdutoApp app = new ProdutoApp(_mockMensageria.Object, _mockRepository.Object);
            var result = app.CadastroAsync(ProdutoMock._produtoSucesso).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(result == 1);

        }
    }
}