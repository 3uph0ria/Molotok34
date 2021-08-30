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
    /// Логика взаимодействия для PageAddEditCategories.xaml
    /// </summary>
    public partial class PageAddEditCategories : Page
    {
        ApiClient apiClient = new ApiClient("NetTcpBinding_IApi");
        private Molotok34.Api.Categories _ccurrnetCategory = new Molotok34.Api.Categories();
        public PageAddEditCategories(Molotok34.Api.Categories selectCategory)
        {
            InitializeComponent();
            if (selectCategory != null)
            {
                _ccurrnetCategory = selectCategory;
            }

            DataContext = _ccurrnetCategory;
        }

        private void BtnAddCategory_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder erros = new StringBuilder();

            if(CurrentUser.AccessCategories == false)
                erros.AppendLine("У Вас нету доступа для внесения изминений в ктаегории");
            else if (String.IsNullOrEmpty(Name.Text))
                erros.AppendLine("Введите наименование");

            if (erros.Length > 0)
            {
                MessageBox.Show(erros.ToString());
                return;
            }

            try
            {
                if (_ccurrnetCategory.Id == 0)
                {
                    apiClient.PostCategories(_ccurrnetCategory);
                }
                else
                {
                    apiClient.PutCategories(_ccurrnetCategory.Id, _ccurrnetCategory);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            MessageBox.Show("Категория успешно сохранена");
            NavManager.AccountFrame.Navigate(new PageCategories());
        }
    }
}
