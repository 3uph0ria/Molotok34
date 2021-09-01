using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Molotok34.Class;
using Molotok34.Models;

namespace Molotok34.Controllers
{
    public class AdminController : Controller
    {
        private HttpCookie cookie = new HttpCookie("UserInfo");
        private Molotok34Entities db = new Molotok34Entities();


        public bool CheckAuth()
        {

            if (Convert.ToInt32(Session["Id"]) <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CheckAccess(string Controller)
        {
            if ((Controller == "CategoriesCreate" || Controller == "CategoriesDelete" || Controller == "CategoriesUpd") && Convert.ToBoolean(Session["AccessCategories"]) == false)
            {
                TempData["error_message"] = "У Вас недостаточно прав для внесения изминений в товары";
                return false;
            }
            else if ((Controller == "ProductsCreate" || Controller == "ProductsDelete" || Controller == "ProductsUpd") && Convert.ToBoolean(Session["AccessProducts"]) == false)
            {
                TempData["error_message"] = "У Вас недостаточно прав для внесения изминений в категории";
                return false;
            }
            else if ((Controller == "ClientsCreate" || Controller == "ClientsDelete" || Controller == "ClientssUpd") && Convert.ToBoolean(Session["AccessClients"]) == false)
            {
                TempData["error_message"] = "У Вас недостаточно прав для внесения изминений в базу клиентов";
                return false;
            }
            else if ((Controller == "SalesCreate" || Controller == "SalesDelete" || Controller == "SalesUpd") && Convert.ToBoolean(Session["AccessProducts"]) == false)
            {
                TempData["error_message"] = "У Вас недостаточно прав для внесения изминений в продажи";
                return false;
            }
            else
            {
                return true;
            }
        }

        public ActionResult Index()
        {
            if (CheckAuth())
            {
                return RedirectToRoute(new { controller = "Admin", action = "ProductsIndex" });
            }
            else
            {
                return RedirectToRoute(new { controller = "Admin", action = "SignIn" });
            }
  
        }

        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn([Bind(Include = "Login,Password")] Admins currentAdmin)
        {
            var admins = Molotok34Entities.GetContext().Admins.ToList();

            string passwordHesh = GetHash(currentAdmin.Password);

            admins = admins.Where(p => p.Login.Contains(currentAdmin.Login)).ToList();
            admins = admins.Where(p => p.Password.Contains(passwordHesh)).ToList();

            var admin = admins.LastOrDefault();

            if (admin != null)
            {
                Session["Id"] = admin.Id;
                Session["AccessProducts"] = (admin.Permissions.AccessProducts == 1) ? true : false;
                Session["AccessCategories"] = (admin.Permissions.AccessCategories == 1) ? true : false;
                Session["AccessClients"] = (admin.Permissions.AccessClients == 1) ? true : false;

                return RedirectToRoute(new { controller = "Admin", action = "ProductsIndex" });
            }
            else
            {
                TempData["error_message"] = "Неверный логин или пароль";
                return View();
            }
        }

        public ActionResult ProductsIndex()
        {
            if(CheckAuth())
            {
                var products = Molotok34Entities.GetContext().Products.ToList();
                return View(products.ToList());
            }
            else
            {
                return RedirectToRoute(new { controller = "Admin", action = "SignIn" });
            }

        }

        public ActionResult ProductsCreate()
        {
            if (CheckAuth() && CheckAccess("ProductsCreate"))
            {
                ViewBag.IdCategory = new SelectList(db.Categories, "Id", "Name");
                return View();
            }
            else
            {
                return RedirectToRoute(new { controller = "Admin", action = "ProductsIndex" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductsCreate([Bind(Include = "Id,IdCategory,Name,Cost,Description,Amount,Img,Stars")] Products products)
        {
            if (CheckAuth() && CheckAccess("ProductsCreate"))
            {
                if (ModelState.IsValid)
                {
                    db.Products.Add(products);
                    db.SaveChanges();
                    return RedirectToAction("ProductsIndex");
                }

                ViewBag.IdCategory = new SelectList(db.Categories, "Id", "Name", products.IdCategory);
                return View(products);
            }
            else
            {
                return RedirectToRoute(new { controller = "Admin", action = "ProductsIndex" });
            }
        }

        public ActionResult ProductsUpd(int? id)
        {
            if (CheckAuth() && CheckAccess("ProductsUpd"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Products products = db.Products.Find(id);
                if (products == null)
                {
                    return HttpNotFound();
                }
                ViewBag.IdCategory = new SelectList(db.Categories, "Id", "Name", products.IdCategory);
                return View(products);
            }
            else
            {
                return RedirectToRoute(new { controller = "Admin", action = "ProductsIndex" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductsUpd([Bind(Include = "Id,IdCategory,Name,Cost,Description,Amount,Img,Stars")] Products products)
        {
            if (CheckAuth() && CheckAccess("ProductsUpd"))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(products).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("ProductsIndex");
                }
                ViewBag.IdCategory = new SelectList(db.Categories, "Id", "Name", products.IdCategory);
                return View(products);
            }
            else
            {
                return RedirectToRoute(new { controller = "Admin", action = "ProductsIndex" });
            }
        }

        public ActionResult ProductsDelete(int? id)
        {
            if (CheckAuth() && CheckAccess("ProductsDelete"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Products products = db.Products.Find(id);
                if (products == null)
                {
                    return HttpNotFound();
                }
                return View(products);
            }
            else
            {
                return RedirectToRoute(new { controller = "Admin", action = "ProductsIndex" });
            }
        }

        [HttpPost, ActionName("ProductsDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = db.Products.Find(id);
            db.Products.Remove(products);
            db.SaveChanges();
            return RedirectToAction("ProductsIndex");
        }

        public ActionResult CategoriesIndex()
        {
            if (CheckAuth())
            {
                var catigories = Molotok34Entities.GetContext().Categories.ToList();
                return View(catigories);
            }
            else
            {
                TempData["notice"] = "Войдите в свой аккаунт";
                return RedirectToRoute(new { controller = "Admin", action = "SignIn" });
            }
        }

        public ActionResult CategoriesCreate()
        {
            if (CheckAuth() && CheckAccess("CategoriesCreate"))
            {
                ViewBag.IdCategory = new SelectList(db.Categories, "Id", "Name");
                return View();
            }
            else
            {
                return RedirectToRoute(new { controller = "Admin", action = "CategoriesIndex" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CategoriesCreate([Bind(Include = "Id,Name")] Categories categories)
        {
            if (CheckAuth() && CheckAccess("CategoriesCreate"))
            {
                if (ModelState.IsValid)
                {
                    db.Categories.Add(categories);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(categories);
            }
            else
            {
                return RedirectToRoute(new { controller = "Admin", action = "CategoriesIndex" });
            }
        }

        public ActionResult CategoriesUpd(int? id)
        {
            if (CheckAuth() && CheckAccess("CategoriesUpd"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Categories categories = db.Categories.Find(id);
                if (categories == null)
                {
                    return HttpNotFound();
                }
                return View(categories);
            }
            else
            {
                return RedirectToRoute(new { controller = "Admin", action = "CategoriesIndex" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CategoriesUpd([Bind(Include = "Id,Name")] Categories categories)
        {
            if (CheckAuth() && CheckAccess("CategoriesUpd"))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(categories).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("CategoriesIndex");
                }
                return View(categories);
            }
            else
            {
                return RedirectToRoute(new { controller = "Admin", action = "CategoriesIndex" });
            }
        }

        public ActionResult CategoriesDelete(int? id)
        {
            if (CheckAuth() && CheckAccess("CategoriesDelete"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Categories categories = db.Categories.Find(id);
                if (categories == null)
                {
                    return HttpNotFound();
                }
                return View(categories);
            }
            else
            {
                return RedirectToRoute(new { controller = "Admin", action = "CategoriesIndex" });
            }
        }

        [HttpPost, ActionName("CategoriesDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult CategoriesDeleteConfirmed(int id)
        {
            Products products = db.Products.Find(id);
            db.Products.Remove(products);
            db.SaveChanges();
            return RedirectToAction("CategoriesIndex");
        }

        public ActionResult ClientsIndex()
        {
            if (CheckAuth())
            {
                var clients = Molotok34Entities.GetContext().Clients.ToList();
                return View(clients);
            }
            else
            {
                TempData["notice"] = "Войдите в Ваш аккаунт";
                return RedirectToRoute(new { controller = "Admin", action = "SignIn" });
            }
        }

        // GET: Clients/Create
        public ActionResult ClientsCreate()
        {
            if (CheckAuth() && CheckAccess("ClientsCreate"))
            {
                return View();
            }
            else
            {
                return RedirectToRoute(new { controller = "Admin", action = "ClientsIndex" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ClientsCreate([Bind(Include = "Id,FullName,Phone,Email")] Clients clients)
        {
            if (CheckAuth() && CheckAccess("ClientsCreate"))
            {
                if (ModelState.IsValid)
                {
                    db.Clients.Add(clients);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(clients);
            }
            else
            {
                return RedirectToRoute(new { controller = "Admin", action = "ClientsIndex" });
            }
        }

        public ActionResult ClientsUpd(int? id)
        {
            if (CheckAuth() && CheckAccess("ClientsUpd"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Clients clients = db.Clients.Find(id);
                if (clients == null)
                {
                    return HttpNotFound();
                }
                return View(clients);
            }
            else
            {
                return RedirectToRoute(new { controller = "Admin", action = "ClientsIndex" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ClientsUpd([Bind(Include = "Id,FullName,Phone,Email")] Clients clients)
        {
            if (CheckAuth() && CheckAccess("ClientsUpd"))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(clients).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(clients);
            }
            else
            {
                return RedirectToRoute(new { controller = "Admin", action = "ClientsIndex" });
            }
        }

        // GET: Clients/Delete/5
        public ActionResult ClientsDelete(int? id)
        {
            if (CheckAuth() && CheckAccess("ClientsDelete"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Clients clients = db.Clients.Find(id);
                if (clients == null)
                {
                    return HttpNotFound();
                }
                return View(clients);
            }
            else
            {
                return RedirectToRoute(new { controller = "Admin", action = "ClientsIndex" });
            }
        }

        [HttpPost, ActionName("ClientsDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult ClientsDeleteConfirmed(int id)
        {
            if (CheckAuth() && CheckAccess("ClientsDelete"))
            {
                Clients clients = db.Clients.Find(id);
                db.Clients.Remove(clients);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToRoute(new { controller = "Admin", action = "ClientsIndex" });
            }
        }

        // Sales
        public ActionResult SalesIndex()
        {
            if (CheckAuth())
            {
                var sales = Molotok34Entities.GetContext().Sales.ToList();
                return View(sales);
            }
            else
            {
                TempData["notice"] = "Войдите в Ваш аккаунт";
                return RedirectToRoute(new { controller = "Admin", action = "SignIn" });
            }
        }

        public ActionResult SalesCreate()
        {
            if (CheckAuth() && CheckAccess("SalesCreate"))
            {
                ViewBag.IdClient = new SelectList(db.Clients, "Id", "FullName");
                ViewBag.IdProduct = new SelectList(db.Products, "Id", "Name");
                return View();
            }
            else
            {
                return RedirectToRoute(new { controller = "Admin", action = "SalesIndex" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SalesCreate([Bind(Include = "Id,IdProduct,IdClient,Date")] Sales sales)
        {
            if (CheckAuth() && CheckAccess("SalesCreate"))
            {
                if (ModelState.IsValid)
                {
                    db.Sales.Add(sales);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.IdClient = new SelectList(db.Clients, "Id", "FullName", sales.IdClient);
                ViewBag.IdProduct = new SelectList(db.Products, "Id", "Name", sales.IdProduct);
                return View(sales);
            }
            else
            {
                return RedirectToRoute(new { controller = "Admin", action = "SalesIndex" });
            }
        }

        public ActionResult SalesUpd(int? id)
        {
            if (CheckAuth() && CheckAccess("SalesUpd"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Sales sales = db.Sales.Find(id);
                if (sales == null)
                {
                    return HttpNotFound();
                }
                ViewBag.IdClient = new SelectList(db.Clients, "Id", "FullName", sales.IdClient);
                ViewBag.IdProduct = new SelectList(db.Products, "Id", "Name", sales.IdProduct);
                return View(sales);
            }
            else
            {
                return RedirectToRoute(new { controller = "Admin", action = "SalesIndex" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SalesUpd([Bind(Include = "Id,IdProduct,IdClient,Date")] Sales sales)
        {
            if (CheckAuth() && CheckAccess("SalesUpd"))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(sales).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.IdClient = new SelectList(db.Clients, "Id", "FullName", sales.IdClient);
                ViewBag.IdProduct = new SelectList(db.Products, "Id", "Name", sales.IdProduct);
                return View(sales);
            }
            else
            {
                return RedirectToRoute(new { controller = "Admin", action = "SalesIndex" });
            }
        }

        public ActionResult SalesDelete(int? id)
        {
            if (CheckAuth() && CheckAccess("SalesDelete"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Sales sales = db.Sales.Find(id);
                if (sales == null)
                {
                    return HttpNotFound();
                }
                return View(sales);
            }
            else
            {
                return RedirectToRoute(new { controller = "Admin", action = "SalesIndex" });
            }
        }

        [HttpPost, ActionName("SalesDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult SalesDeleteConfirmed(int id)
        {
            if (CheckAuth() && CheckAccess("SalesDelete"))
            {
                Sales sales = db.Sales.Find(id);
                db.Sales.Remove(sales);
                db.SaveChanges();
                return RedirectToAction("SalesIndex");
            }
            else
            {
                return RedirectToRoute(new { controller = "Admin", action = "SalesIndex" });
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

        public static string GetHash(string password)
        {
            using (var hash = SHA1.Create())
            {
                return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));
            }
        }

    }
}