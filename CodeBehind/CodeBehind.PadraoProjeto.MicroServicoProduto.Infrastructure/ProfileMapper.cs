//***CODE BEHIND - BY RODOLFO.FONSECA***//

using AutoMapper;
using CodeBehind.PadraoProjeto.MicroServicoProduto.Domain;

namespace CodeBehind.PadraoProjeto.MicroServicoProduto.Infrastructure
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<Produto, ProdutoVM>();
            CreateMap<ProdutoVM, Produto>()
                .ConstructUsing(c => new Produto(c.Nome, c.Preco, c.Quantidade));
        }
    }

}
