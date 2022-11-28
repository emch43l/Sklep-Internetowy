using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sklep_Internetowy.Controllers;
using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy.ViewModels
{
    public enum SortByProperty
    {
        [Display(Name = "Nazwa")]
        Name = 0,
        [Display(Name = "Cena")]
        Price = 1,
        [Display(Name = "Producent")]
        Producer = 2
    }

    public enum SortigOrder
    {
        [Display(Name = "Rosnąco")]
        Ascending = 0,
        [Display(Name = "Malejąco")]
        Descending = 1
    }

    public struct InputRangeData
    {
        public double Min;
        public double Max;
        public double? Current;
    }

    public class ManageProductListModel
    {
        [FromQuery(Name = "Producers")]
        public string[] ProductProducers { get; set; }

        [FromQuery(Name = "Categories")]
        public string[] ProductCategories { get; set; }

        [FromQuery(Name = "MaxPrice")]
        public double? MaxPrice { get; set; }

        [FromQuery(Name = "SortBy")]
        public SortByProperty SortBy { get; set; }

        [FromQuery(Name = "SortOrder")]
        public SortigOrder SortingOrder { get; set; }

        [FromQuery(Name = "Page")]
        public int PageNumber { get; set; }

        [ValidateNever]
        public List<int> NumberOfPages { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> ProducersList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> CategoriesList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> SortProps { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> Order { get; set; }

        [ValidateNever]
        public InputRangeData InputRangeData { get; set; }
        public ManageProductListModel() 
        {
            ProductProducers = new string[0];
            ProductCategories = new string[0];
        }
    }
}
