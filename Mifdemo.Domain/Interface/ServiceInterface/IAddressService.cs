using Mifdemo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mifdemo.Domain.Interface.ServiceInterface
{
    public interface IAddressService
    {
        public Task PostAddressAsync(AddressModel address);
        public Task UpdateAddressAsync(AddressModel address);
        public Task DeleteAddressAsync(int Id);
    }
}