using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab20WpfApp1.Models
{
    internal static class FamiliesOperations
    {
        public static double GetCircumferenceFromRadius (double r)
        {
            return r*Math.PI*2;
        }
        public static double GetAreaFromRadius (double r)
        {
            return r * r * Math.PI;
        }
        public static double GetDiameterFromRadius(double r)
        {
            return 2 * r;
        }
        public static double GetRadiusFromArea(double A)
        {
            return Math.Sqrt(A/ Math.PI);
        }
        public static double GetRadiusFromCircumference(double c)
        {
            return c / Math.PI / 2;
        }
    }
}
