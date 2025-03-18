using System.ComponentModel;

namespace Seguranca.Dominio.Roles.Enumeradores
{
    public enum RoleEnum
    {
        [Description("SuperAdmin")]
        SuperAdmin = 1,
        [Description("MasterTi")]
        MasterTi = 2,
        [Description("AdminEmpresa")]
        AdminEmpresa = 3,
        [Description("Gerente")]
        Gerente = 4,
        [Description("Usuario")]
        Usuario = 5,
        [Description("Convidado")]
        Convidado = 6,
    }
}