using Molotok34.Api;
using Molotok34.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    /// Логика взаимодействия для PageServices.xaml
    /// </summary>
    public partial class PageServices : Page
    {
        ApiClient apiClient = new ApiClient("NetTcpBinding_IApi");
        public PageServices()
        {
            InitializeComponent();
            DGridProducts.ItemsSource = apiClient.GetProducts();
        }

        private void BtnEditService_Click(object sender, RoutedEventArgs e)
        {
            NavManager.AccountFrame.Navigate(new PageAddEditService((sender as Button).DataContext as Molotok34.Api.Products));
        }

        private void BtnAddService_Click(object sender, RoutedEventArgs e)
        {
            NavManager.AccountFrame.Navigate(new PageAddEditService(null));
        }

        public void Update()
        {
            var products = apiClient.GetProducts().ToList();

            products = products.Where(p => p.Name.Contains(Search.Text.ToLower())).ToList();
            DGridProducts.ItemsSource = products;

        }

        private void BtnDelProduct_Click(object sender, RoutedEventArgs e)
        {
            var product = (sender as Button).DataContext as Products;
            if (MessageBox.Show("Вы действительно хотите удалить товар " + product.Name + "?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                apiClient.DeleteProducts(product.Id, product);
                Update();
            }
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }
    }
}
