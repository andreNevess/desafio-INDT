using FluentAssertions;
using ProposalService.Application.Proposals.DTOs;
using ProposalService.Application.Proposals.UseCases;
using ProposalService.Infrastructure.InMemory;


namespace ProposalService.Tests
{
    public class CreateProposalUseCaseTests
    {
        [Fact]
        public async Task Execute_ShouldCreate_AndReturnResponse()
        {
            var repo = new InMemoryProposalRepository();
            var uow = new InMemoryUnitOfWork();
            var uc = new CreateProposal(repo, uow);

            var res = await uc.ExecuteAsync(new CreateProposalRequest("Client X", 500m));

            res.Id.Should().NotBeEmpty();
            res.CustomerName.Should().Be("Client X");
            res.Premium.Should().Be(500m);
            res.Status.Should().Be("UnderReview");
        }
    }
}
