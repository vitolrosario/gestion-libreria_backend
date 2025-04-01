using Features.Clients;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Controllers
{
    [ApiController]
    [Route("api/client")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IEnumerable<Client>> GetClients()
        {
            return await _clientService.GetAllClientsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var client = await _clientService.GetClientByIdAsync(id);
            if (client == null) return NotFound();
            return Ok(client);
        }


        [HttpPost]
        public async Task<ActionResult> AddClient(ClientDto clientDto)
        {
            var id = await _clientService.AddClientAsync(clientDto);
            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ClientDto>> UpdateClient(int id, ClientDto clientDto)
        {
            var client = await _clientService.UpdateClientAsync(id, clientDto);

            if (client == null) return NotFound();

            return Ok(client);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            await _clientService.DeleteClientAsync(id);
            return Ok();
        }
    }
}

