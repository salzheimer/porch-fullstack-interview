using Dapper.FluentMap;
using Dapper.FluentMap.Mapping;
using ClientDashboardAPI.Models;

namespace ClientDashboardAPI.Repository
{
    public class ClientMap : EntityMap<Client>
    {
        public ClientMap()
        {
            Map(p => p.ClientId).ToColumn("client_id");
            Map(p => p.FirstName).ToColumn("first_name");
            Map(p => p.LastName).ToColumn("last_name");
            Map(p => p.Email).ToColumn("email");
            Map(p => p.IsArchived).ToColumn("is_archived");

        }
    }
}