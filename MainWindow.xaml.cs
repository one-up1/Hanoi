using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hanoi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Label dragDisk;

        public MainWindow()
        {
            InitializeComponent();

            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 1; i <= 8; i++)
            {
                Label disk = new Label();
                //disk.Width = 100;
                disk.BorderBrush = Brushes.Black;
                disk.BorderThickness = new Thickness(1);
                disk.HorizontalContentAlignment = HorizontalAlignment.Center;
                disk.Content = i;
                disk.MouseDown += Disk_MouseDown;
                spRod1.Children.Add(disk);
            }
        }

        private void Disk_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dragDisk = (Label)sender;
            DragDrop.DoDragDrop(dragDisk, dragDisk.Content, DragDropEffects.Move);
        }

        private void spRod1_PreviewDrop(object sender, DragEventArgs e)
        {
        }

        private void spRod1_Drop(object sender, DragEventArgs e)
        {
            Console.WriteLine("Drop on 1");
            StackPanel rod = (StackPanel)dragDisk.Parent;
            if (rod != spRod1)
            {
                rod.Children.Remove(dragDisk);
                spRod1.Children.Add(dragDisk);
                Console.WriteLine("Moved to 1");
            }
            dragDisk = null;
        }

        private void spRod2_Drop(object sender, DragEventArgs e)
        {
            Console.WriteLine("Drop on 2");
            StackPanel rod = (StackPanel)dragDisk.Parent;
            if (rod != spRod2)
            {
                rod.Children.Remove(dragDisk);
                spRod2.Children.Add(dragDisk);
                Console.WriteLine("Moved to 2");
            }
            dragDisk = null;
        }

        private void spRod3_Drop(object sender, DragEventArgs e)
        {
            Console.WriteLine("Drop on 3");
            StackPanel rod = (StackPanel)dragDisk.Parent;
            if (rod != spRod3)
            {
                rod.Children.Remove(dragDisk);
                spRod3.Children.Add(dragDisk);
                Console.WriteLine("Moved to 3");
            }
            dragDisk = null;
        }
    }
}
