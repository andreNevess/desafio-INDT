using ProposalService.Domain.Proposals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProposalService.Infrastructure.InMemory
{

    public sealed class InMemoryProposalRepository : IProposalRepository
    {
        private readonly List<Proposal> _items = new();

        public Task AddAsync(Proposal proposal, CancellationToken ct = default)
        { _items.Add(proposal); return Task.CompletedTask; }

        public Task<Proposal?> GetByIdAsync(Guid id, CancellationToken ct = default)
            => Task.FromResult(_items.FirstOrDefault(x => x.Id == id));

        public Task<IReadOnlyList<Proposal>> ListAsync(CancellationToken ct = default)
            => Task.FromResult<IReadOnlyList<Proposal>>(_items.ToList());

        public Task UpdateAsync(Proposal proposal, CancellationToken ct = default)
            => Task.CompletedTask; // nada a fazer no InMemory
    }
}
