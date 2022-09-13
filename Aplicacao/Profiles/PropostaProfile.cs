using Aplicacao.Dto.Proposta;
using Dominio.Modelos;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Profiles
{
    public class PropostaProfile : Profile
    {
        public PropostaProfile()
        {
           CreateMap<PropostaDto, TreinaProposta>();

        }
    }
}
