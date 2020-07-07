using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApiNinjectStudio.Domain.Abstract;
using WebApiNinjectStudio.Domain.Entities;


namespace WebApiNinjectStudio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IProductRepository _Repository;

        public WeatherForecastController(IProductRepository productRepository)
        {
            this._Repository = productRepository;
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _Logger;

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            var newProduct = new Product();
            var newProductImage = new ProductImage();
            var newProductTagList = new List<ProductTag>
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


            this._Repository.SaveProduct(newProduct);

            return this._Repository.Products.ToList();
        }
    }
}
