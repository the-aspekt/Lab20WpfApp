using Lab20WpfApp;
using Lab20WpfApp.Models;
using Lab20WpfApp.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
    class MainWindowViewModel : myCommands, INotifyPropertyChanged
    {        
        private WallPanel wallPanel = new WallPanel(5000, 3000, 1200, 2000, 600, 1200, 2000, 600);

        public WallPanel WallPanel
        {
            get { return wallPanel; }
            set 
            {
                wallPanel = value;
                OnPropertyChanged();
            }
        }
        
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
        
        // команды по включению/выключению проемов
        public ICommand IsLeftAperture {  get; }
        private void OnIsLeftApertureExecute(object sender)
        {
           if (wallPanel.GetApertureWidth(WallPanel.Appertures.leftApperture) > 0)
            {
                wallPanel.RemoveAperture(WallPanel.Appertures.leftApperture);
                wallPanel.LeftAperturePosition = 0;
                wallPanel.LeftApertureWidth = 0;
            }
            else
            {
                wallPanel.CreateAperture(WallPanel.Appertures.leftApperture,
                    wallPanel.GetMiddleWidth() / 2,
                    wallPanel.GetApertureHeight(WallPanel.Appertures.rightApperture) > 0 ?
                        wallPanel.GetApertureHeight(WallPanel.Appertures.rightApperture) :
                        wallPanel.Height * 2 / 3);
                wallPanel.LeftApertureWidth = wallPanel.GetApertureWidth(WallPanel.Appertures.leftApperture);
                wallPanel.LeftAperturePosition = wallPanel.GetAperturePosition(WallPanel.Appertures.leftApperture);
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
                wallPanel.RightAperturePosition = 0;
                wallPanel.RightApertureWidth = 0;
            }
            else
            {
                wallPanel.CreateAperture(WallPanel.Appertures.rightApperture,
                    wallPanel.GetMiddleWidth() / 2,
                    wallPanel.GetApertureHeight(WallPanel.Appertures.leftApperture) > 0 ?
                        wallPanel.GetApertureHeight(WallPanel.Appertures.leftApperture) :
                        wallPanel.Height * 2 / 3);
                wallPanel.RightApertureWidth = wallPanel.GetApertureWidth(WallPanel.Appertures.rightApperture);
                wallPanel.RightAperturePosition = wallPanel.GetAperturePosition(WallPanel.Appertures.rightApperture);
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
            double rw = wallPanel.RightApertureWidth;
            double rp = wallPanel.RightAperturePosition;

            if (wallPanel.GetApertureWidth(WallPanel.Appertures.rightApperture) > 0 && wallPanel.GetApertureWidth(WallPanel.Appertures.leftApperture) > 0)
            {                
                wallPanel.RightAperturePosition = wallPanel.LeftAperturePosition;
                wallPanel.RightApertureWidth = wallPanel.LeftApertureWidth;
                wallPanel.LeftAperturePosition = rp;
                wallPanel.LeftApertureWidth = rw;
            }
            else if (wallPanel.GetApertureWidth(WallPanel.Appertures.rightApperture) > 0)
            {
                wallPanel.CreateAperture(WallPanel.Appertures.leftApperture,
                   wallPanel.RightApertureWidth,
                   wallPanel.BothApertureHeight,
                   wallPanel.RightAperturePosition);
                wallPanel.LeftApertureWidth = wallPanel.GetApertureWidth(WallPanel.Appertures.leftApperture);
                wallPanel.LeftAperturePosition = wallPanel.GetAperturePosition(WallPanel.Appertures.leftApperture);
                wallPanel.RemoveAperture(WallPanel.Appertures.rightApperture);
                wallPanel.RightAperturePosition = 0;
                wallPanel.RightApertureWidth = 0;
            }
            else
            {
                wallPanel.CreateAperture(WallPanel.Appertures.rightApperture,
                   wallPanel.LeftApertureWidth,
                   wallPanel.BothApertureHeight,
                   wallPanel.LeftAperturePosition);
                wallPanel.RightApertureWidth = wallPanel.GetApertureWidth(WallPanel.Appertures.rightApperture);
                wallPanel.RightAperturePosition = wallPanel.GetAperturePosition(WallPanel.Appertures.rightApperture);
                wallPanel.RemoveAperture(WallPanel.Appertures.leftApperture);
                wallPanel.LeftAperturePosition = 0;
                wallPanel.LeftApertureWidth = 0;
            }
        }
        private bool OnMirrorPanelCanExecuted(object sender)
        {
            if ((wallPanel.GetApertureWidth(WallPanel.Appertures.leftApperture) == 0) && (wallPanel.GetApertureWidth(WallPanel.Appertures.rightApperture) == 0))
                return false;
            else return true;
        }
        //сохранение панели
        public ICommand SaveFamily { get; }
        private void OnSaveFamilyExecute(object sender)
        {
            wallPanel.SaveToJSON();
        }
        private bool OnSaveFamilyExecuted(object sender)
        {
            return true;
        }
        //загрузка панели
        public ICommand LoadFamily { get; }
        private void OnLoadFamilyExecute(object sender)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string json = File.ReadAllText(openFileDialog.FileName);
                WallPanel newWallPanel = wallPanel.DecodeJSON<WallPanel>(json);
                installWallPanel(newWallPanel);
            }
        }
        private bool OnLoadFamilyExecuted(object sender)
        {
            return true;
        }
        //попытка сделать выбор панели через команду - непонятно как получить выбранный элемент listbox'a       
        public ICommand SelectPanel { get; }
        private void OnSelectPanelExecute(object sender)
        {
           //(sender as ListBox).SelectedItem as Family;
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

        public void installWallPanel(WallPanel newWallPanel)
                {
                 wallPanel.InstallWallPanel(newWallPanel);            
                }

        public ObservableCollection<Family> Families { get; set; }

        public MainWindowViewModel()
        {
            IsLeftAperture = new RelayCommand(OnIsLeftApertureExecute, OnIsLeftApertureCanExecuted);
            IsRightAperture = new RelayCommand(OnIsRightApertureExecute, OnIsRightApertureCanExecuted);
            MirrorPanel = new RelayCommand(OnMirrorPanelExecute, OnMirrorPanelCanExecuted);
            SelectPanel = new RelayCommand(OnSelectPanelExecute, OnSelectPanelCanExecuted);
            SaveFamily = new RelayCommand(OnSaveFamilyExecute, OnSaveFamilyExecuted);
            LoadFamily = new RelayCommand(OnLoadFamilyExecute, OnLoadFamilyExecuted);

            WallPanel currentWallPanel = new WallPanel(5000, 3000, 1200, 2000, 600, 1200, 2000, 600);
            installWallPanel(currentWallPanel);

            Families = new ObservableCollection<Family>();

            // families.Add(WallPanel Panel);

            Family family1 = new WallPanel(4000, 3000, 1200, 2000, 600)
            {
                AmountOfSamples = 5
            };

            Family family2 = new WallPanel(5500, 3000, 800, 2000, 600, 1200, 2000, 600)
            {
                AmountOfSamples = 3,
            };

            Family currentFamily = currentWallPanel;

            Families.Add(family1);
            Families.Add(family2);
            Families.Add(currentFamily);

            //теперь нужно всё это поместить во всплывающий список сверху и добавить опцию сохранения
            
        }

    }
}
