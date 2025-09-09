using ProposalService.Domain.Proposals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProposalService.Infrastructure.EF
{
    public sealed class EfUnitOfWork : IUnitOfWork
    {
        private readonly ProposalDbContext _db;
        public EfUnitOfWork(ProposalDbContext db) => _db = db;
        public Task CommitAsync(CancellationToken ct = default) => _db.SaveChangesAsync(ct);
    }
}
