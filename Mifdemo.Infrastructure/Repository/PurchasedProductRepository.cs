using Microsoft.EntityFrameworkCore;
using Mifdemo.Domain.Interface.RepositoryInterface;
using Mifdemo.Domain.Models;
using Mifdemo.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mifdemo.Infrastructure.Repository
{
    public class PurchasedProductRepository : BaseRepository<PurchasedProductModel>, IPurchasedProductRepository
    {
        private readonly Context _db;
        public PurchasedProductRepository(Context context):base(context)
        {
            _db = context;
        }

        public override async Task AddAsync(PurchasedProductModel purchasedProduct)
        {
            await _db.AddAsync(purchasedProduct);
            await UnitOfWork.SaveChanges();
            var e = await _db.Account.FindAsync(purchasedProduct.AccountId);
            if(e.AccountType == "Loan")
            {
                var startingDate = purchasedProduct.StartingDate;
                var rate = purchasedProduct.Rate / 12;
                var due = purchasedProduct.OriginalLoan * rate * Math.Pow(1 + rate, purchasedProduct.PaymentContrat) / 
                    (Math.Pow(1 + rate, purchasedProduct.PaymentContrat) - 1);
                due = Math.Round(due, 2);
                var interest = Math.Round(purchasedProduct.OriginalLoan * rate, 2);
                var prindue = Math.Round(due - interest, 2);
                var remain = Math.Round(purchasedProduct.OriginalLoan - prindue, 2);
                for(int i = 1; i < purchasedProduct.PaymentContrat; i++)
                {
                    var newSchedule = new PaymentScheduleModel()
                    {
                        PayingDate = startingDate,
                        PricipalDue = prindue,
                        Interest = interest,
                        Due = due,
                        LoanBalance = remain,
                        PurchasedProductId = purchasedProduct.Id,
                        ClientId = e.ClientId
                    };
                    await _db.PaymentSchedules.AddAsync(newSchedule);
                    await UnitOfWork.SaveChanges();
                    if(i != purchasedProduct.PaymentContrat - 1)
                    {
                        interest = Math.Round((float)remain * rate, 2);
                        prindue = Math.Round(due - interest, 2);
                        remain = Math.Round(remain - prindue, 2);
                    }
                    startingDate = startingDate.AddMonths(1);
                }
                interest = Math.Round((float)remain * rate, 2);
                prindue = remain;
                remain = Math.Round(remain - prindue, 2);
                var newSchedule1 = new PaymentScheduleModel()
                {
                    PayingDate = startingDate,
                    PricipalDue = prindue,
                    LoanBalance = remain,
                    Interest = Math.Round(interest+(due-interest-prindue), 2),
                    Due = due,
                    PurchasedProductId = purchasedProduct.Id,
                    ClientId = e.ClientId
                };
                await _db.PaymentSchedules.AddAsync(newSchedule1);
                await UnitOfWork.SaveChanges();
            }
        }

        public override async Task<IReadOnlyList<PurchasedProductModel>> GetAllAsync()
        {
            return await _db.PurchasedProducts
                .Include(c => c.Account)
                .Include(c => c.PaymentSchedules)
                .Include(c => c.Products)
                .ThenInclude(c => c.Product)
                .AsSingleQuery()
                .ToListAsync();
        }

        public override async Task<PurchasedProductModel> GetByIdAsync(int id)
        {
            return await _db.PurchasedProducts
                .Include(c => c.Account)
                .Include(c => c.PaymentSchedules)
                .Include(c => c.Products)
                .ThenInclude(c => c.Product)
                .AsSingleQuery()
                .SingleOrDefaultAsync(c => c.Id == id);
        }
    }
}
