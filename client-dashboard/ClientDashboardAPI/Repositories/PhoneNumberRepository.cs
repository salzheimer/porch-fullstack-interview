using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using ClientDashboardAPI.Models;
using System;
#nullable enable
namespace ClientDashboardAPI.Repository
{
    public class PhoneNumberRepository : Interfaces.IPhoneNumberRepository
    {
        private readonly IDbConnection _connection;
        public PhoneNumberRepository(IDbConnection conn)
        {
            _connection = conn;
        }
        public async Task<PhoneNumber?> AddPhoneNumberAsync(PhoneNumber phoneNumber)
        {
            const string sql = @"INSERT INTO phone_numbers 
            (client_id,country_code,phone,is_primary,number_type_id)
            VALUES(@ClientId,@CountryCode,@Phone,@IsPrimary,@NumberTypeId);
            SELECT LAST_INSERT_ID();";
            var newId = await _connection.ExecuteScalarAsync(sql, phoneNumber);
            if (newId == null)
                return null;
            phoneNumber.PhoneNumberId = Convert.ToInt32(newId);
            return phoneNumber;
        }

        public async Task<bool> DeletePhoneNumberAsync(int phoneNumberId)
        {
            const string sql = @"DELETE FROM phone_numbers 
            WHERE phone_number_id = @PhoneNumberId";
            var rowsAffected = await _connection.ExecuteAsync(sql, new { PhoneNumberId = phoneNumberId });
            return rowsAffected == 1;
        }

        public async Task<IEnumerable<PhoneNumber>> GetClientPhoneNumbersAsync(int clientId)
        {
            const string sql = @"SELECT 
            phone_number_id, client_id, country_code, phone, is_primary, number_type_id
            FROM phone_numbers
            WHERE client_id=@ClientId";
            var clientNums = await _connection.QueryAsync<PhoneNumber>(sql, new { ClientId = clientId });
            return clientNums;
        }
        public async Task<PhoneNumber?> GetPhoneNumberAsync(int phoneNumberId)
        {
            const string sql = @"SELECT 
            phone_number_id, client_id, country_code, phone, is_primary, number_type_id
            FROM phone_numbers
            WHERE phone_number_id=@PhoneNumberId";
            var number = await _connection.QuerySingleOrDefaultAsync<PhoneNumber>(sql, new { PhoneNumberId = phoneNumberId });
            return number;
        }
        public async Task<PhoneNumber?> UpdatePhoneNumberAsync(PhoneNumber phoneNumber)
        {
            const string sql = @"UPDATE phone_numbers
            SET country_code = @CountryCode,
            phone = @Phone,
            is_primary=@IsPrimary,
            number_type_id=@NumberTypeId
            WHERE phone_number_id= @PhoneNumberId";


            var rowsAffected = await _connection.ExecuteAsync(sql, phoneNumber);
            if (rowsAffected != 1)
                return null;

            return phoneNumber;

        }
    }
}