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
        private Label dragDisk;
        private StackPanel sourceStack;
        private StackPanel targetStack;

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
                disk.Margin = new Thickness(3);
                disk.BorderThickness = new Thickness(1);
                disk.BorderBrush = Brushes.Black;
                disk.FontSize = 14;
                disk.FontWeight = FontWeights.Bold;
                disk.Content = i;
                disk.Tag = i;
                disk.MouseDown += Disk_MouseDown;
                stack1.Children.Add(disk);
            }
        }

        private void Disk_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dragDisk = (Label)sender;
            sourceStack = (StackPanel)dragDisk.Parent;

            // Je mag natuurlijk alleen de bovenste schijf verplaatsen,
            // anders wordt het wel erg makkelijk :)
            if (sourceStack.Children.IndexOf(dragDisk) == 0)
            {
                Console.WriteLine("Starting drag");
                DragDrop.DoDragDrop(dragDisk, dragDisk, DragDropEffects.Move);
            }
        }

        private void Stack_DragOver(object sender, DragEventArgs e)
        {
            targetStack = (StackPanel)((Grid)sender).Children[1];

            // Nooit mag een grotere schijf op een kleinere rusten.
            if (targetStack.Children.Count != 0 &&
                (int)dragDisk.Tag > (int)((Label)targetStack.Children[0]).Tag)
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
            }
        }

        private void Stack_Drop(object sender, DragEventArgs e)
        {
            // Nu gaan we de schijf verplaatsen. Een schijf kan natuurlijk niet van een stok naar
            // dezelfde stok worden verplaatst. Deze controle doen we hier i.p.v. in
            // Stack_DragOver() omdat de gebruiker anders meteen een "verboden" icoon ziet.
            if (targetStack != sourceStack)
            {
                sourceStack.Children.Remove(dragDisk);
                targetStack.Children.Insert(0, dragDisk);
                Console.WriteLine("Disk moved");
            }
        }
    }
}
