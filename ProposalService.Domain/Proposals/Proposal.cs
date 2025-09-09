using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProposalService.Domain.Proposals
{
    public sealed class Proposal
    {
        public Guid Id { get; private set; }
        public string CustomerName { get; private set; }
        public decimal Premium { get; private set; }
        public ProposalStatus Status { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }

        private Proposal() { } 

        public Proposal(string customerName, decimal premium)
        {
            Id = Guid.NewGuid();
            CustomerName = string.IsNullOrWhiteSpace(customerName)
                ? throw new ArgumentException("Customer name is required.", nameof(customerName))
                : customerName.Trim();

            Premium = premium > 0 ? premium
                : throw new ArgumentException("Premium must be greater than zero.", nameof(premium));

            Status = ProposalStatus.UnderReview;
            CreatedAtUtc = DateTime.UtcNow;
        }

        public void ChangeStatus(ProposalStatus newStatus)
        {
            Status = newStatus;
        }
    }
}
