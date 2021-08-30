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
        bool Connect();


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
        void PutCategories(int id, Categories category);

        [OperationContract]
        void DeleteCategories(int id, Categories category);

        [OperationContract]
        void PostCategories(Categories category);


        [OperationContract]
        List<Clients> GetClients();

        [OperationContract]
        void PutClients(int id, Clients client);

        [OperationContract]
        void DeleteClients(int id, Clients client);

        [OperationContract]
        void PostClients(Clients client);



        [OperationContract]
        List<Sales> GetSales();

        [OperationContract]
        void PutSales(int id, Sales sale);

        [OperationContract]
        void DeleteSales(int id, Sales sale);

        [OperationContract]
        void PostSales(Sales sale);


        [OperationContract]
        List<Admins> GetAdmins();
    }
}
