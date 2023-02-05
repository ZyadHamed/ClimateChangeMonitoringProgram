using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_Program
{
    public class StatisticalAnalysis
    {
        public static double GetMean(double[] arr)
        {
            double val = 0;
            foreach(double num in arr)
            {
                val += num;
            }
            val /= arr.Length;
            return double.Round(val, 2);
        }

        public static double GetMedian(double[] arr)
        {
            Array.Sort(arr);
            if(arr.Length % 2 == 0)
            {
                return double.Round((arr[arr.Length / 2 - 1] + arr[arr.Length / 2]) / 2, 2);
            }
            else
            {
                return double.Round(arr[arr.Length / 2], 2);
            }
        }

        public static double GetStandardDeviation(double[] arr)
        {
            double mean = GetMean(arr);
            double sum = 0;
            foreach(double val in arr)
            {
                sum += Math.Pow(val - mean, 2);
            }
            double result = Math.Sqrt(sum / arr.Length);
            return double.Round(result, 2);
        }

        public static double GetCovariance(double[] x, double[] y)
        {
            double xMean = GetMean(x);
            double yMean = GetMean(y);
            double sum = 0;
            for(int i = 0; i < x.Length; i++)
            {
                sum += (x[i] - xMean) * (y[i] - yMean);
            }

            return double.Round(sum / x.Length, 3);
        }

        public static double GetCorrelation(double[] x, double[] y)
        {
            return double.Round(GetCovariance(x, y) / (GetStandardDeviation(x) * GetStandardDeviation(y)), 3);
        }

        public static double GetMinimum(double[] arr)
        {
            Array.Sort(arr);
            return arr[0];
        }

        public static double GetMaximum(double[] arr)
        {
            Array.Sort(arr);
            return arr[arr.Length - 1];
        }

    }
}
