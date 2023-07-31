using System.Collections.Generic;
using System.Threading.Tasks;
using Mifdemo.Domain.Models;

namespace Mifdemo.Domain.Interface.ServiceInterface
{
    public interface IBranchService
    {
        public Task<List<BranchModel>> GetBranches();
        public Task<List<BranchModel>> GetBranchesByAddress(string address);
        public Task<BranchModel> GetBranchesByName(string name);
        public Task<BranchModel> GetBranch(int id);
        public Task PostBranchAsync(BranchModel branch);
        public Task UpdateBranchAsync(BranchModel branch);
        
    }
}