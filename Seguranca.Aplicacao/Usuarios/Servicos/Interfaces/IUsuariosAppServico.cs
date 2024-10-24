using Seguranca.DataTransfer.Usuarios.Requests;
using Seguranca.DataTransfer.Usuarios.Responses;

namespace Seguranca.Aplicacao.Usuarios.Servicos.Interfaces
{
    public interface IUsuariosAppServico
    {
        Task<UsuarioResponse> CriarUsuarioAsync(UsuarioRequest usuarioRequest, CancellationToken cancellationToken = default);
        Task<UsuarioLoginResponse> EfetuarLoginAsync(UsuarioLoginRequest request, CancellationToken cancellationToken = default);
    }
}
