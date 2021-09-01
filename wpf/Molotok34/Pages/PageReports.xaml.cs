using Molotok34.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Molotok34.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageReports.xaml
    /// </summary>
    public partial class PageReports : Page
    {
        ApiClient apiClient = new ApiClient("NetTcpBinding_IApi");
        public PageReports()
        {
            InitializeComponent();

            //try
            //{
            //    var clients = apiClient.GetClients().ToList();

            //    ChatPayments.ChartAreas.Add(new ChartArea("Main"));
            //    var cuurentSerias = new Series("Payments")
            //    {
            //        IsValueShownAsLabel = true
            //    };

            //    CBoxUsers.ItemsSource = clients;
            //    CBoxTypeChart.ItemsSource = Enum.GetValues(typeof(SeriesChartType));

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    return;
            //}
        }

        private void BtnCreateReport_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateChart(object sender, SelectionChangedEventArgs e)
        {
            //if(CBoxUsers.SelectedItem is Clients currentUser && CBoxTypeChart.SelectedItem is SeriesChartType currentType)
            //{
            //    Series currentSeries = ChatPayments.Series.FirstOrDefault();
            //    currentSeries.ChartType = currentType;
            //    currentSeries.Points.Clear();

            //    var categoriesList = apiClient.GetCategories().ToList();
            //    var salesList = apiClient.GetSales().ToList();

            //    foreach (var category in categoriesList)
            //    {
            //        currentSeries.Points.AddXY(category.Name, salesList.Where(p => p.Clients == currentUser && p.Products.Categories == category).Sum(p => p.Products.Cost));
            //    }
            //}
        }
    }
}
