using Mifdemo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mifdemo.Domain.Interface.ServiceInterface
{
    public interface IFamilyService
    {
        public Task PostFamilyAsync(FamilyMembersModel family);
        public Task UpdateFamilyAsync(FamilyMembersModel family);
        public Task DeleteFamilyAsync(int Id);
    }
}