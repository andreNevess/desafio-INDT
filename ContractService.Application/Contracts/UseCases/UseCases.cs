using ContractService.Application.Contracts.DTOs;
using ContractService.Application.Contracts.Mappers;
using ContractService.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractService.Application.Contracts.UseCases
{
    public interface ICreateContract
    {
        Task<ContractResponse> ExecuteAsync(CreateContractRequest req, CancellationToken ct = default);
    }

    public interface IListContracts
    {
        Task<IReadOnlyList<ContractResponse>> ExecuteAsync(CancellationToken ct = default);
    }

    public sealed class CreateContract : ICreateContract
    {
        private readonly IContractRepository _repo;
        private readonly IUnitOfWork _uow;
        private readonly IProposalStatusGateway _proposalGateway;

        public CreateContract(IContractRepository repo, IUnitOfWork uow, IProposalStatusGateway proposalGateway)
        {
            _repo = repo; _uow = uow; _proposalGateway = proposalGateway;
        }

        public async Task<ContractResponse> ExecuteAsync(CreateContractRequest req, CancellationToken ct = default)
        {
            var status = await _proposalGateway.GetStatusAsync(req.ProposalId, ct);
            if (!string.Equals(status, "Approved", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Proposal must be Approved to contract.");

            var contract = new Contract(req.ProposalId);
            await _repo.AddAsync(contract, ct);
            await _uow.CommitAsync(ct);
            return contract.ToResponse();
        }
    }

    public sealed class ListContracts : IListContracts
    {
        private readonly IContractRepository _repo;
        public ListContracts(IContractRepository repo) => _repo = repo;

        public async Task<IReadOnlyList<ContractResponse>> ExecuteAsync(CancellationToken ct = default)
            => (await _repo.ListAsync(ct)).Select(x => x.ToResponse()).ToList();
    }
}
