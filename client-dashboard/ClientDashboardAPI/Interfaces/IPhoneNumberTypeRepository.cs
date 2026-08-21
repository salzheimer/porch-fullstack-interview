using System.Collections.Generic;
using System.Threading.Tasks;
using ClientDashboardAPI.Models;
using Org.BouncyCastle.Crypto.Engines;

namespace ClientDashboardAPI.Interfaces
{
    public interface IPhoneNumberTypeRepository
    {
        Task<IEnumerable<PhoneNumberType>> GetPhoneNumberTypesAsync();
    }
}
