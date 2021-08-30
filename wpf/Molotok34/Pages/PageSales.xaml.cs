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
    /// Логика взаимодействия для PageSales.xaml
    /// </summary>
    public partial class PageSales : Page
    {
        ApiClient apiClient = new ApiClient("NetTcpBinding_IApi");
        public PageSales()
        {
            InitializeComponent();

            try
            {
                DGridSales.ItemsSource = apiClient.GetSales();
            }
            catch
            {
                MessageBox.Show("Включите WCF host и перезапустите приложение.");
                return;
            }
        }

        private void BtnDelSale_Click(object sender, RoutedEventArgs e)
        {
            var sale = (sender as Button).DataContext as Sales;

            try
            {
                if (MessageBox.Show("Вы действительно хотите удалить продажу №" + sale.Id + "?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    apiClient.DeleteSales(sale.Id, sale);
                    DGridSales.ItemsSource = apiClient.GetSales();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void BtnAddSale_Click(object sender, RoutedEventArgs e)
        {
            NavManager.AccountFrame.Navigate(new PageAddEditSales(null));
        }
    }
}
