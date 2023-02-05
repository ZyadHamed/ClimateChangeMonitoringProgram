using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveCharts.Defaults;
using System.Collections.ObjectModel;

namespace Capstone_Program
{
    public class ViewModel
    {
        ObservableCollection<ObservablePoint> points { get; set; } = new ObservableCollection<ObservablePoint>();
        public ISeries[] series { get; set; }
        public ViewModel()
        {
            DataFetcher dt = new DataFetcher();
            (List<double>, List<double>) data = dt.GetTimeTemperatureSeries();
            for (int i = 0; i < data.Item1.Count; i++)
            {
                points.Add(new ObservablePoint(data.Item1[i], data.Item2[i]));
            }
            series = new ISeries[]
            {
                new LineSeries<ObservablePoint>
                {
                    Values = points
                }
            };
        }
        
    }
}
