//***CODE BEHIND - BY RODOLFO.FONSECA***//
using CodeBehind.PadraoProjeto.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeBehind.PadraoProjeto.Repository
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> ListarPor(string nome);
        Task<int> Inserir(Cliente obj);
    }
    public class ClienteRepository : IClienteRepository
    {
        public Task<int> Inserir(Cliente obj)
        {
            Console.WriteLine(obj.Id);

            return Task.FromResult(1);
        }

        public Task<IEnumerable<Cliente>> ListarPor(string nome)
        {
            var lista = new List<Cliente>();
            lista.Add(new Cliente { Id = 1, Nome = "joao" });
            lista.Add(new Cliente { Id = 2, Nome = "maria" });

            return Task.FromResult<IEnumerable<Cliente>>(lista);
        }
    }
}
