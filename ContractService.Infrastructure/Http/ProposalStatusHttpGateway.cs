using ContractService.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ContractService.Infrastructure.Http
{
    public sealed class ProposalStatusHttpGateway : IProposalStatusGateway
    {
        private readonly HttpClient _http;

        public ProposalStatusHttpGateway(HttpClient http) => _http = http;

        public async Task<string?> GetStatusAsync(Guid proposalId, CancellationToken ct = default)
        {
            // Ideal: GET /proposals/{id} no PropostaService.
            // Se você já tiver esse endpoint, descomente a linha abaixo e remova o fallback.
            // var item = await _http.GetFromJsonAsync<ProposalDto>($"/proposals/{proposalId}", ct);
            // return item?.Status;

            // Fallback (se não tiver GET by id): lista e filtra
            var list = await _http.GetFromJsonAsync<List<ProposalDto>>($"/proposals", ct);
            var item = list?.FirstOrDefault(p => p.Id == proposalId);
            return item?.Status;
        }

        private sealed record ProposalDto(Guid Id, string CustomerName, decimal Premium, string Status, DateTime CreatedAtUtc);
    }
}
