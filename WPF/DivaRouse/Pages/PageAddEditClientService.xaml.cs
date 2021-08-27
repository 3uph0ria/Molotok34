using DivaRouse.Classes;
using DivaRouse.Models;
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

namespace DivaRouse.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAddEditClientService.xaml
    /// </summary>
    public partial class PageAddEditClientService : Page
    {
        private ClientService _ccurrnetClientService = new ClientService();
        public PageAddEditClientService(ClientService selectClientService)
        {
            InitializeComponent();
            if (selectClientService != null)
            {
                _ccurrnetClientService = selectClientService;
                CBoxServices.SelectedItem = selectClientService.Services;
                CBoxClients.SelectedItem = selectClientService.Clients;
            }

            DataContext = _ccurrnetClientService;
            CBoxServices.ItemsSource = WildberriesEntities1.GetContext().Services.ToList();
            CBoxClients.ItemsSource = WildberriesEntities1.GetContext().Clients.ToList();
        }

        private void BtnAddservice_Click(object sender, RoutedEventArgs e)
        {
            Services p = (Services)CBoxServices.SelectedItem;
            Clients o = (Clients)CBoxClients.SelectedItem;
            _ccurrnetClientService.IdService = p.Id;
            _ccurrnetClientService.IdClient = o.Id;

            if (_ccurrnetClientService.Id == 0)
                WildberriesEntities1.GetContext().ClientService.Add(_ccurrnetClientService);

            try
            {
                WildberriesEntities1.GetContext().SaveChanges();
                MessageBox.Show("Продажа успешно сохранена");
                NavManager.AccountFrame.Navigate(new PageClientService(NavManager.BtnClientService.Content + ""));
            }
            catch (DbEntityValidationException ex)
            {
                if (ApplicationConfig.IsDev)
                {
                    foreach (var errors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in errors.ValidationErrors)
                        {
                            MessageBox.Show(validationError.ErrorMessage);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Произошла ошибка", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
    }
}
