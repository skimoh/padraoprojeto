using CodeBehind.PadraoProjeto.MicroServicoProduto.Domain;

namespace CodeBehind.PadraoProjeto.MicroServicoProduto.Test
{
    public static class ProdutoMock
    {

        public static Produto _produtoSucesso = new Produto("CD", 10, 1) { };
        public static Produto _produtoQuantidadeInvalida = new Produto("DVD", 10, -1) { };        
        public static Produto _produtoNomeAusente = new Produto("", 10, 10) { };
    }
}
