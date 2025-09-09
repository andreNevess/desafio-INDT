using FluentAssertions;
using ProposalService.Application.Proposals.DTOs;
using ProposalService.Application.Proposals.UseCases;
using ProposalService.Infrastructure.InMemory;


namespace ProposalService.Tests
{
    public class ListProposalsUseCaseTests
    {
        [Fact]
        public async Task Execute_ShouldReturn_Items()
        {
            var repo = new InMemoryProposalRepository();
            var uow = new InMemoryUnitOfWork();

            var create = new CreateProposal(repo, uow);
            await create.ExecuteAsync(new CreateProposalRequest("A", 100m));
            await create.ExecuteAsync(new CreateProposalRequest("B", 200m));

            var list = new ListProposals(repo);
            var items = await list.ExecuteAsync();

            items.Should().HaveCount(2);
            items.Select(i => i.CustomerName).Should().Contain(new[] { "A", "B" });
        }
    }
}
