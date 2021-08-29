using Molotok34.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Molotok34
{
    [ServiceContract]
    public interface IApi
    {
        [OperationContract]
        List<Products> GetProducts();

        [OperationContract]
        void PutProducts(int id, Products product);

        [OperationContract]
        void DeleteProducts(int id, Products product);

        [OperationContract]
        void PostProducts(Products product);

        [OperationContract]
        List<Categories> GetCategories();

        [OperationContract]
        List<Clients> GetClients();

        [OperationContract]
        List<Sales> GetSales();

        [OperationContract]
        List<Admins> GetAdmins();
    }
}
