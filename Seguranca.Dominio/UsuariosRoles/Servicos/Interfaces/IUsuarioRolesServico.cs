using Seguranca.Dominio.Roles.Entidades;
using Seguranca.Dominio.Usuarios.Entidades;
using Seguranca.Dominio.UsuariosRoles.Entidades;

namespace Seguranca.Dominio.UsuariosRoles.Servicos.Interfaces
{
    public interface IUsuarioRolesServico
    {
        Task<UsuarioRole> InserirAsync(int idUsuario, Usuario usuario, int idRole, Role role, CancellationToken cancellationToken);
    }
}
