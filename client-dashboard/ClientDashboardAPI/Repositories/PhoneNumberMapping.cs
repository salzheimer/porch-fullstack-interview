using Dapper.FluentMap.Mapping;
using ClientDashboardAPI.Models;

namespace ClientDashboardAPI.Repository
{
    public class PhoneNumberMap : EntityMap<PhoneNumber>
    {
        public PhoneNumberMap()
        {
            Map(p => p.PhoneNumberId).ToColumn("phone_number_id");
            Map(p => p.ClientId).ToColumn("client_id");
            Map(p => p.CountryCode).ToColumn("country_code");
            Map(p => p.Phone).ToColumn("phone");
            Map(p => p.IsPrimary).ToColumn("is_primary");
            Map(p => p.NumberTypeId).ToColumn("number_type_id");
        }
    }
}