using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
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
    /// Interaction logic for AnalysisPage.xaml
    /// </summary>
    public partial class AnalysisPage : Page
    {
        DataFetcher dt = new DataFetcher();
        int count = 0;
        LinearRegression reg;
        public AnalysisPage()
        {
            InitializeComponent();
            double[] temperatures = dt.GetTimeTemperatureSeries().Item2.ToArray();
            double[] TDS = dt.GetTimeTDSSeries().Item2.ToArray();
            double[] CO2 = dt.GetTimeCO2Series().Item2.ToArray();
            lbTempMean.Text = StatisticalAnalysis.GetMean(temperatures).ToString();
            lbTempMedian.Text = StatisticalAnalysis.GetMedian(temperatures).ToString();
            lbTempDeviation.Text = StatisticalAnalysis.GetStandardDeviation(temperatures).ToString();
            lbTempMin.Text = StatisticalAnalysis.GetMinimum(temperatures).ToString();
            lbTempMax.Text = StatisticalAnalysis.GetMaximum(temperatures).ToString();

            lbTDSMean.Text = StatisticalAnalysis.GetMean(TDS).ToString();
            lbTDSMedian.Text = StatisticalAnalysis.GetMedian(TDS).ToString();
            lbTDSDeviation.Text = StatisticalAnalysis.GetStandardDeviation(TDS).ToString();
            lbTDSMin.Text = StatisticalAnalysis.GetMinimum(TDS).ToString();
            lbTDSMax.Text = StatisticalAnalysis.GetMaximum(TDS).ToString();

            lbCO2Mean.Text = StatisticalAnalysis.GetMean(CO2).ToString();
            lbCO2Median.Text = StatisticalAnalysis.GetMedian(CO2).ToString();
            lbCO2Deviation.Text = StatisticalAnalysis.GetStandardDeviation(CO2).ToString();
            lbCO2Min.Text = StatisticalAnalysis.GetMinimum(CO2).ToString();
            lbCO2Max.Text = StatisticalAnalysis.GetMaximum(CO2).ToString();

            lbTempTDSCorr.Text = StatisticalAnalysis.GetCorrelation(temperatures, TDS).ToString();
            lbTempTDSVar.Text = StatisticalAnalysis.GetCovariance(temperatures, TDS).ToString();

            lbCO2TempCorr.Text = StatisticalAnalysis.GetCorrelation(CO2, temperatures).ToString();
            lbCO2TempVar.Text = StatisticalAnalysis.GetCovariance(CO2, temperatures).ToString();

            lbCO2TDSCorr.Text = StatisticalAnalysis.GetCorrelation(CO2, TDS).ToString();
            lbCO2TDSVar.Text = StatisticalAnalysis.GetCovariance(CO2, TDS).ToString();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
