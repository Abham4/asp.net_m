using Mifdemo.Domain.Interface.RepositoryInterface;
using Mifdemo.Domain.Models;
using Mifdemo.Infrastructure.Data;

namespace Mifdemo.Infrastructure.Repository
{
    public class PaymentScheduleRepository : BaseRepository<PaymentScheduleModel>, IPaymentScheduleRepository
    {
        public PaymentScheduleRepository(Context context) : base(context)
        {
        }
    }
}