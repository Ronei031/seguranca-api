using Seguranca.Dominio.Roles.Entidades;
using Seguranca.Dominio.Roles.Enumeradores;

namespace Seguranca.Dominio.Roles.Servicos.Interfaces
{
    public interface IRolesServico
    {
        Role RecuperarPorNome(string nomeRole);
    }
}
