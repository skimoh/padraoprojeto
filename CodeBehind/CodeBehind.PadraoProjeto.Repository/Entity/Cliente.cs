//***CODE BEHIND - BY RODOLFO.FONSECA***//
using System.ComponentModel.DataAnnotations;

namespace CodeBehind.PadraoProjeto.Repository.Entity
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }
    }
}
