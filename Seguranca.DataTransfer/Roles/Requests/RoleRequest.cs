using Seguranca.Dominio.Roles.Enumeradores;

namespace Seguranca.DataTransfer.Roles.Requests
{
    public class RoleRequest
    {
        public RoleEnum Nome { get; set; }
        public string Descricao { get; set; }
    }
}
