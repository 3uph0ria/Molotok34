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
    /// Логика взаимодействия для PageAddEditCLients.xaml
    /// </summary>
    public partial class PageAddEditCLients : Page
    {
        ApiClient apiClient = new ApiClient("NetTcpBinding_IApi");
        private Molotok34.Api.Clients _ccurrnetClient = new Molotok34.Api.Clients();
        public PageAddEditCLients(Molotok34.Api.Clients selectClient)
        {
            InitializeComponent();
            if (selectClient != null)
            {
                _ccurrnetClient = selectClient;
            }

            DataContext = _ccurrnetClient;
        }

        private void BtnAddClient_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder erros = new StringBuilder();

            if (CurrentUser.AccessClients == false)
                erros.AppendLine("У Вас нету доступа для внесения изминений в базу клиентов");
            else if (String.IsNullOrEmpty(FullName.Text))
                erros.AppendLine("Введите ФИО");
            else if (String.IsNullOrEmpty(Phone.Text))
                erros.AppendLine("Введите Телефон");
            else if (String.IsNullOrEmpty(Email.Text))
                erros.AppendLine("Введите Email");

            if (erros.Length > 0)
            {
                MessageBox.Show(erros.ToString());
                return;
            }

            try
            {
                if (_ccurrnetClient.Id == 0)
                {
                    apiClient.PostClients(_ccurrnetClient);
                }
                else
                {
                    apiClient.PutClients(_ccurrnetClient.Id, _ccurrnetClient);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            MessageBox.Show("Товар успешно сохранен");
            NavManager.AccountFrame.Navigate(new PageClients());
        }
    }
}
