using System.Collections.Generic;
using System.Threading.Tasks;
using ClientDashboardAPI.Models;
#nullable enable
namespace ClientDashboardAPI.Interfaces
{
    public interface IClientRepository
    {
        Task<Client?> GetClientAsync(int clientId);
        Task<Client?> GetClientDetailsAsync(int clientId);
        Task<IEnumerable<Client>> GetAllActiveClientsAsync();
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task<Client?> AddClientAsync(Client client);
        Task<bool> ArchiveClientAsync(int clientId);
        Task<Client?> UpdateClientAsync(Client client);
    }
}