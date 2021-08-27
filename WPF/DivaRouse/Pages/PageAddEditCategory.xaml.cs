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
    /// Логика взаимодействия для PageAddEditCategory.xaml
    /// </summary>
    public partial class PageAddEditCategory : Page
    {
        private Categoris _ccurrnetClients = new Categoris();
        public PageAddEditCategory(Categoris selectClient)
        {
            InitializeComponent();
            if (selectClient != null)
            {
                _ccurrnetClients = selectClient;
            }

            DataContext = _ccurrnetClients;
        }

        private void BtnAddClient_Click(object sender, RoutedEventArgs e)
        {
            if (_ccurrnetClients.Id == 0)
                WildberriesEntities1.GetContext().Categoris.Add(_ccurrnetClients);

            try
            {
                WildberriesEntities1.GetContext().SaveChanges();
                MessageBox.Show("Категория успешно сохранена");
                NavManager.AccountFrame.Navigate(new PageCategorys());
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
