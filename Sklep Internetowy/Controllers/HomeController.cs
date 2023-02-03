using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy.Contexts;
using Sklep_Internetowy.Models;
using Sklep_Internetowy.Repositories;
using Sklep_Internetowy.Repositories.Interfaces;
using Sklep_Internetowy.Services;
using Sklep_Internetowy.Services.Interfaces;
using Sklep_Internetowy.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Collections.Generic;

namespace Sklep_Internetowy.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IConfiguration _configuration;

        private readonly IDirectoryConfigurationReader _reader;

        private readonly DataContext _context;

        private readonly IProductRepository _pRepo;

        public HomeController(
            ILogger<HomeController> logger,
            IConfiguration configuration, 
            IDirectoryConfigurationReader uploader,
            IProductRepository pRepo,
            DataContext context)
        {
            _context = context;
            _configuration= configuration;
            _logger = logger;
            _pRepo = pRepo;
            _reader = uploader;
        }

        public async Task<IActionResult> Index(ManageProductListViewModel model)
        {

            ViewData["ImagesPath"] = _reader.GetDirectory(TargetFolder.Images);

            IEnumerable<Product> products = await _pRepo.GetProductsWithAssociatedEntities();

            products = FindProduct(products, model.ProductProducers, model.ProductCategories );

            double minPrice =  (double?)(await _pRepo.GetCheapest())?.Price ?? 0;
            double maxPrice =  (double?)(await _pRepo.GetMostExpensive())?.Price ?? 0;

            products = ProductsOrderBy(products, model.SortingOrder, model.SortBy);

            if (model.MaxPrice != null)
            {
                products = FilterByPrice(products, (double)model.MaxPrice);
            }
            else
            {
                model.MaxPrice = maxPrice;
            }

            EntityPaginator<Product> paginator = new EntityPaginator<Product>(products);

            if (model.PageNumber < 1)
                model.PageNumber = 1;

            paginator.SetPageEntityNumber(5);
            products = paginator.GetPaginatedData(model.PageNumber);
            model.NumberOfPages = paginator.GetPagesNumber(model.PageNumber);

            model.InputRangeData = new InputRangeData
            {
                Min = minPrice,
                Max = maxPrice,
                Current = model.MaxPrice
            };

            model.ProducersList = GetProducers(model.ProductProducers);
            model.CategoriesList = GetCategories(model.ProductCategories);
            model.Order = new List<SelectListItem>
            {
                new SelectListItem { Text = "Rosnąco", Value = ((int) SortigOrder.Ascending).ToString() },
                new SelectListItem { Text = "Malejąco", Value = ((int) SortigOrder.Descending).ToString() }
            };

            model.SortProps = new List<SelectListItem>
            {
                new SelectListItem { Text = "Nazwa", Value = ((int) SortByProperty.Name).ToString() },
                new SelectListItem { Text = "Cena", Value = ((int) SortByProperty.Price).ToString() },
                new SelectListItem { Text = "Ocena", Value = ((int) SortByProperty.Rating).ToString() },
                new SelectListItem { Text = "Producent", Value = ((int) SortByProperty.Producer).ToString() }
            };

            return View(new Tuple<IEnumerable<Product>, ManageProductListViewModel>(
                            item1: products,
                            item2: model
                                ));          
        }

        private List<SelectListItem> GetProducers(string[] Producers)
        {
            return _context.Producers
                .Select(p =>
                    new SelectListItem(
                        p.Name, 
                        p.Guid.ToString(), 
                        Producers.Contains(p.Guid.ToString())
                    )
                ).ToList();
        }

        private List<SelectListItem> GetCategories(string[] Categories)
        {
            return _context.ProductCategories
                .Select(p =>
                     new SelectListItem(
                        p.Name,
                        p.Guid.ToString(),
                        Categories.Contains(p.Guid.ToString())
                    )
                ).ToList();
        }

        private List<Product> FilterByPrice(IEnumerable<Product> products, double MaxPrice)
        {
            return products.Where(p => p.Price <= (decimal)MaxPrice).Select(p => p).ToList();
        }

        private List<Product> ProductsOrderBy(IEnumerable<Product> products, SortigOrder order, SortByProperty prop)
        {
            switch(prop)
            {
                case SortByProperty.Name:
                    products = (order == SortigOrder.Descending) ? 
                       products.OrderByDescending(p => p.Name) : products.OrderBy(p => p.Name);
                    break;
                case SortByProperty.Producer:
                    products = (order == SortigOrder.Descending) ?
                       products.OrderByDescending(p => p.Producer.Name) : products.OrderBy(p => p.Producer.Name);
                    break;
                case SortByProperty.Price:
                    products = (order == SortigOrder.Descending) ?
                       products.OrderByDescending(p => (long)p.Price) : products.OrderBy(p => (long)p.Price);
                    break;
                case SortByProperty.Rating:
                    products = (order == SortigOrder.Descending) ?
                       products.OrderByDescending(p => p.GetRating()) : products.OrderBy(p => p.GetRating());
                    break;
            }

            return products.ToList();
        }

        private List<Product> FindProduct(IEnumerable<Product> products, string[] Producers, string[] Categories)
        {
            return products
                .Where(p =>
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
               .ToList();
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