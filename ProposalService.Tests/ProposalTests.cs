using FluentAssertions;
using ProposalService.Domain.Proposals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProposalService.Tests
{
    public class ProposalTests
    {
        [Fact]
        public void CreateProposal_ShouldStart_UnderReview()
        {
            var p = new Proposal("John Doe", 100.0m);
            p.Status.Should().Be(ProposalStatus.UnderReview);
        }

        [Fact]
        public void ChangeStatus_ToApproved_ShouldWork()
        {
            var p = new Proposal("Jane", 200m);
            p.ChangeStatus(ProposalStatus.Approved);
            p.Status.Should().Be(ProposalStatus.Approved);
        }
    }
}
