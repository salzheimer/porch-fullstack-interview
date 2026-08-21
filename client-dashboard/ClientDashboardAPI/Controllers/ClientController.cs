using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientDashboardAPI.Contracts;
using ClientDashboardAPI.Interfaces;
using ClientDashboardAPI.Models;
using Microsoft.AspNetCore.Mvc;


namespace ClientDashboardAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        public ClientsController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] CreateClientRequest clientRequest)
        {
            var client = new Client
            {
                FirstName = clientRequest.FirstName,
                LastName = clientRequest.LastName,
                Email = clientRequest.Email,
                IsArchived = false
            };

            var newClient = await _clientRepository.AddClientAsync(client);
            if (newClient == null)
                return BadRequest("An Error occurred while adding client");

            var response = new ClientResponse
            {
                ClientId = newClient.ClientId,
                FirstName = newClient.FirstName,
                LastName = newClient.LastName,
                Email = newClient.Email,
                IsArchived = newClient.IsArchived

            };
            return Ok(response);

        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveClients()
        {

            var activeClients = await _clientRepository.GetAllActiveClientsAsync();

            var responses = activeClients.Select(c =>
            new ClientResponse
            {
                ClientId = c.ClientId,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                IsArchived = c.IsArchived
            }).ToList();

            return Ok(responses);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {

            var activeClients = await _clientRepository.GetAllClientsAsync();

            var responses = activeClients.Select(c =>
            new ClientResponse
            {
                ClientId = c.ClientId,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                IsArchived = c.IsArchived
            }).ToList();

            return Ok(responses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClient(int id)
        {
            var client = await _clientRepository.GetClientDetailsAsync(id);

            var response = new ClientDetailsResponse
            {
                ClientId = client.ClientId,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                IsArchived = client.IsArchived,
                PhoneNumbers = client.PhoneNumbers.Select(n => new PhoneNumberResponse{
                        PhoneNumberId = n.PhoneNumberId,
                        ClientId=n.ClientId,
                        CountryCode=n.CountryCode,
                        PhoneNumber = n.Phone,
                        PhoneNumberType = n.NumberTypeId,
                        IsPrimary = n.IsPrimary
                }).ToList()
            };

            return Ok(response);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> ArchiveClient([FromRoute] int id)
        {
            var isArchived = await _clientRepository.ArchiveClientAsync(id);

            if (isArchived)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] UpdateClientRequest updateClient)
        {
            var existingClient = await _clientRepository.GetClientAsync(id);
            if (existingClient == null)
                return BadRequest("Unable to locate the client record in the database");
            //Create client model for update with existing values where values are not in the request 
            var client = new Client
            {
                ClientId = id,
                FirstName = String.IsNullOrEmpty(updateClient.FirstName) ? existingClient.FirstName : updateClient.FirstName,
                LastName = String.IsNullOrEmpty(updateClient.LastName) ? existingClient.LastName : updateClient.LastName,
                Email = String.IsNullOrEmpty(updateClient.Email) ? existingClient.Email : updateClient.Email,
                IsArchived = updateClient.IsArchived.HasValue == false ? existingClient.IsArchived : updateClient.IsArchived.Value
            };

            var result = await _clientRepository.UpdateClientAsync(client);
            if (result == null)
                return BadRequest();

            var response = new ClientResponse
            {
                ClientId = result.ClientId,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Email = result.Email,
                IsArchived = result.IsArchived
            };

            return Ok(response);
        }
    }

}