using System.Collections.Generic;
using System.Threading.Tasks;
using ClientDashboardAPI.Models;

#nullable enable

namespace ClientDashboardAPI.Interfaces
{
    public interface IPhoneNumberRepository
    {
        Task<PhoneNumber> AddPhoneNumberAsync(PhoneNumber phoneNumber);
        Task<PhoneNumber?> GetPhoneNumberAsync(int phoneNumberId);
        Task<IEnumerable<PhoneNumber>> GetClientPhoneNumbersAsync(int clientId);
        Task<PhoneNumber> UpdatePhoneNumberAsync(PhoneNumber phoneNumber);
        Task<bool> DeletePhoneNumberAsync(int phoneNumberId);
    }
}