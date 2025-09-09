using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProposalService.Application.Proposals.DTOs
{
    public record CreateProposalRequest(string CustomerName, decimal Premium);
    public record ProposalResponse(Guid Id, string CustomerName, decimal Premium, string Status, DateTime CreatedAtUtc);
    public record ChangeStatusRequest(string Status);
}
