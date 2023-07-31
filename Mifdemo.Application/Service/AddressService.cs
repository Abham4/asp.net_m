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
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _bs;
        public AddressService(IAddressRepository Repository)
        {
            _bs = Repository;
        }
        public async Task PostAddressAsync(AddressModel address)
        {
            await _bs.AddAsync(address);
            await _bs.UnitOfWork.SaveChanges();
        }
        public async Task UpdateAddressAsync(AddressModel address)
        {
            await _bs.UpdateAsync(address);
            await _bs.UnitOfWork.SaveChanges();
        }
        public async Task DeleteAddressAsync(int id)
        {
            var address = await _bs.GetByIdAsync(id);
            await _bs.DeleteAsync(address);
            await _bs.UnitOfWork.SaveChanges();
        }
    }
}
