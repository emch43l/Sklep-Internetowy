﻿using System;
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
using Sklep_Internetowy.Repositories.Interfaces;
using Sklep_Internetowy.Repositories;

namespace Sklep_Internetowy.Controllers
{
    public class AdminController : Controller
    {
        private readonly DataContext _context;

        private readonly string _appEnviroment;

        private readonly IFileUploader _fileUploader;

        private readonly IProducerRepository _prodRepo;
        private readonly IProductRepository _pRepo;
        private readonly IProductCategoryRepository _pcRepo;
        private readonly IImageRepository _imgRepo;

        public AdminController(IWebHostEnvironment environment, IFileUploader fileUploader, DataContext context)
        {
            _context = context;
            _fileUploader = fileUploader;
            _appEnviroment = environment.WebRootPath;

            //repositories initialization

            _prodRepo = new ProducerRepository(_context);
            _pRepo = new ProductRepository(_context);
            _pcRepo = new ProductCategoryRepository(_context);
            _imgRepo = new ImageRepository(_context);
        }

        public IActionResult Index()
        {
            return View(_context.Products.Include(p => p.ProductDetail).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(ProductDeleteModel model)
        {
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

            ProductDetail productDetails = new ProductDetail();
            Product entity = new Product();
            List<UploadedFile> images = new List<UploadedFile>();

            if (ModelState.IsValid)
            {
                Producer? producer = _prodRepo.GetProducerByGuid(Guid.Parse(product.ProducerId));
                if(producer == null)
                {
                    ModelState.AddModelError("ProducerNotFound", "Selected producer doesnt exist !");
                    return View(product);
                }

                entity.Name = product.Name;
                entity.ProductDetail = productDetails;
                entity.Price = decimal.Parse(product.Price.ToString());
                entity.Producer = producer;
                entity.Categories = new List<ProductCategory>();

                productDetails.Description = product.Description;
                productDetails.Creation_Date = DateTime.Now;
                productDetails.Information = product.AditionalInformations.ToList();

                if(product.CategoryId != null)
                {
                    foreach(string id in product.CategoryId)
                    {
                        ProductCategory? productCategory = _pcRepo.GetProductCategoryByGuid(Guid.Parse(id));
                        if(productCategory != null)
                            entity.Categories.Add(productCategory);
                    }
                }
   

                if(product.Images != null && product.Images.Count != 0) 
                {
                    foreach (IFormFile image in product.Images)
                        images.Add(await _fileUploader.UploadFile(image));

                    productDetails.Images = images.Select(image =>
                        new Image() 
                        {
                            Title = image.File.FileName,
                            Name = image.UploadedFileName
                        }
                    ).ToList();
                }

                Product createdEntity = _pRepo.AddProduct(entity);
                _pRepo.Save();

                return RedirectToAction("Index","Home");
            }
            product.Categories = new List<ProductCategory>(_pcRepo.GetCategories());
            product.Producers = GetProducers();
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
