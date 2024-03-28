//***CODE BEHIND - BY RODOLFO.FONSECA***//

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeBehind.PadraoProjeto.MicroServicoProduto.Domain
{
    //dominio / entidade
    [Table("Produtos")]
    public class Produto
    {
        [Key]
        public int IdProduto { get; private set; }
        public string? Nome { get; private set; }
        public decimal Preco { get; private set; }
        public int Quantidade { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public DateTime? DataAlteracao { get; private set; }

        public Produto(string nome, decimal preco, int quantidade)
        {
            Nome = nome;
            Preco = preco;
            Quantidade = quantidade;
            DataCadastro = DateTime.Now;
            DataAlteracao = null;
        }

        public void BaixarEstoque()
        {
            Quantidade--;
        }

        public void IncrementaEstoque()
        {
            Quantidade++;
        }

        public string RetornaPrimeiroNome()
        {
            var arr = Nome?.Split(' ');
            if (arr.Length > 1)
            {
                return arr[0];
            }
            return Nome;
        }

        public void ValidarProduto()
        {
            if (Quantidade < 1)
            {
                throw new Exception("Quantidade inválida");
            }

            if (string.IsNullOrEmpty(Nome))
            {
                throw new Exception("Nome inválido");
            }
        }
    }
}
