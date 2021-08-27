using DivaRouse.Classes;
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
    /// Логика взаимодействия для Account.xaml
    /// </summary>
    public partial class Account : Page
    {
        public Account()
        {
            InitializeComponent();

            if (CurrentUser.AccessClients == false) BtnClients.Visibility = Visibility.Hidden;
            if (CurrentUser.AccessClientsServicrs == false) BtnClientService.Visibility = Visibility.Hidden;
            //if (CurrentUser.AccessReport == false) BtnReports.Visibility = Visibility.Hidden;
            if (CurrentUser.AccessServices == false) BtnServices.Visibility = Visibility.Hidden;
            if(CurrentUser.PermissionName == "Администратор") BtnCategorys1.Visibility = Visibility.Hidden;


            NavManager.AccountFrame = AccountFrame;
            NavManager.BtClients = BtnClients;
            NavManager.BtnServices = BtnServices;
            NavManager.BtnClientService = BtnClientService;
            //NavManager.BtnReports = BtnReports;

            AccountFrame.Navigate(new PageServices(NavManager.BtnServices.Content + ""));
        }

        private void BtnServices_Click_1(object sender, RoutedEventArgs e)
        {
            AccountFrame.Navigate(new PageServices(NavManager.BtnServices.Content + ""));
        }

        private void BtnClients_Click_1(object sender, RoutedEventArgs e)
        {
            AccountFrame.Navigate(new PageClients(NavManager.BtClients.Content + ""));
        }

        private void BtnClientService_Click_1(object sender, RoutedEventArgs e)
        {
            AccountFrame.Navigate(new PageClientService(NavManager.BtnClientService.Content + ""));
        }


        private void Btnback_Click(object sender, RoutedEventArgs e)
        {
            NavManager.MainFrame.Navigate(new SignIn());
        }

        private void BtnCategorys_Click(object sender, RoutedEventArgs e)
        {
            AccountFrame.Navigate(new PageCategorys());
        }

        private void BtnCategorys_Click_1(object sender, RoutedEventArgs e)
        {
            AccountFrame.Navigate(new PageCustumers());
        }
    }
}
