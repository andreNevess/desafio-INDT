using ContractService.Application.Contracts;
using ContractService.Application.Contracts.UseCases;
using ContractService.Domain.Contracts;
using ContractService.Infrastructure.Http;
using ContractService.Infrastructure.InMemory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContractService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddContractInfrastructure(this IServiceCollection services, IConfiguration cfg)
        {
            // Persistence InMemory (plugar EF depois se quiser)
            services.AddSingleton<IContractRepository, InMemoryContractRepository>();
            services.AddSingleton<IUnitOfWork, InMemoryUnitOfWork>();

            // Use cases
            services.AddScoped<ICreateContract, CreateContract>();
            services.AddScoped<IListContracts, ListContracts>();

            // HTTP client para falar com o PropostaService
            var baseUrl = cfg.GetSection("ProposalService")["BaseUrl"] ?? "http://localhost:5075";
            services.AddHttpClient<IProposalStatusGateway, ProposalStatusHttpGateway>(c =>
            {
                c.BaseAddress = new Uri(baseUrl);
            });

            return services;
        }
    }
}
