using DrawGraph.Models;
using DrawGraph.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DrawGraph
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Graph graph;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonCreate_Click(object sender, RoutedEventArgs e)
        {
            int vertices;
            float edgesPercentage;

            if (!int.TryParse(textBoxVertices.Text, out vertices)) return;
            if (float.TryParse(textBoxEdgesPercentage.Text, out edgesPercentage))
                edgesPercentage /= 100;
            else return;

            theCanvas.Children.Clear();
            graph = GraphLayout.Generate(vertices, edgesPercentage);
            MainViewModel.Visualize(theCanvas, graph);
        }

        private void buttonPaint_Click(object sender, RoutedEventArgs e)
        {
            if (graph == null) return;
            int resultColor = MainViewModel.PaintGraph(theCanvas, graph);
            labelResultCountOfColor.Content = "Количество цветов: " + resultColor;
        }

        private void buttonFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = @"D:\study\BNTU\7 sem\Прихожий\course\DrawGraph";
            if (openFileDialog.ShowDialog() == true)
            {
                string text = File.ReadAllText(openFileDialog.FileName);
                
                theCanvas.Children.Clear();
                graph = GraphLayout.GenerateWithFile(text);
                MainViewModel.Visualize(theCanvas, graph);
            }
        }
    }
}
