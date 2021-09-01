using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Molotok34.Models;

namespace Molotok34.Controllers
{
    public class CatalogController : Controller
    {
        private Molotok34Entities db = new Molotok34Entities();

        // GET: Catalog
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Categories);
            return View(products.ToList());
        }

        // GET: Catalog/Details/5
        //public ActionResult Product(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Products products = db.Products.Find(id);
        //    if (products == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(products);
        //}

        public ActionResult Product(int? id, [Bind(Include = "FullName,Phone,Email")] Clients clients)
        {
            if (String.IsNullOrEmpty(clients.FullName) == false)
            {
                var clientsTmp = Molotok34Entities.GetContext().Clients.ToList();
                clientsTmp = clientsTmp.Where(p => p.Email.Contains(clients.Email)).ToList();

                var client = clientsTmp.LastOrDefault();

                if (client == null)
                {
                    db.Clients.Add(clients);
                    db.SaveChanges();

                    var clientsTmp2 = Molotok34Entities.GetContext().Clients.ToList();
                    clientsTmp2 = clientsTmp2.Where(p => p.Email.Contains(clients.Email)).ToList();
                    client = clientsTmp2.LastOrDefault();
                }

                Sales sales = new Sales();

                sales.IdClient = client.Id;
                sales.IdProduct = Convert.ToInt32(id);
                sales.Date = DateTime.Now;

                db.Sales.Add(sales);
                db.SaveChanges();

                TempData["notice"] = "Вы успешно оплатили товар, вся информация отправлена на Вашу электронную почту, которую Вы указали";

                Products products = db.Products.Find(sales.IdProduct);
                if (products == null)
                {
                    return HttpNotFound();
                }
                return View(products);
            }
            else
            {
                Products products = db.Products.Find(id);
                if (products == null)
                {
                    return HttpNotFound();
                }
                return View(products);
            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
