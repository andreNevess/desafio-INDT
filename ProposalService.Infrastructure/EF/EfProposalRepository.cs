using Microsoft.EntityFrameworkCore;
using ProposalService.Domain.Proposals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProposalService.Infrastructure.EF
{
    public sealed class EfProposalRepository : IProposalRepository
    {
        private readonly ProposalDbContext _db;
        public EfProposalRepository(ProposalDbContext db) => _db = db;

        public async Task AddAsync(Proposal proposal, CancellationToken ct = default)
            => await _db.Proposals.AddAsync(proposal, ct);

        public Task<Proposal?> GetByIdAsync(Guid id, CancellationToken ct = default)
            => _db.Proposals.FirstOrDefaultAsync(p => p.Id == id, ct);

        public async Task<IReadOnlyList<Proposal>> ListAsync(CancellationToken ct = default)
            => await _db.Proposals.AsNoTracking().OrderByDescending(p => p.CreatedAtUtc).ToListAsync(ct);

        public Task UpdateAsync(Proposal proposal, CancellationToken ct = default)
        { _db.Proposals.Update(proposal); return Task.CompletedTask; }
    }
}
