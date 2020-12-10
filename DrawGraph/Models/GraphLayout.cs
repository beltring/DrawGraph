using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawGraph.Models
{
    class GraphLayout
    {
        #region PublicMethods

        public static Graph Generate(int verticesCount, float edgesPercentage)
        {
            Graph graphData = new Graph();

            for (int i = 0; i < verticesCount; i++)
            {
                var vertex = new Vertex("" + i) { ID = i + 1 };
                graphData.Vertices.Add(vertex);
            }

            var edgesCount = CalculateEdgesCount(verticesCount, edgesPercentage);
            bool[,] adjacencyMatrix = GenerateAdjacencyMatrix(verticesCount, edgesCount);
            if (edgesPercentage != 0)
            {
                for (int i = 0; i < verticesCount; i++)
                {
                    for (int j = 0; j < verticesCount; j++)
                    {
                        if (adjacencyMatrix[i, j])
                        {
                            var edge = new Edge(graphData.Vertices[i], graphData.Vertices[j]);
                            graphData.Edges.Add(edge);

                            graphData.Vertices[i].IncidentEdges.Add(edge);
                            graphData.Vertices[j].IncidentEdges.Add(edge);
                        }
                    }
                }
            }
            return graphData;
        }

        public static Graph GenerateWithFile(string text)
        {
            List<string[]> list = new List<string[]>();
            var str = text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < str.Length; i++)
            {
                list.Add(str[i].Split(' '));
            }

            Graph graphData = new Graph();

            var matrix = Converter(list);

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                var vertex = new Vertex("" + i) { ID = i + 1 };
                graphData.Vertices.Add(vertex);
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    if (matrix[i, j])
                    {
                        var edge = new Edge(graphData.Vertices[i], graphData.Vertices[j]);
                        graphData.Edges.Add(edge);

                        graphData.Vertices[i].IncidentEdges.Add(edge);
                        graphData.Vertices[j].IncidentEdges.Add(edge);
                    }
                }
            }

            return graphData;
        }

        public static int CalculateEdgesCount(int verticesCount, float edgesPercentage)
            => Convert.ToInt32(verticesCount * (verticesCount - 1) * edgesPercentage / 2);
        #endregion

        #region PrivateMethods
        private static bool[,] GenerateAdjacencyMatrix(int verticesCount, int edgesCount)
        {
            int vertex1, vertex2;
            Random rand = new Random();
            bool[,] adjacencyMatrix = new bool[verticesCount, verticesCount];

            adjacencyMatrix[0, 1] = true;
            for (int i = 2; i < verticesCount; i++)
            {
                vertex1 = i;
                vertex2 = rand.Next(0, i);

                adjacencyMatrix[vertex1, vertex2] = true;
            }

            int edgesToAdd = edgesCount - (verticesCount - 1);
            for (int i = 0; i < edgesToAdd; i++)
            {
                do
                {
                    vertex1 = rand.Next(0, verticesCount);
                    vertex2 = rand.Next(0, verticesCount);
                } while (vertex1 == vertex2 || adjacencyMatrix[vertex1, vertex2] || adjacencyMatrix[vertex2, vertex1]);

                adjacencyMatrix[vertex1, vertex2] = true;
            }

            for (int i = 0; i < verticesCount; i++)
            {
                for (int j = 0; j < verticesCount; j++)
                {
                    if (i > j && adjacencyMatrix[i, j])
                    {
                        adjacencyMatrix[j, i] = adjacencyMatrix[i, j];
                        adjacencyMatrix[i, j] = false;
                    }
                }
            }

            return adjacencyMatrix;
        }

        private static bool[,] Converter(List<string[]> matrix)
        {
            bool[,] adjacencyMatrix = new bool[matrix.Count, matrix[0].Length];

            for (int i = 0; i < matrix.Count; i++)
            {
                var row = matrix[i];
                for (int j = 0; j < matrix[0].Length; j++)
                {
                    if (row[j] == "1")
                    {
                        adjacencyMatrix[i,j] = true;
                    }
                    else
                    {
                        adjacencyMatrix[i, j] = false;
                    }
                }
            }

            return adjacencyMatrix;
        }
        #endregion
    }
}
