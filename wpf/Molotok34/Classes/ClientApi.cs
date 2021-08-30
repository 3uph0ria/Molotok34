using Molotok34.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Molotok34.Classes
{
    class ClientApi
    {
        public static void CheckConnect()
        {
            ApiClient apiClient = new ApiClient("NetTcpBinding_IApi");

            try
            {
               bool check = apiClient.Connect();
            }
            catch
            {
                MessageBox.Show("Включите WCF host!");
                return;
            }
        }
    }
}
