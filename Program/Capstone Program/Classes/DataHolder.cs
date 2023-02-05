using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_Program
{
    public class DataHolder
    {
        public double XVal { get; set; }
        public double YVal { get; set; }
        public DataHolder(double x, double y)
        {
            XVal = x;
            YVal = y;
        }
    }
}
