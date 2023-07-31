using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mifdemo.Domain.Models;

namespace Mifdemo.Infrastructure.EntityConfigurations
{
    internal class ClientsEntityTypeConfigurations : IEntityTypeConfiguration<ClientModel>
    {
        public void Configure(EntityTypeBuilder<ClientModel> builder)
        {
            builder.HasIndex(v => v.PassBookNumber)
                .IsUnique();

            builder.Property(c => c.FirstName)
                .IsRequired();

            builder.Property(c => c.LastName)
                .IsRequired();
        }
    }
}