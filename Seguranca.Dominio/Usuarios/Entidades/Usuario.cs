using Seguranca.Dominio.TokensAcesso.Entidades;
using Seguranca.Dominio.UsuariosRoles.Entidades;
using Seguranca.Dominio.Util.Enumeradores;
using Seguranca.Dominio.Utils.Excecoes;
using System.Text.RegularExpressions;

namespace Seguranca.Dominio.Usuarios.Entidades
{
    public partial class Usuario
    {
        public virtual int Id { get; protected set; }
        public virtual string NomeCompleto { get; protected set; }
        public virtual string NomeUsuario { get; protected set; }
        public virtual string Email { get; protected set; }
        public virtual string SenhaHash { get; protected set; }
        public virtual AtivoInativoEnum Status { get; protected set; }
        public virtual DateTime DataCriacao { get; protected set; }
        public virtual DateTime? UltimoLogin { get; protected set; }
        public virtual IList<UsuarioRole> UsuarioRoles { get; protected set; } = [];
        public virtual IList<TokenAcesso> TokensAcesso { get; protected set; } = [];

        protected Usuario()
        {
        }

        public Usuario(string nomeCompleto, string nomeUsuario, string email, string senhaHash)
        {
            SetNomeCompleto(nomeCompleto);
            SetNomeUsuario(nomeUsuario);
            SetEmail(email);
            SetSenhaHash(senhaHash);
            Status = AtivoInativoEnum.Ativo;
            DataCriacao = DateTime.Now;
        }

        public virtual void SetNomeCompleto(string nomeCompleto)
        {
            if (string.IsNullOrWhiteSpace(nomeCompleto))
            {
                throw new AtributoObrigatorioExcecao("NomeCompleto");
            }

            NomeCompleto = nomeCompleto;
        }

        public virtual void SetNomeUsuario(string nomeUsuario)
        {
            if (string.IsNullOrWhiteSpace(nomeUsuario))
            {
                throw new AtributoObrigatorioExcecao("NomeUsuario");
            }

            // Validação de formato: apenas "nome.sobrenome" sem números
            if (!NomeUsuarioRegex().IsMatch(nomeUsuario))
            {
                throw new RegraDeNegocioExcecao("O nome de usuário deve seguir o formato 'nome.sobrenome' e não deve conter números.");
            }

            NomeUsuario = nomeUsuario;
        }

        public virtual void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new AtributoObrigatorioExcecao("Email");
            }

            if (email.Length > 100)
            {
                throw new TamanhoDeAtributoInvalidoExcecao("Email", 1, 100);
            }

            if (!EmailRegex().IsMatch(email))
            {
                throw new RegraDeNegocioExcecao("O e-mail fornecido não está no formato adequado. Por favor, insira um e-mail válido, por exemplo, usuario@dominio.");

            }

            Email = email;
        }

        public virtual void SetSenhaHash(string senhaHash)
        {
            if (string.IsNullOrWhiteSpace(senhaHash))
            {
                throw new AtributoObrigatorioExcecao("Senha Hash");
            }

            SenhaHash = senhaHash;
        }

        public virtual void SetStatus(AtivoInativoEnum status)
        {
            Status = status;
        }

        public virtual void SetUltimoLogin(DateTime? ultimoLogin)
        {
            UltimoLogin = ultimoLogin;
        }

        #region Métodos privados
        private bool ValidarSenha(string senha)
        {
            // Expressão regular para validar a senha
            Regex regex = SenhaRegex();

            return regex.IsMatch(senha);
        }

        [GeneratedRegex(@"^[a-zA-Z]+\.[a-zA-Z]+$")]
        private static partial Regex NomeUsuarioRegex();

        [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
        private static partial Regex EmailRegex();

        [GeneratedRegex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{9,}$")]
        private static partial Regex SenhaRegex();

        #endregion
    }
}
