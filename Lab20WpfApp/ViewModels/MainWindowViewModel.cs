using Lab20WpfApp;
using Lab20WpfApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Xml.Linq;

namespace Lab20WpfApp1.ViewModels
{


    class MainWindowViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Family> familiesSelector = new ObservableCollection<Family>();

        // families.Add(WallPanel Panel);

        Family defaultFamily1 = new WallPanel()
        {
            AmountOfSamples = 5
        };

        Family defaultFamily2 = new WallPanel()
        {
            AmountOfSamples = 3,
        };
             

        public WallPanel wallPanel = new WallPanel(5000, 3000, 1200, 2000, 600, 1200, 2000, 600);
        
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
                wallPanel.Height = x;
                height = wallPanel.Height;
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
                wallPanel.SetApertureHeight(WallPanel.Appertures.leftApperture,x);
                wallPanel.SetApertureHeight(WallPanel.Appertures.rightApperture, x);
                bothApertureHeight = wallPanel.GetApertureHeight(WallPanel.Appertures.leftApperture);               
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
                wallPanel.Width = x;
                width = wallPanel.Width;
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
                wallPanel.SetApertureWidth(WallPanel.Appertures.leftApperture, x);
                leftApertureWidth = wallPanel.GetApertureWidth(WallPanel.Appertures.leftApperture);
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
                wallPanel.SetApertureWidth(WallPanel.Appertures.rightApperture, x);
                rightApertureWidth = wallPanel.GetApertureWidth(WallPanel.Appertures.rightApperture);
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
                wallPanel.SetAperturePosition(WallPanel.Appertures.leftApperture, x);
                    leftAperturePosition = wallPanel.GetAperturePosition(WallPanel.Appertures.leftApperture);
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
                wallPanel.SetAperturePosition(WallPanel.Appertures.rightApperture, x);
                rightAperturePosition = wallPanel.GetAperturePosition(WallPanel.Appertures.rightApperture);
                //sliderForRightAperturePosition = (Width - rightAperturePosition);
                GridWidthRefresh();
                OnPropertyChanged();
            }
        }
        // команды по включению/выключению проемов
        public ICommand IsLeftAperture {  get; }
        private void OnIsLeftApertureExecute(object sender)
        {
           if (wallPanel.GetApertureWidth(WallPanel.Appertures.leftApperture) > 0)
            {
                wallPanel.RemoveAperture(WallPanel.Appertures.leftApperture);
                LeftAperturePosition = 0;
                LeftApertureWidth = 0;
            }
            else
            {
                wallPanel.CreateAperture(WallPanel.Appertures.leftApperture,
                    wallPanel.GetMiddleWidth() / 2,
                    wallPanel.GetApertureHeight(WallPanel.Appertures.rightApperture) > 0 ?
                        wallPanel.GetApertureHeight(WallPanel.Appertures.rightApperture) :
                        wallPanel.Height * 2 / 3);
                LeftApertureWidth = wallPanel.GetApertureWidth(WallPanel.Appertures.leftApperture);
                LeftAperturePosition = wallPanel.GetAperturePosition(WallPanel.Appertures.leftApperture);
            }           
        }

        private bool OnIsLeftApertureCanExecuted(object sender)
        {
            if ((wallPanel.GetMiddleWidth() < 500) && (wallPanel.GetApertureWidth(WallPanel.Appertures.leftApperture) == 0))
                return false;
            else return true;
        }

        public ICommand IsRightAperture { get; }
        private void OnIsRightApertureExecute(object sender)
        {
            if (wallPanel.GetApertureWidth(WallPanel.Appertures.rightApperture) > 0)
            {
                wallPanel.RemoveAperture(WallPanel.Appertures.rightApperture);
                RightAperturePosition = 0;
                RightApertureWidth = 0;
            }
            else
            {
                wallPanel.CreateAperture(WallPanel.Appertures.rightApperture,
                    wallPanel.GetMiddleWidth() / 2,
                    wallPanel.GetApertureHeight(WallPanel.Appertures.leftApperture) > 0 ?
                        wallPanel.GetApertureHeight(WallPanel.Appertures.leftApperture) :
                        wallPanel.Height * 2 / 3);
                RightApertureWidth = wallPanel.GetApertureWidth(WallPanel.Appertures.rightApperture);
                RightAperturePosition = wallPanel.GetAperturePosition(WallPanel.Appertures.rightApperture);
            }
        }

        private bool OnIsRightApertureCanExecuted(object sender)
        {
            if ((wallPanel.GetMiddleWidth() < 500) && (wallPanel.GetApertureWidth(WallPanel.Appertures.rightApperture) == 0))
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

            if (wallPanel.GetApertureWidth(WallPanel.Appertures.rightApperture) > 0 && wallPanel.GetApertureWidth(WallPanel.Appertures.leftApperture) > 0)
            {                
                RightAperturePosition = LeftAperturePosition;
                RightApertureWidth = LeftApertureWidth;
                LeftAperturePosition = rp;
                LeftApertureWidth = rw;
            }
            else if (wallPanel.GetApertureWidth(WallPanel.Appertures.rightApperture) > 0)
            {
                wallPanel.CreateAperture(WallPanel.Appertures.leftApperture,
                   RightApertureWidth,
                   BothApertureHeight,
                   RightAperturePosition);
                LeftApertureWidth = wallPanel.GetApertureWidth(WallPanel.Appertures.leftApperture);
                LeftAperturePosition = wallPanel.GetAperturePosition(WallPanel.Appertures.leftApperture);
                wallPanel.RemoveAperture(WallPanel.Appertures.rightApperture);
                RightAperturePosition = 0;
                RightApertureWidth = 0;
            }
            else
            {
                wallPanel.CreateAperture(WallPanel.Appertures.rightApperture,
                   LeftApertureWidth,
                   BothApertureHeight,
                   LeftAperturePosition);
                RightApertureWidth = wallPanel.GetApertureWidth(WallPanel.Appertures.rightApperture);
                RightAperturePosition = wallPanel.GetAperturePosition(WallPanel.Appertures.rightApperture);
                wallPanel.RemoveAperture(WallPanel.Appertures.leftApperture);
                LeftAperturePosition = 0;
                LeftApertureWidth = 0;
            }
        }

        private bool OnMirrorPanelCanExecuted(object sender)
        {
            if ((wallPanel.GetApertureWidth(WallPanel.Appertures.leftApperture) == 0) && (wallPanel.GetApertureWidth(WallPanel.Appertures.rightApperture) == 0))
                return false;
            else return true;
        }

        public void installWallPanel (WallPanel wallPanel)
        {
            Height = wallPanel.Height;
            Width = wallPanel.Width;
            BothApertureHeight = wallPanel.GetApertureHeight(WallPanel.Appertures.leftApperture);
            LeftApertureWidth = wallPanel.GetApertureWidth(WallPanel.Appertures.leftApperture);
            LeftAperturePosition = wallPanel.GetAperturePosition(WallPanel.Appertures.leftApperture);
            RightAperturePosition = wallPanel.GetAperturePosition(WallPanel.Appertures.rightApperture);
            RightApertureWidth = wallPanel.GetApertureWidth(WallPanel.Appertures.rightApperture);

        }


        //попытка сделать выбор панели через команду - непонятно как получить выбранный элемент listbox'a
        public ICommand SelectPanel { get; }
        private void OnSelectPanelExecute(object sender)
        {
            //(sender as ListBox).SelectedItem as Family);
            MessageBox.Show("Вы выбрали ");
        }

        private bool OnSelectPanelCanExecuted(object sender)
        {
            return true;
        }

        public void familySelected(object sender, SelectionChangedEventArgs e)
        {
            //Family selectedFamily = (Family)(sender as ListBox).SelectedItem;
            // string newMessage = selectedFamily.Name;
            // MessageBox.Show("Вы выбрали " + newMessage);
            MessageBox.Show("Hallo");
        }

        public MainWindowViewModel()
        {
            installWallPanel(wallPanel);
            IsLeftAperture = new RelayCommand(OnIsLeftApertureExecute, OnIsLeftApertureCanExecuted);
            IsRightAperture = new RelayCommand(OnIsRightApertureExecute, OnIsRightApertureCanExecuted);
            MirrorPanel = new RelayCommand(OnMirrorPanelExecute, OnMirrorPanelCanExecuted);
            SelectPanel = new RelayCommand(OnSelectPanelExecute, OnSelectPanelCanExecuted);


        }

    }
}
