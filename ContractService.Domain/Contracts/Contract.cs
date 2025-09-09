using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractService.Domain.Contracts
{
    public sealed class Contract
    {
        public Guid Id { get; private set; }
        public Guid ProposalId { get; private set; }
        public DateTime ContractedAtUtc { get; private set; }

        private Contract() { }

        public Contract(Guid proposalId)
        {
            if (proposalId == Guid.Empty) throw new ArgumentException("ProposalId is required.", nameof(proposalId));
            Id = Guid.NewGuid();
            ProposalId = proposalId;
            ContractedAtUtc = DateTime.UtcNow;
        }
    }
}
