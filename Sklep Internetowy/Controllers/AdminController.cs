using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy.Models;
using Sklep_Internetowy.Models.Contexts;
using Sklep_Internetowy.ViewModels;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace Sklep_Internetowy.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context = new AppDbContext();

        private readonly string _appEnviroment;

        public AdminController(IWebHostEnvironment environment)
        {
            _appEnviroment = environment.WebRootPath;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                Product entity = new Product()
                {
                    Name = product.Name,
                    Creation_Date = DateTime.Now,
                    Description = product.Description,
                    Price = decimal.Parse(product.Price.ToString())

                };
                var createdEntity = _context.Add(entity);
                List<Image> images = UploadImage(product.Images, createdEntity.Entity).Result;
                images.ForEach(image => _context.Images.Add(image));
                _context.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            return View(product);
        }

        private async Task<List<Image>> UploadImage(ICollection<IFormFile>? imgs, Product product)
        {
            List<Image> images = new List<Image> { };
            if (imgs == null)
                return images;
            long fileSize = imgs.Sum(x => x.Length);
            foreach (IFormFile file in imgs)
            {
                if (file.Length > 0)
                {
                    string imagesPath = _appEnviroment + "/images/";
                    CreatDirectoryIfNotExists(imagesPath);
                    string fileName = Guid.NewGuid() + "_" + string.Join('_', file.FileName.Split(Path.GetInvalidFileNameChars()));
                    using (Stream stream = System.IO.File.Create(imagesPath + fileName))
                    {
                        await file.CopyToAsync(stream);
                        images.Add(new Image()
                        {
                            Title = file.Name,
                            Name = fileName,
                            Product = product

                        });
                    }
                }
            }

            return images;
        }

        

        private void CreatDirectoryIfNotExists(string path)
        {
            if(!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
