using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_Program
{
    public class LinearRegression
    {
        public double[] xVals { get; set; }
        public double[] yVals { get; set; }
        public double slope { get; set; }
        public double yIntercept { get; set; }
        public LinearRegression(double[] _xVals, double[] _yVals) 
        {
            xVals= _xVals;
            yVals= _yVals;
        }

        (double, double) GradientDescent(double learningRate, double currSlope, double currInter)
        {
            double m_gradient = 0;
            double b_gradient = 0;
            int itemsNum = xVals.Length;
            for(int i = 0; i < itemsNum; i++)
            {
                m_gradient +=  -1 * xVals[i] * (yVals[i] - (xVals[i] * currSlope + currInter)) / itemsNum;
                b_gradient += -1 * (yVals[i] - (xVals[i] * currSlope + currInter)) / itemsNum;
            }

            double s = currSlope - m_gradient * learningRate;
            double b = currInter - b_gradient * learningRate;
            return (s, b);
        }

        public void Train2(double learningRate, int epochs)
        {
            double ss = 1;
            double bb = 1;
            for (int e = 0; e <= epochs; e++)
            {
                (ss, bb) = GradientDescent(learningRate, ss, bb);
            }

            slope = ss;
            yIntercept = bb;
        }
    }
}
