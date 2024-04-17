using Lab20WpfApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab20WpfApp.Models
{
    class Apperture
    {
        private double width;
        private double height;
        private double position;

        public double Width { get { return width; } set => width = FamiliesOperations.SetNonZeroValue(value); }
        public double Height { get { return height; } set => height = FamiliesOperations.SetNonZeroValue(value); }
        public double Position { get { return position; } set => position = Math.Abs(value); }

        public Apperture(double width, double height, double position)
        {
            Width = width;
            Height = height;
            Position = position;
        }
    }
}
