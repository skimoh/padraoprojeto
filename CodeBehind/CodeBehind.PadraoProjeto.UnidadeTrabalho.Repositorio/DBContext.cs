//***CODE BEHIND - BY RODOLFO.FONSECA***//
using CodeBehind.PadraoProjeto.UnidadeTrabalho.Repositorio.Entidade;
using Microsoft.EntityFrameworkCore;

namespace CodeBehind.PadraoProjeto.UnidadeTrabalho.Repositorio
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Postagem> Postagems { get; set; }
    }
}
