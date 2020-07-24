using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiNinjectStudio.Domain.Abstract;
using WebApiNinjectStudio.Domain.Entities;
using WebApiNinjectStudio.Services;
using WebApiNinjectStudio.V1.Dtos;

namespace WebApiNinjectStudio.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class IntegrationsController : ControllerBase
    {
        private readonly AccountService _AccountService;
        private readonly Pbkdf2Security _Pbkdf2Security;

        public IntegrationsController(AccountService accountService, Pbkdf2Security pbkdf2Security)
        {
            this._AccountService = accountService;
            this._Pbkdf2Security = pbkdf2Security;
        }

        // POST: /api/v1/integrations/customer/token
        /// <summary>
        /// Create access token for admin given the customer credentials.
        /// </summary>
        [AllowAnonymous]
        [HttpPost]
        [Produces("application/json")]
        [Route("customer/token")]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, Array>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]        
        public IActionResult CustomerToken([FromBody] LoginDto login)
        {
            try
            {
                login.Password = this._Pbkdf2Security.HashPassword(login.Password);
                var jwtToken = this._AccountService.GetToken(login.Email, login.Password);
                if (jwtToken == null)
                {
                    //return Unauthorized();
                    return BadRequest(new { Message = "Account does not exist" });
                }
                return Ok(new { token = jwtToken });
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }


    }
}
