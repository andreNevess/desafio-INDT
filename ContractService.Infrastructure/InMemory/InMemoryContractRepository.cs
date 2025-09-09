using ContractService.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractService.Infrastructure.InMemory
{
    public sealed class InMemoryContractRepository : IContractRepository
    {
        private readonly List<Contract> _items = new();

        public Task AddAsync(Contract contract, CancellationToken ct = default)
        { _items.Add(contract); return Task.CompletedTask; }

        public Task<IReadOnlyList<Contract>> ListAsync(CancellationToken ct = default)
            => Task.FromResult<IReadOnlyList<Contract>>(_items.ToList());
    }
}
