using Lab20WpfApp.Models;
using Lab20WpfApp1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab20WpfApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        //пытаюсь настроить ширину thumb соответствующую ширине проема
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ////slider1.;
            //ControlTemplate sliderTemplate = (ControlTemplate)this.FindResource("SliderThumbHorizontalDefault");
            //Thumb customThumb = new Thumb();
            //customThumb.Template = sliderTemplate;
            //Rectangle rectangle = (Rectangle)sliderTemplate.FindName("grip", customThumb);
            //rectangle.Width = 40;

            //ControlTemplate existingTemplate = (ControlTemplate)this.FindResource("SliderThumbHorizontalDefault");
            //ControlTemplate newTemplate = new ControlTemplate();

            //FrameworkElementFactory root = new FrameworkElementFactory(typeof(FrameworkElement));
            //// Примените изменения к элементам ControlTemplate
            //// Например, для изменения всех текстовых блоков в шаблоне:
            //foreach (FrameworkElementFactory child in existingTemplate.VisualTree.NextSibling)
            //{
            //    if (child.Type == typeof(TextBlock))
            //    {
            //        child.SetValue(TextBlock.ForegroundProperty, Brushes.Red);
            //        // Продолжайте добавлять дополнительные изменения по мере необходимости
            //    }
            //}

            //newTemplate.VisualTree = root;

        }
    }
}
