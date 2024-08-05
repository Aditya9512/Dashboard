using Microsoft.AspNetCore.Mvc;

namespace DashServer.Models
{
	public class ClientsResponse
	{
		public IEnumerable<Client>? NewSignups { get; set; }
		public int Total { get; set; }
		public int Year { get; set; }
		public int Month { get; set; }
		public int Week { get; set; }
		public int premium { get; set; }
		public int platinum { get; set; }
		public IEnumerable<Package>? Packages { get; set; }
		public List<Graph>? Graphs { get; set; }
		public String grouped { get; set; }
	}
}
