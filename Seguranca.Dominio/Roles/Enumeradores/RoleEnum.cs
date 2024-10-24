using System.ComponentModel;

namespace Seguranca.Dominio.Roles.Enumeradores
{
    public enum RoleEnum
    {
        [Description("Usuário")]
        Usuario = 1,
        [Description("Administrador")]
        Administrador = 2,
        [Description("Visitante")]
        Visitante = 3,
        [Description("Desenvolvedor")]
        Desenvolvedor = 4
    }
}
