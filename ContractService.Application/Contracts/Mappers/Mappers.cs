using ContractService.Application.Contracts.DTOs;
using ContractService.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractService.Application.Contracts.Mappers
{
    public static class Mapper
    {
        public static ContractResponse ToResponse(this Contract c)
            => new(c.Id, c.ProposalId, c.ContractedAtUtc);
    }
}
