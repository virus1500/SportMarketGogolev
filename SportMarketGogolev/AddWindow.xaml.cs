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
using System.Windows.Shapes;

namespace SportMarketGogolev
{
    /// <summary>
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public Product product {  get; set; }
        public AddWindow()
        {
            InitializeComponent();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var costParse = decimal.TryParse(CostTB.Text, out decimal result);
            if (costParse == false)
            {
                MessageBox.Show("Введите число");
            }
            else
            {

            product =  new Product()
            {
                Name = NameTB.Text,
                Cost = result,
                Compountd = CompoundTB.Text
            };
            this.DialogResult = true;
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
