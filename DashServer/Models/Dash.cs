using Microsoft.AspNetCore.Routing.Internal;

namespace DashServer.Models
{
 
    public class Dash
    {
        public IEnumerable<Stat>? stats { get; set; }

        public List<Graph>? Graphs { get; set; }


        public List<GraphPoint>? graph1 { get; set; }
        public List<GraphPoint>? graph2 { get; set; }

    }
}

