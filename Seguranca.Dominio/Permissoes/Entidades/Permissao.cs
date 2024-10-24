using Seguranca.Dominio.RolesPermissoes.Entidades;
using Seguranca.Dominio.Utils.Excecoes;

namespace Seguranca.Dominio.Permissoes.Entidades
{
    public class Permissao
    {
        public virtual int Id { get; protected set; }
        public virtual string Nome { get; protected set; }
        public virtual string Descricao { get; protected set; }
        public virtual IList<RolePermissao> RolePermissoes { get; set; } = [];

        protected Permissao()
        {
        }

        public Permissao(string nome, string descricao)
        {
            SetNome(nome);
            SetDescricao(descricao);
        }

        public virtual void SetNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new AtributoObrigatorioExcecao("Nome");
            }

            Nome = nome;
        }

        public virtual void SetDescricao(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
            {
                throw new AtributoObrigatorioExcecao("Descricao");
            }

            Descricao = descricao;
        }
    }
}
