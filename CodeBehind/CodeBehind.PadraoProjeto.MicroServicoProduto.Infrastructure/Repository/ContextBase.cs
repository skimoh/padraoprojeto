//***CODE BEHIND - BY RODOLFO.FONSECA***//

using CodeBehind.PadraoProjeto.MicroServicoProduto.Domain;
using Microsoft.EntityFrameworkCore;

namespace CodeBehind.PadraoProjeto.MicroServicoProduto.Infrastructure
{
    public class ContextBase : DbContext
    {
        public ContextBase(DbContextOptions<ContextBase> options) : base(options)
        {
        }

        public DbSet<Produto> Produtos { set; get; }
    }
}
