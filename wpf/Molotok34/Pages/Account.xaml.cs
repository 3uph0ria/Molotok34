using Molotok34.Classes;
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

namespace Molotok34.Pages
{
    /// <summary>
    /// Логика взаимодействия для Account.xaml
    /// </summary>
    public partial class Account : Page
    {
        public Account()
        {
            InitializeComponent();
            AccountFrame.Navigate(new PageServices());
            NavManager.AccountFrame = AccountFrame;
        }

        private void Btnback_Click(object sender, RoutedEventArgs e)
        {
            NavManager.MainFrame.Navigate(new SignIn());
        }

        private void BtnCategories_Click(object sender, RoutedEventArgs e)
        {
            AccountFrame.Navigate(new PageCategories());
        }

        private void BtnSales_Click(object sender, RoutedEventArgs e)
        {
            AccountFrame.Navigate(new PageSales());
        }

        private void BtnClients_Click(object sender, RoutedEventArgs e)
        {
            AccountFrame.Navigate(new PageClients());
        }

        private void BtnProducts_Click(object sender, RoutedEventArgs e)
        {
            AccountFrame.Navigate(new PageServices());
        }

        private void BtnReports_Click(object sender, RoutedEventArgs e)
        {
            AccountFrame.Navigate(new PageReports());
        }
    }
}
