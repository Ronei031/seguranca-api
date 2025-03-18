using Microsoft.AspNetCore.Mvc;
using Seguranca.Aplicacao.Usuarios.Servicos.Interfaces;
using Seguranca.DataTransfer.Usuarios.Requests;
using Seguranca.DataTransfer.Usuarios.Responses;

namespace Seguranca.API.Controllers.Usuarios
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController : Controller
    {
        private readonly IUsuariosAppServico usuariosAppServico;

        public UsuariosController(IUsuariosAppServico usuariosAppServico)
        {
            this.usuariosAppServico = usuariosAppServico;
        }

        /// <summary>
        /// Adiciona um novo usuário 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("cadastrar")]
        public async Task<ActionResult<UsuarioResponse>> Inserir([FromBody] UsuarioRequest request, CancellationToken cancellationToken)
        {
            UsuarioResponse response = await usuariosAppServico.CriarUsuarioAsync(request, cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Efetuar Login 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult<UsuarioLoginResponse>> EfetuarLogin([FromBody] UsuarioLoginRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Tenta efetuar o login
                UsuarioLoginResponse response = await usuariosAppServico.EfetuarLoginAsync(request, cancellationToken);

                // Adiciona o token no cabeçalho da resposta
                if (!string.IsNullOrEmpty(response.Token))
                {
                    Response.Headers.Append("Authorization", $"Bearer {response.Token}");
                }

                // Retorna um OK sem incluir o token no body, se preferir manter apenas no header
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = "Ocorreu um erro inesperado." });
            }
        }


    }
}
