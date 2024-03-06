using AutoMapper;
using CodeBehind.PadraoProjeto.Application;
using CodeBehind.PadraoProjeto.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBehind.PadraoProjeto.Teste
{
    public class MyProfile : Profile
    {
        public MyProfile()
        {
            CreateMap<ClienteInserirCommand, Cliente>();
        }
    }
}
