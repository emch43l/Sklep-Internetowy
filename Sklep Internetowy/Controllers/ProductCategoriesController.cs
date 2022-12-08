using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy.Models;
using Sklep_Internetowy.Models.Contexts;
using Sklep_Internetowy.Repositories.Interfaces;

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

        [Route("admin/categories")]
        public async Task<IActionResult> Index()
        {
            IEnumerable<ProductCategory> categories = _pcRepo.GetCategories();
            return View(categories);
        }

        [Route("admin/categories/details/{id}")]
        // GET: Producers/Details/guid
        public IActionResult Details(string id)
        {
            ProductCategory? category = _pcRepo.GetProductCategoryByGuid(id);
            if (category == null)
                return RedirectToAction("Index", "Producers");
            return View(category);
        }

        [Route("admin/categories/create")]
        // GET: Producers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Producers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/categories/create")]
        public IActionResult Create([Bind("Name")] ProductCategory category)
        {
            if (ModelState.IsValid)
            {
                _pcRepo.AddProductCategory(category);
                _pcRepo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        [Route("admin/categories/edit/{id}")]
        // GET: Producers/Edit/5
        public IActionResult Edit(string id)
        {
            ProductCategory? category = _pcRepo.GetProductCategoryByGuid(id);
            if (category == null)
                return RedirectToAction("Index", "Producers");

            return View(category);
        }

        // POST: Producers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/categories/edit/{id}")]
        public IActionResult Edit(string id, [Bind("Name")] ProductCategory category)
        {

            if (ModelState.IsValid)
            {
                ProductCategory? entity = _pcRepo.GetProductCategoryByGuid(id);

                if (entity == null)
                    return RedirectToAction("Index", "Producers");

                entity.Name = category.Name;
                _pcRepo.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        [Route("admin/categories/delete/{id}")]
        // GET: Producers/Delete/5
        public IActionResult Delete(string id)
        {
            ProductCategory? category = _pcRepo.GetProductCategoryByGuid(id);

            if (category == null)
            {
                return RedirectToAction("Index", "Producers");
            }

            return View(category);
        }

        // POST: Producers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("admin/categories/delete/{id}")]
        public IActionResult DeleteConfirmed(string id)
        {
            ProductCategory? category = _pcRepo.GetProductCategoryByGuid(id);
            if (category == null)
            {
                return RedirectToAction("Index", "Producers");
            }
            _pcRepo.RemoveProductCategory(id);
            _pcRepo.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
