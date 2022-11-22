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
using Sklep_Internetowy.Services;
using Sklep_Internetowy.Interfaces;
using System.Dynamic;

namespace Sklep_Internetowy.Controllers
{
    public class AdminController : Controller
    {
        private readonly DataContext _context;

        private readonly string _appEnviroment;

        private readonly IFileUploader _fileUploader;

        public AdminController(IWebHostEnvironment environment, IFileUploader fileUploader, DataContext context)
        {
            _context = context;
            _fileUploader = fileUploader;
            _appEnviroment = environment.WebRootPath;
        }

        public IActionResult Index()
        {
            dynamic model = new ExpandoObject();
            model.Products = _context.Products.ToList();
            model.ProductDeleteModel = new ProductDeleteModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(ProductDeleteModel model)
        {
            Product? product = _context.Products.Find(model.Id);
            if (product == null)
                return RedirectToAction("Index");

            if (model.DeleteImagesOnRemoval)
            {
                foreach(Image file in product.Images)
                {
                    _fileUploader.DeleteFile(file);
                    _context.Images.Remove(file);
                }
            }
            _context.Products.Remove(product);
            _context.SaveChanges();

            return RedirectToAction("Index");

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel product)
        {
            _fileUploader.SetTargetFolderTo(TargetFolder.Images);
            if (ModelState.IsValid)
            {
                Product entity = new Product()
                {
                    Name = product.Name,
                    Creation_Date = DateTime.Now,
                    Description = product.Description,
                    Price = decimal.Parse(product.Price.ToString())

                };
                Product createdEntity = _context.Add(entity).Entity;
                List<UploadedFile> images = new List<UploadedFile>();
                foreach (IFormFile image in product.Images)
                {
                    images.Add(await _fileUploader.UploadFile(image));
                }
                images.ForEach(image => _context.Images.Add(new Image()
                {
                    Title = image.File.FileName,
                    Name = image.UploadedFileName,
                    Product = createdEntity
                }));

                _context.SaveChanges();

                return RedirectToAction("Index","Home");
            }

            return View(product);
        }
    }
}
