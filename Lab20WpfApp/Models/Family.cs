using System;
using System.Collections.Generic;
using System.Linq;
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

    public abstract class Family
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public FamilyTypes FamilyType { get; set; }
        public int AmountOfSamples { get; set; }
        public string ImagePath { get; set; }
        public string Label { get; set; }

        public abstract void RefreshName();

    }
}
