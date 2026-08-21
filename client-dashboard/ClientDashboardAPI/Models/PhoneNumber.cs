namespace ClientDashboardAPI.Models
{
    public class PhoneNumber
    {
        public int PhoneNumberId { get; set; }
        public int ClientId { get; set; }
        public int CountryCode { get; set; }
        public string Phone { get; set; }
        public bool IsPrimary { get; set; }
        public int NumberTypeId { get; set; }
    }

    public class PhoneNumberType
    {
        public int PhoneNumberTypeId { get; set; }
        public string TypeName { get; set; }
        public string DisplayName { get; set; }
        public int SortOrder { get; set; }
    }
}