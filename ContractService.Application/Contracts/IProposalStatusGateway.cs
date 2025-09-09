using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractService.Application.Contracts
{
    public interface IProposalStatusGateway
    {
        Task<string?> GetStatusAsync(Guid proposalId, CancellationToken ct = default);
    }
}
