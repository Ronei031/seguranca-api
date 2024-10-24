using Seguranca.Dominio.Utils.Enumeradores;
using Seguranca.Dominio.Utils.Paginacao;

namespace Seguranca.Dominio.Utils.Genericos
{
    public interface IGenericoRepositorio<T> where T : class
    {
        T Recuperar(int id);
        T Inserir(T entidade);
        T Editar(T entidade);
        void Excluir(T entidade);
        PaginacaoConsulta<T> Listar(IQueryable<T> query, int qt, int pg, string cpOrd, TipoOrdenacaoEnum tpOrd);
        IQueryable<T> Query();

        // Métodos assíncronos com suporte a CancellationToken
        Task<T> RecuperarAsync(int id, CancellationToken cancellationToken);
        Task<T> InserirAsync(T entidade, CancellationToken cancellationToken);
        Task<T> EditarAsync(T entidade, CancellationToken cancellationToken);
        Task ExcluirAsync(T entidade, CancellationToken cancellationToken);
        Task<PaginacaoConsulta<T>> ListarAsync(IQueryable<T> query, int qt, int pg, string cpOrd, TipoOrdenacaoEnum tpOrd, CancellationToken cancellationToken);
        Task<IQueryable<T>> QueryAsync(CancellationToken cancellationToken);
    }
}
