using Seguranca.Dominio.Usuarios.Entidades;
using Seguranca.Dominio.Util.Enumeradores;

namespace Seguranca.Dominio.Usuarios.Servicos.Interfaces
{
    public interface IUsuariosServico
    {
        Task<Usuario> ValidarAsync(int id, CancellationToken cancellationToken = default);
        Usuario RecuperarPorNomeUsuario(string nomeUsuario);
        void ValidarUsuarioOuEmailExistente(string nomeUsuario, string email);
        string CriptografarSenha(string senha);
        bool VerificarSenha(string senha, string hashSenha);
        void VerificarStatusUsuario(AtivoInativoEnum status);
    }
}
