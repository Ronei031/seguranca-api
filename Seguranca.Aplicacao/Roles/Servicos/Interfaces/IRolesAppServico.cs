using Seguranca.DataTransfer.Roles.Requests;
using Seguranca.DataTransfer.Roles.Responses;

namespace Seguranca.Aplicacao.Roles.Servicos.Interfaces
{
    public interface IRolesAppServico
    {
        Task<RoleResponse> InserirAsync(RoleRequest request, CancellationToken cancellationToken);
    }
}
