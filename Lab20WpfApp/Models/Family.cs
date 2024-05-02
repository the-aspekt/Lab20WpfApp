using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab20WpfApp.Models
{

    public enum FamilyTypes
    {
        WallPanel,
        FloorPanel,
        Beam,
        Column
    }

    public abstract class Family : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        private string description;
        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged();
            }
        }

        public FamilyTypes FamilyType { get; set; }

        private int amountOfSamples;
        public int AmountOfSamples
        {
            get => amountOfSamples;
            set
            {
                amountOfSamples = value;
                OnPropertyChanged();
            }
        }

        public string ImagePath { get; set; }
        
        private string label;
        public  string Label
        {
            get => label;
            set
            {
                label = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public abstract void SaveToJSON();
        public abstract T DecodeJSON<T>(string json) where T : Family;
    }
}
