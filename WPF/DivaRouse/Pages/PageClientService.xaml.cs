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
    /// Логика взаимодействия для PageClientService.xaml
    /// </summary>
    public partial class PageClientService : Page
    {
        public PageClientService(string header)
        {
            InitializeComponent();
            Header.Text = header;
            DGridClientServices.ItemsSource = WildberriesEntities1.GetContext().ClientService.ToList();
        }

        private void BtnEditClientService_Click(object sender, RoutedEventArgs e)
        {
            NavManager.AccountFrame.Navigate(new PageAddEditClientService((sender as Button).DataContext as ClientService));
        }

        private void BtnAddClientService_Click(object sender, RoutedEventArgs e)
        {
            NavManager.AccountFrame.Navigate(new PageAddEditClientService(null));
        }

        private void BtndelClientService_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtndelService_Click(object sender, RoutedEventArgs e)
        {
            ClientService currentService = (sender as Button).DataContext as ClientService;
            if (MessageBox.Show("Вы действительно хотите удалить продажу №" + currentService.Id + "?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                WildberriesEntities1.GetContext().ClientService.Remove(currentService);
                WildberriesEntities1.GetContext().SaveChanges();
                NavManager.AccountFrame.Navigate(new PageClientService(NavManager.BtnClientService.Content + ""));
            }
        }
    }
}
