using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Features.Clients
{
    public class ClientRepository : IClientRepository
    {
        private readonly string _connectionString;

        public ClientRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("db");
        }

        // Get All Clients
        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Client>(
                "sp_GetAllClients", 
                commandType: CommandType.StoredProcedure
            );
        }

        // Get Client by ID
        public async Task<Client> GetClientByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Client>(
                "sp_GetClientById",
                new { Id = id },
                commandType: CommandType.StoredProcedure
            );
        }

        // Add Client (with output parameter)
        public async Task<int> AddClientAsync(Client client)
        {
            using var connection = new SqlConnection(_connectionString);
            var parameters = new DynamicParameters();
            parameters.Add("@Name", client.Name);
            parameters.Add("@Identification", client.Identification);
            parameters.Add("@Phone", client.Phone);
            parameters.Add("@Address", client.Address);
            parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await connection.ExecuteAsync(
                "sp_AddClient", 
                parameters, 
                commandType: CommandType.StoredProcedure
            );

            return parameters.Get<int>("@Id");
        }

        // Update Client
        public async Task<bool> UpdateClientAsync(Client client)
        {
            using var connection = new SqlConnection(_connectionString);
            var rowsAffected = await connection.ExecuteAsync(
                "sp_UpdateClient",
                new { client.Id, client.Name, client.Identification, client.Phone, client.Address },
                commandType: CommandType.StoredProcedure
            );
            return rowsAffected > 0;
        }

        // Delete Client
        public async Task<bool> DeleteClientAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var rowsAffected = await connection.ExecuteAsync(
                "sp_DeleteClient",
                new { Id = id },
                commandType: CommandType.StoredProcedure
            );
            return rowsAffected > 0;
        }
    }
}
