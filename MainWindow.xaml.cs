using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Hanoi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 1; i <= 8; i++)
            {
                Label disk = new Label();
                disk.HorizontalContentAlignment = HorizontalAlignment.Center;
                disk.Margin = new Thickness(2);
                disk.BorderBrush = Brushes.Black;
                disk.BorderThickness = new Thickness(1);
                disk.Content = i;
                disk.MouseDown += Disk_MouseDown;
                spRod1.Children.Add(disk);
            }
        }

        private void Disk_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragDrop.DoDragDrop((Label)sender, sender, DragDropEffects.Move);
        }

        private void Rod_Drop(object sender, DragEventArgs e)
        {
            Console.WriteLine("Drop");

            Label disk = (Label)e.Data.GetData(typeof(Label));
            StackPanel sourceRod = (StackPanel)disk.Parent;

            StackPanel targetRod = (StackPanel)((Grid)((Border)sender).Child).Children[1];
            
            if (targetRod != sourceRod)
            {
                sourceRod.Children.Remove(disk);
                targetRod.Children.Insert(0, disk);
                Console.WriteLine("Moved");
            }
        }
    }
}
