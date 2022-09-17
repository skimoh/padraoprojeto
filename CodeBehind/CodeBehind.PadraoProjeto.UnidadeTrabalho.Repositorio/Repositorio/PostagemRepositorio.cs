//***CODE BEHIND - BY RODOLFO.FONSECA***//
using CodeBehind.PadraoProjeto.UnidadeTrabalho.Repositorio.Entidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CodeBehind.PadraoProjeto.UnidadeTrabalho.Repositorio
{
    public interface IPostagemRepositorio : IRepositorioGenerico<Postagem>
    {
        Task<IEnumerable<Postagem>> Listar();
        Task<bool> Excluir(Guid id);
    }

    public class PostagemRepositorio : RepositorioGenerico<Postagem>, IPostagemRepositorio
    {
        private readonly DBContext _dBContext;
        private readonly ILogger _logger;

        public PostagemRepositorio(DBContext context, ILogger logger) : base(context, logger)
        {
            _dBContext = context;
            _logger = logger;

        }

        public override async Task<IEnumerable<Postagem>> Listar()
        {
            try
            {
                _logger.LogInformation("UOW-Listar");
                return await dbSet.Include(x => x.Usuario).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ERRO Listar: {ex.Message}", typeof(PostagemRepositorio));
                return new List<Postagem>();
            }
        }

        public override async Task<bool> Excluir(Guid id)
        {
            try
            {
                _logger.LogInformation("UOW-Excluir");
                var exist = await dbSet.Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();

                if (exist == null) 
                    return false;

                dbSet.Remove(exist);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ERRO Excluir: {ex.Message}", typeof(PostagemRepositorio));
                return false;
            }
        }
    }
}