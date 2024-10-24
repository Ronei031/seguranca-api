using Seguranca.Dominio.Usuarios.Entidades;

namespace Seguranca.Dominio.TokensAcesso.Servicos.Interfaces
{
    public interface ITokensAcessoServico
    {
        Task<string> GerarTokenAsync(Usuario usuario, CancellationToken cancellationToken = default);
    }
}
