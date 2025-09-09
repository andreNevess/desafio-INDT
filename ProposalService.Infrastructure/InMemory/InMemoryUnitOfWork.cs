using ProposalService.Domain.Proposals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProposalService.Infrastructure.InMemory
{
    public sealed class InMemoryUnitOfWork : IUnitOfWork
    {
        public Task CommitAsync(CancellationToken ct = default) => Task.CompletedTask;
    }
}
