using Seguranca.Dominio.RolesPermissoes.Entidades;
using Seguranca.Dominio.UsuariosRoles.Entidades;
using Seguranca.Dominio.Utils.Excecoes;

namespace Seguranca.Dominio.Roles.Entidades
{
    public class Role
    {
        public virtual int Id { get; protected set; }
        public virtual string Nome { get; protected set; }
        public virtual string Descricao { get; protected set; }
        public virtual IList<UsuarioRole> UsuarioRoles { get; protected set; } = [];
        public virtual IList<RolePermissao> RolePermissoes { get; protected set; } = [];

        protected Role()
        {
        }

        public Role(string nome, string descricao)
        {
            SetNome(nome);
            SetDescricao(descricao);
        }

        public virtual void SetNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new AtributoObrigatorioExcecao("Descricao");
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
