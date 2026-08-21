using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#nullable enable
namespace ClientDashboardAPI.Contracts
{
    public class CreateClientRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }

    }
    public class UpdateClientRequest
    {

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }
        public bool? IsArchived { get; set; }
    }
    public class ClientResponse
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsArchived { get; set; }
    }
    public class ClientDetailsResponse
    {
        public ClientDetailsResponse()
        {
            PhoneNumbers = new List<PhoneNumberResponse>();
        }
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsArchived { get; set; }
        public List<PhoneNumberResponse> PhoneNumbers {get;set;} 
    }
}