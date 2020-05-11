using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApiNinjectStudio.Domain.Entities;
using WebApiNinjectStudio.Domain.Abstract;


namespace WebApiNinjectStudio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private IProductRepository repository;    
        

        public WeatherForecastController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        //public WeatherForecastController(ILogger<WeatherForecastController> logger)
        //{
        //    _logger = logger;
        //}

        [HttpGet]
        //public IEnumerable<Product> Get()
        //{
        //    return this.repository.Products.ToList();
        //}
        public IEnumerable<Product> Get()
        {
            Product newProduct = new Product();
            ProductImage newProductImage = new ProductImage();
            List<ProductTag> newProductTagList = new List<ProductTag>
            {
                new ProductTag {Name = "Wheat2"},
                new ProductTag {Name = "Red Color2"}
            };

            newProductImage.Url = "www.dr.dk/ghi.jpg";

            newProduct.ProductID = 0;
            newProduct.Name = "Test2";
            newProduct.Description = "Description2";
            newProduct.Price = 1415;
            newProduct.ProductImage = newProductImage;
            newProduct.ProductTag = newProductTagList;


            this.repository.SaveProduct(newProduct);

            return this.repository.Products.ToList();
            //return "abc";
        }
        //
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}


    }
}
