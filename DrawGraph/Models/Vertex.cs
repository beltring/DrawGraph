using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DrawGraph.Models
{
    class Vertex
    {
        public int ID { get; set; }
        public Point Position { get; set; }
        public string Text { get; set; }
        public SolidColorBrush Color { get; set; }
        public List<Edge> IncidentEdges { get; set; }

        public Vertex(string text)
        {
            this.Text = text;
            this.Color = null;
            this.IncidentEdges = new List<Edge>();
        }

        public List<Vertex> GetNearbyVertices()
        {
            List<Vertex> neighbors = new List<Vertex>();
            foreach (var edge in IncidentEdges)
            {
                neighbors.Add(edge.OtherEdge(this));
            }
            return neighbors;
        }
    }
}
