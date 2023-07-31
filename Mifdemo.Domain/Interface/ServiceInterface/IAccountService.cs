using Mifdemo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mifdemo.Domain.Interface.ServiceInterface
{
    public interface IAccountService
    {
        public Task<List<AccountModel>> GetallAccountAsync();
        public Task<AccountModel> GetAccountAsync(int Id);
        public Task PostAccountAsync(AccountModel account);
        public Task UpdateAccountAsync(AccountModel account);
        public Task DeleteAccountAsync(int Id);
    }
}
