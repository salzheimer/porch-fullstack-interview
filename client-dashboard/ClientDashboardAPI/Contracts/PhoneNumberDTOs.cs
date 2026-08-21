#nullable enable
using System.ComponentModel.DataAnnotations;

namespace ClientDashboardAPI.Contracts
{
    public class CreatePhoneNumberRequest
    {
        [Required]
        public int ClientId { get; set; }
        [Required]
        public int CountryCode { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public int PhoneNumberType { get; set; }
        public bool? IsPrimary { get; set; }

    }

    public class PhoneNumberResponse
    {
        public int PhoneNumberId { get; set; }
        public int ClientId { get; set; }
        public int CountryCode { get; set; }
        public string PhoneNumber { get; set; }
        public int PhoneNumberType { get; set; }
        public bool IsPrimary { get; set; }
    }
    public class UpdatePhoneNumberRequest
    {

        public int? CountryCode { get; set; }
        public string? PhoneNumber { get; set; }
        public int? PhoneNumberType { get; set; }
        public bool? IsPrimary { get; set; }

    }
}