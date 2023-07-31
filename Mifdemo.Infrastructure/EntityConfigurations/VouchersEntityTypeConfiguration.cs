using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mifdemo.Domain.Models;

namespace Mifdemo.Infrastructure.EntityConfigurations
{
    internal class VouchersEntityTypeConfiguration : IEntityTypeConfiguration<VoucherModel>
    {
        public void Configure(EntityTypeBuilder<VoucherModel> builder)
        {
            builder.Property(v => v.Reason).IsRequired();
        }
    }
}