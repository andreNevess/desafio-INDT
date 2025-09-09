using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using ProposalService.Application.Proposals.DTOs;
using System.Net;
using System.Net.Http.Json;


namespace ProposalService.Tests
{
    public class ApiSmokeTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ApiSmokeTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(b =>
            {
                b.UseSolutionRelativeContentRoot("src/ProposalService.Api");
                // b.UseEnvironment("Development");
            });
        }

        [Fact]
        public async Task Post_Then_Get_ShouldWork()
        {
            var client = _factory.CreateClient();

            var createReq = new CreateProposalRequest("API Client", 777m);
            var post = await client.PostAsJsonAsync("/proposals", createReq);
            post.StatusCode.Should().Be(HttpStatusCode.Created);

            var created = await post.Content.ReadFromJsonAsync<ProposalResponse>();
            created!.CustomerName.Should().Be("API Client");

            var list = await client.GetFromJsonAsync<List<ProposalResponse>>("/proposals");
            list!.Any(p => p.Id == created.Id).Should().BeTrue();

            var patch = await client.PatchAsJsonAsync($"/proposals/{created.Id}/status",
                new ChangeStatusRequest("Approved"));
            patch.StatusCode.Should().Be(HttpStatusCode.OK);

            var updated = await patch.Content.ReadFromJsonAsync<ProposalResponse>();
            updated!.Status.Should().Be("Approved");
        }

        [Fact]
        public async Task Patch_WithInvalidStatus_ShouldReturnBadRequest()
        {
            var client = _factory.CreateClient();

            var createReq = new CreateProposalRequest("Bad Status", 10m);
            var post = await client.PostAsJsonAsync("/proposals", createReq);
            var created = await post.Content.ReadFromJsonAsync<ProposalResponse>();

            var patch = await client.PatchAsJsonAsync($"/proposals/{created!.Id}/status",
                new ChangeStatusRequest("NOPE"));
            patch.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
