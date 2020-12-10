using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawGraph.Models
{
    class Edge
    {
        public Vertex Source { get; set; }
        public Vertex Target { get; set; }

        public Edge(Vertex source, Vertex target)
        {
            this.Source = source;
            this.Target = target;
        }

        public Vertex OtherEdge(Vertex vertex)
        {
            if (Source.Equals(vertex)) return Target;
            else if (Target.Equals(vertex)) return Source;
            else return null;
        }
    }
}
