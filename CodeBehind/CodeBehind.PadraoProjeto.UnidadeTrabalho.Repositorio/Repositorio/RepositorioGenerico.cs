//***CODE BEHIND - BY RODOLFO.FONSECA***//
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace CodeBehind.PadraoProjeto.UnidadeTrabalho.Repositorio
{
    public interface IRepositorioGenerico<T> where T : class
    {
        Task<IEnumerable<T>> Listar();
        Task<T> SelecionarPorId(Guid id);
        Task<bool> Inserir(T entity);
        Task<bool> Excluir(Guid id);
        Task<bool> Atualizar(T entity);
        Task<IEnumerable<T>> Selecionar(Expression<Func<T, bool>> predicate);
    }

    public class RepositorioGenerico<T> : IRepositorioGenerico<T> where T : class
    {
        protected DBContext context;
        internal DbSet<T> dbSet;
        protected readonly ILogger _logger;

        public RepositorioGenerico(DBContext context, ILogger logger)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
            this._logger = logger;

        }

        public virtual Task<IEnumerable<T>> Listar()
        {
            throw new NotImplementedException();
        }

        //base ja implementa selecionar
        public virtual async Task<T> SelecionarPorId(Guid id)
        {
            return await dbSet.FindAsync(id);
        }

        //base ja implementa inserir
        public virtual async Task<bool> Inserir(T entity)
        {
            await dbSet.AddAsync(entity);
            return true;
        }

        public virtual async Task<bool> Excluir(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> Atualizar(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<IEnumerable<T>> Selecionar(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }
    }
}
