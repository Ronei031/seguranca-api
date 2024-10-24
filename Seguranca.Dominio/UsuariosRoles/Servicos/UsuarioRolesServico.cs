using Seguranca.Dominio.Roles.Entidades;
using Seguranca.Dominio.Usuarios.Entidades;
using Seguranca.Dominio.UsuariosRoles.Entidades;
using Seguranca.Dominio.UsuariosRoles.Repositorios;
using Seguranca.Dominio.UsuariosRoles.Servicos.Interfaces;

namespace Seguranca.Dominio.UsuariosRoles.Servicos
{
    public class UsuarioRolesServico : IUsuarioRolesServico
    {
        private readonly IUsuariosRolesRepositorio usuariosRolesRepositorio;

        public UsuarioRolesServico(IUsuariosRolesRepositorio usuariosRolesRepositorio)
        {
            this.usuariosRolesRepositorio = usuariosRolesRepositorio;
        }

        public async Task<UsuarioRole> InserirAsync(int idUsuario, Usuario usuario, int idRole, Role role, CancellationToken cancellationToken)
        {
            UsuarioRole usuarioRole = new(idUsuario, usuario, idRole, role);

            await usuariosRolesRepositorio.InserirAsync(usuarioRole, cancellationToken);

            return usuarioRole;
        }
    }
}
