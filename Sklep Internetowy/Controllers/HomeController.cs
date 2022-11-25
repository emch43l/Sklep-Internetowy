using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy.Models;
using Sklep_Internetowy.Models.Contexts;
using Sklep_Internetowy.Services;
using System.Diagnostics;

namespace Sklep_Internetowy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly DataContext _context;

        private readonly IConfiguration _configuration;

        private readonly IDirectoryConfigurationReader _reader;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IDirectoryConfigurationReader uploader, DataContext context)
        {
            _context = context;
            _configuration= configuration;
            _logger = logger;
            _reader = uploader;
        }

        public IActionResult Index()
        {
            ViewData["ImagesPath"] = _reader.GetDirectory(TargetFolder.Images);
            return View(
                new Tuple<IEnumerable<Product>, IEnumerable<Producer>, IEnumerable<ProductCategory>>(
                item1: _context.Products
                        .Include(x => x.Rating)
                        .Include(x => x.ProductDetail)
                        .ThenInclude(x => x.Images).ToList(),
                item2: _context.Producers.ToList(),
                item3: _context.ProductCategories.ToList()
                )
            );           
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