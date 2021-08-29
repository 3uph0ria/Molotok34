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
            DGridCategories.ItemsSource = apiClient.GetCategories();
        }
    }
}
