using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mifdemo.Domain.Models;

namespace Mifdemo.Infrastructure.EntityConfigurations
{
    internal class TokenManagerEntityTypeConfigurations : IEntityTypeConfiguration<TokenManagerModel>
    {
        public void Configure(EntityTypeBuilder<TokenManagerModel> builder)
        {
            builder.HasIndex(c => c.RefreshToken)
                .IsUnique();
        }
    }
}