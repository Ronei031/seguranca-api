using NHibernate;
using Seguranca.Dominio.Utils.Enumeradores;
using Seguranca.Dominio.Utils.Genericos;
using Seguranca.Dominio.Utils.Paginacao;
using System.Linq.Dynamic.Core;

namespace Seguranca.Infra.Utils.Genericos
{
    public class GenericoRepositorio<T> : IGenericoRepositorio<T> where T : class
    {
        private readonly ISession session;
        public GenericoRepositorio(ISession session)
        {
            this.session = session;
        }

        public T Editar(T entidade)
        {
            session.Update(entidade);
            return entidade;
        }

        public void Excluir(T entidade)
        {
            session.Delete(entidade);
        }

        public T Inserir(T entidade)
        {
            session.Save(entidade);
            return entidade;
        }

        public PaginacaoConsulta<T> Listar(IQueryable<T> query, int qt, int pg, string cpOrd, TipoOrdenacaoEnum tpOrd)
        {
            try
            {
                query = query.OrderBy(cpOrd + " " + tpOrd.ToString());
                return Paginar(query, qt, pg);
            }
            catch
            {
                throw new ArgumentException("Campo da ordenação não informado");
            }
        }

        private static PaginacaoConsulta<T> Paginar(IQueryable<T> query, int qt, int pg)
        {
            return new PaginacaoConsulta<T>
            {
                Registros = query.Skip((pg - 1) * qt).Take(qt).ToList(),
                Total = query.LongCount()
            };
        }

        public IQueryable<T> Query()
        {
            return session.Query<T>();
        }

        public T Recuperar(int id)
        {
            return session.Get<T>(id);
        }

        // Métodos Async com CancellationToken

        public async Task<T> EditarAsync(T entidade, CancellationToken cancellationToken)
        {
            await session.UpdateAsync(entidade, cancellationToken);
            return entidade;
        }

        public async Task ExcluirAsync(T entidade, CancellationToken cancellationToken)
        {
            await session.DeleteAsync(entidade, cancellationToken);
        }

        public async Task<T> InserirAsync(T entidade, CancellationToken cancellationToken)
        {
            await session.SaveAsync(entidade, cancellationToken);
            return entidade;
        }

        public async Task<PaginacaoConsulta<T>> ListarAsync(IQueryable<T> query, int qt, int pg, string cpOrd, TipoOrdenacaoEnum tpOrd, CancellationToken cancellationToken)
        {
            try
            {
                query = query.OrderBy(cpOrd + " " + tpOrd.ToString());
                return await PaginarAsync(query, qt, pg, cancellationToken);
            }
            catch
            {
                throw new ArgumentException("Campo da ordenação não informado");
            }
        }

        private static async Task<PaginacaoConsulta<T>> PaginarAsync(IQueryable<T> query, int qt, int pg, CancellationToken cancellationToken)
        {
            var registros = await Task.Run(() => query.Skip((pg - 1) * qt).Take(qt).ToList(), cancellationToken);
            var total = await Task.Run(() => query.LongCount(), cancellationToken);

            return new PaginacaoConsulta<T>
            {
                Registros = registros,
                Total = total
            };
        }

        public async Task<IQueryable<T>> QueryAsync(CancellationToken cancellationToken)
        {
            return await Task.Run(() => session.Query<T>(), cancellationToken);
        }

        public async Task<T> RecuperarAsync(int id, CancellationToken cancellationToken)
        {
            return await session.GetAsync<T>(id, cancellationToken);
        }
    }
}
