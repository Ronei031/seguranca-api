using Microsoft.IdentityModel.Tokens;
using Seguranca.Dominio.TokensAcesso.Entidades;
using Seguranca.Dominio.TokensAcesso.Repositorios;
using Seguranca.Dominio.TokensAcesso.Servicos.Interfaces;
using Seguranca.Dominio.Usuarios.Entidades;
using Seguranca.Dominio.Utils.Enumeradores;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Seguranca.Dominio.TokensAcesso.Servicos
{
    public class TokensAcessoServico : ITokensAcessoServico
    {
        private readonly ITokenAcessoRepositorio tokenAcessoRepositorio;

        public TokensAcessoServico(ITokenAcessoRepositorio tokenAcessoRepositorio)
        {
            this.tokenAcessoRepositorio = tokenAcessoRepositorio;
        }

        public async Task<string> GerarTokenAsync(Usuario usuario, CancellationToken cancellationToken = default)
        {
            JwtSecurityTokenHandler manipuladorDeToken = new();

            byte[] key = Encoding.ASCII.GetBytes("5jXNOmtDSks9zY0qQeMffTccqx8L3VXDL1E6qJK4q0c=");

            SecurityTokenDescriptor descritorToken = new()
            {
                Subject = new ClaimsIdentity(
                [
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                 new Claim(ClaimTypes.Role, string.Join(",", usuario.UsuarioRoles.Select(ur => ur.Role.Nome))),
                new Claim(ClaimTypes.Email, usuario.Email)
                ]),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = "admin",
                Audience = "seguranca"
            };

            SecurityToken token = manipuladorDeToken.CreateToken(descritorToken);

            string tokenString = manipuladorDeToken.WriteToken(token);

            TokenAcesso tokenAcesso = new(
                tokenString,
                usuario.Id,
                usuario,
                SimNaoEnum.Nao,
                DateTime.UtcNow.AddHours(2)
            );

            await tokenAcessoRepositorio.InserirAsync(tokenAcesso, cancellationToken);

            return tokenString;
        }
    }
}
