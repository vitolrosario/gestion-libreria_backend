using AutoMapper;

namespace Features.Clients
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            return await _clientRepository.GetAllClientsAsync();
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            return await _clientRepository.GetClientByIdAsync(id);
        }

        public async Task<int> AddClientAsync(ClientDto clientDto)
        {
            var client = _mapper.Map<Client>(clientDto);
            return await _clientRepository.AddClientAsync(client);
        }

        public async Task<bool> UpdateClientAsync(int id, ClientDto clientDto)
        {
            var client = await _clientRepository.GetClientByIdAsync(id);
            if (client == null) return false;

            _mapper.Map(clientDto, client);
            return await _clientRepository.UpdateClientAsync(client);
        }

        public async Task DeleteClientAsync(int id)
        {
            await _clientRepository.DeleteClientAsync(id);
        }
    }
}
