using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mifdemo.Domain.Models;
using Mifdemo.Domain.Seed;

namespace Mifdemo.Infrastructure.Data
{
    public class Context : IdentityDbContext<ApplicationUserModel>
    {
        public Context(DbContextOptions<Context> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<ProductPurchasedProduct>().HasKey( c => new { c.ProductId, c.PurchasedProductId });        
            
            modelBuilder.Entity<ApplicationUserModel>()
                .HasIndex(c => c.Email)
                .IsUnique();
            
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<ClientModel> Clients { get; set; }
        public DbSet<BranchModel> Branches { get; set; }
        public DbSet<VoucherModel> Vouchers { get; set; }
        public DbSet<ProductPurchasedProduct> ProductPurchasedProducts { get; set; }
        public DbSet<PurchasedProductModel> PurchasedProducts { get; set; }
        public DbSet<AccountModel> Account { get; set; }
        public DbSet<AccountsModel> Accounts { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<FamilyMembersModel> Families { get; set; }
        public DbSet<AddressModel> Addresses { get; set; }
        public DbSet<IdentifierModel> Identifiers { get; set; }
        public DbSet<DocumentModel> Documents { get; set; }
        public DbSet<PaymentScheduleModel> PaymentSchedules { get; set; }
        public DbSet<TokenManagerModel> TokenManagers { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseAuditModel>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        break;
                        
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
