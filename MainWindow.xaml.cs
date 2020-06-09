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

        private int diskCount;
        private int moveCount;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AddDisks(8);
        }

        private void bNewGame_Click(object sender, RoutedEventArgs e)
        {
            // Parse aantal schijven.
            try
            {
                diskCount = int.Parse(tbDiskCount.Text);
                tbDiskCount.Background = Brushes.Transparent;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error parsing disk count: " + ex);
                tbDiskCount.Background = Brushes.Red;
                tbDiskCount.SelectAll();
                tbDiskCount.Focus();
                return;
            }
            if (diskCount < 2)
            {
                MessageBox.Show("Geef minstens 2 schijven op", "Fout",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                tbDiskCount.SelectAll();
                tbDiskCount.Focus();
                return;
            }

            // Beetje jammer als je per ongeluk op "Nieuw spel" drukt als je al heel ver bent...
            if (moveCount > 2)
            {
                if (MessageBox.Show("Opnieuw beginnen?", "Torens van Hanoi",
                        MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
                {
                    return;
                }
            }

            // Maak alle stokken leeg, reset het aantal zetten en voeg nieuwe schijven toe.
            stack1.Children.Clear();
            stack2.Children.Clear();
            stack3.Children.Clear();

            moveCount = 0;
            lMoveCount.Content = "0";

            AddDisks(diskCount);
        }

        private void AddDisks(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Label disk = new Label();
                disk.HorizontalContentAlignment = HorizontalAlignment.Center;
                disk.Margin = new Thickness(3);
                disk.BorderThickness = new Thickness(1);
                disk.BorderBrush = Brushes.Black;
                disk.FontWeight = FontWeights.Bold;
                disk.Content = i + 1;
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
                lMoveCount.Content = ++moveCount;
                Console.WriteLine("Disk moved");

                if (targetStack.Children.Count == diskCount)
                {
                    MessageBox.Show("De wereld zal nu eindigen", "Klaar!",
                        MessageBoxButton.OK, MessageBoxImage.Warning);

                    // Anders komt er een waarschuwing bij het starten van een nieuw spel.
                    moveCount = 0;
                }
            }
        }
    }
}
