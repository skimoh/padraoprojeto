//***CODE BEHIND - BY RODOLFO.FONSECA***//
using CodeBehind.PadraoProjeto.SegurancaJWT.Models;

namespace CodeBehind.PadraoProjeto.SegurancaJWT.Repository
{
    public interface IUsuarioRepository
    {
        Usuario Get(string usuario, string senha);
    }

    public class UsuarioRepository : IUsuarioRepository
    {
        public UsuarioRepository()
        {
        }

        public Usuario Get(string usuario, string senha)
        {
            var users = new List<Usuario>
            {
                new Usuario { Id = 1, Username = "admin", Password = "1234", Role = "gerente", Email="admin@oi.com" },
                new Usuario { Id = 2, Username = "convidado", Password = "1234", Role = "visitante", Email = "visitante@oi.com" }
            };

            return users.FirstOrDefault(x => x.Username.ToLower() == usuario.ToLower() && x.Password == senha);
        }
    }
}
