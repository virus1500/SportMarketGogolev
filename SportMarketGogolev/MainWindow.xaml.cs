using SportMarketGogolev.Model;
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
        private List<Product> products = App.context.Product.ToList();
        public MainWindow()
        {
            InitializeComponent();

            InfoLV.ItemsSource = products;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow();
            if (addWindow.ShowDialog() == true)
            {
                var newItem = addWindow.product;

                App.context.Product.Add(newItem);
                App.context.SaveChanges();

                products.Add(newItem);
                InfoLV.ItemsSource = null;
                InfoLV.ItemsSource = products;
            }
            else
            {
                return;
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Product)InfoLV.SelectedItem;

            var product = App.context.Product.Find(selectedItem.id);

            if (selectedItem != null)
            {
                product.Order.Clear();
                App.context.Product.Remove(selectedItem);
                App.context.SaveChanges();

                products.Remove(selectedItem);
                InfoLV.ItemsSource = null;
                InfoLV.ItemsSource = products;
            }
            else
            {
                MessageBox.Show("Выберите продукт");
            }
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            InfoLV.ItemsSource = products.Where(searchProd => searchProd.Name.ToLower().Contains(SearchTB.Text.ToLower())|| searchProd.Compountd.ToLower().Contains(SearchTB.Text.ToLower()));
        }
    }
}
