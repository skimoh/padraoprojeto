//***CODE BEHIND - BY RODOLFO.FONSECA***//
namespace CodeBehind.PadraoProjeto.SegurancaJWT.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public string? Email { get; set; }
    }
}
