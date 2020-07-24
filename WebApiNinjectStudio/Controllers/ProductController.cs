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
    /// <summary>
    /// Operations about product
    /// </summary>
    //[Authorize]    
    [ApiController]
    [ApiVersion("0.9", Deprecated = true)]
    [ApiVersion("1.0")]    
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _Repository;

        public ProductController(IProductRepository productRepository)
        {
            this._Repository = productRepository;
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns></returns>
        // GET: api/v{version}/Product
        [HttpGet]
        //[Authorize("Permission")]
        public IEnumerable<Product> Get()
        {
            return this._Repository.Products.ToList();
        }

        /// <summary>
        /// Get product by product id
        /// </summary>
        /// <param name="productID">The ID of a product</param>
        /// <returns></returns>
        // GET: api/v{version}/Product/5
        [HttpGet]
        [Produces("application/json")]
        [Authorize("Permission")]
        [Route("GetProductByID/{productID}")]
        public Product Get(int productID)
        {
            return this._Repository.Products.FirstOrDefault(p => p.ProductID == productID);
        }

        /// <summary>
        /// Add a new product
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // POST: api/v{version}/Product
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

namespace WebApiNinjectStudio.Controllers2
{

    [ApiController]
    [ApiVersion("2.0")]
    [ApiVersion("3.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductController : ControllerBase
    {
        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns></returns>
        // GET: api/v{version}/Product
        [HttpGet]
        [MapToApiVersion("2.0")]
        //[Authorize("Permission")]
        public IActionResult Get()
        {
            return Ok("Hello World 2.0");
        }

        [HttpGet]
        [MapToApiVersion("3.0")]
        public IActionResult Get_V3_0()
        {
            return Ok("Hello World 3.0");
        }
    }
}
