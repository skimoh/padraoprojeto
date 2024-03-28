//***CODE BEHIND - BY RODOLFO.FONSECA***//

using System.Runtime.Serialization;

namespace CodeBehind.PadraoProjeto.MicroServicoProduto.Domain
{
    public class ProdutoVM
    {
        [IgnoreDataMember]
        public int IdProduto { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }        
    }
}
