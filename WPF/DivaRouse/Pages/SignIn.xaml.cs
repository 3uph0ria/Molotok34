using DivaRouse.Classes;
using DivaRouse.Models;
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

namespace DivaRouse.Pages
{
    /// <summary>
    /// Логика взаимодействия для SignIn.xaml
    /// </summary>
    public partial class SignIn : Page
    {
        public SignIn()
        {
            InitializeComponent();
        }

        private void BtnSignInAdmin_Click(object sender, RoutedEventArgs e)
        {
            var user =  WildberriesEntities1.GetContext().Admins.ToList();


            if (ApplicationConfig.IsDev)
            {
                user = user.Where(p => p.Login.ToLower().Contains("director".ToLower())).ToList();
                user = user.Where(p => p.Password.ToLower().Contains("1234".ToLower())).ToList();
            }
            else
            {
                StringBuilder erros = new StringBuilder();

                if (String.IsNullOrEmpty(Login.Text))
                    erros.AppendLine("Введите логин");
                else if (String.IsNullOrEmpty(Password.Password))
                    erros.AppendLine("Введите пароль");

                if (erros.Length > 0)
                {
                    MessageBox.Show(erros.ToString());
                    return;
                }

                user = user.Where(p => p.Login.ToLower().Contains(Login.Text.ToLower())).ToList();
                user = user.Where(p => p.Password.ToLower().Contains(Password.Password.ToLower())).ToList();
            }

            Admins searchuser = user.FirstOrDefault();
            if (searchuser == null)
            {
                MessageBox.Show("Наверный логин или пароль", "внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            CurrentUser.PermissionName = searchuser.Permissions.Name;

            if (searchuser.Permissions.AccessClients == 1) CurrentUser.AccessClients = true;
            else CurrentUser.AccessClients = false;

            if (searchuser.Permissions.AccessServices == 1) CurrentUser.AccessServices = true;
            else CurrentUser.AccessServices = false;

            if (searchuser.Permissions.AccessClientService == 1) CurrentUser.AccessClientsServicrs = true;
            else CurrentUser.AccessClientsServicrs = false;

            if (searchuser.Permissions.AccessReports == 1) CurrentUser.AccessReport = true;
            else CurrentUser.AccessReport = false;

            NavManager.MainFrame.Navigate(new Account());
        }

    }
}
