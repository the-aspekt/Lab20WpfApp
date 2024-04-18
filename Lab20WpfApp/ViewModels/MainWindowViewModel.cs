using Lab20WpfApp;
using Lab20WpfApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Lab20WpfApp1.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public WallPanel Panel = new WallPanel(6000, 3000, 1200, 2000, 600, 1200, 2000, 600);
        
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //округлить все размеры до 5
        double RoundDistance (double distance)
        {
            return Math.Round((double)distance / 5) * 5;
        }
        //разметка панели
        public string upperLintelGridHeight = "0.3*";
        public string UpperLintelGridHeight
        {
            get { return upperLintelGridHeight; }
            set
            {
                upperLintelGridHeight = value;
                OnPropertyChanged();
            }
        }

        public string apertureGridHeight = "0.7*";
        public string ApertureGridHeight
        {
            get { return apertureGridHeight; }
            set
            {
                apertureGridHeight = value;
                OnPropertyChanged();
            }
        }

        public string leftAperturePositionGridWidth = "0.1*";
        public string LeftAperturePositionGridWidth
        {
            get { return leftAperturePositionGridWidth; }
            set
            {
                leftAperturePositionGridWidth = value;
                
                OnPropertyChanged();
            }
        }

        public string rightAperturePositionGridWidth = "0.1*";
        public string RightAperturePositionGridWidth
        {
            get { return rightAperturePositionGridWidth; }
            set
            {
                rightAperturePositionGridWidth = value;
                OnPropertyChanged();
            }
        }

        public string leftApertureGridWidth = "0.2*";
        public string LeftApertureGridWidth
        {
            get { return leftApertureGridWidth; }
            set
            {
                leftApertureGridWidth = value;
                OnPropertyChanged();
            }
        }

        public string rightApertureGridWidth = "0.2*";
        public string RightApertureGridWidth
        {
            get { return rightApertureGridWidth; }
            set
            {
                rightApertureGridWidth = value;
                OnPropertyChanged();
            }
        }

        public string mainSegmentGridWidth = "0.4*";
        public string MainSegmentGridWidth
        {
            get { return mainSegmentGridWidth; }
            set
            {
                mainSegmentGridWidth = value;
                OnPropertyChanged();
            }
        }
        //обновление разметки панели
        void GridHeightRefresh ()
        {
            UpperLintelGridHeight = ((Height - bothApertureHeight) / Height).ToString() + "*";
            ApertureGridHeight = ((bothApertureHeight) / Height).ToString() + "*";
        }

        void GridWidthRefresh()
        {
            UpperLintelGridHeight = ((Height - bothApertureHeight) / Height).ToString() + "*";
            ApertureGridHeight = ((bothApertureHeight) / Height).ToString() + "*";
            LeftAperturePositionGridWidth = ((LeftAperturePosition) / Width).ToString() + "*";
            LeftApertureGridWidth = ((LeftApertureWidth) / Width).ToString() + "*";
            MainSegmentGridWidth = ((Width -
                LeftApertureWidth - LeftAperturePosition -
                RightApertureWidth - RightAperturePosition) / Width).ToString() + "*";
            RightApertureGridWidth = ((RightApertureWidth) / Width).ToString() + "*";
            RightAperturePositionGridWidth = ((RightAperturePosition) / Width).ToString() + "*";
        }
        //параметры активной панели
        public double height;
        public double Height
        {
            get { return height; }
            set
            {
                double x = RoundDistance(value);
                Panel.Height = x;
                height = Panel.Height;
                GridHeightRefresh();
                OnPropertyChanged();
            }
        }
        public double bothApertureHeight;
        public double BothApertureHeight
        {
            get { return bothApertureHeight; }
            set
            {
                double x = RoundDistance(value);
                Panel.SetApertureHeight(WallPanel.Appertures.leftApperture,x);
                Panel.SetApertureHeight(WallPanel.Appertures.rightApperture, x);
                bothApertureHeight = Panel.GetApertureHeight(WallPanel.Appertures.leftApperture);               
                GridHeightRefresh();
                OnPropertyChanged();
            }
        }

        private double width;
        public double Width
        {
            get { return width; }
            set
            {
                double x = RoundDistance(value);
                Panel.Width = x;
                width = Panel.Width;
                GridWidthRefresh();
                OnPropertyChanged();
            }
        }

        private double leftApertureWidth;
        public double LeftApertureWidth
        {
            get { return leftApertureWidth; }
            set
            {
                double x = RoundDistance(value);
                Panel.SetApertureWidth(WallPanel.Appertures.leftApperture, x);
                leftApertureWidth = Panel.GetApertureWidth(WallPanel.Appertures.leftApperture);
                GridWidthRefresh();
                //код меняющий ширину скрола прокрутки в соответствии с шириной проема
                OnPropertyChanged();                
            }
        }
        private double rightApertureWidth;
        public double RightApertureWidth
        {
            get { return rightApertureWidth; }
            set
            {
                double x = RoundDistance(value);
                Panel.SetApertureWidth(WallPanel.Appertures.rightApperture, x);
                rightApertureWidth = Panel.GetApertureWidth(WallPanel.Appertures.rightApperture);
                GridWidthRefresh();
                //код меняющий ширину скрола прокрутки в соответствии с шириной проема
                OnPropertyChanged();

            }
        }

        private double leftAperturePosition;
        public double LeftAperturePosition
        {
            get { return leftAperturePosition; }
            set
            {
                    double x = RoundDistance(value);
                    Panel.SetAperturePosition(WallPanel.Appertures.leftApperture, x);
                    leftAperturePosition = Panel.GetAperturePosition(WallPanel.Appertures.leftApperture);
                    GridWidthRefresh();
                    OnPropertyChanged();
            }
        }
        private double rightAperturePosition;
        public double RightAperturePosition
        {
            get { return rightAperturePosition; }
            set
            {
                double x = RoundDistance(value);
                Panel.SetAperturePosition(WallPanel.Appertures.rightApperture, x);
                rightAperturePosition = Panel.GetAperturePosition(WallPanel.Appertures.rightApperture);
                //sliderForRightAperturePosition = (Width - rightAperturePosition);
                GridWidthRefresh();
                OnPropertyChanged();
            }
        }
        // команды по включению/выключению проемов
        public ICommand IsLeftAperture {  get; }
        private void OnIsLeftApertureExecute(object sender)
        {
           if (Panel.GetApertureWidth(WallPanel.Appertures.leftApperture) > 0)
            {
                Panel.RemoveAperture(WallPanel.Appertures.leftApperture);
                LeftAperturePosition = 0;
                LeftApertureWidth = 0;
            }
            else
            {
                Panel.CreateAperture(WallPanel.Appertures.leftApperture,
                    Panel.GetMiddleWidth() / 2,
                    Panel.GetApertureHeight(WallPanel.Appertures.rightApperture) > 0 ?
                        Panel.GetApertureHeight(WallPanel.Appertures.rightApperture) :
                        Panel.Height * 2 / 3);
                LeftApertureWidth = Panel.GetApertureWidth(WallPanel.Appertures.leftApperture);
                LeftAperturePosition = Panel.GetAperturePosition(WallPanel.Appertures.leftApperture);
            }           
        }

        private bool OnIsLeftApertureCanExecuted(object sender)
        {
            if ((Panel.GetMiddleWidth() < 500) && (Panel.GetApertureWidth(WallPanel.Appertures.leftApperture) == 0))
                return false;
            else return true;
        }

        public ICommand IsRightAperture { get; }
        private void OnIsRightApertureExecute(object sender)
        {
            if (Panel.GetApertureWidth(WallPanel.Appertures.rightApperture) > 0)
            {
                Panel.RemoveAperture(WallPanel.Appertures.rightApperture);
                RightAperturePosition = 0;
                RightApertureWidth = 0;
            }
            else
            {
                Panel.CreateAperture(WallPanel.Appertures.rightApperture,
                    Panel.GetMiddleWidth() / 2,
                    Panel.GetApertureHeight(WallPanel.Appertures.leftApperture) > 0 ?
                        Panel.GetApertureHeight(WallPanel.Appertures.leftApperture) :
                        Panel.Height * 2 / 3);
                RightApertureWidth = Panel.GetApertureWidth(WallPanel.Appertures.rightApperture);
                RightAperturePosition = Panel.GetAperturePosition(WallPanel.Appertures.rightApperture);
            }
        }

        private bool OnIsRightApertureCanExecuted(object sender)
        {
            if ((Panel.GetMiddleWidth() < 500) && (Panel.GetApertureWidth(WallPanel.Appertures.rightApperture) == 0))
                return false;
            else return true;
        }
        //отзеркаливание панели
        public ICommand MirrorPanel { get; }
        private void OnMirrorPanelExecute(object sender)
        {
            //Slider leftApertureSlider = slider1.Value;
            //SliderWidthChange();
            double rw = RightApertureWidth;
            double rp = RightAperturePosition;

            if (Panel.GetApertureWidth(WallPanel.Appertures.rightApperture) > 0 && Panel.GetApertureWidth(WallPanel.Appertures.leftApperture) > 0)
            {                
                RightAperturePosition = LeftAperturePosition;
                RightApertureWidth = LeftApertureWidth;
                LeftAperturePosition = rp;
                LeftApertureWidth = rw;
            }
            else if (Panel.GetApertureWidth(WallPanel.Appertures.rightApperture) > 0)
            {
                Panel.CreateAperture(WallPanel.Appertures.leftApperture,
                   RightApertureWidth,
                   BothApertureHeight,
                   RightAperturePosition);
                LeftApertureWidth = Panel.GetApertureWidth(WallPanel.Appertures.leftApperture);
                LeftAperturePosition = Panel.GetAperturePosition(WallPanel.Appertures.leftApperture);
                Panel.RemoveAperture(WallPanel.Appertures.rightApperture);
                RightAperturePosition = 0;
                RightApertureWidth = 0;
            }
            else
            {
                Panel.CreateAperture(WallPanel.Appertures.rightApperture,
                   LeftApertureWidth,
                   BothApertureHeight,
                   LeftAperturePosition);
                RightApertureWidth = Panel.GetApertureWidth(WallPanel.Appertures.rightApperture);
                RightAperturePosition = Panel.GetAperturePosition(WallPanel.Appertures.rightApperture);
                Panel.RemoveAperture(WallPanel.Appertures.leftApperture);
                LeftAperturePosition = 0;
                LeftApertureWidth = 0;
            }
        }

        private bool OnMirrorPanelCanExecuted(object sender)
        {
            if ((Panel.GetApertureWidth(WallPanel.Appertures.leftApperture) == 0) && (Panel.GetApertureWidth(WallPanel.Appertures.rightApperture) == 0))
                return false;
            else return true;
        }

        public MainWindowViewModel()
        {
            Height = Panel.Height;
            Width = Panel.Width;
            BothApertureHeight = Panel.GetApertureHeight(WallPanel.Appertures.leftApperture);
            LeftApertureWidth = Panel.GetApertureWidth(WallPanel.Appertures.leftApperture);
            LeftAperturePosition = Panel.GetAperturePosition(WallPanel.Appertures.leftApperture);
            RightAperturePosition = Panel.GetAperturePosition(WallPanel.Appertures.rightApperture);
            RightApertureWidth = Panel.GetApertureWidth(WallPanel.Appertures.rightApperture);

            IsLeftAperture = new RelayCommand(OnIsLeftApertureExecute, OnIsLeftApertureCanExecuted);
            IsRightAperture = new RelayCommand(OnIsRightApertureExecute, OnIsRightApertureCanExecuted);
            MirrorPanel = new RelayCommand(OnMirrorPanelExecute, OnMirrorPanelCanExecuted);

        }

    }
}
