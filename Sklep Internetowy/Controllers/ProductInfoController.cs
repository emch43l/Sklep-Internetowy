﻿using Azure.Core;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Sklep_Internetowy.Models;
using Sklep_Internetowy.Repositories.Interfaces;
using Sklep_Internetowy.Services;
using Sklep_Internetowy.ViewModels;
using System.Reflection.PortableExecutable;

namespace Sklep_Internetowy.Controllers
{
    public class ProductInfoController : Controller
    {
        private readonly IProductRepository _pRepo;

        private readonly IDirectoryConfigurationReader _reader;

        public ProductInfoController(IProductRepository pRepo, IDirectoryConfigurationReader reader)
        {
            _pRepo = pRepo;
            _reader = reader;
        }

        public IActionResult AddOpinion(AddOpinionModel opinion) 
        {
            string referer = Request.Headers["Referer"].ToString();
            if(ModelState.IsValid)
            {
                Product? product = _pRepo.GetProductWithAditionalData(opinion.Id);
                if(product != null)
                {
                    product.Rating.Add(new ProductRating
                    {
                        Description = opinion.Text,
                        Rating = opinion.Rating
                    });

                    _pRepo.Save();
                }
            }

            return Redirect(referer);
        }

        public IActionResult Index(string id)
        {
            ViewData["ImagesPath"] = _reader.GetDirectory(TargetFolder.Images);
            Product? product = _pRepo.GetProductWithAditionalData(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
