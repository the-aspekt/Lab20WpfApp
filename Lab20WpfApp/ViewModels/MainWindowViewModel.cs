using Lab20WpfApp1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Lab20WpfApp1.ViewModels
{
    class MainWindowViewModel : DependencyObject , INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static DependencyProperty HeightProperty;
        public static DependencyProperty WidthProperty;
        public static DependencyProperty LeftApertureWidthProperty;
        public static DependencyProperty RightApertureWidthProperty;
        public static DependencyProperty BothApertureHeightProperty;
        public static DependencyProperty LeftAperturePositionProperty;
        public static DependencyProperty RightAperturePositionProperty;

        static MainWindowViewModel()
        {
            HeightProperty = DependencyProperty.Register(
                nameof(Height),
                typeof(double),
                typeof(MainWindowViewModel),
                new FrameworkPropertyMetadata(
                    3000.0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure,
                    null,
                    RoundDistanceTo5CoerceCallback),
                HeightValidation);
        }

        private static object RoundDistanceTo5CoerceCallback(DependencyObject d, object baseValue)
        {
            try
            {
                double v = Math.Round((double)baseValue/5)*5;
                return v;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Невозможно преобразовать к числу " + ex.ToString());
                return null;
            }
        }

        private static bool HeightValidation(object value)
        {
            double v = (double)value;
            if (v > 0)
            {
                return true;
            }
            else
                return false;
        }
                
        public double Height
        { 
            get => (double)GetValue(HeightProperty);
            set 
            {
                SetValue(HeightProperty, value);                
                OnPropertyChanged();                
            }
        }
       
        private double width;
        public double Width
        {
            get { return width; }
            set
            {
                width = value;
                OnPropertyChanged();
            }
        }

        private double leftApertureWidth;
        public double LeftApertureWidth
        {
            get { return leftApertureWidth; }
            set
            {
                leftApertureWidth = value;
                OnPropertyChanged();
                
            }
        }
        private double rightApertureWidth;
        public double RightApertureWidth
        {
            get { return rightApertureWidth; }
            set
            {
                rightApertureWidth = value;
                OnPropertyChanged();

            }
        }

        private double area;
        public double Area
        {
            get { return area; }
            set
            {
                area = value;
                OnPropertyChanged();
                
            }
        }


        public ICommand Refresh {  get; }
        private void OnRefreshExecute(object sender)
        {
            Height = FamiliesOperations.GetAreaFromRadius(Width);
            
        }

        private bool OnRefreshCanExecuted(object sender)
        {
            if (Width > 0)
                return true;
            else return false;
        }

        public MainWindowViewModel()
        {
            Refresh = new RelayCommand(OnRefreshExecute, OnRefreshCanExecuted);
        }

    }
}
