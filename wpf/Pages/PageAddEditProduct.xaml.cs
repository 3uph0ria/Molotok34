using Molotok34.Classes;
using Molotok34.Models;
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
        private Products _ccurrnetProduct = new Products();
        public PageAddEditService(Products selectProduct)
        {
            InitializeComponent();
            if (selectProduct != null)
            {
                _ccurrnetProduct = selectProduct;
                CBoxServices.SelectedItem = selectProduct.Categories;
            }
                
            DataContext = _ccurrnetProduct;
            CBoxServices.ItemsSource = Api.GetCategories();
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
                Api.PostProducts(_ccurrnetProduct);
            }
            else
            {
                Api.PutProducts(_ccurrnetProduct.Id, _ccurrnetProduct);
            }

            MessageBox.Show("Товар успешно сохранен");
            NavManager.AccountFrame.Navigate(new PageServices());
        }
    }
}
