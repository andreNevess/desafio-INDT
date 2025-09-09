using FluentAssertions;
using ProposalService.Application.Proposals.DTOs;
using ProposalService.Application.Proposals.UseCases;
using ProposalService.Domain.Proposals;
using ProposalService.Infrastructure.InMemory;


namespace ProposalService.Tests
{
    public class ChangeStatusUseCaseTests
    {
        [Fact]
        public async Task Execute_ShouldChangeStatus_WhenFound()
        {
            var repo = new InMemoryProposalRepository();
            var uow = new InMemoryUnitOfWork();

            var created = new CreateProposal(repo, uow);
            var resp = await created.ExecuteAsync(new CreateProposalRequest("Alice", 300m));

            var change = new ChangeProposalStatus(repo, uow);
            var updated = await change.ExecuteAsync(resp.Id, new ChangeStatusRequest("Approved"));

            updated.Should().NotBeNull();
            updated!.Status.Should().Be(nameof(ProposalStatus.Approved));
        }

        [Fact]
        public async Task Execute_ShouldReturnNull_WhenNotFound()
        {
            var repo = new InMemoryProposalRepository();
            var uow = new InMemoryUnitOfWork();
            var change = new ChangeProposalStatus(repo, uow);

            var updated = await change.ExecuteAsync(Guid.NewGuid(), new ChangeStatusRequest("Approved"));
            updated.Should().BeNull();
        }

        [Fact]
        public async Task Execute_InvalidStatus_ShouldThrow()
        {
            var repo = new InMemoryProposalRepository();
            var uow = new InMemoryUnitOfWork();
            var created = new CreateProposal(repo, uow);
            var resp = await created.ExecuteAsync(new CreateProposalRequest("Bob", 150m));

            var change = new ChangeProposalStatus(repo, uow);
            var act = async () => await change.ExecuteAsync(resp.Id, new ChangeStatusRequest("INVALID"));

            await act.Should().ThrowAsync<ArgumentException>();
        }
    }
}
