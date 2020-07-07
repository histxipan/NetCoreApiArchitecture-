using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiNinjectStudio.Domain.Abstract;
using WebApiNinjectStudio.Domain.Entities;
using WebApiNinjectStudio.Dtos;
using WebApiNinjectStudio.Services;

namespace WebApiNinjectStudio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _Repository;

        public ProductController(IProductRepository productRepository)
        {
            this._Repository = productRepository;
        }

        // GET: api/Product
        [HttpGet]
        [Authorize("Permission")]
        public IEnumerable<Product> Get()
        {
            return this._Repository.Products.ToList();
        }

        // GET: api/Product/5
        [HttpGet]
        [Authorize("Permission")]
        [Route("GetProductByID/{productID}")]
        public Product Get(int id)
        {
            return this._Repository.Products.FirstOrDefault(p => p.ProductID == id);
        }

        // POST: api/Product
        [HttpPost]
        [Authorize("Permission")]
        public IActionResult Post([FromBody] Product value)
        {
            if (this._Repository.SaveProduct(value) > 0)
            {
                return Ok(value);
            }
            return BadRequest();
        }
    }
}
