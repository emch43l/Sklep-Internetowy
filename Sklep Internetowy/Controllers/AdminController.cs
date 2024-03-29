﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sklep_Internetowy.Contexts;
using Sklep_Internetowy.ViewModels;

namespace Sklep_Internetowy.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly DataContext _context;

        public AdminController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(new AdminStatisticsViewModel()
            {
                NumberOfCategories = _context.ProductCategories.Count(),
                NumberOfProducers = _context.Producers.Count(),
                NumberOfProducts= _context.Products.Count(),
                NumberOfUsers= _context.Users.Count(),
            });
        }
    }
}
