using Mifdemo.Domain.Interface.RepositoryInterface;
using Mifdemo.Domain.Interface.ServiceInterface;
using Mifdemo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mifdemo.Application.Service
{
    internal class AccountsService : IAccountsService
    {
            private readonly IAccountsRepository _bs;
            public AccountsService(IAccountsRepository accountsRepository)
            {
                _bs = accountsRepository;
            }
            public async Task<List<AccountsModel>> GetallAccountsAsync()
            {
                var all = await _bs.GetAllAsync();
                return all.ToList();
            }
            public async Task<AccountsModel> GetAccountsAsync(int id)
            {
                return await _bs.GetByIdAsync(id);
            }
            public async Task PostAccountsAsync(AccountsModel account)
            {
                await _bs.AddAsync(account);
                await _bs.UnitOfWork.SaveChanges();
            }
            public async Task UpdateAccountsAsync(AccountsModel account)
            {
                await _bs.UpdateAsync(account);
                await _bs.UnitOfWork.SaveChanges();
            }
            public async Task DeleteAccountsAsync(int id)
            {
                var account = await _bs.GetByIdAsync(id);
                await _bs.DeleteAsync(account);
                await _bs.UnitOfWork.SaveChanges();
            }
        
    }
}