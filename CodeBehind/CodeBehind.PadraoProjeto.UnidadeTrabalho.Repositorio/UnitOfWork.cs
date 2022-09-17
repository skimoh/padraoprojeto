//***CODE BEHIND - BY RODOLFO.FONSECA***//
using Microsoft.Extensions.Logging;

namespace CodeBehind.PadraoProjeto.UnidadeTrabalho.Repositorio
{
    public interface IUnitOfWork
    {
        IUsuarioRepositorio RUsuario { get; }
        IPostagemRepositorio RPostagem { get; }
        Task CompleteAsync();
        void Dispose();
    }

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DBContext _context;
        private readonly ILogger _logger;

        public IUsuarioRepositorio RUsuario { get; private set; }
        public IPostagemRepositorio RPostagem { get; private set; }

        public UnitOfWork(DBContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");

            RUsuario = new UsuarioRepositorio(context, _logger);
            RPostagem = new PostagemRepositorio(context, _logger);
        }

        public async Task CompleteAsync()
        {
            _logger.LogInformation("UOW-Confirmando Transacao");
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

