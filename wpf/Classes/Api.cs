using Molotok34.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Molotok34.Classes
{
    class Api
    {
        private static string url = "http://gsportfolio-001-site1.btempurl.com/api/";
        private static WebClient client = new WebClient();

        // Products
        public static List<Products> GetProducts()
        {
            var responce = Encoding.UTF8.GetString(client.DownloadData(url + "Products"));
            return JsonConvert.DeserializeObject<List<Products>>(responce);
        }

        public static void PutProducts(int id, Products product)
        {
            var httpRequest = (HttpWebRequest)WebRequest.Create(url + "Products?id=" + id);
            httpRequest.Method = "PUT";
            httpRequest.ContentType = "application/json";


            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(JsonConvert.SerializeObject(product));
            }

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

        }

        public static void DeleteProducts(int id, Products product)
        {
            var httpRequest = (HttpWebRequest)WebRequest.Create(url + "Products?id=" + id);
            httpRequest.Method = "DELETE";
            httpRequest.ContentType = "application/json";


            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(JsonConvert.SerializeObject(product));
            }

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
        }

        public static void PostProducts(Products product)
        {
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            var result = client.UploadString(url + "Products", JsonConvert.SerializeObject(product));
        }

        // Categories
        public static List<Categories> GetCategories()
        {
            var responce = Encoding.UTF8.GetString(client.DownloadData(url + "Categories"));
            return JsonConvert.DeserializeObject<List<Categories>>(responce);
        }

        // Clients
        public static List<Clients> GetClients()
        {
            var responce = Encoding.UTF8.GetString(client.DownloadData(url + "Clients"));
            return JsonConvert.DeserializeObject<List<Clients>>(responce);
        }

        // Sales
        public static List<Sales> GetSales()
        {
            var responce = Encoding.UTF8.GetString(client.DownloadData(url + "Sales"));
            return JsonConvert.DeserializeObject<List<Sales>>(responce);
        }


        // Admins
        public static List<Admins> GetAdmins()
        {
            var responce = Encoding.UTF8.GetString(client.DownloadData(url + "Admins"));
            return JsonConvert.DeserializeObject<List<Admins>>(responce);
        }
    }
}

