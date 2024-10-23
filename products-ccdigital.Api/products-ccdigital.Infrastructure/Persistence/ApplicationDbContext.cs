using System.Reflection;
using Microsoft.EntityFrameworkCore;
using products_ccdigital.Domain.Common;
using products_ccdigital.Domain.Products;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using products_ccdigital.Application.Common.Interfaces;
using products_ccdigital.Infrastructure.Persistence.Configurations;

namespace products_ccdigital.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products => Set<Product>();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = "Admin";
                        entry.Entity.Created = DateTime.UtcNow;
                        entry.Entity.RowVersion = Guid.NewGuid();
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = "Admin";
                        entry.Entity.LastModified = DateTime.UtcNow;
                        entry.Entity.RowVersion = Guid.NewGuid();
                        break;
                }
            }

            var result = 0;

            try
            {
                result = await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Update the values of the entity that failed to save from the store (https://docs.microsoft.com/es-es/ef/ef6/saving/concurrency)
                ex.Entries.Single().Reload();
            }

            return result;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.ApplyConfiguration(new ProductConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
