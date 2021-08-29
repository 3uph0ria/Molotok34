using Molotok34.Api;
using Molotok34.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
    public partial class PageAddEditService : Page
    {
        ApiClient apiClient = new ApiClient("NetTcpBinding_IApi");
        private Molotok34.Api.Products _ccurrnetProduct = new Molotok34.Api.Products();
        public PageAddEditService(Molotok34.Api.Products selectProduct)
        {
            InitializeComponent();
            if (selectProduct != null)
            {
                _ccurrnetProduct = selectProduct;
                CBoxServices.SelectedItem = selectProduct.Categories;
            }
                
            DataContext = _ccurrnetProduct;
            CBoxServices.ItemsSource = apiClient.GetCategories();
        }

        private void BtnAddservice_Click(object sender, RoutedEventArgs e)
        {
            if (_ccurrnetProduct.Cost <= 0)
            {
                MessageBox.Show("Цена должна быть больше 0");
                return;
            }

            Categories p = (Categories)CBoxServices.SelectedItem;
            _ccurrnetProduct.IdCategory = p.Id;

            if (_ccurrnetProduct.Id == 0)
            {
                apiClient.PostProducts(_ccurrnetProduct);
            }
            else
            {
                apiClient.PutProducts(_ccurrnetProduct.Id, _ccurrnetProduct);
            }

            MessageBox.Show("Товар успешно сохранен");
            NavManager.AccountFrame.Navigate(new PageServices());
        }
    }
}
