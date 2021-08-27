using DivaRouse.Classes;
using DivaRouse.Models;
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

namespace DivaRouse.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageServices.xaml
    /// </summary>
    public partial class PageServices : Page
    {
        public PageServices(string header)
        {
            InitializeComponent();
            ListClient.ItemsSource = WildberriesEntities1.GetContext().Services.ToList();
            Header.Text = header;
            SortCategory.ItemsSource = WildberriesEntities1.GetContext().Categoris.ToList();
        }

        private void BtnEditService_Click(object sender, RoutedEventArgs e)
        {
            NavManager.AccountFrame.Navigate(new PageAddEditService((sender as Button).DataContext as Services));
        }

        private void BtnAddService_Click(object sender, RoutedEventArgs e)
        {
            NavManager.AccountFrame.Navigate(new PageAddEditService(null));
        }

        private void BtndelService_Click(object sender, RoutedEventArgs e)
        {
            Services currentService = (sender as Button).DataContext as Services;
            if (MessageBox.Show("Вы действительно хотите удалить товар " + currentService.Name + "?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                WildberriesEntities1.GetContext().Services.Remove(currentService);
                WildberriesEntities1.GetContext().SaveChanges();
                NavManager.AccountFrame.Navigate(new PageServices(NavManager.BtnServices.Content + ""));
            }
        }

        private void SortCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        public void Update()
        {
            var services = WildberriesEntities1.GetContext().Services.ToList();

            switch (SortCategory.SelectedIndex)
            {
                case 5:
                    services = services.ToList();
                    break;
                case 0:
                    services = services.Where(p => Convert.ToString(p.IdCategory).Contains("1")).ToList(); break;
                case 1:
                    services = services.Where(p => Convert.ToString(p.IdCategory).Contains("2")).ToList(); break;
                case 2:
                    services = services.Where(p => Convert.ToString(p.IdCategory).Contains("3")).ToList(); break;
                case 3:
                    services = services.Where(p => Convert.ToString(p.IdCategory).Contains("4")).ToList(); break;
                case 4:
                    services = services.Where(p => Convert.ToString(p.IdCategory).Contains("5")).ToList(); break;
            }

            ListClient.ItemsSource = services;

        }
    }
}
