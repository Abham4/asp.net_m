using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mifdemo.Domain.Interface.RepositoryInterface;
using Mifdemo.Domain.Interface.ServiceInterface;
using Mifdemo.Domain.Models;

namespace Mifdemo.Application.Service
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _repo;
        public BranchService(IBranchRepository repository)
        {
            _repo = repository;
        }
        public async Task<BranchModel> GetBranch(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<List<BranchModel>> GetBranches()
        {
            var braches = await _repo.GetAllAsync();
            return braches.ToList();
        }

        public async Task<List<BranchModel>> GetBranchesByAddress(string address)
        {
            var braches = await _repo.GetBranchesByAddress(address);
            return braches.ToList();
        }

        public async Task<BranchModel> GetBranchesByName(string name)
        {
            return await _repo.GetBranchesByName(name);
        }

        public async Task PostBranchAsync(BranchModel branch)
        {
            await _repo.AddAsync(branch);
            await _repo.UnitOfWork.SaveChanges();
        }

        public async Task UpdateBranchAsync(BranchModel branch)
        {
            await _repo.UpdateAsync(branch);
            await _repo.UnitOfWork.SaveChanges();
        }
    }
}