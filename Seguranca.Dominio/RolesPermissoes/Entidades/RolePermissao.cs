using Seguranca.Dominio.Permissoes.Entidades;
using Seguranca.Dominio.Roles.Entidades;

namespace Seguranca.Dominio.RolesPermissoes.Entidades
{
    public class RolePermissao
    {
        public virtual int IdRole { get; protected set; }
        public virtual Role Role { get; protected set; }
        public virtual int IdPermissao { get; protected set; }
        public virtual Permissao Permissao { get; protected set; }

        protected RolePermissao()
        {
        }

        public RolePermissao(int idRole, Role role, int idPermissao, Permissao permissao)
        {
            SetRole(role);
            SetIdRole(idRole);
            SetPermissao(permissao);
            SetIdPermissao(idPermissao);
        }

        public virtual void SetRole(Role role)
        {
            Role = role;
        }

        public virtual void SetPermissao(Permissao permissao)
        {
            Permissao = permissao;
        }

        public virtual void SetIdRole(int idRole)
        {
            IdRole = idRole;
        }

        public virtual void SetIdPermissao(int idPermissao)
        {
            IdPermissao = idPermissao;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (RolePermissao)obj;
            return IdRole == other.IdRole && IdPermissao == other.IdPermissao;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdRole, IdPermissao);
        }
    }
}