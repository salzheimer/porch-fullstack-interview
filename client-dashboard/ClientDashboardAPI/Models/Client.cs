using System.Collections.Generic;

namespace ClientDashboardAPI.Models
{
    public class Client
    {
        public Client()
        {
            PhoneNumbers = new List<PhoneNumber>();    
        }
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsArchived { get; set; }
        public List<PhoneNumber> PhoneNumbers {get;set;}

    }
}