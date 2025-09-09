using FluentAssertions;
using ProposalService.Domain.Proposals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProposalService.Tests
{
    public class ProposalDomainTests
    {
        [Fact]
        public void NewProposal_ShouldStart_UnderReview()
        {
            var p = new Proposal("John Doe", 100m);
            p.Status.Should().Be(ProposalStatus.UnderReview);
            p.CustomerName.Should().Be("John Doe");
            p.Premium.Should().Be(100m);
            p.Id.Should().NotBeEmpty();
        }

        [Fact]
        public void ChangeStatus_ShouldUpdate_Status()
        {
            var p = new Proposal("Jane", 250m);
            p.ChangeStatus(ProposalStatus.Approved);
            p.Status.Should().Be(ProposalStatus.Approved);
        }

        [Theory]
        [InlineData("", 10)]
        [InlineData("   ", 10)]
        public void InvalidName_ShouldThrow(string name, decimal premium)
        {
            var act = () => new Proposal(name, premium);
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void NonPositivePremium_ShouldThrow()
        {
            var act = () => new Proposal("Valid", 0m);
            act.Should().Throw<ArgumentException>();
        }
    }
}
