using Microsoft.AspNetCore.Mvc;
using Sklep_Internetowy.Controllers;
using Sklep_Internetowy.Services;
using Xunit;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using Sklep_Internetowy.Models;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Net.WebSockets;
using Sklep_Internetowy.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Http;

namespace SklepInternetowyTest
{
    public class ApiTest
    {
        [Fact]
        public void GetTest()
        {

            ProductRepoTest product = new ProductRepoTest();
            ProducerRepoTest producer = new ProducerRepoTest();
            ProductCategoryRepoTest category = new ProductCategoryRepoTest();

            ProductDetail dummyDetail1 = new ProductDetail()
            {
                Description = "Cos",
                Information = new List<string>(),
                Images = new List<Image>()
            };

            ProductDetail dummyDetail2 = new ProductDetail()
            {
                Description = "Tak",
                Information = new List<string>(),
                Images = new List<Image>(),
            };

            Product p1 = new Product()
            {
                Name = "Bułka",
                Price = 200,
                ProductDetail = dummyDetail1,
                Rating = new List<ProductRating>(),
                Producer = new Producer()
                {
                    Description = "Random producer description",
                    Name = "Xiaomi"
                },
                Categories = new List<ProductCategory>()
            };

            Product p2 = new Product()
            {
                Name = "Masło",
                Price = 1000,
                ProductDetail = dummyDetail2,
                Rating = new List<ProductRating>(),
                Producer = new Producer()
                {
                    Description = "Random producer description 2",
                    Name = "Samsung"
                },
                Categories = new List<ProductCategory>()
            };

            string p1Id = p1.Guid.ToString();
            string p2Id = p2.Guid.ToString();
            string p3Id = Guid.NewGuid().ToString();

            product.AddProduct(p1);
            product.AddProduct(p2);


            AdminProductApiController controller = new AdminProductApiController(
                product,
                new DirectoryConfigurationReaderTest(),
                producer,
                category
                );

            var result = controller.GetProduct(p1Id);
            Assert.IsType<OkObjectResult>(result);

            object data = ((OkObjectResult)result).Value;
            Assert.NotNull(data);
            Assert.Equal(p1.Name, data?.GetType().GetProperty("name").GetValue(data));


            result = controller.GetProduct(p2Id);
            Assert.IsType<OkObjectResult>(result);
            data = ((OkObjectResult)result).Value;
            Assert.NotNull(data);
            Assert.Equal(p2.Name, data?.GetType().GetProperty("name").GetValue(data));

            result = controller.GetProduct(p3Id);
            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public void PaginationTest()
        {
            ProductRepoTest product = new ProductRepoTest();
            ProducerRepoTest producer = new ProducerRepoTest();
            ProductCategoryRepoTest category = new ProductCategoryRepoTest();

            ProductDetail dummyDetail1 = new ProductDetail()
            {
                Description = "Cos",
                Information = new List<string>(),
                Images = new List<Image>()
            };

            ProductDetail dummyDetail2 = new ProductDetail()
            {
                Description = "Tak",
                Information = new List<string>(),
                Images = new List<Image>(),
            };

            Product p1 = new Product()
            {
                Name = "Bułka",
                Price = 200,
                ProductDetail = dummyDetail1,
                Rating = new List<ProductRating>(),
                Producer = new Producer()
                {
                    Description = "Random producer description",
                    Name = "Xiaomi"
                },
                Categories = new List<ProductCategory>()
            };

            Product p2 = new Product()
            {
                Name = "Masło",
                Price = 1000,
                ProductDetail = dummyDetail2,
                Rating = new List<ProductRating>(),
                Producer = new Producer()
                {
                    Description = "Random producer description 2",
                    Name = "Samsung"
                },
                Categories = new List<ProductCategory>()
            };

            string p1Id = p1.Guid.ToString();
            string p2Id = p2.Guid.ToString();
            string p3Id = Guid.NewGuid().ToString();

            product.AddProduct(p1);
            product.AddProduct(p2);


            AdminProductApiController controller = new AdminProductApiController(
                product,
                new DirectoryConfigurationReaderTest(),
                producer,
                category
                );

            var result = controller.GetProducts(-1);
            Assert.IsType<BadRequestResult>(result);

            result = controller.GetProducts(1);
            Assert.IsType<OkObjectResult>(result);
            var data = ((OkObjectResult)result).Value;
            IEnumerable rows = data?.GetType().GetProperty("products").GetValue(data) as IEnumerable;
            var jarray = JArray.FromObject(rows);
            Assert.Equal(2,jarray.Count);

            result = controller.GetProducts(2);
            Assert.IsType<OkObjectResult>(result);
            data = ((OkObjectResult)result).Value;
            rows = data?.GetType().GetProperty("products").GetValue(data) as IEnumerable;
            jarray = JArray.FromObject(rows);
            Assert.Equal(0, jarray.Count);



        }
        [Fact]
        public void DeleteTest()
        {
            ProductRepoTest product = new ProductRepoTest();
            ProducerRepoTest producer = new ProducerRepoTest();
            ProductCategoryRepoTest category = new ProductCategoryRepoTest();

            ProductDetail dummyDetail1 = new ProductDetail()
            {
                Description = "Cos",
                Information = new List<string>(),
                Images = new List<Image>()
            };

            Product p1 = new Product()
            {
                Name = "Bułka",
                Price = 200,
                ProductDetail = dummyDetail1,
                Rating = new List<ProductRating>(),
                Producer = new Producer()
                {
                    Description = "Random producer description",
                    Name = "Xiaomi"
                },
                Categories = new List<ProductCategory>()
            };

            string p1Id = p1.Guid.ToString();
            string p3Id = Guid.NewGuid().ToString();

            product.AddProduct(p1);


            AdminProductApiController controller = new AdminProductApiController(
                product,
                new DirectoryConfigurationReaderTest(),
                producer,
                category
                );

            var result = controller.DeleteProduct(p1Id);
            Assert.IsType<OkResult>( result );
            result = controller.DeleteProduct(p1Id);
            Assert.IsType<NotFoundResult>( result );

        }

        [Fact]
        public void PutTest()
        {
            ProductRepoTest product = new ProductRepoTest();
            ProducerRepoTest producer = new ProducerRepoTest();
            ProductCategoryRepoTest category = new ProductCategoryRepoTest();

            ProductDetail dummyDetail1 = new ProductDetail()
            {
                Description = "Cos",
                Information = new List<string>(),
                Images = new List<Image>()
            };

            List<ProductCategory> categories = new List<ProductCategory>();
            categories.Add(new ProductCategory() { Name = "Budowlane" });

            Product p1 = new Product()
            {
                Name = "Bułka",
                Price = 200,
                ProductDetail = dummyDetail1,
                Rating = new List<ProductRating>(),
                Producer = new Producer()
                {
                    Description = "Random producer description",
                    Name = "Xiaomi"
                },
                Categories = categories
            };

            Producer dummyProducer = new Producer()
            {
                Name = "dummy",
                Description = "dummy desc"
            };

            string prodId = dummyProducer.Guid.ToString();
            producer.AddProducer(dummyProducer);

            string p1Id = p1.Guid.ToString();
            product.AddProduct(p1);

            AdminProductApiController controller = new AdminProductApiController(
                product,
                new DirectoryConfigurationReaderTest(),
                producer,
                category
             );

            List<string> toUpdateCategories = new List<string>();
            toUpdateCategories.Add("Budowlane");
            toUpdateCategories.Add("Kolorowanki");

            ApiProductCreateModel toUpdate = new ApiProductCreateModel()
            {
                Name = "Butelka wody",
                Producer = prodId,
                Price = 5,
                Informations = new List<string>(),
                Description = "Description",
                Categories = toUpdateCategories
            };

            var result = controller.UpdateProduct(p1Id, toUpdate);
            Assert.IsType<OkObjectResult>(result);
            var data = ((OkObjectResult)result).Value;
            Assert.NotEqual("Bułka", data?.GetType().GetProperty("name").GetValue(data));
        }


        [Fact]
        public void PostTest()
        {
            ProductRepoTest product = new ProductRepoTest();
            ProducerRepoTest producer = new ProducerRepoTest();
            ProductCategoryRepoTest category = new ProductCategoryRepoTest();

            Producer dummyProducer = new Producer()
            {
                Name = "dummy",
                Description = "dummy desc"
            };

            producer.AddProducer( dummyProducer );

            AdminProductApiController controller = new AdminProductApiController(
                product,
                new DirectoryConfigurationReaderTest(),
                producer,
                category
                );

            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            ApiProductCreateModel toCreate = new ApiProductCreateModel()
            {
                Name = "Butelka wody",
                Producer = dummyProducer.Guid.ToString(),
                Price = 5,
                Informations = new List<string>(),
                Description = "Description",
                Categories = new List<string>()
            };

            var result = controller.CreateProduct(toCreate);
            Assert.IsType<CreatedResult>(result);
            var data = ((CreatedResult)result).Value;
            Assert.NotNull(data);
            Assert.Equal(toCreate.Name, data?.GetType().GetProperty("name").GetValue(data));
            Assert.Equal(1, product.GetProducts().Count());

            toCreate = new ApiProductCreateModel()
            {
                Name = null,
                Producer = Guid.NewGuid().ToString(),
                Price = 5,
                Informations = new List<string>(),
                Description = "Description",
                Categories = new List<string>()
            };

            result = controller.CreateProduct(toCreate);
            Assert.IsType<BadRequestResult>(result);

        }
    }
}
