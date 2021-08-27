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
    /// Логика взаимодействия для PageCustumers.xaml
    /// </summary>
    public partial class PageCustumers : Page
    {
        public PageCustumers()
        {
            InitializeComponent();
            DGridClients.ItemsSource = WildberriesEntities1.GetContext().Custumers.ToList();
        }

        private void BtnAddClient_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEditClient_Click(object sender, RoutedEventArgs e)
        {
            NavManager.AccountFrame.Navigate(new PageAddEditCustumers((sender as Button).DataContext as Custumers));
        }

        private void BtndelService_Click(object sender, RoutedEventArgs e)
        {
            Custumers currentService = (sender as Button).DataContext as Custumers;
            if (MessageBox.Show("Вы действительно хотите удалить поставщика " + currentService.Name + "?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                WildberriesEntities1.GetContext().Custumers.Remove(currentService);
                WildberriesEntities1.GetContext().SaveChanges();
                NavManager.AccountFrame.Navigate(new PageCustumers());
            }
        }

        private void BtnAddClient_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAddClient_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
