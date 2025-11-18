using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MvcAdoNetProjem.Models;
using System;

namespace MvcAdoNetProjem.Controllers
{
    public class AccountController : Controller
    {
        private readonly DBHelper _dbHelper;

        public AccountController(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            _dbHelper = new DBHelper(connectionString);
        }

        // Login sayfası: ürünleri tanıtım olarak listeler, modallar üzerinden login/register yapılır
        public IActionResult Login()
        {
            var products = _dbHelper.GetAllProducts();
            ViewBag.Products = products;
            return View();
        }

        // Giriş işlemi
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            try
            {
                User user = _dbHelper.ValidateUser(email, password);

                if (user != null)
                {
                    HttpContext.Session.SetInt32("UserId", user.Id);
                    HttpContext.Session.SetString("UserName", user.Name);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var products = _dbHelper.GetAllProducts();
                    ViewBag.Products = products;
                    ViewBag.Error = "Geçersiz e-posta veya şifre!";
                    return View("Login");
                }
            }
            catch (Exception ex)
            {
                var products = _dbHelper.GetAllProducts();
                ViewBag.Products = products;
                ViewBag.Error = "Giriş işlemi sırasında bir hata oluştu: " + ex.Message;
                return View("Login");
            }
        }

        // Kayıt işlemi (popup üzerinden yapılır)
        [HttpPost]
        public IActionResult Register(User user)
        {
            var products = _dbHelper.GetAllProducts();
            ViewBag.Products = products;

            if (ModelState.IsValid)
            {
                try
                {
                    bool isSuccess = _dbHelper.RegisterUser(user);

                    if (isSuccess)
                    {
                        TempData["SuccessMessage"] = "Kayıt işlemi başarıyla tamamlandı!";
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ViewBag.Error = "Bu e-posta adresi zaten kullanılıyor!";
                        return View("Login");
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Kayıt işlemi sırasında bir hata oluştu: " + ex.Message;
                    return View("Login");
                }
            }

            return View("Login");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
