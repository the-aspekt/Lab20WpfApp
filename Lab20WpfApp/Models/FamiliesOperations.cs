using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab20WpfApp1.Models
{
    internal static class FamiliesOperations
    {
        public static double SetNonZeroValue(double value, double defaultValue = 1)
        {
            if (value != 0)
                return Math.Abs(value);
            else
                return defaultValue;
        }
    }

}
