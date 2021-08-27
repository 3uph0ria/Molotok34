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
    /// Логика взаимодействия для PageClients.xaml
    /// </summary>
    public partial class PageClients : Page
    {
        public PageClients(string header)
        {
            InitializeComponent();
            Header.Text = header;
            DGridClients.ItemsSource = WildberriesEntities1.GetContext().Clients.ToList();
        }

        private void BtnEditClient_Click(object sender, RoutedEventArgs e)
        {
            NavManager.AccountFrame.Navigate(new PageAddEditClient((sender as Button).DataContext as Clients));
        }

        private void BtnAddClient_Click(object sender, RoutedEventArgs e)
        {
            
            NavManager.AccountFrame.Navigate(new PageAddEditClient(null));
        }

        private void BtndelClient_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtndelService_Click(object sender, RoutedEventArgs e)
        {
            Clients currentService = (sender as Button).DataContext as Clients;
            if (MessageBox.Show("Вы действительно хотите удалить клиента " + currentService.Fullname + "?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                WildberriesEntities1.GetContext().Clients.Remove(currentService);
                WildberriesEntities1.GetContext().SaveChanges();
                NavManager.AccountFrame.Navigate(new PageClients(NavManager.BtClients.Content + ""));
            }
        }
    }
}
