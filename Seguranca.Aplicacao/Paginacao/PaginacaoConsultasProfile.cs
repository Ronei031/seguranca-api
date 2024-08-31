using AutoMapper;
using Seguranca.Dominio.Util;

namespace Seguranca.Aplicacao.Paginacao;

public class PaginacaoConsultasProfile : Profile
{
    public PaginacaoConsultasProfile()
    {
        CreateMap(typeof(PaginacaoConsulta<>), typeof(PaginacaoConsulta<>));
    }
}
