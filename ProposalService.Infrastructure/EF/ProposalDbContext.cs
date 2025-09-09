


using Microsoft.EntityFrameworkCore;
using ProposalService.Domain.Proposals;

namespace ProposalService.Infrastructure.EF
{
    public sealed class ProposalDbContext : DbContext
    {
        public ProposalDbContext(DbContextOptions<ProposalDbContext> options) : base(options) { }
        public DbSet<Proposal> Proposals => Set<Proposal>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var e = modelBuilder.Entity<Proposal>();
            e.ToTable("Proposals");
            e.HasKey(x => x.Id);
            e.Property(x => x.CustomerName).IsRequired().HasMaxLength(200);
            e.Property(x => x.Premium).HasColumnType("decimal(18,2)");
            e.Property(x => x.Status)
                .HasConversion<string>()        // salva como texto: UnderReview/Approved/Rejected
                .IsRequired();
            e.Property(x => x.CreatedAtUtc).IsRequired();
        }
    }

}
