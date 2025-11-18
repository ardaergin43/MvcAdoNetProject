using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MvcAdoNetProjem.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MvcAdoNetProjem.Controllers
{
    public class HomeController : Controller
    {
        private readonly DBHelper _db;
        private readonly string _imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

        public HomeController(DBHelper db)
        {
            _db = db;
        }

        // Ana sayfa - ürünleri listeler
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserName") == null)
                return RedirectToAction("Login", "Account");

            var products = _db.GetAllProducts();
            ViewBag.Products = products;
            return View();
        }

        // Ürün ekleme
        [HttpPost]
        public async Task<IActionResult> AddProduct(string ProductName, string Description, decimal Price, int Stock, IFormFile ImageFile)
        {
            if (HttpContext.Session.GetString("UserName") == null)
                return RedirectToAction("Login", "Account");

            string imageUrl = string.Empty;

            // Resim dosyasını kaydetme işlemi
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                var filePath = Path.Combine(_imagePath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                imageUrl = $"/images/{fileName}";
            }

            var product = new Product
            {
                ProductName = ProductName,
                Description = Description,
                Price = Price,
                Stock = Stock,
                CreatedDate = DateTime.Now,
                ImageUrl = imageUrl
            };

            _db.AddProduct(product, imageUrl);
            return RedirectToAction("Index");
        }

        // Ürün silme
        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            _db.DeleteProduct(id);
            return RedirectToAction("Index");
        }

        // Ürün güncelleme
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(Product product, IFormFile ImageFile)
        {
            if (HttpContext.Session.GetString("UserName") == null)
                return RedirectToAction("Login", "Account");

            string imageUrl = product.ImageUrl;

            if (ImageFile != null && ImageFile.Length > 0)
            {
                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", product.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                var filePath = Path.Combine(_imagePath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                imageUrl = $"/images/{fileName}";
            }

            _db.UpdateProduct(product, imageUrl);

            return RedirectToAction("Index");
        }
    }
}
