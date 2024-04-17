using Lab20WpfApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Lab20WpfApp.Models.WallPanel;

namespace Lab20WpfApp.Models
{
    internal class WallPanel
    {
            private double width = 6000;
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
            }
        }
            private double height = 3000;
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
            }
        }
            private Apperture leftApperture = null;
            private Apperture rightApperture = null;
            public enum Appertures
            {
                leftApperture,
                rightApperture
            }

            public WallPanel()  { }
            public WallPanel(double width, double height) 
            {
                this.Width = width;
                this.Height = height;
            }
            public WallPanel(double width, double height, double widthApp, double heightApp, double positioningApp)
            {
                this.Width = width;
                this.Height = height;
                this.leftApperture = new Apperture(widthApp, heightApp, positioningApp);
            }
            public WallPanel(double width, double height, double widthApp, double heightApp, double positioningApp, double widthApp2, double heightApp2, double positioningApp2)
            {
                this.Width = width;
                this.Height = height;
                this.leftApperture = new Apperture(widthApp, heightApp, positioningApp);
                this.rightApperture = new Apperture(widthApp2, heightApp2, positioningApp2);
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
            public double GetApertureReversedPosition(Appertures apperture)
                {
                    if (apperture == Appertures.leftApperture && this.leftApperture != null)
                    {
                        return Width - this.leftApperture.Position;
                    }
                    else if (apperture == Appertures.rightApperture && this.rightApperture != null)
                    {
                        return Width - this.rightApperture.Position;
                    }
                    return 0;
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
    }
}
