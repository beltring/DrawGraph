using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawGraph.Models
{
    class Graph
    {
        public List<Edge> Edges { get; set; }
        public List<Vertex> Vertices { get; set; }

        public int EdgesCount
        {
            get { return Edges.Count; }
        }
        public int VerticesCount
        {
            get { return Vertices.Count; }
        }

        public bool[,] AdjacencyMatrix { get; set; }

        public Graph()
        {
            Edges = new List<Edge>();
            Vertices = new List<Vertex>();
        }
    }
}
