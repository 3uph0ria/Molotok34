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
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DivaRouse.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageReports.xaml
    /// </summary>
    public partial class PageReports : Page
    {
        public PageReports(string header)
        {
            InitializeComponent();
            Header.Text = header;
            ChatPayments.ChartAreas.Add(new System.Windows.Forms.DataVisualization.Charting.ChartArea());
            ChatPayments.Series.Add("Прибыль"); // Add this line
            var payments = WildberriesEntities1.GetContext().ClientService.ToList();
            foreach (var r in payments)
            {
                ChatPayments.Series["Прибыль"].Points.AddXY(r.Date, r.Services.Cost);
            }
            ChatPayments.DataBind();
        }
    }
}
