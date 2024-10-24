using Seguranca.Dominio.Roles.Entidades;
using Seguranca.Dominio.Usuarios.Entidades;

namespace Seguranca.Dominio.UsuariosRoles.Entidades
{
    public class UsuarioRole
    {
        public virtual int IdUsuario { get; protected set; }
        public virtual Usuario Usuario { get; protected set; }
        public virtual int IdRole { get; protected set; }
        public virtual Role Role { get; protected set; }

        protected UsuarioRole()
        {
        }

        public UsuarioRole(int idUsuario, Usuario usuario, int idRole, Role role)
        {
            SetUsuario(usuario);
            SetIdUsuario(idUsuario);
            SetRole(role);
            SetIdRole(idRole);
        }

        public virtual void SetUsuario(Usuario usuario)
        {
            Usuario = usuario;
        }

        public virtual void SetRole(Role role)
        {
            Role = role;
        }

        public virtual void SetIdUsuario(int idUsuario)
        {
            IdUsuario = idUsuario;
        }

        public virtual void SetIdRole(int idRole)
        {
            IdRole = idRole;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (UsuarioRole)obj;
            return IdUsuario == other.IdUsuario && IdRole == other.IdRole;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdUsuario, IdRole);
        }
    }
}