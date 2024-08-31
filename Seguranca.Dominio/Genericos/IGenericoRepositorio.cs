using Seguranca.Dominio.Util;
using Seguranca.Dominio.Util.Enumeradores;

namespace Seguranca.Dominio.Genericos;

public interface IGenericoRepositorio<T> where T : class
{
    T Recuperar(int id);
    T Inserir(T entidade);
    T Editar(T entidade);
    void Excluir(T entidade);
    PaginacaoConsulta<T> Listar(IQueryable<T> query, int qt, int pg, string cpOrd, TipoOrdenacaoEnum tpOrd);
    IQueryable<T> Query();
}
