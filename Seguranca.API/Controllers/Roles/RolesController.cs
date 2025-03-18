using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Seguranca.Aplicacao.Roles.Servicos.Interfaces;
using Seguranca.DataTransfer.Roles.Requests;
using Seguranca.DataTransfer.Roles.Responses;

namespace Seguranca.API.Controllers.Roles
{
    public class RolesController : Controller
    {
        private readonly IRolesAppServico rolesAppServico;

        public RolesController(IRolesAppServico rolesAppServico)
        {
            this.rolesAppServico = rolesAppServico;
        }

        /// <summary>
        /// Adiciona uma nova role 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("cadastrar")]
        public async Task<ActionResult<RoleResponse>> Inserir([FromBody] RoleRequest request, CancellationToken cancellationToken)
        {
            RoleResponse response = await rolesAppServico.InserirAsync(request, cancellationToken);

            return Ok(response);
        }
    }
}
