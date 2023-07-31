using Mifdemo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mifdemo.Domain.Interface.ServiceInterface
{
    public interface IAccountsService
    {
        public Task<List<AccountsModel>> GetallAccountsAsync();
        public Task<AccountsModel> GetAccountsAsync(int Id);
        public Task PostAccountsAsync(AccountsModel account);
        public Task UpdateAccountsAsync(AccountsModel account);
        public Task DeleteAccountsAsync(int Id);
    }
}