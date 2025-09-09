using ProposalService.Application.Proposals.DTOs;
using ProposalService.Domain.Proposals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProposalService.Application.Proposals.Mappers
{
    public static class ProposalMapper
    {
        public static ProposalResponse ToResponse(this Proposal p)
            => new(p.Id, p.CustomerName, p.Premium, p.Status.ToString(), p.CreatedAtUtc);
    }
}
