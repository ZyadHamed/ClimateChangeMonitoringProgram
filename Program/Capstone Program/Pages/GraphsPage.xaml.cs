using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
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
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;
using FireSharp;

namespace Capstone_Program
{
    /// <summary>
    /// Interaction logic for GraphsPage.xaml
    /// </summary>
    public partial class GraphsPage : Page
    {
        DataFetcher dt = new DataFetcher();
        ObservableCollection<ObservablePoint> temptimepoints = new ObservableCollection<ObservablePoint>();
        ObservableCollection<ObservablePoint> tdstimepoints = new ObservableCollection<ObservablePoint>();
        ObservableCollection<ObservablePoint> co2timepoints = new ObservableCollection<ObservablePoint>();
        ObservableCollection<ObservablePoint> temptdspoints = new ObservableCollection<ObservablePoint>();
        public GraphsPage()
        {
            InitializeComponent();
            (List<double>, List<double>) temptimedata = dt.GetTimeTemperatureSeries();
            (List<double>, List<double>) tdstimedata = dt.GetTimeTDSSeries();
            (List<double>, List<double>) co2timedata = dt.GetTimeCO2Series();
            (List<double>, List<double>) temptdsdata = dt.GetTempTDSSeries();
            for (int i = 0; i < temptimedata.Item1.Count; i++)
            {
                temptimepoints.Add(new ObservablePoint(temptimedata.Item1[i], temptimedata.Item2[i]));
            }

            for (int i = 0; i < tdstimedata.Item1.Count; i++)
            {
                tdstimepoints.Add(new ObservablePoint(tdstimedata.Item1[i], tdstimedata.Item2[i]));
            }

            for (int i = 0; i < tdstimedata.Item1.Count; i++)
            {
                co2timepoints.Add(new ObservablePoint(co2timedata.Item1[i], co2timedata.Item2[i]));
            }

            for (int i = 0; i < tdstimedata.Item1.Count; i++)
            {
                temptdspoints.Add(new ObservablePoint(temptdsdata.Item1[i], temptdsdata.Item2[i]));
            }

            ISeries[] temptimeseries = new ISeries[]
            {
               new LiveChartsCore.SkiaSharpView.LineSeries<ObservablePoint>
               {
                   Name = "",
                   Values = temptimepoints,
                   Mapping = (hold, point) =>
                   {
                       point.PrimaryValue = hold.Y;
                       point.SecondaryValue = hold.X;
                   }
                   
               }
            };

            ISeries[] tdstimeseries = new ISeries[]
            {
               new LiveChartsCore.SkiaSharpView.LineSeries<ObservablePoint>
               {
                   Name = "",
                   Values = tdstimepoints,
                   Mapping = (hold, point) =>
                   {
                       point.PrimaryValue = hold.Y;
                       point.SecondaryValue = hold.X;
                   }

               }
            };

            ISeries[] co2timeseries = new ISeries[]
            {
               new LiveChartsCore.SkiaSharpView.LineSeries<ObservablePoint>
               {
                   Name = "",
                   Values = co2timepoints,
                   Mapping = (hold, point) =>
                   {
                       point.PrimaryValue = hold.Y;
                       point.SecondaryValue = hold.X;
                   }

               }
            };

            ISeries[] temptdsseries = new ISeries[]
{
               new LiveChartsCore.SkiaSharpView.ScatterSeries<ObservablePoint>
               {
                   Name = "",
                   Values = temptdspoints,
                   Mapping = (hold, point) =>
                   {
                       point.PrimaryValue = hold.Y;
                       point.SecondaryValue = hold.X;
                   }

               }
};
            temptimechart.Series = temptimeseries;
            tdstimechart.Series = tdstimeseries;
            co2timechart.Series = co2timeseries;
            tempTDSchart.Series = temptdsseries;
        }

        private void btnToggle_Click(object sender, RoutedEventArgs e)
        {
            if(((Button)sender).Name == "btnAll")
            {
                Grid.SetColumnSpan(temptimechart, 1);
                Grid.SetRowSpan(temptimechart, 1);
                Grid.SetRow(temptimechart, 0);
                Grid.SetColumn(temptimechart, 0);


                Grid.SetColumnSpan(tdstimechart, 1);
                Grid.SetRowSpan(tdstimechart, 1);
                Grid.SetRow(tdstimechart, 1);
                Grid.SetColumn(tdstimechart, 0);

                Grid.SetColumnSpan(co2timechart, 1);
                Grid.SetRowSpan(co2timechart, 1);
                Grid.SetRow(co2timechart, 0);
                Grid.SetColumn(co2timechart, 1);

                Grid.SetColumnSpan(tempTDSchart, 1);
                Grid.SetRowSpan(tempTDSchart, 1);
                Grid.SetRow(tempTDSchart, 1);
                Grid.SetColumn(tempTDSchart, 1);

                temptimechart.Visibility = Visibility.Visible;
                tempTDSchart.Visibility = Visibility.Visible;
                tdstimechart.Visibility = Visibility.Visible;
                co2timechart.Visibility = Visibility.Visible;

            }

            else if (((Button)sender).Name == "btnTemptime")
            {
                Grid.SetRow(temptimechart, 0);
                Grid.SetColumn(temptimechart, 0);
                Grid.SetRow(tdstimechart, 0);
                Grid.SetColumn(tdstimechart, 0);
                Grid.SetRow(co2timechart, 0);
                Grid.SetColumn(co2timechart, 0);
                Grid.SetRow(tempTDSchart, 0);
                Grid.SetColumn(tempTDSchart, 0);


                temptimechart.Visibility = Visibility.Visible;
                Grid.SetColumnSpan(temptimechart, 2);
                Grid.SetRowSpan(temptimechart, 2);
                tempTDSchart.Visibility = Visibility.Collapsed;
                tdstimechart.Visibility = Visibility.Collapsed;
                co2timechart.Visibility = Visibility.Collapsed;

            }

            else if (((Button)sender).Name == "btnTDStime")
            {
                Grid.SetRow(temptimechart, 0);
                Grid.SetColumn(temptimechart, 0);
                Grid.SetRow(tdstimechart, 0);
                Grid.SetColumn(tdstimechart, 0);
                Grid.SetRow(co2timechart, 0);
                Grid.SetColumn(co2timechart, 0);
                Grid.SetRow(tempTDSchart, 0);
                Grid.SetColumn(tempTDSchart, 0);

                tdstimechart.Visibility = Visibility.Visible;
                Grid.SetColumnSpan(tdstimechart, 2);
                Grid.SetRowSpan(tdstimechart, 2);
                tempTDSchart.Visibility = Visibility.Collapsed;
                temptimechart.Visibility = Visibility.Collapsed;
                co2timechart.Visibility = Visibility.Collapsed;
            }

            else if (((Button)sender).Name == "btnCO2time")
            {
                Grid.SetRow(temptimechart, 0);
                Grid.SetColumn(temptimechart, 0);
                Grid.SetRow(tdstimechart, 0);
                Grid.SetColumn(tdstimechart, 0);
                Grid.SetRow(co2timechart, 0);
                Grid.SetColumn(co2timechart, 0);
                Grid.SetRow(tempTDSchart, 0);
                Grid.SetColumn(tempTDSchart, 0);

                co2timechart.Visibility = Visibility.Visible;
                Grid.SetColumnSpan(co2timechart, 2);
                Grid.SetRowSpan(co2timechart, 2);
                tempTDSchart.Visibility = Visibility.Collapsed;
                temptimechart.Visibility = Visibility.Collapsed;
                tdstimechart.Visibility = Visibility.Collapsed;
            }

            else if (((Button)sender).Name == "btnTempTDS")
            {
                Grid.SetRow(temptimechart, 0);
                Grid.SetColumn(temptimechart, 0);
                Grid.SetRow(tdstimechart, 0);
                Grid.SetColumn(tdstimechart, 0);
                Grid.SetRow(co2timechart, 0);
                Grid.SetColumn(co2timechart, 0);
                Grid.SetRow(tempTDSchart, 0);
                Grid.SetColumn(tempTDSchart, 0);

                tempTDSchart.Visibility = Visibility.Visible;
                Grid.SetColumnSpan(tempTDSchart, 2);
                Grid.SetRowSpan(tempTDSchart, 2);
                co2timechart.Visibility = Visibility.Collapsed;
                temptimechart.Visibility = Visibility.Collapsed;
                tdstimechart.Visibility = Visibility.Collapsed;
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            (List<double>, List<double>) data = dt.GetTimeTemperatureSeries();
            IProgress<(string, string)> progress = new Progress<(string, string)>(value =>
            {
                string modifiedPath = value.Item2.Remove(0, 1).Split('/')[0];
                double time = DateTime.Parse(modifiedPath).Subtract(DateTime.Parse(dt.OriginalDate)).TotalSeconds;
                double TDS = double.Parse(value.Item1.Split(';')[0]);
                double Temperature = double.Parse(value.Item1.Split(';')[1]);
                temptimepoints.Add(new ObservablePoint(time, Temperature));
                tdstimepoints.Add(new ObservablePoint(time, TDS));
            });
            IFirebaseConfig ifc = new FirebaseConfig()
            {
                BasePath = "https://capstone327masterbase-default-rtdb.europe-west1.firebasedatabase.app/",
                AuthSecret = "da5ZCzqZlfvZHKHDggs5NXPvQDh0LqMqOoSXEbcA"
            };
            IFirebaseClient client = new FirebaseClient(ifc);
            int count = 1;
            int maxNow = data.Item1.Count;
            EventStreamResponse res = await client.OnAsync(@"UsersData/4QJojUdQ61OtiaYiUh7ENMUmHgf1/readings/",
            added: (s, args, context) =>
            {
                if (count <= maxNow)
                {
                    count++;
                }
                else
                {
                    progress.Report((args.Data, args.Path));
                }
            },
            changed: (s, args, context) =>
            {

            },
            removed: (s, args, context) =>
            {

            });
        }
    }
}
