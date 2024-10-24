using Seguranca.Dominio.Utils.Enumeradores;

namespace Seguranca.DataTransfer.Roles.Responses
{
    public class RoleResponse
    {
        public int Id { get; set; }
        public EnumValue Nome { get; set; }
        public string Descricao { get; set; }
    }
}
