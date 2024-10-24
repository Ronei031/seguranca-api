namespace Seguranca.Aplicacao.Utils.Transacoes.Interface;

public interface IUnitOfWork : IDisposable
{
    void BeginTransaction();
    void Commit();
    void Rollback();
}
