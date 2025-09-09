using ProposalService.Application.Proposals.DTOs;
using ProposalService.Application.Proposals.Mappers;
using ProposalService.Domain.Proposals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProposalService.Application.Proposals.UseCases
{
    public interface ICreateProposal
    {
        Task<ProposalResponse> ExecuteAsync(CreateProposalRequest request, CancellationToken ct = default);
    }
    public interface IListProposals
    {
        Task<IReadOnlyList<ProposalResponse>> ExecuteAsync(CancellationToken ct = default);
    }
    public interface IChangeProposalStatus
    {
        Task<ProposalResponse?> ExecuteAsync(Guid id, ChangeStatusRequest request, CancellationToken ct = default);
    }

    public sealed class CreateProposal : ICreateProposal
    {
        private readonly IProposalRepository _repo;
        private readonly IUnitOfWork _uow;

        public CreateProposal(IProposalRepository repo, IUnitOfWork uow)
        {
            _repo = repo; _uow = uow;
        }

        public async Task<ProposalResponse> ExecuteAsync(CreateProposalRequest request, CancellationToken ct = default)
        {
            var entity = new Proposal(request.CustomerName, request.Premium);
            await _repo.AddAsync(entity, ct);
            await _uow.CommitAsync(ct);
            return entity.ToResponse();
        }
    }

    public sealed class ListProposals : IListProposals
    {
        private readonly IProposalRepository _repo;
        public ListProposals(IProposalRepository repo) => _repo = repo;

        public async Task<IReadOnlyList<ProposalResponse>> ExecuteAsync(CancellationToken ct = default)
            => (await _repo.ListAsync(ct)).Select(p => p.ToResponse()).ToList();
    }

    public sealed class ChangeProposalStatus : IChangeProposalStatus
    {
        private readonly IProposalRepository _repo;
        private readonly IUnitOfWork _uow;

        public ChangeProposalStatus(IProposalRepository repo, IUnitOfWork uow)
        { _repo = repo; _uow = uow; }

        public async Task<ProposalResponse?> ExecuteAsync(Guid id, ChangeStatusRequest request, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity is null) return null;

            if (!Enum.TryParse<ProposalStatus>(request.Status, ignoreCase: true, out var newStatus))
                throw new ArgumentException("Invalid status. Use: UnderReview, Approved, Rejected.");

            entity.ChangeStatus(newStatus);
            await _repo.UpdateAsync(entity, ct);
            await _uow.CommitAsync(ct);

            return entity.ToResponse();
        }
    }
}
