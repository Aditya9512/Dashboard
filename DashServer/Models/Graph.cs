
using Microsoft.AspNetCore.Routing.Internal;

namespace DashServer.Models
{
    public class Graph
    {
        public string? Title { get; set; }
        public List<DateTime>? XAxis { get; set; }
        public List<GraphDataSet>? DataSets { get; set; }
    }
}
