using System.Collections.Generic;
using System.Threading.Tasks;
using Mifdemo.Domain.Models;

namespace Mifdemo.Domain.Interface.RepositoryInterface
{
    public interface IBranchRepository : IBaseRepository<BranchModel>
    {
        public Task<BranchModel> GetBranchesByName(string name); 
        public Task<IReadOnlyList<BranchModel>> GetBranchesByAddress(string address); 
    }
}