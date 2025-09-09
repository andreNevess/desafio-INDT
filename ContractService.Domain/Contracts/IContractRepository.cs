using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractService.Domain.Contracts
{
    public interface IContractRepository
    {
        Task AddAsync(Contract contract, CancellationToken ct = default);
        Task<IReadOnlyList<Contract>> ListAsync(CancellationToken ct = default);
    }
}
