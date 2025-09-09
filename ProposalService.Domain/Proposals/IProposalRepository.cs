using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProposalService.Domain.Proposals
{
    public interface IProposalRepository
    {
        Task AddAsync(Proposal proposal, CancellationToken ct = default);
        Task<Proposal?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IReadOnlyList<Proposal>> ListAsync(CancellationToken ct = default);
        Task UpdateAsync(Proposal proposal, CancellationToken ct = default);
    }
}
