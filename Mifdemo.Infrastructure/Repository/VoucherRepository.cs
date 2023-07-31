using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mifdemo.Domain.Interface.RepositoryInterface;
using Mifdemo.Domain.Models;
using Mifdemo.Infrastructure.Data;

namespace Mifdemo.Infrastructure.Repository
{
    public class VoucherRepository : BaseRepository<VoucherModel>, IVoucherRepository
    {
        private readonly Context _con;
        public VoucherRepository(Context context) : base(context)
        {
            _con = context;
        }

        public override async Task<IReadOnlyList<VoucherModel>> GetAllAsync()
        {
            return await _con.Vouchers
                .Include(c => c.Branch)
                .Include(c => c.PurchasedProduct)
                .Include(c => c.Client)
                .AsSingleQuery()
                .ToListAsync();
        }

        public override async Task AddAsync(VoucherModel voucher)
        {
            var pp = await _con.PurchasedProducts.SingleOrDefaultAsync(c => c.Id == voucher.PurchasedProductId);
            var acc = await _con.Account.SingleOrDefaultAsync(d => d.Id == pp.AccountId);
            if(acc.AccountType.Equals("Saving"))
            {
                voucher.Code = "Code 1";
                await _con.Vouchers.AddAsync(voucher);
                await UnitOfWork.SaveChanges();
            }
            else if(acc.AccountType.Equals("Sharing"))
            {
                voucher.Code = "Code 1";
                await _con.Vouchers.AddAsync(voucher);
                await UnitOfWork.SaveChanges();
            }
            else
            {
                if(voucher.Reference != 0)
                {
                    var pp2 = await _con.PurchasedProducts.SingleOrDefaultAsync(c => c.Id == voucher.Reference);
                    if (voucher.Amount > pp2.OriginalLoan)
                    {
                        throw new ApplicationException("Can't Pay");
                    }
                    pp2.OriginalLoan = pp2.OriginalLoan - (float)voucher.Amount;
                    _con.PurchasedProducts.Update(pp2);
                    await UnitOfWork.SaveChanges();
                }
         
                string [] vouchertype = {"Principal", "Interest", "Penality"};
                var vouchers = await _con.Vouchers.Where(c => c.PurchasedProductId == voucher.PurchasedProductId)
                    .Where(d => d.VoucherType == "Principal")
                    .Where(e => e.CreatedDate <= DateTime.Now)
                    .OrderBy(c => c.CreatedDate)
                    .ToListAsync();
                var vouchersPayment = await _con.PaymentSchedules
                    .Where(c => c.Paid == c.Due)
                    .OrderBy(c => c.PayingDate)
                    .ToArrayAsync();
                var rate = await _con.PurchasedProducts
                    .SingleOrDefaultAsync(c => c.Id == voucher.PurchasedProductId);
                if(vouchers.Count() > 0)
                {
                    var lastPaymentDate = DateTime.Now;
                    if(vouchers[vouchers.Count()-1].CreatedDate.Date > vouchersPayment[vouchersPayment.Count()-1].PayingDate.Date)
                        lastPaymentDate = vouchers[vouchers.Count()-1].CreatedDate.Date;
                    else
                        lastPaymentDate = vouchersPayment[vouchersPayment.Count()-1].PayingDate.Date;
                
                    var vouchers2 = _con.Vouchers.Where(c => c.PurchasedProductId == voucher.PurchasedProductId)
                        .Where(c => c.VoucherType == "Principal")
                        .Sum(c => c.Amount);
                    var remainAmount = pp.OriginalLoan - vouchers2;
                    if(remainAmount == 0)
                        throw new ApplicationException("You have finished paying your loan");
                    var dateDiff = DateTime.Now.Date - lastPaymentDate;
                    // var interest = 0.0;
                    var penality = 0.0;
                    var intOnly = (int)dateDiff.TotalDays;

                    // if(intOnly > 0)
                    //     interest = (intOnly * rate.Rate * remainAmount) / 365;
                    
                    if(intOnly > 0 && ((int)(intOnly/30) - 1) > 0)
                        penality = ((int)(intOnly / 30)) - 1 * 0.05 * remainAmount;

                    // var principal = voucher.Amount - interest - penality;

                    var date = DateTime.Parse("0001-01-01");

                    var prininte1 = await _con.PaymentSchedules
                        .Where(c => c.PurchasedProductId == voucher.PurchasedProductId)
                        .Where(c => c.PaidDate.Date == date.Date)
                        .ToArrayAsync();

                    if(penality != 0)
                    {
                        if(prininte1[0].Penality == 0)
                        {
                            prininte1[0].Penality = penality;
                            prininte1[0].Due += penality;
                            _con.PaymentSchedules.Update(prininte1[0]);
                            await UnitOfWork.SaveChanges();
                        }

                        if(voucher.Amount <= penality && prininte1[0].Paid < penality)
                        {
                            var vou4pen = new VoucherModel()
                            {
                                    Code = "Code 3",
                                    VoucherType = vouchertype[2],
                                    ClientId = voucher.ClientId,
                                    Reason = voucher.Reason,
                                    Reference = voucher.Reference,
                                    LastOpration = voucher.LastOpration,
                                    Amount = voucher.Amount,
                                    TimeStamp = voucher.TimeStamp,
                                    PurchasedProductId = voucher.PurchasedProductId
                            };
                            await _con.Vouchers.AddAsync(vou4pen);
                            await UnitOfWork.SaveChanges();
                            prininte1[0].Paid += voucher.Amount;
                            _con.PaymentSchedules.Update(prininte1[0]);
                            await UnitOfWork.SaveChanges();
                        }
                        else
                        {
                            double principal = 0.0, interest = 0.0, remain = 0.0;
                            int iterator = 1;
                            var voucherAmount = voucher.Amount - penality;

                            var payedInterestPS = _con.PaymentSchedules
                                .Where(c => c.PurchasedProductId == voucher.PurchasedProductId)
                                .Where(c => c.Due == c.Paid)
                                .Sum(c => c.Interest);
                            
                            var payedInterestV = _con.Vouchers
                                .Where(c => c.PurchasedProductId == voucher.PurchasedProductId)
                                .Where(c => c.VoucherType == "Interest")
                                .Sum(c => c.Amount);

                            if(payedInterestPS - payedInterestV == 0)
                            {
                                if(voucherAmount <= prininte1[0].Interest)
                                {
                                    // interest only voucher will be calculated
                                    var vou4int = new VoucherModel()
                                    {
                                        Code = "Code 2",
                                        VoucherType = vouchertype[1],
                                        ClientId = voucher.ClientId,
                                        Reason = voucher.Reason,
                                        Reference = voucher.Reference,
                                        LastOpration = voucher.LastOpration,
                                        Amount = voucherAmount,
                                        TimeStamp = voucher.TimeStamp,
                                        PurchasedProductId = voucher.PurchasedProductId
                                    };
                                    await _con.Vouchers.AddAsync(vou4int);
                                    await UnitOfWork.SaveChanges();
                                    prininte1[0].Paid += voucherAmount;
                                    if(prininte1[0].Paid == prininte1[0].Due)
                                        prininte1[0].PaidDate = vou4int.CreatedDate;
                                    _con.PaymentSchedules.Update(prininte1[0]);
                                    await UnitOfWork.SaveChanges();
                                    iterator = 0;
                                }
                                else
                                {
                                    principal = voucherAmount - prininte1[0].Interest;
                                    interest = prininte1[0].Interest;
                                    if((int)voucher.Amount / prininte1[0].Due > 0)
                                        iterator = (int)(voucher.Amount / prininte1[0].Due);
                                    if((int)voucher.Amount / prininte1[0].Due > prininte1.Count())
                                        iterator = prininte1.Count();
                                    remain = Math.Round(voucher.Amount % prininte1[0].Due, 2);
                                    // principal and interest will be calculated and iteration value will be known
                                }
                            }

                            else
                            {
                                var firstDept = payedInterestPS - payedInterestV;
                                if(voucher.Amount <= firstDept)
                                {
                                    // interest only voucher will be calculated
                                    var vou4int = new VoucherModel()
                                    {
                                        Code = "Code 2",
                                        VoucherType = vouchertype[1],
                                        ClientId = voucher.ClientId,
                                        Reason = voucher.Reason,
                                        Reference = voucher.Reference,
                                        LastOpration = voucher.LastOpration,
                                        Amount = voucher.Amount,
                                        TimeStamp = voucher.TimeStamp,
                                        PurchasedProductId = voucher.PurchasedProductId
                                    };
                                    await _con.Vouchers.AddAsync(vou4int);
                                    await UnitOfWork.SaveChanges();
                                    prininte1[0].Paid += voucher.Amount;
                                    if(prininte1[0].Paid == prininte1[0].Due)
                                        prininte1[0].PaidDate = vou4int.CreatedDate;
                                    _con.PaymentSchedules.Update(prininte1[0]);
                                    await UnitOfWork.SaveChanges();
                                    iterator = 0;
                                }
                                else
                                {
                                    principal = voucher.Amount - firstDept;
                                    interest = firstDept;
                                    if((int)voucher.Amount / prininte1[0].Due > 0)
                                        iterator = (int)(voucher.Amount / prininte1[0].Due);
                                    if((int)voucher.Amount / prininte1[0].Due > prininte1.Count())
                                    {
                                        iterator = prininte1.Count();
                                        remain = 0;
                                    }
                                    else
                                    remain = Math.Round(voucher.Amount % prininte1[0].Due, 2);
                                    // pricipal and interest voucher will be calculated and iteration value will be known
                                }
                            }

                            double [] amount = {principal, interest};
                            
                            var vouche = new VoucherModel();

                            for (int l = 0; l < iterator; l++)
                            {
                                if(l>0)
                                {
                                    amount[0] = prininte1[l].PricipalDue;
                                    amount[1] = prininte1[l].Interest;
                                }
                                
                                for( int i = 0; i < 2; i++)
                                {
                                    vouche = new VoucherModel()
                                    {
                                        Code = "Code "+(i+1),
                                        VoucherType = vouchertype[i],
                                        ClientId = voucher.ClientId,
                                        Reason = voucher.Reason,
                                        Reference = voucher.Reference,
                                        LastOpration = voucher.LastOpration,
                                        Amount = amount[i],
                                        TimeStamp = voucher.TimeStamp,
                                        PurchasedProductId = voucher.PurchasedProductId
                                    };
                                    await _con.Vouchers.AddAsync(vouche);
                                    await UnitOfWork.SaveChanges();
                                }    

                                prininte1[l].Paid += amount[0] + amount[1];
                                prininte1[l].PaidDate = vouche.CreatedDate;
                                _con.PaymentSchedules.Update(prininte1[l]);
                                await UnitOfWork.SaveChanges();
                            }
                            if(remain != 0)
                            {
                                int iter = 1;
                                double [] am = {0.0};
                                double ams = 0.0;
                                if(remain <= prininte1[iterator].Interest)
                                {
                                    am[0] = remain;
                                }
                                else
                                {
                                    am[0] = Math.Round(remain - prininte1[iterator].Interest, 2);
                                    am[1] = prininte1[iterator].Interest;
                                    iter = 2;
                                }

                                var vo = new VoucherModel();
                                
                                for (int i = 0; i < iter; i++)
                                {
                                    vo = new VoucherModel()
                                    {
                                        Code = "Code "+(i+1),
                                        VoucherType = vouchertype[i],
                                        ClientId = voucher.ClientId,
                                        Reason = voucher.Reason,
                                        Reference = voucher.Reference,
                                        LastOpration = voucher.LastOpration,
                                        Amount = am[i],
                                        TimeStamp = voucher.TimeStamp,
                                        PurchasedProductId = voucher.PurchasedProductId
                                    };
                                    await _con.Vouchers.AddAsync(vo);
                                    await UnitOfWork.SaveChanges();
                                    ams += am[i];
                                }

                                prininte1[iterator].Paid = Math.Round(ams, 2);
                                prininte1[iterator].PaidDate = vo.CreatedDate;
                                _con.PaymentSchedules.Update(prininte1[iterator]);
                                await UnitOfWork.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        double principal = 0.0, interest = 0.0, remain = 0.0;
                        int iterator = 1;

                        var payedInterestPS = _con.PaymentSchedules
                            .Where(c => c.PurchasedProductId == voucher.PurchasedProductId)
                            .Where(c => c.Paid >= c.Interest)
                            .Sum(c => c.Interest);
                        
                        var payedInterestV = _con.Vouchers
                            .Where(c => c.PurchasedProductId == voucher.PurchasedProductId)
                            .Where(c => c.VoucherType == "Interest")
                            .Sum(c => c.Amount);

                        if(payedInterestPS - payedInterestV == 0)
                        {
                            if(prininte1[0].Paid == 0.0)
                            {
                                if(voucher.Amount <= prininte1[0].Interest)
                                {
                                    // interest only voucher will be calculated
                                    var vou4int = new VoucherModel()
                                    {
                                        Code = "Code 2",
                                        VoucherType = vouchertype[1],
                                        ClientId = voucher.ClientId,
                                        Reason = voucher.Reason,
                                        Reference = voucher.Reference,
                                        LastOpration = voucher.LastOpration,
                                        Amount = voucher.Amount,
                                        TimeStamp = voucher.TimeStamp,
                                        PurchasedProductId = voucher.PurchasedProductId,
                                        BranchId = voucher.BranchId
                                    };
                                    await _con.Vouchers.AddAsync(vou4int);
                                    await UnitOfWork.SaveChanges();
                                    prininte1[0].Paid += voucher.Amount;
                                    if(prininte1[0].Paid == prininte1[0].Due)
                                        prininte1[0].PaidDate = vou4int.CreatedDate;
                                    _con.PaymentSchedules.Update(prininte1[0]);
                                    await UnitOfWork.SaveChanges();
                                    iterator = 0;
                                }
                                else
                                {
                                    if(voucher.Amount - prininte1[0].Interest <= prininte1[0].PricipalDue)
                                    {
                                        var v = new VoucherModel();
                                        double [] amo = {voucher.Amount - prininte1[0].Interest, prininte1[0].Interest};
                                        for( int i = 0; i < 2; i++)
                                        {
                                            v = new VoucherModel()
                                            {
                                                Code = "Code "+(i+1),
                                                VoucherType = vouchertype[i],
                                                ClientId = voucher.ClientId,
                                                Reason = voucher.Reason,
                                                Reference = voucher.Reference,
                                                LastOpration = voucher.LastOpration,
                                                Amount = Math.Round(amo[i], 2),
                                                TimeStamp = voucher.TimeStamp,
                                                PurchasedProductId = voucher.PurchasedProductId,
                                                BranchId = voucher.BranchId
                                            };
                                            await _con.Vouchers.AddAsync(v);
                                            await UnitOfWork.SaveChanges();
                                        }    
                                        prininte1[0].Paid = voucher.Amount;
                                        if(prininte1[0].Paid == prininte1[0].Due)
                                            prininte1[0].PaidDate = v.CreatedDate;
                                        _con.PaymentSchedules.Update(prininte1[0]);
                                        await UnitOfWork.SaveChanges();
                                        iterator = 0;
                                    }
                                    else
                                    {
                                        var v = new VoucherModel();
                                        var lyu = voucher.Amount - prininte1[0].Due;
                                        double [] amo = {prininte1[0].PricipalDue, prininte1[0].Interest};
                                        for( int i = 0; i < 2; i++)
                                        {
                                            v = new VoucherModel()
                                            {
                                                Code = "Code "+(i+1),
                                                VoucherType = vouchertype[i],
                                                ClientId = voucher.ClientId,
                                                Reason = voucher.Reason,
                                                Reference = voucher.Reference,
                                                LastOpration = voucher.LastOpration,
                                                Amount = amo[i],
                                                TimeStamp = voucher.TimeStamp,
                                                PurchasedProductId = voucher.PurchasedProductId,
                                                BranchId = voucher.BranchId
                                            };
                                            await _con.Vouchers.AddAsync(v);
                                            await UnitOfWork.SaveChanges();
                                        }    
                                        prininte1[0].Paid = prininte1[0].Due;
                                        prininte1[0].PaidDate = v.CreatedDate;
                                        _con.PaymentSchedules.Update(prininte1[0]);
                                        await UnitOfWork.SaveChanges();

                                        if(lyu <= prininte1[1].Interest)
                                        {
                                            principal = 0;
                                            interest = lyu;
                                        }
                                        else if(lyu - prininte1[1].Interest <= prininte1[1].PricipalDue)
                                        {
                                            principal = lyu - prininte1[1].Interest;
                                            interest = prininte1[1].Interest;
                                        }
                                        else
                                        {
                                            principal = prininte1[1].PricipalDue;
                                            interest = prininte1[1].Interest;
                                            if((int)(lyu / prininte1[1].Due) > 0)
                                                iterator = (int)(lyu / prininte1[1].Due);
                                            if((int)lyu / prininte1[1].Due > prininte1.Count() -1)
                                            {
                                                iterator = prininte1.Count()-1;
                                                remain = 0;
                                            }
                                            if(lyu > prininte1[0].Due)
                                                remain = Math.Round(lyu % prininte1[0].Due, 2);
                                        }
                                    }
                                    // principal and interest will be calculated and iteration value will be known
                                }
                            }
                            else
                            {
                                if(voucher.Amount <= prininte1[0].PricipalDue - (prininte1[0].Paid - prininte1[0].Interest))
                                {
                                    var v = new VoucherModel()
                                    {
                                        Code = "Code 1",
                                        VoucherType = vouchertype[0],
                                        ClientId = voucher.ClientId,
                                        Reason = voucher.Reason,
                                        Reference = voucher.Reference,
                                        LastOpration = voucher.LastOpration,
                                        Amount = voucher.Amount,
                                        TimeStamp = voucher.TimeStamp,
                                        PurchasedProductId = voucher.PurchasedProductId,
                                        BranchId = voucher.BranchId
                                    };
                                    await _con.Vouchers.AddAsync(v);
                                    await UnitOfWork.SaveChanges();   
                                    prininte1[0].Paid += voucher.Amount;
                                    if(prininte1[0].Due == prininte1[0].Paid)
                                        prininte1[0].PaidDate = v.CreatedDate;
                                    _con.PaymentSchedules.Update(prininte1[0]);
                                    await UnitOfWork.SaveChanges();
                                    iterator = 0;
                                }
                                else
                                {
                                    var remainPrincipal = prininte1[0].PricipalDue - (prininte1[0].Paid - prininte1[0].Interest);
                                    var lyu = voucher.Amount - remainPrincipal;
                                    var v = new VoucherModel()
                                    {
                                        Code = "Code 1",
                                        VoucherType = vouchertype[0],
                                        ClientId = voucher.ClientId,
                                        Reason = voucher.Reason,
                                        Reference = voucher.Reference,
                                        LastOpration = voucher.LastOpration,
                                        Amount = Math.Round(remainPrincipal),
                                        TimeStamp = voucher.TimeStamp,
                                        PurchasedProductId = voucher.PurchasedProductId,
                                        BranchId = voucher.BranchId
                                    };
                                    await _con.Vouchers.AddAsync(v);
                                    await UnitOfWork.SaveChanges();   
                                    prininte1[0].Paid = prininte1[0].Due;
                                    prininte1[0].PaidDate = v.CreatedDate;
                                    _con.PaymentSchedules.Update(prininte1[0]);
                                    await UnitOfWork.SaveChanges();

                                    if(lyu <= prininte1[1].Interest)
                                    {
                                        principal = 0;
                                        interest = lyu;
                                    }
                                    else if(lyu - prininte1[1].Interest <= prininte1[1].PricipalDue)
                                    {
                                        principal = lyu - prininte1[1].Interest;
                                        interest = prininte1[1].Interest;
                                    }
                                    else
                                    {
                                        principal = prininte1[1].PricipalDue;
                                        interest = prininte1[1].Interest;
                                        if((int)lyu / prininte1[1].Due > 0)
                                            iterator = (int)(lyu / prininte1[1].Due);
                                        if((int)lyu / prininte1[1].Due > prininte1.Count() -1)
                                        {
                                            iterator = prininte1.Count() -1;
                                            remain = 0;
                                        }
                                        if(lyu > prininte1[0].Due && ((int)(lyu / prininte1[0].Due) < prininte1.Count()-1))
                                        remain = Math.Round(lyu % prininte1[0].Due, 2);
                                    }
                                }
                            }
                        }

                        else
                        {
                            var firstDept = prininte1[0].Interest + payedInterestPS - payedInterestV;  
                            
                            firstDept = Math.Round(firstDept, 2);

                            if(voucher.Amount <= firstDept)
                            {
                                // interest only voucher will be calculated
                                var vou4int = new VoucherModel()
                                {
                                    Code = "Code 2",
                                    VoucherType = vouchertype[1],
                                    ClientId = voucher.ClientId,
                                    Reason = voucher.Reason,
                                    Reference = voucher.Reference,
                                    LastOpration = voucher.LastOpration,
                                    Amount = voucher.Amount,
                                    TimeStamp = voucher.TimeStamp,
                                    PurchasedProductId = voucher.PurchasedProductId,
                                    BranchId = voucher.BranchId
                                };
                                await _con.Vouchers.AddAsync(vou4int);
                                await UnitOfWork.SaveChanges();
                                prininte1[0].Paid += voucher.Amount;
                                if(prininte1[0].Paid == prininte1[0].Due)
                                    prininte1[0].PaidDate = vou4int.CreatedDate;
                                _con.PaymentSchedules.Update(prininte1[0]);
                                await UnitOfWork.SaveChanges();
                                iterator = 0;
                            }
                            else
                            {
                                if(voucher.Amount - firstDept <= prininte1[0].PricipalDue)
                                {
                                    double [] rrr = {voucher.Amount - firstDept, firstDept};
                                    var vou4int = new VoucherModel();
                                    for (int i = 0; i < 2; i++)
                                    {
                                        vou4int = new VoucherModel()
                                        {
                                            Code = "Code "+(i+1),
                                            VoucherType = vouchertype[i],
                                            ClientId = voucher.ClientId,
                                            Reason = voucher.Reason,
                                            Reference = voucher.Reference,
                                            LastOpration = voucher.LastOpration,
                                            Amount = rrr[i],
                                            TimeStamp = voucher.TimeStamp,
                                            PurchasedProductId = voucher.PurchasedProductId,
                                            BranchId = voucher.BranchId
                                        };
                                        await _con.Vouchers.AddAsync(vou4int);
                                        await UnitOfWork.SaveChanges();
                                    }
                                    prininte1[0].Paid += voucher.Amount;
                                    if(prininte1[0].Paid == prininte1[0].Due)
                                        prininte1[0].PaidDate = vou4int.CreatedDate;
                                    _con.PaymentSchedules.Update(prininte1[0]);
                                    await UnitOfWork.SaveChanges();
                                }
                                else
                                {
                                    var lyu = voucher.Amount - (prininte1[0].PricipalDue + firstDept);
                                    double [] rrr = {prininte1[0].PricipalDue, firstDept};
                                    var vou4int = new VoucherModel();
                                    for (int i = 0; i < 2; i++)
                                    {
                                        vou4int = new VoucherModel()
                                        {
                                            Code = "Code "+(i+1),
                                            VoucherType = vouchertype[i],
                                            ClientId = voucher.ClientId,
                                            Reason = voucher.Reason,
                                            Reference = voucher.Reference,
                                            LastOpration = voucher.LastOpration,
                                            Amount = Math.Round(rrr[i], 2),
                                            TimeStamp = voucher.TimeStamp,
                                            PurchasedProductId = voucher.PurchasedProductId,
                                            BranchId = voucher.BranchId
                                        };
                                        await _con.Vouchers.AddAsync(vou4int);
                                        await UnitOfWork.SaveChanges();
                                    }
                                    prininte1[0].Paid = prininte1[0].Due;
                                    prininte1[0].PaidDate = vou4int.CreatedDate;
                                    _con.PaymentSchedules.Update(prininte1[0]);
                                    await UnitOfWork.SaveChanges();
                                    if(lyu <= prininte1[1].Interest)
                                    {
                                        vou4int = new VoucherModel()
                                        {
                                            Code = "Code 2",
                                            VoucherType = vouchertype[1],
                                            ClientId = voucher.ClientId,
                                            Reason = voucher.Reason,
                                            Reference = voucher.Reference,
                                            LastOpration = voucher.LastOpration,
                                            Amount = Math.Round(lyu, 2),
                                            TimeStamp = voucher.TimeStamp,
                                            PurchasedProductId = voucher.PurchasedProductId,
                                            BranchId = voucher.BranchId
                                        };
                                        await _con.Vouchers.AddAsync(vou4int);
                                        await UnitOfWork.SaveChanges();
                                        prininte1[1].Paid += lyu;
                                        _con.PaymentSchedules.Update(prininte1[1]);
                                        await UnitOfWork.SaveChanges();
                                    }
                                    else
                                    {
                                        if(lyu - prininte1[1].Interest <= prininte1[1].PricipalDue)
                                        {
                                            principal = lyu - prininte1[1].Interest;
                                            interest = prininte1[1].Interest;
                                        }
                                        else
                                        {
                                            principal = prininte1[1].PricipalDue;
                                            interest = prininte1[1].Interest;
                                            if((int)lyu / prininte1[1].Due > 0)
                                            iterator = (int)(lyu / prininte1[1].Due);
                                            if((int)lyu / prininte1[1].Due > prininte1.Count() -1)
                                            {
                                                iterator = prininte1.Count() -1;
                                            }
                                            if(lyu > prininte1[0].Due && ((int)(lyu / prininte1[0].Due) < prininte1.Count()-1))
                                            remain = Math.Round(lyu % prininte1[0].Due, 2);
                                        }
                                    }
                                }
                                
                                // pricipal and interest voucher will be calculated and iteration value will be known
                            }
                        }

                        double [] amount = {principal, interest};
                        
                        var vouche = new VoucherModel();

                        for (int l = 1; l <= iterator; l++)
                        {
                            if(l>1)
                            {
                                amount[0] = prininte1[l].PricipalDue;
                                amount[1] = prininte1[l].Interest;
                            }
                            
                            for( int i = 0; i < 2; i++)
                            {
                                vouche = new VoucherModel()
                                {
                                    Code = "Code "+(i+1),
                                    VoucherType = vouchertype[i],
                                    ClientId = voucher.ClientId,
                                    Reason = voucher.Reason,
                                    Reference = voucher.Reference,
                                    LastOpration = voucher.LastOpration,
                                    Amount = Math.Round(amount[i], 2),
                                    TimeStamp = voucher.TimeStamp,
                                    PurchasedProductId = voucher.PurchasedProductId,
                                    BranchId = voucher.BranchId
                                };
                                await _con.Vouchers.AddAsync(vouche);
                                await UnitOfWork.SaveChanges();
                            }    

                            double pPaid = Math.Round(amount[0] + amount[1], 2);
                            double temp = prininte1[l].Paid;
                            temp += pPaid;
                            if(pPaid == prininte1[l].Due || temp >= prininte1[1].Due)
                            {
                                prininte1[l].Paid = pPaid;
                                prininte1[l].PaidDate = vouche.CreatedDate;
                            }
                            else
                                prininte1[1].Paid = temp;
                            _con.PaymentSchedules.Update(prininte1[l]);
                            await UnitOfWork.SaveChanges();
                        }
                        if(remain != 0)
                        {
                            double [] am = {0.0, 0.0};
                            double ams = 0.0;
                            if(remain <= prininte1[iterator+1].Interest)
                            {
                                var voi = new VoucherModel()
                                {
                                    Code = "Code 2",
                                    VoucherType = vouchertype[1],
                                    ClientId = voucher.ClientId,
                                    Reason = voucher.Reason,
                                    Reference = voucher.Reference,
                                    LastOpration = voucher.LastOpration,
                                    Amount = remain,
                                    TimeStamp = voucher.TimeStamp,
                                    PurchasedProductId = voucher.PurchasedProductId,
                                    BranchId = voucher.BranchId
                                };
                                await _con.Vouchers.AddAsync(voi);
                                await UnitOfWork.SaveChanges();
                                prininte1[iterator+1].Paid += Math.Round(remain, 2);
                                _con.PaymentSchedules.Update(prininte1[iterator+1]);
                                await UnitOfWork.SaveChanges();
                            }
                            else
                            {
                                am[0] = Math.Round(remain - prininte1[iterator+1].Interest, 2);
                                am[1] = prininte1[iterator+1].Interest;

                                var vo = new VoucherModel();
                                
                                for (int i = 0; i < 2; i++)
                                {
                                    vo = new VoucherModel()
                                    {
                                        Code = "Code "+(i+1),
                                        VoucherType = vouchertype[i],
                                        ClientId = voucher.ClientId,
                                        Reason = voucher.Reason,
                                        Reference = voucher.Reference,
                                        LastOpration = voucher.LastOpration,
                                        Amount = am[i],
                                        TimeStamp = voucher.TimeStamp,
                                        PurchasedProductId = voucher.PurchasedProductId,
                                        BranchId = voucher.BranchId
                                    };
                                    await _con.Vouchers.AddAsync(vo);
                                    await UnitOfWork.SaveChanges();
                                    ams += am[i];
                                }

                                prininte1[iterator+1].Paid = Math.Round(ams, 2);
                                _con.PaymentSchedules.Update(prininte1[iterator+1]);
                                await UnitOfWork.SaveChanges();
                            }
                        }
                    }
                }
                // Creating New Voucher
                else
                {
                    var x = await _con.PurchasedProducts
                        .SingleOrDefaultAsync(c => c.Id == voucher.PurchasedProductId);
                    var dateDiff2 = DateTime.Now - x.CreatedDate;
                    // var interest2 = 0.0;
                    var penality2 = 0.0;
                    var intOnly2 = (int)dateDiff2.TotalDays;

                    // if(intOnly2 > 0)
                    //     interest2 = (intOnly2 * rate.Rate * pp.OriginalLoan) / 365;
                    
                    if(intOnly2 > 0 && ((intOnly2/30) - 1) > 0)
                        penality2 = ((int)(intOnly2 / 30)) - 1 * 0.05 * pp.OriginalLoan;

                    // var principal2 = voucher.Amount - interest2 - penality2;

                    var date = DateTime.Parse("0001-01-01");

                    var prininte = await _con.PaymentSchedules
                        .Where(c => c.PurchasedProductId == voucher.PurchasedProductId)
                        .Where(c => c.PaidDate.Date == date.Date)
                        .ToArrayAsync();

                    if(penality2 != 0)
                    {
                        prininte[0].Penality = penality2;
                        prininte[0].Due += penality2;
                        _con.PaymentSchedules.Update(prininte[0]);
                        await UnitOfWork.SaveChanges();

                        if(voucher.Amount <= penality2)
                        {
                            var vou4pen = new VoucherModel()
                            {
                                Code = "Code 3",
                                VoucherType = vouchertype[2],
                                ClientId = voucher.ClientId,
                                Reason = voucher.Reason,
                                Reference = voucher.Reference,
                                LastOpration = voucher.LastOpration,
                                Amount = voucher.Amount,
                                TimeStamp = voucher.TimeStamp,
                                PurchasedProductId = voucher.PurchasedProductId
                            };
                            await _con.Vouchers.AddAsync(vou4pen);
                            await UnitOfWork.SaveChanges();
                            prininte[0].Paid += voucher.Amount;
                            _con.PaymentSchedules.Update(prininte[0]);
                            await UnitOfWork.SaveChanges();
                        }

                        else
                        {
                            double principal = 0.0, interest = 0.0, remain = 0.0;
                            int iterator = 1;
                            var voucherAmount = voucher.Amount - penality2;
                            if(voucherAmount <= prininte[0].Interest)
                            {
                                // interest only voucher will be calculated
                                var vou4int = new VoucherModel()
                                {
                                    Code = "Code 2",
                                    VoucherType = vouchertype[1],
                                    ClientId = voucher.ClientId,
                                    Reason = voucher.Reason,
                                    Reference = voucher.Reference,
                                    LastOpration = voucher.LastOpration,
                                    Amount = voucherAmount,
                                    TimeStamp = voucher.TimeStamp,
                                    PurchasedProductId = voucher.PurchasedProductId
                                };
                                await _con.Vouchers.AddAsync(vou4int);
                                await UnitOfWork.SaveChanges();
                                prininte[0].Paid += voucherAmount;
                                if(prininte[0].Paid == prininte[0].Due)
                                    prininte[0].PaidDate = vou4int.CreatedDate;
                                _con.PaymentSchedules.Update(prininte[0]);
                                await UnitOfWork.SaveChanges();
                                iterator = 0;
                            }
                            else
                            {
                                principal = voucherAmount - prininte[0].Interest;
                                interest = prininte[0].Interest;
                                if((int)voucher.Amount / prininte[0].Due > 0)
                                    iterator = (int)(voucher.Amount / prininte[0].Due);
                                if((int)voucher.Amount / prininte[0].Due > prininte.Count())
                                {
                                    iterator = prininte.Count();
                                    remain = 0;
                                }
                                if(voucherAmount > prininte[0].Due)
                                remain = Math.Round(voucher.Amount % prininte[0].Due, 2);
                                // principal and interest will be calculated and iteration value will be known
                            }

                            double [] amount = {principal, interest};
                        
                            var vouche = new VoucherModel();

                            for (int l = 0; l < iterator; l++)
                            {
                                if(l>0)
                                {
                                    amount[0] = prininte[l].PricipalDue;
                                    amount[1] = prininte[l].Interest;
                                }
                                
                                for( int i = 0; i < 2; i++)
                                {
                                    vouche = new VoucherModel()
                                    {
                                        Code = "Code "+(i+1),
                                        VoucherType = vouchertype[i],
                                        ClientId = voucher.ClientId,
                                        Reason = voucher.Reason,
                                        Reference = voucher.Reference,
                                        LastOpration = voucher.LastOpration,
                                        Amount = amount[i],
                                        TimeStamp = voucher.TimeStamp,
                                        PurchasedProductId = voucher.PurchasedProductId
                                    };
                                    await _con.Vouchers.AddAsync(vouche);
                                    await UnitOfWork.SaveChanges();
                                }    

                                prininte[l].Paid = amount[0] + amount[1];
                                prininte[l].PaidDate = vouche.CreatedDate;
                                _con.PaymentSchedules.Update(prininte[l]);
                                await UnitOfWork.SaveChanges();
                            }
                            if(remain != 0)
                            {
                                int iter = 1;
                                double [] am = {0.0};
                                double ams = 0.0;
                                if(remain <= prininte[iterator].Interest)
                                {
                                    am[0] = remain;
                                }
                                else
                                {
                                    am[0] = Math.Round(remain - prininte[iterator].Interest, 2);
                                    am[1] = prininte[iterator].Interest;
                                    iter = 2;
                                }

                                var vo = new VoucherModel();
                                
                                for (int i = 0; i < iter; i++)
                                {
                                    vo = new VoucherModel()
                                    {
                                        Code = "Code "+(i+1),
                                        VoucherType = vouchertype[i],
                                        ClientId = voucher.ClientId,
                                        Reason = voucher.Reason,
                                        Reference = voucher.Reference,
                                        LastOpration = voucher.LastOpration,
                                        Amount = am[i],
                                        TimeStamp = voucher.TimeStamp,
                                        PurchasedProductId = voucher.PurchasedProductId
                                    };
                                    await _con.Vouchers.AddAsync(vo);
                                    await UnitOfWork.SaveChanges();
                                    ams += am[i];
                                }

                                prininte[iterator].Paid = Math.Round(ams, 2);
                                prininte[iterator].PaidDate = vo.CreatedDate;
                                _con.PaymentSchedules.Update(prininte[iterator]);
                                await UnitOfWork.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        double principal = 0.0, interest = 0.0, remain = 0.0;
                        int iterator = 1;

                        if(voucher.Amount <= prininte[0].Interest)
                        {
                            // interest only voucher will be calculated
                            var vou4int = new VoucherModel()
                            {
                                Code = "Code 2",
                                VoucherType = vouchertype[1],
                                ClientId = voucher.ClientId,
                                Reason = voucher.Reason,
                                Reference = voucher.Reference,
                                LastOpration = voucher.LastOpration,
                                Amount = voucher.Amount,
                                TimeStamp = voucher.TimeStamp,
                                PurchasedProductId = voucher.PurchasedProductId,
                                BranchId = voucher.BranchId
                            };
                            await _con.Vouchers.AddAsync(vou4int);
                            await UnitOfWork.SaveChanges();
                            prininte[0].Paid += voucher.Amount;
                            _con.PaymentSchedules.Update(prininte[0]);
                            await UnitOfWork.SaveChanges();
                            iterator = 0;
                        }
                        else
                        {
                            if(voucher.Amount - prininte[0].Interest <= prininte[0].PricipalDue)
                            {
                                var v = new VoucherModel();
                                double [] amo = {voucher.Amount - prininte[0].Interest, prininte[0].Interest};
                                for( int i = 0; i < 2; i++)
                                {
                                    v = new VoucherModel()
                                    {
                                        Code = "Code "+(i+1),
                                        VoucherType = vouchertype[i],
                                        ClientId = voucher.ClientId,
                                        Reason = voucher.Reason,
                                        Reference = voucher.Reference,
                                        LastOpration = voucher.LastOpration,
                                        Amount = amo[i],
                                        TimeStamp = voucher.TimeStamp,
                                        PurchasedProductId = voucher.PurchasedProductId,
                                        BranchId = voucher.BranchId
                                    };
                                    await _con.Vouchers.AddAsync(v);
                                    await UnitOfWork.SaveChanges();
                                }
                                prininte[0].Paid += voucher.Amount;
                                if(prininte[0].Paid == prininte[0].Due)
                                    prininte[0].PaidDate = v.CreatedDate;
                                _con.PaymentSchedules.Update(prininte[0]);
                                await UnitOfWork.SaveChanges();
                                iterator = 0;
                            }
                            else
                            {
                                var v = new VoucherModel();
                                var lyu = voucher.Amount - prininte[0].Due;
                                double [] amo = {prininte[0].PricipalDue, prininte[0].Interest};
                                for( int i = 0; i < 2; i++)
                                {
                                    v = new VoucherModel()
                                    {
                                        Code = "Code "+(i+1),
                                        VoucherType = vouchertype[i],
                                        ClientId = voucher.ClientId,
                                        Reason = voucher.Reason,
                                        Reference = voucher.Reference,
                                        LastOpration = voucher.LastOpration,
                                        Amount = amo[i],
                                        TimeStamp = voucher.TimeStamp,
                                        PurchasedProductId = voucher.PurchasedProductId,
                                        BranchId = voucher.BranchId
                                    };
                                    await _con.Vouchers.AddAsync(v);
                                    await UnitOfWork.SaveChanges();
                                }    
                                prininte[0].Paid = prininte[0].Due;
                                prininte[0].PaidDate = v.CreatedDate;
                                _con.PaymentSchedules.Update(prininte[0]);
                                await UnitOfWork.SaveChanges();

                                if(lyu <= prininte[1].Interest)
                                {
                                    principal = 0;
                                    interest = lyu;
                                }
                                else if(lyu - prininte[1].Interest <= prininte[1].PricipalDue)
                                {
                                    principal = lyu - prininte[1].Interest;
                                    interest = prininte[1].Interest;
                                }
                                else
                                {
                                    principal = prininte[1].PricipalDue;
                                    interest = prininte[1].Interest;
                                    if((int)(lyu / prininte[1].Due) > 0)
                                        iterator = (int)(lyu / prininte[1].Due);
                                    if((int)(lyu / prininte[1].Due) > prininte.Count() - 1)
                                    {
                                        iterator = prininte.Count()-1;
                                    }
                                    if(lyu > prininte[0].Due && ((int)(lyu / prininte[0].Due) < prininte.Count()-1))
                                    remain = Math.Round(lyu % prininte[0].Due, 2);
                                }
                            }
                            // principal and interest will be calculated and iteration value will be known
                        }

                        double [] amount = {principal, interest};
                      
                        var vouche = new VoucherModel();

                        for (int l = 1; l <= iterator; l++)
                        {
                            if(l>1)
                            {
                                amount[0] = prininte[l].PricipalDue;
                                amount[1] = prininte[l].Interest;
                            }
                            
                            for( int i = 0; i < 2; i++)
                            {
                                vouche = new VoucherModel()
                                {
                                    Code = "Code "+(i+1),
                                    VoucherType = vouchertype[i],
                                    ClientId = voucher.ClientId,
                                    Reason = voucher.Reason,
                                    Reference = voucher.Reference,
                                    LastOpration = voucher.LastOpration,
                                    Amount = Math.Round(amount[i], 2),
                                    TimeStamp = voucher.TimeStamp,
                                    PurchasedProductId = voucher.PurchasedProductId,
                                    BranchId = voucher.BranchId
                                };
                                await _con.Vouchers.AddAsync(vouche);
                                await UnitOfWork.SaveChanges();
                            }    

                            double pPaid = Math.Round(amount[0] + amount[1], 2);
                            double temp = prininte[l].Paid;
                            temp += pPaid;
                            if(pPaid == prininte[l].Due || temp >= prininte[l].Due)
                            {
                                prininte[l].Paid = pPaid;
                                prininte[l].PaidDate = vouche.CreatedDate;
                            }
                            else
                            prininte[l].Paid = temp;
                            _con.PaymentSchedules.Update(prininte[l]);
                            await UnitOfWork.SaveChanges();
                        }
                        
                        if(remain != 0.0)
                        {
                            int iter = 1;
                            double [] am = {0.0, 0.0};
                            double ams = 0.0;
                            if(remain <= prininte[iterator+1].Interest)
                            {
                                am[0] = remain;
                            }
                            else
                            {
                                am[0] = Math.Round(remain - prininte[iterator+1].Interest, 2);
                                am[1] = prininte[iterator+1].Interest;
                                iter = 2;
                            }

                            var vo = new VoucherModel();

                            for (int i = 0; i < iter; i++)
                            {
                                vo = new VoucherModel()
                                {
                                    Code = "Code "+(i+1),
                                    VoucherType = vouchertype[i],
                                    ClientId = voucher.ClientId,
                                    Reason = voucher.Reason,
                                    Reference = voucher.Reference,
                                    LastOpration = voucher.LastOpration,
                                    Amount = am[i],
                                    TimeStamp = voucher.TimeStamp,
                                    PurchasedProductId = voucher.PurchasedProductId,
                                    BranchId = voucher.BranchId                                };
                                await _con.Vouchers.AddAsync(vo);
                                await UnitOfWork.SaveChanges();
                                ams += am[i];
                            }

                            prininte[iterator+1].Paid += Math.Round(ams, 2);
                            _con.PaymentSchedules.Update(prininte[iterator+1]);
                            await UnitOfWork.SaveChanges();
                        }
                    }
                }
            }
        }

        public override async Task<VoucherModel> GetByIdAsync(int id)
        {
            return await _con.Vouchers
                .Include(c => c.Branch)
                .Include(c => c.PurchasedProduct)
                .Include(c => c.Client)
                .AsSingleQuery()
                .SingleOrDefaultAsync(c => c.PurchasedProductId == id);
        }

        public async Task<IReadOnlyList<VoucherModel>> GetVouchersListByClient(int clientId)
        {
            return await _con.Vouchers.Where(c => c.ClientId == clientId)
                .Include(c => c.Branch)
                .Include(c => c.PurchasedProduct)
                .Include(c => c.Client)
                .AsSingleQuery()
                .ToListAsync();
        }
    }
}