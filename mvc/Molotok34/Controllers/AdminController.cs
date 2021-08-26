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
            if (CurrentUser.Id <= 0)
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
            if ((Controller == "CategoriesCreate" || Controller == "CategoriesDelete" || Controller == "CategoriesUpd") && CurrentUser.AccessProducts == false)
            {
                TempData["error_message"] = "У Вас недостаточно прав для внесения изминений в товары";
                return false;
            }
            else if ((Controller == "ProductsCreate" || Controller == "ProductsDelete" || Controller == "ProductsUpd") && CurrentUser.AccessProducts == false)
            {
                TempData["error_message"] = "У Вас недостаточно прав для внесения изминений в категории";
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
                return View();
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
                CurrentUser.Id = admin.Id;
                CurrentUser.AccessProducts = (admin.Permissions.AccessProducts == 1) ? true : false;
                cookie["IdUser"] = admin.Id + "";
                cookie["PermissionsName"] = admin.Permissions.Name;
                cookie["AccessProducts"] = admin.Permissions.AccessProducts + "";
                cookie["AccessClients"] = admin.Permissions + "";

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
                TempData["notice"] = "Войдите в Ваш аккаунт";
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
                TempData["notice"] = "У Вас недостаточно прав для внесения изминений в каталог товаров";
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
                TempData["notice"] = "У Вас недостаточно прав для внесения изминений в каталог товаров";
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
                TempData["notice"] = "У Вас недостаточно прав для внесения изминений в каталог товаров";
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
                TempData["notice"] = "У Вас недостаточно прав для внесения изминений в каталог товаров";
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
                TempData["notice"] = "У Вас недостаточно прав для внесения изминений в каталог товаров";
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
                TempData["notice"] = "У Вас недостаточно прав для внесения изминений в категории";
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
                TempData["notice"] = "У Вас недостаточно прав для внесения изминений в категории";
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
                TempData["notice"] = "У Вас недостаточно прав для внесения изминений в категории";
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
                TempData["notice"] = "У Вас недостаточно прав для внесения изминений в категории";
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
                TempData["notice"] = "У Вас недостаточно прав для внесения изминений в категории";
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