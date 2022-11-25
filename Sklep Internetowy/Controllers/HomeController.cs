using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy.Models;
using Sklep_Internetowy.Models.Contexts;
using Sklep_Internetowy.Repositories;
using Sklep_Internetowy.Repositories.Interfaces;
using Sklep_Internetowy.Services;
using Sklep_Internetowy.ViewModels;
using System.Diagnostics;

namespace Sklep_Internetowy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly DataContext _context;

        private readonly IConfiguration _configuration;

        private readonly IDirectoryConfigurationReader _reader;

        private readonly IProductRepository _pRepo;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IDirectoryConfigurationReader uploader, DataContext context)
        {
            _context = context;
            _configuration= configuration;
            _logger = logger;
            _reader = uploader;
            _pRepo = new ProductRepository(_context);
        }

        public IActionResult Index([FromQuery] string[] ProductProducers, [FromQuery] string[] ProductCategories)
        {

            ViewData["ImagesPath"] = _reader.GetDirectory(TargetFolder.Images);

            IEnumerable<Product> products = FindProduct(ProductProducers, ProductCategories);


            return View(
                new Tuple<IEnumerable<Product>, IEnumerable<SelectListItem>, IEnumerable<SelectListItem>>(
                item1: products,
                item2: GetProducers(ProductProducers),
                item3: GetCategories(ProductCategories)
                )
            );           
        }

        private IEnumerable<SelectListItem> GetProducers(string[] Producers)
        {
            return _context.Producers
                .ToList().Select(p =>
                    new SelectListItem { 
                        Text = p.Name, 
                        Value = p.Guid.ToString(), 
                        Selected = Producers.Contains(p.Guid.ToString())
                    });
        }


        private IEnumerable<SelectListItem> GetCategories(string[] Categories)
        {
            return _context.ProductCategories
                .ToList().Select(p => 
                    new SelectListItem { 
                        Text = p.Name, 
                        Value = p.Guid.ToString(), 
                        Selected = Categories.Contains(p.Guid.ToString()) 
                    });
        }
        private IEnumerable<Product> FindProduct(string[] Producers, string[] Categories)
        {
            return _pRepo.GetProductsWithAditionalData().
                Where(p =>
                    (Producers.Length == 0) ? 
                        true : 
                        Producers.Any(pr => 
                            p.Producer.Guid.ToString() == pr)
               ).Where(p =>
                    (Categories.Length == 0) ? 
                        true : 
                        Categories.Any(pc => 
                            p.Categories.FirstOrDefault(c => 
                                c.Guid.ToString() == pc) != null))
               .Select(p => p);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}