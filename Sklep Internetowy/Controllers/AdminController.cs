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
            return View(_context.Products.Include(p => p.ProductDetail).ToList());
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
                foreach(Image file in product.ProductDetail.Images)
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
            return View(new ProductViewModel { Producers = GetProducers(), Categories = _context.ProductCategories.ToList()});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel product)
        {
            _fileUploader.SetTargetFolderTo(TargetFolder.Images);
            if (ModelState.IsValid)
            {
                Producer? producer =  _context.Producers.Where(p => p.Guid == new Guid(product.ProducerId)).Select(p => p).First();
                if(producer == null)
                {
                    ModelState.AddModelError("ProducerNotFound", "Selected producer doesnt exist !");
                    return View(product);
                }
                ProductDetail productDetails = new ProductDetail();
                Product entity = new Product()
                {
                    Name = product.Name,
                    ProductDetail = productDetails,
                    Price = decimal.Parse(product.Price.ToString()),
                    Producer = producer,
                    Categories = new List<ProductCategory>()
                };
                if(product.CategoryId != null)
                {
                    foreach(string id in product.CategoryId)
                    {
                        entity.Categories.Add(_context.ProductCategories.Where(c => c.Guid == new Guid(id)).Select(c => c).First());
                    }
                }

                productDetails.Description = product.Description;
                productDetails.Creation_Date = DateTime.Now;

                List<UploadedFile> images = new List<UploadedFile>();
                if(product.Images != null) 
                {
                    foreach (IFormFile image in product.Images)
                    {
                        images.Add(await _fileUploader.UploadFile(image));
                    }

                    productDetails.Images = images.Select(image => new Image()
                    {
                        Title = image.File.FileName,
                        Name = image.UploadedFileName
                    }).ToList();
                }

                Product createdEntity = _context.Add(entity).Entity;

                _context.SaveChanges();

                return RedirectToAction("Index","Home");
            }

            return View(product);
        }

        private List<SelectListItem> GetProducers()
        {
            return _context.Producers.Select(p => new SelectListItem() { Text = p.Name, Value = p.Guid.ToString()}).ToList();
        }

        private List<SelectListItem> GetCategories()
        {
            return _context.ProductCategories.Select(p => new SelectListItem() { Text = p.Name, Value = p.Name }).ToList();
        }
    }
}
