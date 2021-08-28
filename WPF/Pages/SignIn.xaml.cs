using Molotok34.Classes;
using Molotok34.Models;
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
        public SignIn()
        {
            InitializeComponent();
        }

        private void BtnSignInAdmin_Click(object sender, RoutedEventArgs e)
        {
            var user = Api.GetAdmins();

            if (ApplicationConfig.IsDev)
            {
                user = user.Where(p => p.Login.ToLower().Contains("amdin")).ToList();
                user = user.Where(p => p.Password.ToLower().Contains(GetHash("1234"))).ToList();
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
            }

            Admins searchuser = user.FirstOrDefault();

            if (searchuser == null)
            {
                MessageBox.Show("Наверный логин или пароль", "внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

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
