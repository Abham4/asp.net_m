using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mifdemo.Domain.Models;

namespace Mifdemo.Infrastructure.EntityConfigurations
{
    public class DocumentsEntityTypeConfigurations : IEntityTypeConfiguration<DocumentModel>
    {
        public void Configure(EntityTypeBuilder<DocumentModel> builder)
        {
            builder.HasIndex(c => c.FileName)
                .IsUnique();
        }
    }
}