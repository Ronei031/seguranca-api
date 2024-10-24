using AutoMapper;
using Seguranca.DataTransfer.Usuarios.Responses;
using Seguranca.Dominio.Usuarios.Entidades;
using Seguranca.Dominio.Util.Enumeradores;
using Seguranca.Dominio.Utils.Enumeradores;

namespace Seguranca.Aplicacao.Usuarios.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, UsuarioResponse>();

            CreateMap<AtivoInativoEnum, EnumValue>().ConvertUsing(src => src.GetValue());
        }
    }
}
