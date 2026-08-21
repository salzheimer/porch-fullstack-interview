using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using ClientDashboardAPI.Interfaces;
using ClientDashboardAPI.Models;
using Dapper;

namespace ClientDashboardAPI.Repository
{
    public class PhoneNumberTypeRepository : IPhoneNumberTypeRepository
    {
        private readonly IDbConnection _connection;

        public PhoneNumberTypeRepository(IDbConnection conn)
        {
            _connection = conn;
        }
        public async Task<IEnumerable<PhoneNumberType>> GetPhoneNumberTypesAsync()
        {
            const string sql = @"SELECT * FROM phone_number_types";

            return await _connection.QueryAsync<PhoneNumberType>(sql);
        }
    }
}