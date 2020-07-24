using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiNinjectStudio.Services;
using WebApiNinjectStudio.V1.Dtos;
using WebApiNinjectStudio.Domain.Concrete;
using Microsoft.AspNetCore.Authorization;
using WebApiNinjectStudio.Domain.Abstract;
using AutoMapper;
using WebApiNinjectStudio.Domain.Entities;

namespace WebApiNinjectStudio.V1.Controllers
{
    [ApiVersion("1.0")]
    //[Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _CategoryRepository;
        private readonly IMapper _Mapper;

        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this._CategoryRepository = categoryRepository;
            this._Mapper = mapper;
        }

        // POST: /​api​/v1​/categories​/
        /// <summary>
        /// Create category 
        /// </summary>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ReturnCategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, Array>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] CreateCategoryDto createCategoryDto)
        {
            try
            {
                var newCategory = this._Mapper.Map<CreateCategoryDto, Category>(createCategoryDto);
                if (this._CategoryRepository.SaveCategory(newCategory) > 0)
                {
                    return Ok(
                        this._Mapper.Map<Category, ReturnCategoryDto>(newCategory)
                        );
                }
                else
                {
                    return BadRequest(new { Message = "Category fails to create." });
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: /​api​/v1​/categories​/
        /// <summary>
        /// Get category list
        /// </summary>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<ReturnCategoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            try
            {
                var categories = this._CategoryRepository.Categories.ToList();
                return Ok(this._Mapper.Map<List<Category>, List<ReturnCategoryDto>>(categories));
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: /v1/categories/1
        /// <summary>
        /// Get info about category by category id
        /// </summary>
        /// <param name="categoryId">The ID of a category</param>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ReturnCategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, Array>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{categoryId}")]
        public IActionResult Get(int categoryId)
        {
            try
            {
                var category = this._CategoryRepository.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
                if (category == null)
                {
                    return BadRequest(new { Message = "Find not category." });
                }
                return Ok(this._Mapper.Map<Category, ReturnCategoryDto>(category));
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: /v1/categories/1
        /// <summary>
        /// Delete category by identifier
        /// </summary>
        /// <param name="categoryId">The ID of a category</param>
        [HttpDelete]
        [Produces("application/json")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, Array>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{categoryId}")]
        public IActionResult Delete(int categoryId)
        {
            try
            {
                if (this._CategoryRepository.DelCategory(categoryId) > 0)
                {
                    return Ok(true);
                }
                else
                {
                    return BadRequest(new { Message = "Category fails to delete. ID does not exist or Category has products" });
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: /v1/categories/1
        /// <summary>
        /// Update category by identifier
        /// </summary>
        /// <param name="categoryId">The ID of a category</param>
        /// <param name="updateCategoryDto">Object category</param>
        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ReturnCategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, Array>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{categoryId}")]
        public IActionResult Put(int categoryId, [FromBody] UpdateCategoryDto updateCategoryDto)
        {
            try
            {
                var updateCategory = this._Mapper.Map<UpdateCategoryDto, Category>(updateCategoryDto);
                updateCategory.CategoryId = categoryId;
                if (this._CategoryRepository.SaveCategory(updateCategory) > 0)
                {
                    return Ok(
                        this._Mapper.Map<Category, ReturnCategoryDto>(updateCategory)
                        );
                }
                else
                {
                    return BadRequest(new { Message = "Category fails to update. ID does not exist" });
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
