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
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(EmailTB.Text) || string.IsNullOrEmpty(LoginTB.Text)
               || string.IsNullOrEmpty(PassPB.Password) || string.IsNullOrEmpty(PassSPB.Password))
            {
                MessageBox.Show("Введите данные");
            }
            else
            {

                if (App.context.User.Any(u => u.Email == EmailTB.Text || u.Login == LoginTB.Text) == false)
                {
                    if (PassPB.Password == PassSPB.Password)
                    {
                        User userN = new User()
                        {
                            Email = EmailTB.Text,
                            Password = PassPB.Password,
                            Roleid = 2,
                            Login = LoginTB.Text,
                        };

                        App.context.User.Add(userN);
                        App.context.SaveChanges();
                        MessageBox.Show("Аккаун создан успешно", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        AuthorizationWindow authorization = new AuthorizationWindow();
                        authorization.Show();
                        this.Close();
                    }
                    else { MessageBox.Show("Пароли не совпадают"); }

                }
                else { MessageBox.Show("Логин или телефон или почта уже зарегестрированны", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }
    }
}
