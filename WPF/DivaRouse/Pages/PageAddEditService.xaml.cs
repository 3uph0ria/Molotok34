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
    public partial class PageAddEditService : Page
    {
        private Services _ccurrnetServices = new Services();
        public PageAddEditService(Services selectService)
        {
            InitializeComponent();
            if (selectService != null)
            {
                _ccurrnetServices = selectService;
                CBoxServices.SelectedItem = selectService.Categoris;
                CBoxCustumers.SelectedItem = selectService.Custumers;
            }
                
            DataContext = _ccurrnetServices;
            CBoxServices.ItemsSource = WildberriesEntities1.GetContext().Categoris.ToList();
            CBoxCustumers.ItemsSource = WildberriesEntities1.GetContext().Custumers.ToList();
        }

        private void BtnAddservice_Click(object sender, RoutedEventArgs e)
        {
            if(_ccurrnetServices.Cost <= 0)
            {
                MessageBox.Show("Цена должна быть больше 0");
                return;
            }

            Categoris p = (Categoris)CBoxServices.SelectedItem;
            _ccurrnetServices.IdCategory = p.Id;
            Custumers o = (Custumers)CBoxCustumers.SelectedItem;
            _ccurrnetServices.IdCustumer = p.Id;

            if (_ccurrnetServices.Id == 0)
                WildberriesEntities1.GetContext().Services.Add(_ccurrnetServices);

            try
            {
                WildberriesEntities1.GetContext().SaveChanges();
                MessageBox.Show("Услуга успешно сохранена");
                NavManager.AccountFrame.Navigate(new PageServices(NavManager.BtnServices.Content + ""));
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
