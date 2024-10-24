using AutoMapper;
using Seguranca.Dominio.Utils.Paginacao;

namespace Seguranca.Aplicacao.Utils.Paginacao;

public class PaginacaoConsultasProfile : Profile
{
    public PaginacaoConsultasProfile()
    {
        CreateMap(typeof(PaginacaoConsulta<>), typeof(PaginacaoConsulta<>));
    }
}
