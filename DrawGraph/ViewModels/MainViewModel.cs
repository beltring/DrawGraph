using DrawGraph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrawGraph.ViewModels
{
    class MainViewModel
    {
        public static void Visualize(Canvas canvas, Graph graph, int vertexRadius = 15)
        {
            Point center = GetCenter(canvas);
            int distY = (int)(canvas.ActualHeight / 2 - vertexRadius * 5);
            int distX = (int)(canvas.ActualWidth / 2 - vertexRadius * 6);
            int verticesCount = graph.Vertices.Count;

            double angle = 0;
            double deltaAngle = 360 / verticesCount;

            double x, y;
            for (int i = 0; i < verticesCount; i++)
            {
                x = center.X - distX * Math.Sin(angle * Math.PI / 180);
                y = center.Y - distY * Math.Cos(angle * Math.PI / 180);
                graph.Vertices[i].Position = new Point(x, y);
                angle += deltaAngle;
            }

            foreach (var edge in graph.Edges)
            {
                Line line = new Line();
                line.Stroke = Brushes.Silver;
                line.Fill = Brushes.Silver;
                line.X1 = edge.Source.Position.X;
                line.Y1 = edge.Source.Position.Y;
                line.X2 = edge.Target.Position.X;
                line.Y2 = edge.Target.Position.Y;
                canvas.Children.Add(line);
            }

            foreach (var vertex in graph.Vertices)
            {
                DrawVertex(canvas, vertex, Brushes.White);
            }
        }

        public static void DrawVertex(Canvas canvas, Vertex vertex, SolidColorBrush color)
        {
            vertex.Color = color;

            Ellipse ellipse = new Ellipse();
            ellipse.Stroke = Brushes.Silver;
            ellipse.Fill = color;
            ellipse.SetValue(Canvas.TopProperty, vertex.Position.Y - 20);
            ellipse.SetValue(Canvas.LeftProperty, vertex.Position.X - 20);
            ellipse.Height = 20 * 2;
            ellipse.Width = 20 * 2;
            canvas.Children.Add(ellipse);

            TextBlock textBlock = new TextBlock();
            textBlock.Text = vertex.Text;
            Canvas.SetLeft(textBlock, vertex.Position.X - 15 / 2);
            Canvas.SetTop(textBlock, vertex.Position.Y - 15 / 2);
            canvas.Children.Add(textBlock);
        }

        public static int PaintGraph(Canvas canvas, Graph graph)
        {
            List<SolidColorBrush> brushes = new List<SolidColorBrush>();
            foreach (var vertex in graph.Vertices)
            {
                SolidColorBrush color = null;
                bool flag = false;
                foreach (var brush in brushes)
                {
                    int count = vertex.IncidentEdges.Where(e => e.OtherEdge(vertex).Color.Equals(brush)).Count();
                    Thread.Sleep(10);
                    if (count == 0)
                    {
                        color = brush;
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    do
                    {
                        color = PickBrush();
                    } while (brushes.Contains(color));
                }
                brushes.Add(color);
                vertex.Color = color;
                DrawVertex(canvas, vertex, color);
            }

            var brushes1 = brushes.ToList().Distinct();
            return brushes1.Count();
        }

        private static Point GetCenter(Canvas canvas)
        {
            Point center = new Point();
            center.X = canvas.ActualWidth / 2;
            center.Y = canvas.ActualHeight / 2;

            return center;
        }

        private static SolidColorBrush PickBrush()
        {
            var rnd = new Random();
            var r = rnd.Next(125, 255);
            var g = rnd.Next(125, 255);
            var b = rnd.Next(125, 255);

            return new SolidColorBrush(Color.FromRgb((byte)r, (byte)g, (byte)b));
        }
    }
}
