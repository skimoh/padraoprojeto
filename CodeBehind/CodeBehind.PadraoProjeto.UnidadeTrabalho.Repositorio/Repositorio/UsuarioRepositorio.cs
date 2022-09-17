//***CODE BEHIND - BY RODOLFO.FONSECA***//
using CodeBehind.PadraoProjeto.UnidadeTrabalho.Repositorio.Entidade;
using Microsoft.Extensions.Logging;

namespace CodeBehind.PadraoProjeto.UnidadeTrabalho.Repositorio
{
    public interface IUsuarioRepositorio : IRepositorioGenerico<Usuario>
    {
    }

    public class UsuarioRepositorio : RepositorioGenerico<Usuario>, IUsuarioRepositorio
    {
        private readonly DBContext _dBContext;
        private readonly ILogger _logger;

        public UsuarioRepositorio(DBContext context, ILogger logger) : base(context, logger)
        {
            _dBContext = context;
        }        
    }
}