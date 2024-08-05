/*
using DashServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Xml.Linq;


namespace DashServer.Controllers
{
    [ApiController]
    [Route("[controller]")]

   
    public class DashController : ControllerBase
    {
        
        private readonly static string[] STAT_NAMES = { "statA", "statB", "statC", "statD", "statE" };



        [HttpGet(Name = "GetDashController")]

         public Dash Get()
         {
             List<Graph> graphs = new();

           for (int i = 1; i <= 1; ++i)
             {
                 Graph graph = new Graph
                 {
                     Title = string.Format("Graph {0}", i),
                     XAxis = new(),
                     DataSets = new(),
                 };
                
                    for (int j = 0; j <= 10; ++j)
                    {
                        graph.XAxis.Add(j);
                    }
                
                    for (int k = 1; k <= 2; ++k)
                    {
                        GraphDataSet dataSet = new()
                        {
                            Label = string.Format("GraphDataSet {0} ", k),
                            YAxis = new(),
                            //Colour = new byte[]{ 255, 99, 132 },
                        };
                        if (k == 1)
                        {
                            dataSet.borderColor = new int[] { 255, 99, 132 };
							dataSet.backgroundColor = new double[] { 255, 99, 132, 0.5 };
                        }
                        else
                        {
                            dataSet.borderColor = new int[] { 53, 162, 235 };
							dataSet.backgroundColor = new double[] { 53, 162, 235, 0.5 };
    }

                  for (int x = 1; x <= 10; ++x)
                        {
                            dataSet.YAxis.Add(Random.Shared.Next(-1000, 1000));
                        }      

                        //...
						graph.DataSets.Add(dataSet);
                    }
                
                graphs.Add(graph);
            }


         
    

             *//*   List<GraphPoint> graph1 = new();

            for ( int i=1; i<=10; ++i )
            { 
                graph1.Add(new GraphPoint { xAxis = i, yAxis1 = Random.Shared.Next(-1000, 1000), yAxis2 = Random.Shared.Next(-1000, 1000) });


            }
            List<GraphPoint> graph2 = new();

            for (int i = 1; i <= 10; ++i)
            {
                graph2.Add(new GraphPoint { xAxis = i, yAxis1 = Random.Shared.Next(-1000, 1000), yAxis2 = Random.Shared.Next(-1000, 1000) });


            } *//*

            IEnumerable<Stat>? stats;

            //for (int j = 0; j <= 4; ++j) {
            //    stats = STAT_NAMES.Select((textAbove, value) =>
            //          new { value = Random.Shared.Next(-1000, 1000), str = textAbove.STAT_NAMES(j) });
            //}
            stats = STAT_NAMES.Select(stat => new Stat {
                textAbove = stat,
                value = Random.Shared.Next(-1000, 1000)
            });


            //IEnumerable<string> x = new string[];
            //IEnumerable<string> y = new List<string>();




            return new Dash
            {
               stats = stats,
               // graph1 = graph1,
                //graph2 = graph2,
                Graphs = graphs,
            };
        }


    }
}
    
*/
