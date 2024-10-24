using Seguranca.Dominio.Roles.Entidades;
using Seguranca.Dominio.Roles.Enumeradores;
using Seguranca.Dominio.Roles.Repositorios;
using Seguranca.Dominio.Roles.Servicos.Interfaces;
using Seguranca.Dominio.Utils.Enumeradores;
using Seguranca.Dominio.Utils.Excecoes;

namespace Seguranca.Dominio.Roles.Servicos
{
    public class RolesServico : IRolesServico
    {
        private readonly IRolesRepositorio rolesRepositorio;

        public RolesServico(IRolesRepositorio rolesRepositorio)
        {
            this.rolesRepositorio = rolesRepositorio;
        }

        public Role RecuperarPorNome(RoleEnum nomeRole)
        {
            IQueryable<Role> query = rolesRepositorio.Query();

            Role role = query.FirstOrDefault(u => u.Nome == nomeRole);

            return role ?? throw new RegraDeNegocioExcecao($"'{nomeRole.GetDescription()}' não encontrado.");
        }
    }
}
