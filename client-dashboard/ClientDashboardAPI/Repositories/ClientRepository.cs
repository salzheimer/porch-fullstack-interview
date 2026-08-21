using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using ClientDashboardAPI.Models;
using ClientDashboardAPI.Interfaces;
using Dapper;
using System;
using System.Linq;
#nullable enable
namespace ClientDashboardAPI.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly IDbConnection _connection;

        public ClientRepository(IDbConnection conn)
        {
            _connection = conn;
        }
        public async Task<Client?> AddClientAsync(Client client)
        {
            const string sql = @"INSERT INTO clients
            (first_name,last_name,email,is_archived)
            VALUES(@FirstName,@LastName,@Email,@IsArchived); SELECT LAST_INSERT_ID();";

            // ExecuteScalarAsync returns the newly inserted id. Use object and convert to int to be robust.
            var newId = await _connection.ExecuteScalarAsync(sql, client);

            if (newId == null)
                return null;

            client.ClientId = Convert.ToInt32(newId);
            return client;
        }
        public async Task<Client?> UpdateClientAsync(Client client)
        {
            const string sql = @"UPDATE 
            clients 
            SET 
            first_name =@FirstName,
            last_name=@LastName,
            email=@Email,
            is_archived = @IsArchived            
            WHERE client_id=@ClientId";
            var affectedRows = await _connection.ExecuteAsync(sql, client);
            if (affectedRows == 1)
            {
                return client;
            }
            else
            {
                return null;
            }
        }
        public async Task<bool> ArchiveClientAsync(int clientId)
        {
            const string sql = @"UPDATE 
            clients 
            SET is_archived = true WHERE 
            client_id=@ClientId";
            var affectedRows = await _connection.ExecuteAsync(sql, new { ClientId = clientId });
            return affectedRows > 0;
        }

        public async Task<IEnumerable<Client>> GetAllActiveClientsAsync()
        {
            const string sql = @"SELECT 
            client_id,first_name,last_name,email 
            FROM clients
            WHERE is_archived = false";
            return await _connection.QueryAsync<Client>(sql);

        }
        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            const string sql = @"SELECT 
            client_id,first_name,last_name,email 
            FROM clients";
            return await _connection.QueryAsync<Client>(sql);

        }

        public async Task<Client?> GetClientAsync(int clientId)
        {
            const string sql = @"SELECT 
            client_id,first_name,last_name,email,is_archived 
            FROM clients 
            WHERE client_id =@ClientId";
            var client = await _connection.QueryFirstOrDefaultAsync<Client>(sql, new { ClientId = clientId });
            return client;
        }

        public async Task<Client?> GetClientDetailsAsync(int clientId)
        {
            Client client = new Client();
            const string sql = @"SELECT 
            client_id,first_name,last_name,email,is_archived 
            FROM clients c
            WHERE client_id =@ClientId;
            SELECT phone_number_id, client_id, country_code, phone, is_primary, number_type_id
            FROM phone_numbers
            WHERE client_id= @ClientId; 
            ";
            using (var multiple = await _connection.QueryMultipleAsync(sql, new { ClientId = clientId }))
            {
                client = (await multiple.ReadAsync<Client>()).SingleOrDefault();
                client.PhoneNumbers = (await multiple.ReadAsync<PhoneNumber>()).ToList();
            }

            return client;
        }
    }
}