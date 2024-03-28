//***CODE BEHIND - BY RODOLFO.FONSECA***//

using CodeBehind.PadraoProjeto.MicroServicoProduto.Domain;
using Microsoft.EntityFrameworkCore;

namespace CodeBehind.PadraoProjeto.MicroServicoProduto.Infrastructure
{
    public interface IProdutoRepository
    {
        Task<int> CadastroAsync(Produto produto);
        Task<Produto> SelecionarPorIdAsync(int idProduto);
    }

    public class ProdutoRepository : IProdutoRepository
    {
        public readonly ContextBase _context;

        public ProdutoRepository(ContextBase context)
        {
            _context = context;
        }

        public async Task<int> CadastroAsync(Produto produto)
        {
            _context.Add(produto);
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<Produto> SelecionarPorIdAsync(int idProduto)
        {
            return await _context.Produtos.FirstOrDefaultAsync<Produto>(x => x.IdProduto == idProduto);
        }
    }
}
