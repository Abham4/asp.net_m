using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mifdemo.Domain.Models;

namespace Mifdemo.Infrastructure.EntityConfigurations
{
    public class BranchesEntityTypeConfigurations : IEntityTypeConfiguration<BranchModel>
    {
        public void Configure(EntityTypeBuilder<BranchModel> builder)
        {
            builder.HasIndex(c => c.Name)
                .IsUnique();
        }
    }
}