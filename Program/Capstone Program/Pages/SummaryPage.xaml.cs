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
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;
using FireSharp;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using FireSharp.EventStreaming;
namespace Capstone_Program
{
    /// <summary>
    /// Interaction logic for SummaryPage.xaml
    /// </summary>
    public partial class SummaryPage : Page
    {
        DataFetcher dt = new DataFetcher();
        public SummaryPage()
        {
            InitializeComponent();
            
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            int finalElementIndex = dt.GetTimeTDSSeries().Item1.Count - 1;
            double temp = dt.GetTimeTemperatureSeries().Item2[finalElementIndex];
            double TDS = dt.GetTimeTDSSeries().Item2[finalElementIndex];
            double CO2 = dt.GetTimeCO2Series().Item2[finalElementIndex];
            lbTemp.Text = temp.ToString() + " C";
            lbTDS.Text = TDS.ToString() + " ppm";
            lbCO2.Text = CO2.ToString() + " ppm";
            IProgress<string> progress = new Progress<string>(value =>
            {
                if(value.Length <= 20)
                {
                    lbTemp.Text = value.Split(';')[1] + " C";
                    lbTDS.Text = value.Split(';')[0] + " ppm";
                    lbCO2.Text = value.Split(';')[2].Replace("\n", "").Trim() + " ppm";
                }

                
            });

            IFirebaseConfig ifc = new FirebaseConfig()
            {
                BasePath = "https://capstone327masterbase-default-rtdb.europe-west1.firebasedatabase.app/",
                AuthSecret = "da5ZCzqZlfvZHKHDggs5NXPvQDh0LqMqOoSXEbcA"
            };
            IFirebaseClient client = new FirebaseClient(ifc);
            int count = 1;
            int maxNow = finalElementIndex + 1;
            EventStreamResponse res = await client.OnAsync(@"UsersData/4QJojUdQ61OtiaYiUh7ENMUmHgf1/readings",
            added: (s, args, context) =>
            {
                if(count <= maxNow)
                {
                    count++;
                }
                else
                {
                    progress.Report(args.Data);
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
