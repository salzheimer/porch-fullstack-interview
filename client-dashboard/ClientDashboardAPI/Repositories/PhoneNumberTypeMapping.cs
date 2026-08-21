using System.Runtime.InteropServices.WindowsRuntime;
using Dapper.FluentMap.Mapping;
using ClientDashboardAPI.Models;

namespace ClientDashboardAPI.Repository
{
    public class PhoneNumberTypeMap : EntityMap<PhoneNumberType>
    {

        public PhoneNumberTypeMap()
        {

            Map(p => p.PhoneNumberTypeId).ToColumn("phone_number_type_id");
            Map(p => p.TypeName).ToColumn("type_name");
            Map(p => p.DisplayName).ToColumn("display_name");
            Map(p => p.SortOrder).ToColumn("sort_order");
        }
    }
}