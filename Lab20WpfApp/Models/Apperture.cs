using Lab20WpfApp1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab20WpfApp.Models
{
    class Apperture : INotifyPropertyChanged
    {
        private double width;
        private double height;
        private double position;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public double Width { get { return width; } 
            set
            {
                width = FamiliesOperations.SetNonZeroValue(value);
                OnPropertyChanged();
            } 
        }
        public double Height { get { return height; } 
            set
            {
                height = FamiliesOperations.SetNonZeroValue(value);
                OnPropertyChanged();
            }
         }
        public double Position { get { return position; }
            set
            {
                position = Math.Abs(value);
                OnPropertyChanged();
            }
        }

        public Apperture(double width, double height, double position)
        {
            Width = width;
            Height = height;
            Position = position;
        }
    }
}
