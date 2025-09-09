using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractService.Application.Contracts.DTOs
{
    public record CreateContractRequest(Guid ProposalId);
    public record ContractResponse(Guid Id, Guid ProposalId, DateTime ContractedAtUtc);
}
