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

                List<UploadedFile> images = _fileUploader.UploadFiles(product.Images).Result;
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
