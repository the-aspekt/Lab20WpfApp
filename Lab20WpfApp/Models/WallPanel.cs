using Lab20WpfApp1.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Lab20WpfApp.Models.WallPanel;
using System.Xml;
using Newtonsoft.Json;
using Microsoft.Win32;

namespace Lab20WpfApp.Models
{
    internal class WallPanel : Family
    {
            
            private string grade;
            public string Grade
            {
                get => grade;
                set
                {
            grade = value;
                    OnPropertyChanged();
                }
            }

            private double width;
            public double Width 
            {
                get
                {
                    return width;
                } 
                set
                {
                    double x = Math.Abs(value);
                    if (GetMiddleWidth() - (width - x) >= 0)
                        width = x;
                    RefreshName();
                GridWidthRefresh();
                OnPropertyChanged();
                }
            }
            private double height;
            public double Height
            {
                get 
                {
                    return height;
                }
                set
                {
                    double x = Math.Abs(value);
                    if (x - 
                        Math.Max(GetApertureHeight(Appertures.leftApperture),
                                GetApertureHeight(Appertures.rightApperture)) > 0)
                        height = x;
                    RefreshName();
                GridHeightRefresh();
                OnPropertyChanged();
                }
            }
            private double thickness;
            public double Thickness
            {
                get
                {
                    return thickness;
                }
                set
                {
                    double x = Math.Abs(value);
                    if (x > 0)
                        height = x;
                    RefreshName();
                    OnPropertyChanged();
                }
            }

            private double bothApertureHeight;
            public double BothApertureHeight
            {
                get { return bothApertureHeight; }
                set
                {
                    double x = Math.Abs(value);
                    this.SetApertureHeight(Appertures.leftApperture, x);
                    this.SetApertureHeight(Appertures.rightApperture, x);
                    bothApertureHeight = this.GetApertureHeight(Appertures.leftApperture);
                GridHeightRefresh();
                OnPropertyChanged();
                }
            }

        private double leftApertureWidth;
        public double LeftApertureWidth
        {
            get { return leftApertureWidth; }
            set
            {
                double x = Math.Abs(value);
                this.SetApertureWidth(Appertures.leftApperture, x);
                leftApertureWidth = this.GetApertureWidth(Appertures.leftApperture);
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
                double x = Math.Abs(value);
                this.SetApertureWidth(Appertures.rightApperture, x);
                rightApertureWidth = this.GetApertureWidth(Appertures.rightApperture);
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
                double x = Math.Abs(value);
                this.SetAperturePosition(Appertures.leftApperture, x);
                leftAperturePosition = this.GetAperturePosition(Appertures.leftApperture);
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
                double x = Math.Abs(value);
                this.SetAperturePosition(Appertures.rightApperture, x);
                rightAperturePosition = this.GetAperturePosition(Appertures.rightApperture);
                //sliderForRightAperturePosition = (Width - rightAperturePosition);
                GridWidthRefresh();
                OnPropertyChanged();
            }
        }

        public Apperture leftApperture = null;
            public Apperture rightApperture = null;
            
            public enum Appertures
            {
                leftApperture,
                rightApperture
            }

            public WallPanel() 
            {
                this.FamilyType = FamilyTypes.WallPanel;
                this.Description = "Стеновая панель";
                this.Grade = "НСН";
                RefreshName();
            }
            public WallPanel(double width, double height)
            {
                this.Width = width;
                this.Height = height;
            this.FamilyType = FamilyTypes.WallPanel;
            this.Description = "Стеновая панель";
            this.Grade = "НСН";
            RefreshName();
            }
            public WallPanel(double width, double height, double widthApp, double heightApp, double positioningApp)
            {
                this.Width = width;
                this.Height = height;
            this.FamilyType = FamilyTypes.WallPanel;
            this.Description = "Стеновая панель";
            this.Grade = "НСН";
            if (positioningApp > width/2)
            {
                this.rightApperture = new Apperture(widthApp, heightApp, width - positioningApp);
                this.rightAperturePosition = positioningApp;
                this.rightApertureWidth = widthApp;
            }
            else
            {
                this.leftApperture = new Apperture(widthApp, heightApp, positioningApp);
                this.leftAperturePosition = positioningApp;
                this.leftApertureWidth = widthApp;
            }
                    
            this.BothApertureHeight = heightApp;
            
            RefreshName();
            }
            public WallPanel(double width, double height, double widthApp, double heightApp, double positioningApp, double widthApp2, double heightApp2, double positioningApp2)
            {
                this.Width = width;
                this.Height = height;
            this.FamilyType = FamilyTypes.WallPanel;
            this.Description = "Стеновая панель";
            this.Grade = "НСН";
            this.leftApperture = new Apperture(widthApp, heightApp, positioningApp);
            this.rightApperture = new Apperture(widthApp2, heightApp2, positioningApp2);
            this.rightAperturePosition = positioningApp2;
            this.rightApertureWidth = widthApp2;
            this.leftAperturePosition = positioningApp;
            this.leftApertureWidth = widthApp;
            this.BothApertureHeight = heightApp;
            RefreshName();
            }

            public double GetMiddleWidth()
            {
                return (Width   - GetApertureWidth(Appertures.leftApperture)
                                - GetAperturePosition(Appertures.leftApperture)
                                - GetApertureWidth(Appertures.rightApperture)
                                - GetAperturePosition(Appertures.rightApperture));
            }
            public double GetApertureWidth(Appertures apperture)
            {
                if (apperture == Appertures.leftApperture && this.leftApperture != null)
                {
                    return this.leftApperture.Width;
                }
                else if (apperture == Appertures.rightApperture && this.rightApperture != null)
                {
                    return this.rightApperture.Width;
                }
                return 0;
            }
            public double GetApertureHeight(Appertures apperture)
            {
                if (apperture == Appertures.leftApperture && this.leftApperture != null)
                {
                    return this.leftApperture.Height;
                }
                else if (apperture == Appertures.rightApperture && this.rightApperture != null)
                {
                    return this.rightApperture.Height;
                }
                return 0;
            }
            public double GetAperturePosition(Appertures apperture)
            {
                if (apperture == Appertures.leftApperture && this.leftApperture != null)
                {
                    return this.leftApperture.Position;
                }
                else if (apperture == Appertures.rightApperture && this.rightApperture != null)
                {
                    return this.rightApperture.Position;
                }
                return 0;
            }
            public void SetApertureWidth(Appertures apperture, double width)
            {
            double x = Math.Abs(width);
            if (GetMiddleWidth() - (x- GetApertureWidth(apperture)) >= 0)
                {
                    if (apperture == Appertures.leftApperture && this.leftApperture != null)
                    {
                        this.leftApperture.Width = FamiliesOperations.SetNonZeroValue(width);
                    }
                    else if (apperture == Appertures.rightApperture && this.rightApperture != null)
                    {
                        this.rightApperture.Width = FamiliesOperations.SetNonZeroValue(width);
                    }
                }
            }
            public void SetApertureHeight(Appertures apperture, double height)
            {
            double x = Math.Abs(height);
            if (Height - (x - GetApertureHeight(apperture)) >= 0)
            {
                if (apperture == Appertures.leftApperture && this.leftApperture != null)
                {
                    this.leftApperture.Height = FamiliesOperations.SetNonZeroValue(height);
                }
                else if (apperture == Appertures.rightApperture && this.rightApperture != null)
                {
                    this.rightApperture.Height = FamiliesOperations.SetNonZeroValue(height);
                }
            }
            }
            public void SetAperturePosition(Appertures apperture, double position)
                {
                double x = Math.Abs(position);
                if (GetMiddleWidth() - (x - GetAperturePosition(apperture)) >= 0)
                    {
                        if (apperture == Appertures.leftApperture && this.leftApperture != null)
                        {
                            this.leftApperture.Position = x;
                        }
                        else if (apperture == Appertures.rightApperture && this.rightApperture != null)
                        {
                            this.rightApperture.Position = x;
                        }
                    }
                }
            public void CreateAperture(Appertures apperture, double width, double height, double position = 0)
                {
                    if (GetMiddleWidth() - (Math.Abs(width) + Math.Abs(position)) > 0 && (Height - Math.Abs(height)) > 0)
                    { 
                          if (apperture == Appertures.leftApperture)
                            {                           
                                this.leftApperture = new Apperture(width, height, position);
                        
                            }
                            else if (apperture == Appertures.rightApperture)
                            {
                                this.rightApperture = new Apperture(width, height, position);
                            }
                    }
                    else
                    MessageBox.Show("Невозможно создать панель с параметрами ширины, равной " + width + " и высоты равной " + height);
                }
            public void RemoveAperture(Appertures apperture)
                {
                    if (apperture == Appertures.leftApperture)
                    {
                        this.leftApperture = null;
                    }
                    else if (apperture == Appertures.rightApperture)
                    {
                        this.rightApperture = null;
                    }
                }       

            public void RefreshName()
            {
                this.Name = this.Grade + " " + Convert.ToString(Width/100) + "." + Convert.ToString(Height / 100) + "." + Convert.ToString(Thickness / 100) + this.Label;
            }

        public override void SaveToJSON()
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == true)
            {
                string json = JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(saveFileDialog.FileName, json);
            }
        }

        public string EncodeJSON()
        {
            return JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);       
        }

        public override T DecodeJSON<T>(string json)
        {
            WallPanel wallPanel = new WallPanel();
            wallPanel = JsonConvert.DeserializeObject<WallPanel>(json);
            return (T)(Family)wallPanel;
        }

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
        public void GridHeightRefresh()
        {
            UpperLintelGridHeight = ((this.Height - this.BothApertureHeight) / this.Height).ToString() + "*";
            ApertureGridHeight = ((this.BothApertureHeight) / this.Height).ToString() + "*";
        }
        public void GridWidthRefresh()
        {
            UpperLintelGridHeight = ((this.Height - this.BothApertureHeight) / this.Height).ToString() + "*";
            ApertureGridHeight = ((this.BothApertureHeight) / this.Height).ToString() + "*";
            LeftAperturePositionGridWidth = ((this.LeftAperturePosition) / this.Width).ToString() + "*";
            LeftApertureGridWidth = ((this.LeftApertureWidth) / this.Width).ToString() + "*";
            MainSegmentGridWidth = ((this.Width -
                this.LeftApertureWidth - this.LeftAperturePosition -
                this.RightApertureWidth - this.RightAperturePosition) / this.Width).ToString() + "*";
            RightApertureGridWidth = ((this.RightApertureWidth) / this.Width).ToString() + "*";
            RightAperturePositionGridWidth = ((this.RightAperturePosition) / this.Width).ToString() + "*";
        }
        public void InstallWallPanel(WallPanel newWallPanel)
        {
            this.Height = newWallPanel.Height;
            this.Width = newWallPanel.Width;
            this.BothApertureHeight = newWallPanel.GetApertureHeight(WallPanel.Appertures.leftApperture);
            this.LeftApertureWidth = newWallPanel.GetApertureWidth(WallPanel.Appertures.leftApperture);
            this.LeftAperturePosition = newWallPanel.GetAperturePosition(WallPanel.Appertures.leftApperture);
            this.RightAperturePosition = newWallPanel.GetAperturePosition(WallPanel.Appertures.rightApperture);
            this.RightApertureWidth = newWallPanel.GetApertureWidth(WallPanel.Appertures.rightApperture);
            this.Height = newWallPanel.Height;
            this.Width = newWallPanel.Width;
            this.GridWidthRefresh();
            this.GridHeightRefresh();
        }
    }
}
