using Microsoft.EntityFrameworkCore;
using Mifdemo.Domain.Interface.RepositoryInterface;
using Mifdemo.Domain.Models;
using Mifdemo.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mifdemo.Infrastructure.Repository
{
    public class ClientsRepository : BaseRepository<ClientModel>, IClientsRepository
    {
        private readonly Context _db;
        public ClientsRepository(Context context):base(context)
        {
            _db = context;
        }
        public override async Task<IReadOnlyList<ClientModel>> GetAllAsync()
        {
            return await _db.Clients
                .Include(c => c.Account)
                .ThenInclude(c => c.PurchasedProducts)
                .ThenInclude(c => c.Products)
                .ThenInclude(c => c.Product)
                .Include(c => c.Addresses)
                .Include(c => c.Families)
                .Include(c => c.Identifiers)
                .Include(c => c.Branch)
                .AsSingleQuery()
                .ToListAsync();
        }
        
        public override async Task<ClientModel> GetByIdAsync(int id)
        {
            return await _db.Clients
                .Include(c => c.Account)
                .ThenInclude(c => c.PurchasedProducts)
                .ThenInclude(c => c.Products)
                .ThenInclude(c => c.Product)
                .Include(c => c.Addresses)
                .Include(c => c.Families)
                .Include(c => c.Identifiers)
                .Include(c => c.Branch)
                .AsSingleQuery()
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        public override async Task AddAsync(ClientModel client)
        {
            var accstr = "Joshua-";
            var ExistedAccNo = await _db.Account.ToArrayAsync();
            int accno;
            if(ExistedAccNo.Count() == 0)
                accno = 123456;
            else
            {
                int [] p = new int[ExistedAccNo.Count()];
                for (int i=0;i<ExistedAccNo.Count();i++)
                {
                    string e = ExistedAccNo[i].AccountNo.ToString();
                    p[i] = int.Parse(e.Substring(7));
                }
                accno = p.Max()+1;
            }
            string accnum;
            await _db.Clients.AddAsync(client);
            await UnitOfWork.SaveChanges();
            string [] acctype = {"Sharing", "Loan", "Saving"};
            for (int i = 0; i < 3; i++)
            {
                accnum = accstr + accno.ToString();
                var acc = new AccountModel()
                {
                    ClientId = client.Id,
                    AccountNo = accnum,
                    AccountType = acctype[i]
                };
                await _db.Account.AddAsync(acc);
                await UnitOfWork.SaveChanges();
                accno++;
            }
        }
    }
}
