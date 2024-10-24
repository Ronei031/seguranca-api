using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Seguranca.Aplicacao.Usuarios.Servicos.Interfaces;
using Seguranca.DataTransfer.Usuarios.Requests;
using Seguranca.DataTransfer.Usuarios.Responses;

namespace Seguranca.API.Controllers
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
        [Authorize(Policy = "Admin")]
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

                // Retorna um OK com o token gerado
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                // Retorna um BadRequest (400) se houver uma ArgumentException
                return BadRequest(new { Error = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                // Retorna um Unauthorized (401) se houver falha de autenticação
                return Unauthorized(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                // Retorna um InternalServerError (500) para qualquer outro erro não tratado
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = "Ocorreu um erro inesperado." });
            }
        }

    }
}
