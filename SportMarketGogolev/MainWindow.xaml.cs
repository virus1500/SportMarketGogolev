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

namespace SportMarketGogolev
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InfoLV.ItemsSource = App.context.Order.ToList();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = InfoLV.SelectedItems;
            if (selectedItem != null)
            {
                App.context.Order.Remove(InfoLV.SelectedItems);
                App.context.SaveChanges();
                InfoLV.ItemsSource = App.context.Order.ToList();
            }
            else
            {
                MessageBox.Show("Выберите продукт");
            }
        }
    }
}
