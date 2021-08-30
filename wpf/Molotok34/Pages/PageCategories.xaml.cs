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
    /// Логика взаимодействия для PageCategories.xaml
    /// </summary>
    public partial class PageCategories : Page
    {
        ApiClient apiClient = new ApiClient("NetTcpBinding_IApi");
        public PageCategories()
        {
            InitializeComponent();

            try
            {
                DGridCategories.ItemsSource = apiClient.GetCategories();
            }
            catch
            {
                MessageBox.Show("Включите WCF host и перезапустите приложение.");
                return;
            }
        }

        private void BtnEditCategory_Click(object sender, RoutedEventArgs e)
        {
            NavManager.AccountFrame.Navigate(new PageAddEditCategories((sender as Button).DataContext as Molotok34.Api.Categories));
        }

        private void BtnDelCategory_Click(object sender, RoutedEventArgs e)
        {
            var category = (sender as Button).DataContext as Categories;

            try
            {
                if (MessageBox.Show("Вы действительно хотите удалить категорию " + category.Name + "?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    apiClient.DeleteCategories(category.Id, category);
                    DGridCategories.ItemsSource = apiClient.GetCategories();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void BtnAddCategory_Click(object sender, RoutedEventArgs e)
        {
            NavManager.AccountFrame.Navigate(new PageAddEditCategories(null));
        }
    }
}
