using Molotok34.Api;
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
    /// Логика взаимодействия для PageClients.xaml
    /// </summary>
    public partial class PageClients : Page
    {
        ApiClient apiClient = new ApiClient("NetTcpBinding_IApi");
        public PageClients()
        {
            InitializeComponent();

            try
            {
                DGridClients.ItemsSource = apiClient.GetClients();
            }
            catch
            {
                MessageBox.Show("Включите WCF host и перезапустите приложение.");
                return;
            }
        }

        private void BtnEditClients_Click(object sender, RoutedEventArgs e)
        {
            NavManager.AccountFrame.Navigate(new PageAddEditCLients((sender as Button).DataContext as Molotok34.Api.Clients));
        }

        private void BtnAddClient_Click(object sender, RoutedEventArgs e)
        {
            NavManager.AccountFrame.Navigate(new PageAddEditCLients(null));
        }

        private void BtnDelClient_Click(object sender, RoutedEventArgs e)
        {
            var client = (sender as Button).DataContext as Clients;

            try
            {
                if (MessageBox.Show("Вы действительно хотите удалить клиента " + client.FullName + "?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    apiClient.DeleteClients(client.Id, client);
                    DGridClients.ItemsSource = apiClient.GetClients();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
    }
}
