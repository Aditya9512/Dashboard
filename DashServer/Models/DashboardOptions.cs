using ChilliDBApi.MembershipManager;
using ChilliDBApi.DataManager;

namespace DashServer.Models
{

	public class DashboardOptions
	{
	

		public string apiBaseUrl { get; set; } = String.Empty;
		public string systemId { get; set; } = String.Empty;
		public string systemUser { get; set; } = String.Empty;
		public string systemPassword { get; set; } = String.Empty;
		public int apiMaxMessageSize { get; set; }
		public int apiTimeOutSeconds { get; set; }
		public  int[] membershipPackageIds { get; set; }

		

	}
}
