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
    public class FamilyService : IFamilyService
    {
        private readonly IFamilyRepository _bs;
        public FamilyService(IFamilyRepository Repository)
        {
            _bs = Repository;
        }
        public async Task PostFamilyAsync(FamilyMembersModel family)
        {
            await _bs.AddAsync(family);
            await _bs.UnitOfWork.SaveChanges();
        }
        public async Task UpdateFamilyAsync(FamilyMembersModel family)
        {
            await _bs.UpdateAsync(family);
            await _bs.UnitOfWork.SaveChanges();
        }
        public async Task DeleteFamilyAsync(int id)
        {
            var family = await _bs.GetByIdAsync(id);
            await _bs.DeleteAsync(family);
            await _bs.UnitOfWork.SaveChanges();
        }
    }
}
