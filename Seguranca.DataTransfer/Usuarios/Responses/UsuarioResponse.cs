using Seguranca.Dominio.Utils.Enumeradores;

namespace Seguranca.DataTransfer.Usuarios.Responses
{
    public class UsuarioResponse
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string NomeUsuario { get; set; }
        public string Email { get; set; }
        public EnumValue Status { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
