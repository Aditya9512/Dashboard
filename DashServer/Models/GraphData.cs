namespace DashServer.Models
{
    public class GraphData
    {
 
    }

    public class GraphDataSet
    {
        public string? Label { get; set; }
        public List<int>? YAxis { get; set; }
        public byte[]? Colour { get; set; }
    }
    public class Graph
    {
        public string? Title { get; set; }
        public List<int>? XAxis { get; set; }
        public List<GraphDataSet>? DataSets { get; set; }
    }

}
