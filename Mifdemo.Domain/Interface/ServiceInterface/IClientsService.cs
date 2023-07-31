using Mifdemo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mifdemo.Domain.Interface.ServiceInterface
{
    public interface IClientsService
    {
        public Task<List<ClientModel>> GetallClientsAsync();
        public Task<ClientModel> GetClientsAsync(int Id);
        public Task PostClientsAsync(ClientModel clients);
        public Task UpdateClientsAsync(ClientModel clients);
        public Task DeleteClientsAsync(int Id);
    }
}
