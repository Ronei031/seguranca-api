using AutoMapper;
using Seguranca.Aplicacao.Roles.Servicos.Interfaces;
using Seguranca.Aplicacao.Utils.Transacoes.Interface;
using Seguranca.DataTransfer.Roles.Requests;
using Seguranca.DataTransfer.Roles.Responses;
using Seguranca.Dominio.Roles.Entidades;
using Seguranca.Dominio.Roles.Repositorios;
using Seguranca.Dominio.Roles.Servicos.Interfaces;

namespace Seguranca.Aplicacao.Roles.Servicos
{
    public class RolesAppServico : IRolesAppServico
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IRolesRepositorio rolesRepositorio;
        private readonly IRolesServico rolesServico;

        public RolesAppServico(IRolesRepositorio rolesRepositorio, IMapper mapper, IUnitOfWork unitOfWork, IRolesServico rolesServico)
        {
            this.rolesRepositorio = rolesRepositorio;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.rolesServico = rolesServico;
        }


        public async Task<RoleResponse> InserirAsync(RoleRequest request, CancellationToken cancellationToken)
        {
            try
            {
                unitOfWork.BeginTransaction();

                Role role = new(request.Nome, request.Descricao);

                await rolesRepositorio.InserirAsync(role, cancellationToken);

                unitOfWork.Commit();

                return mapper.Map<RoleResponse>(role);
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
                throw new Exception(ex.Message);
            }

        }
    }
}
