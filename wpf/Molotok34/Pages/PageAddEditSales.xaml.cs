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
    /// Логика взаимодействия для PageAddEditSales.xaml
    /// </summary>
    public partial class PageAddEditSales : Page
    {
        ApiClient apiClient = new ApiClient("NetTcpBinding_IApi");
        private Molotok34.Api.Sales _ccurrnetSale = new Molotok34.Api.Sales();
        public PageAddEditSales(Molotok34.Api.Sales selectSale)
        {
            InitializeComponent();
            if (selectSale != null)
            {
                _ccurrnetSale = selectSale;
                CBoxClients.SelectedItem = _ccurrnetSale.Clients;
                CBoxProducts.SelectedItem = _ccurrnetSale.Products;
            }

            DataContext = _ccurrnetSale;
            CBoxClients.ItemsSource = apiClient.GetClients().ToList();
            CBoxProducts.ItemsSource = apiClient.GetProducts().ToList();
        }

        private void BtnAddSale_Click(object sender, RoutedEventArgs e)
        {
            Categories idClient = (Categories)CBoxClients.SelectedItem;
            _ccurrnetSale.IdClient = idClient.Id;
            Categories idProduct = (Categories)CBoxProducts.SelectedItem;
            _ccurrnetSale.IdProduct = idProduct.Id;
            _ccurrnetSale.Date = DateTime.Now;

            StringBuilder erros = new StringBuilder();

            if (CurrentUser.AccessCategories == false)
                erros.AppendLine("У Вас нету доступа для внесения изминений в продажи");
            else if (_ccurrnetSale.IdClient <= 0)
                erros.AppendLine("Выберите клиента");
            else if (_ccurrnetSale.IdProduct <= 0)
                erros.AppendLine("Выберите товар");

            if (erros.Length > 0)
            {
                MessageBox.Show(erros.ToString());
                return;
            }

            try
            {
                if (_ccurrnetSale.Id == 0)
                {
                    apiClient.PostSales(_ccurrnetSale);
                }
                else
                {
                    apiClient.PutSales(_ccurrnetSale.Id, _ccurrnetSale);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            MessageBox.Show("Продажа успешно сохранена");
            NavManager.AccountFrame.Navigate(new PageSales());
        }
    }
}
