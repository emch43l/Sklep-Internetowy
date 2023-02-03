using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy.Models;
using Sklep_Internetowy.Repositories.Interfaces;
using Sklep_Internetowy.ViewModels.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Sklep_Internetowy.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProductCategoriesController : Controller
    {
        private readonly IProductCategoryRepository _pcRepo;

        public ProductCategoriesController(IProductCategoryRepository pcRepo)
        {
            _pcRepo = pcRepo;
        }

        [Route("/admin/categories")]
        public async Task<IActionResult> Index()
        {
            IEnumerable<ProductCategory> categories = await _pcRepo.GetAll();
            return View(new Tuple<IEnumerable<ProductCategory>,ProductCategory>(categories,new ProductCategory()));
        }

        [Route("/admin/categories/create")]
        // GET: Producers/Create
        public IActionResult Create()
        {
            string? from = HttpContext.Request.Headers["Referer"];
            ViewData["Referer"] = from;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/admin/categories/create")]
        public async Task<IActionResult> Create(CategoryDTO data, string? From)
        {
            ProductCategory category = data.MapTo();

            if(!ModelState.IsValid)
               return View(category);
            if(await IsCategoryExist(category.Name))
            {
                ModelState.AddModelError("Name", "Given category already exist !");
                return View(category);
            }

            await _pcRepo.Add(category);
            await _pcRepo.SaveChanges();

            if(From == null)
               return RedirectToAction(nameof(Index));
            return Redirect(From);
            
        }

        [Route("/admin/categories/edit/{id}")]
        // GET: Producers/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            ProductCategory? category = await _pcRepo.GetOneByGuid(id);

            if (category == null)
                return RedirectToAction("Index", "ProductCategories");

            return View(category);
        }

        // POST: Producers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/admin/categories/edit/{id}")]
        public async Task<IActionResult> Edit(Guid id, CategoryDTO data)
        {
            ProductCategory category = data.MapTo();

            if (!ModelState.IsValid)
                return View(category);

            if (await IsCategoryExist(category.Name))
            {
                ModelState.AddModelError("Name", "Given category already exist !");
                return View(category);
            }
            ProductCategory? entity = await _pcRepo.GetOneByGuid(id);

            if (entity == null)
                return RedirectToAction("Index", "ProductCategories");

            entity.Name = category.Name;
            _pcRepo.SaveChanges();

            return RedirectToAction(nameof(Index));

        }

        // POST: Producers/Delete/5
        [Route("/admin/categories/delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            ProductCategory? category = await _pcRepo.GetOneByGuid(id);
            if (category == null)
            {
                return RedirectToAction("Index", "ProductCategories");
            }
            await _pcRepo.Remove(id);
            await _pcRepo.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> IsCategoryExist(string name)
        {
            return await _pcRepo.GetCategoryByName(name) == null ? false : true;
        }
    }
}
