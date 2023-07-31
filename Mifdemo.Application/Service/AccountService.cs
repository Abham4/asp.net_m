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
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _bs;
        public AccountService(IAccountRepository accountRepository)
        {
            _bs = accountRepository;
        }
        public async Task<List<AccountModel>> GetallAccountAsync()
        {
            var all = await _bs.GetAllAsync();
            return all.ToList();
        }
        public async Task<AccountModel> GetAccountAsync(int id)
        {
            return await _bs.GetByIdAsync(id);
        }
        public async Task PostAccountAsync(AccountModel account)
        {
            await _bs.AddAsync(account);
            await _bs.UnitOfWork.SaveChanges();
        }
        public async Task UpdateAccountAsync(AccountModel account)
        {
            await _bs.UpdateAsync(account);
            await _bs.UnitOfWork.SaveChanges();
        }
        public async Task DeleteAccountAsync(int id)
        {
            var account = await _bs.GetByIdAsync(id);
            await _bs.DeleteAsync(account);
            await _bs.UnitOfWork.SaveChanges();
        }
    }
}
