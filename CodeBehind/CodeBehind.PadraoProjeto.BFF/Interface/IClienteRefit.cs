//***CODE BEHIND - BY RODOLFO.FONSECA***//
using Refit;
using System.Threading.Tasks;

namespace CodeBehind.BFF.Interface
{
    public interface IClienteRefit
    {
        [Get("/cliente?id={idCliente}")]
        Task<Cliente> SelecionarAsync(int idCliente);
    }
}
