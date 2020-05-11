using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiNinjectStudio.Services;
using WebApiNinjectStudio.Dtos;
using WebApiNinjectStudio.Domain.Entities;
using WebApiNinjectStudio.Domain.Abstract;
using Microsoft.AspNetCore.Authorization;

namespace WebApiNinjectStudio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository repository;

        public ProductController(IProductRepository _productRepository)
        {
            this.repository = _productRepository;
        }

        // GET: api/Product
        [HttpGet]
        [Authorize("Permission")]
        public IEnumerable<Product> Get()
        {
            return this.repository.Products.ToList();
        }

        // GET: api/Product/5
        [HttpGet]
        [Authorize("Permission")]
        [Route("GetProductByID/{productID}")]
        public Product Get(int id)
        {
            return  this.repository.Products.FirstOrDefault( p => p.ProductID == id);            
        }

        // POST: api/Product
        [HttpPost]
        [Authorize("Permission")]
        public IActionResult Post([FromBody] Product value)
        {
            if (this.repository.SaveProduct(value) > 0)
            {
                return Ok(value);
            }
            return BadRequest();
        }
    }
}
