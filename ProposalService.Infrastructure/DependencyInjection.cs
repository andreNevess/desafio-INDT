using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProposalService.Application.Proposals.UseCases;
using ProposalService.Domain.Proposals;
using ProposalService.Infrastructure.EF;
using ProposalService.Infrastructure.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProposalService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProposalInfrastructure(this IServiceCollection services, IConfiguration config)
        {

            // Use Provider from appsettings
            var provider = config.GetSection("Persistence")["Provider"] ?? "InMemory";

            if (provider.Equals("SqlServer", StringComparison.OrdinalIgnoreCase))
            {
                services.AddDbContext<ProposalDbContext>(opt =>
                    opt.UseSqlServer(config.GetConnectionString("ProposalDb")));
                services.AddScoped<IProposalRepository, EfProposalRepository>();
                services.AddScoped<IUnitOfWork, EfUnitOfWork>();
            }
            else if (provider.Equals("PostgreSql", StringComparison.OrdinalIgnoreCase))
            {
                services.AddDbContext<ProposalDbContext>(opt =>
                    opt.UseNpgsql(config.GetConnectionString("ProposalDb")));
                services.AddScoped<IProposalRepository, EfProposalRepository>();
                services.AddScoped<IUnitOfWork, EfUnitOfWork>();
            }
            else
            {
                // InMemory fallback
                services.AddSingleton<IProposalRepository, InMemoryProposalRepository>();
                services.AddSingleton<IUnitOfWork, InMemoryUnitOfWork>();
            }

            // Use cases
            services.AddScoped<ICreateProposal, CreateProposal>();
            services.AddScoped<IListProposals, ListProposals>();
            services.AddScoped<IChangeProposalStatus, ChangeProposalStatus>();

            return services;
        }
    }
}
