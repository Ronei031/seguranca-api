using AutoMapper;
using Seguranca.DataTransfer.Roles.Responses;
using Seguranca.Dominio.Roles.Entidades;
using Seguranca.Dominio.Utils.Enumeradores;

namespace Seguranca.Aplicacao.Roles.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleResponse>()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome.GetValue()));
        }
    }
}
