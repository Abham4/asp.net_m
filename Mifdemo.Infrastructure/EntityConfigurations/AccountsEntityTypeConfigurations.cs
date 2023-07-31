using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mifdemo.Domain.Models;

namespace Mifdemo.Infrastructure.EntityConfigurations
{
    public class AccountsEntityTypeConfigurations : IEntityTypeConfiguration<AccountModel>
    {
        public void Configure(EntityTypeBuilder<AccountModel> builder)
        {
            builder.HasIndex(x => x.AccountNo)
                .IsUnique();
                
            builder.Property(v => v.AccountType)
                .IsRequired();
                
            builder.HasOne(x => x.Clients)
                .WithMany(x => x.Account)
                .HasForeignKey(x => x.ClientId);
        }
    }
}