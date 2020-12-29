//***CODE BEHIND - BY RODOLFO.FONSECA***//
using Refit;
using System.Threading.Tasks;

namespace CodeBehind.BFF.Interface
{
    public interface IPedidoRefit
    {
        [Get("/pedido?id={idPedido}")]
        Task<Pedido> SelecionarAsync(int idPedido);
    }
}
