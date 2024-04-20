using Lab20WpfApp.Models;
using Lab20WpfApp1.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<Family> families;

        public MainWindow()
        {
            InitializeComponent();

            families = new ObservableCollection<Family>();

        // families.Add(WallPanel Panel);

        Family family1 = new WallPanel()
        {            
            AmountOfSamples = 5
        };

        Family family2 = new WallPanel()
        {            
            AmountOfSamples = 3,
        };
            MainWindowViewModel viewModel = new MainWindowViewModel();
            Family currentFamily = viewModel.wallPanel;

            families.Add(family1);
            families.Add(family2);
            families.Add(currentFamily);

            //теперь нужно всё это поместить во всплывающий список сверху и добавить опцию сохранения
            listBox.ItemsSource = families;
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
               
        //попытка изменить свойства выбранной панели - не понятно как связать между собой объекты window и WindowViewModel
        private void listBox_Selected(object sender, SelectionChangedEventArgs e)
        {
            string newMessage = (listBox.SelectedItem as Family).Name;
            
            MessageBox.Show("Вы выбрали " + newMessage);
            
        }
    }
}
