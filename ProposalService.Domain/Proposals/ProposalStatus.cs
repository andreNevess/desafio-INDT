using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProposalService.Domain.Proposals
{
    public enum ProposalStatus
    {
        UnderReview = 0, // Em Análise
        Approved = 1, // Aprovada
        Rejected = 2  // Rejeitada
    }
}
