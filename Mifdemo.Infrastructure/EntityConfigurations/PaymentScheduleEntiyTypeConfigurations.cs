using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mifdemo.Domain.Models;

namespace Mifdemo.Infrastructure.EntityConfigurations
{
    public class PaymentScheduleEntiyTypeConfigurations : IEntityTypeConfiguration<PaymentScheduleModel>
    {
        public void Configure(EntityTypeBuilder<PaymentScheduleModel> builder)
        {
            
        }
    }
}