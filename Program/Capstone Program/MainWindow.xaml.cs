using System;
using System.Collections.Generic;
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

namespace Capstone_Program
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string lastBtnName = "";
        public MainWindow()
        {
            InitializeComponent();
            fmContent.Content = new SummaryPage();
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if(lastBtnName != ((Button)sender).Name)
            {
                
                if(((Button)sender).Name == "btnSummary")
                {
                    btnSummary.Background = new SolidColorBrush(Colors.DarkBlue);
                    btnGraphs.Background = new SolidColorBrush(Colors.DarkGray);
                    btnAnalysis.Background = new SolidColorBrush(Colors.DarkGray);
                    fmContent.Content = new SummaryPage();
                }
                else if (((Button)sender).Name == "btnGraphs")
                {
                    btnGraphs.Background = new SolidColorBrush(Colors.DarkBlue);
                    btnSummary.Background = new SolidColorBrush(Colors.DarkGray);
                    btnAnalysis.Background = new SolidColorBrush(Colors.DarkGray);
                    fmContent.Content = new GraphsPage();
                }
                else if (((Button)sender).Name == "btnAnalysis")
                {
                    btnAnalysis.Background = new SolidColorBrush(Colors.DarkBlue);
                    btnGraphs.Background = new SolidColorBrush(Colors.DarkGray);
                    btnSummary.Background = new SolidColorBrush(Colors.DarkGray);
                    fmContent.Content = new AnalysisPage();
                }
                lastBtnName = ((Button)sender).Name;
            }
        }
    }
}
