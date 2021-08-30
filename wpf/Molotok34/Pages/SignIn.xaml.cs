using Molotok34.Api;
using Molotok34.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace Molotok34.Pages
{
    /// <summary>
    /// Логика взаимодействия для SignIn.xaml
    /// </summary>
    public partial class SignIn : Page
    {
        ApiClient apiClient = new ApiClient("NetTcpBinding_IApi");
        public SignIn()
        {
            InitializeComponent();
        }

        private void BtnSignInAdmin_Click(object sender, RoutedEventArgs e)
        {
            var user = new List<Admins>();
            Molotok34.Api.Admins searchUser = new Admins();

            try
            {
                user = apiClient.GetAdmins().ToList();
            }
            catch
            {
                MessageBox.Show("Включите WCF host и перезапустите приложение.");
                return;
            }

            if (ApplicationConfig.IsDev)
            {
                user = user.Where(p => p.IdPermission == 1).ToList();
                searchUser = user.FirstOrDefault();
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
                user = user.Where(p => p.Password.ToLower().Contains(GetHash(Password.Password).ToLower())).ToList();
                searchUser = user.FirstOrDefault();
            }


            if (searchUser == null)
            {
                MessageBox.Show("Наверный логин или пароль", "внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (searchUser.Permissions.AccessProducts == 1) CurrentUser.AccessProducts = true;
            if (searchUser.Permissions.AccessClients == 1) CurrentUser.AccessClients = true;
            if (searchUser.Permissions.AccessCategories == 1) CurrentUser.AccessCategories = true;
            CurrentUser.PermissionName = searchUser.Permissions.Name;

            NavManager.MainFrame.Navigate(new Account());
        }

        public static string GetHash(string password)
        {
            using (var hash = SHA1.Create())
            {
                return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));
            }
        }
    }
}
