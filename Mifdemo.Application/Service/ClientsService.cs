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
    public class ClientsService : IClientsService
    {
        private readonly IClientsRepository _bs;
        public ClientsService(IClientsRepository Repository)
        {
            _bs = Repository;
        }
        public async Task<List<ClientModel>> GetallClientsAsync()
        {
            var all = await _bs.GetAllAsync();
            return all.ToList();
        }
        public async Task<ClientModel> GetClientsAsync(int id)
        {
            return await _bs.GetByIdAsync(id);
        }
        public async Task PostClientsAsync(ClientModel clients)
        {
            await _bs.AddAsync(clients);
        }
        public async Task UpdateClientsAsync(ClientModel clients)
        {
            await _bs.UpdateAsync(clients);
            await _bs.UnitOfWork.SaveChanges();

        }
        public async Task DeleteClientsAsync(int id)
        {
            var client = await _bs.GetByIdAsync(id);
            await _bs.DeleteAsync(client);
            await _bs.UnitOfWork.SaveChanges();
        }
    }
}
