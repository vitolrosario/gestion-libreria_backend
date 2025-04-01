namespace Features.Clients
{
    public interface IClientService
    {
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task<Client> GetClientByIdAsync(int id);
        Task<int> AddClientAsync(ClientDto clientDto);
        Task<bool> UpdateClientAsync(int id, ClientDto clientDto);
        Task DeleteClientAsync(int id);
    }
}
