//***CODE BEHIND - BY RODOLFO.FONSECA***//

using CodeBehind.PadraoProjeto.MicroServicoProduto.Domain;
using System.Text.Json;
using CodeBehind.PadraoProjeto.MicroServicoProduto.Infrastructure;
using CodeBehind.PadraoProjeto.MicroServicoProduto.Infrastructure.Service;

namespace CodeBehind.PadraoProjeto.MicroServicoProduto.Application
{
    public interface IProdutoApp
    {
        Task<int> CadastroAsync(Produto produto);
        Task<Produto> SelecionarPorIdAsync(int idProduto);
    }

    public class ProdutoApp : IProdutoApp
    {
        private readonly IMensageriaService _producerService;
        public readonly IProdutoRepository _repository;

        public ProdutoApp(IMensageriaService producerService, IProdutoRepository repository)
        {
            _producerService = producerService;
            _repository = repository;
        }

        public async Task<int> CadastroAsync(Produto produto)
        {

            produto.ValidarProduto();

            //inserindo na base atraves da infraestrutura
            await _repository.CadastroAsync(produto);

            //enviando para mensageria
            var message = JsonSerializer.Serialize(produto);
            await _producerService.ProduceAsync("Estoque", message);

            return 1;
        }

        public async Task<Produto> SelecionarPorIdAsync(int idProduto)
        {
            if (idProduto < 1)
                throw new Exception("Id inválido");

            //consultando atraves da infraestrutura
            return await _repository.SelecionarPorIdAsync(idProduto);
        }
    }
}
