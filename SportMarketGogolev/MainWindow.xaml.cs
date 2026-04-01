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
        public MainWindow()
        {
            InitializeComponent();

            SwithCmb.Items.Add("Продукты");
            SwithCmb.Items.Add("Заказы");
            SwithCmb.Items.Add("Пользователи");

            SwithCmb.SelectedIndex = 0;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SwithCmb.SelectedIndex == 0)
            {
                var products = App.context.Product.ToList();

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
            else
            {
                return;
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {

            if (SwithCmb.SelectedIndex == 0)
            {
                var selectedItem = (Product)InfoLV.SelectedItem;
                var product = App.context.Product.Find(selectedItem.id);

                var products = App.context.Product.ToList();

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
            else { return; }
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SwithCmb.SelectedIndex == 0)
            {
                var products = App.context.Product.ToList();

                InfoLV.ItemsSource = products.Where(searchProd => searchProd.Name.ToLower().Contains(SearchTB.Text.ToLower())|| searchProd.Compountd.ToLower().Contains(SearchTB.Text.ToLower()));
            }
            if (SwithCmb.SelectedIndex == 1)
            {
                var orders = App.context.Order.ToList();

                InfoLV.ItemsSource = orders.Where(search => search.id.ToString().Contains(SearchTB.Text));
            }
            if (SwithCmb.SelectedIndex == 2)
            {
                var users = App.context.User.ToList();

                InfoLV.ItemsSource = users.Where(search => search.Login.ToLower().Contains(SearchTB.Text.ToLower()) || search.Email.ToLower().Contains(SearchTB.Text.ToLower()));
            }
        }

        private void SwithCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SwithCmb.SelectedIndex == 0)
            {
                InfoLV.ItemsSource = App.context.Product.ToList();
                SetColumns("Product");
                SearchTB.Text = "";
                AddBtn.Visibility = Visibility.Visible;
                Remove.Visibility = Visibility.Visible;
            }

            if (SwithCmb.SelectedIndex == 1)
            {
                InfoLV.ItemsSource = App.context.Order.ToList();
                SetColumns("Order");
                SearchTB.Text = "";
                AddBtn.Visibility = Visibility.Collapsed;
                Remove.Visibility = Visibility.Collapsed;
            }

            if (SwithCmb.SelectedIndex == 2)
            {
                InfoLV.ItemsSource = App.context.User.ToList();
                SetColumns("User");
                SearchTB.Text = "";
                AddBtn.Visibility = Visibility.Collapsed;
                Remove.Visibility = Visibility.Collapsed;
            }
        }
        private void SetColumns(string type)
        {
            GridView gridView = new GridView();
            InfoLV.View = gridView;

            if (type == "Product")
            {
                gridView.Columns.Add(new GridViewColumn { Header = "Название", DisplayMemberBinding = new Binding("Name") });
                gridView.Columns.Add(new GridViewColumn { Header = "Цена", DisplayMemberBinding = new Binding("Cost") });
                gridView.Columns.Add(new GridViewColumn { Header = "Состав", DisplayMemberBinding = new Binding("Compountd") });
            }

            if (type == "Order")
            {
                gridView.Columns.Add(new GridViewColumn { Header = "ID", DisplayMemberBinding = new Binding("id") });
                gridView.Columns.Add(new GridViewColumn { Header = "Дата", DisplayMemberBinding = new Binding("DateOrder") });
                gridView.Columns.Add(new GridViewColumn { Header = "UserId", DisplayMemberBinding = new Binding("User.Login") });
            }

            if (type == "User")
            {
                gridView.Columns.Add(new GridViewColumn { Header = "Логин", DisplayMemberBinding = new Binding("Login") });
                gridView.Columns.Add(new GridViewColumn { Header = "Email", DisplayMemberBinding = new Binding("Email") });
            }
        }
    }
}
