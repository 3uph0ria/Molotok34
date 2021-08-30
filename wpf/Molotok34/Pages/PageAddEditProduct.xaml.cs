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
                CBoxServices.SelectedItem = _ccurrnetProduct.Categories;
            }
                
            DataContext = _ccurrnetProduct;
            CBoxServices.ItemsSource = apiClient.GetCategories().ToList();
        }

        private void BtnAddservice_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder erros = new StringBuilder();

            if (CurrentUser.AccessProducts == false)
                erros.AppendLine("У Вас нету доступа для внесения изминений в каталог товаров");
            else if (String.IsNullOrEmpty(Name.Text))
                erros.AppendLine("Введите наименование");
            else if (String.IsNullOrEmpty(Cost.Text))
                erros.AppendLine("Введите цену");
            else if (String.IsNullOrEmpty(Img.Text))
                erros.AppendLine("Введите картинку");
            else if (String.IsNullOrEmpty(Amount.Text))
                erros.AppendLine("Введите кол-во товара на складе");
            else if (String.IsNullOrEmpty(Stars.Text))
                erros.AppendLine("Введите кол-во звезд товара");
            else if (_ccurrnetProduct.Cost <= 0)
                erros.AppendLine("Цена должна быть больше 0");
            else if (_ccurrnetProduct.Amount < 0)
                erros.AppendLine("Кол-во товара на складе должно быть больше или ровно 0");
            else if (_ccurrnetProduct.Stars < 0)
                erros.AppendLine("Кол-во звезд товара должно быть больше или ровно 0");

            if (erros.Length > 0)
            {
                MessageBox.Show(erros.ToString());
                return;
            }

            Categories p = (Categories)CBoxServices.SelectedItem;
            _ccurrnetProduct.IdCategory = p.Id;

            try
            {
                if (_ccurrnetProduct.Id == 0)
                {
                    apiClient.PostProducts(_ccurrnetProduct);
                }
                else
                {
                    apiClient.PutProducts(_ccurrnetProduct.Id, _ccurrnetProduct);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            MessageBox.Show("Товар успешно сохранен");
            NavManager.AccountFrame.Navigate(new PageServices());
        }
    }
}
