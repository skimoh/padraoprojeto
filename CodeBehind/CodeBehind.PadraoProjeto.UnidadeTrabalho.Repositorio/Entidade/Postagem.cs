//***CODE BEHIND - BY RODOLFO.FONSECA***//
namespace CodeBehind.PadraoProjeto.UnidadeTrabalho.Repositorio.Entidade
{
    public class Postagem
    {
        public Guid Id { get; set; }
        public Guid IdUsuario { get; set; }
        public DateTime DataPostagem { get; set; }
        public string Conteudo { get; set; }
        public Usuario Usuario { get; set; }
    }
}
