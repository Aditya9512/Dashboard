using Microsoft.AspNetCore.Routing.Internal;

namespace DashServer.Models


{
    public class GraphDataSet
    {
        public string? Label { get; set; }
        public List<int>? YAxis { get; set; }
        public int[]? borderColor { get; set; }
        public double[]? backgroundColor { get; set; }
    }
   

}
