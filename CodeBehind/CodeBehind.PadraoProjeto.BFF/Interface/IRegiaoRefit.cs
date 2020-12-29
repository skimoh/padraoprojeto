//***CODE BEHIND - BY RODOLFO.FONSECA***//
using CodeBehind.BFF.Model;
using Refit;
using System.Threading.Tasks;

namespace CodeBehind.BFF.Interface
{
    public interface IRegiaoRefit
    {

        [Get("/ws/{cep}/json")]
        Task<Regiao> SelecionarPorCepAsync(string cep);

    }
}
