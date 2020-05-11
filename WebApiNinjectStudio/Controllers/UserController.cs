using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

using WebApiNinjectStudio.Services;
using WebApiNinjectStudio.Dtos;
using WebApiNinjectStudio.Domain.Entities;
using WebApiNinjectStudio.Domain.Abstract;

namespace WebApiNinjectStudio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository repository;        
        private readonly AccountService accountService;

        public UserController(IUserRepository _userRepository, AccountService _accountService)
        {
            this.repository = _userRepository;
            this.accountService = _accountService;
        }

        // GET: api/User/
        [HttpGet]
        [Authorize("Permission")]
        public IActionResult Get()
        {
            return Ok(this.repository.Users.ToList());       
        }

        // GET: api/User/GetUserID
        [HttpGet]
        [Authorize("Permission")]
        [Route("GetUserID")]
        public IActionResult GetUserID()
        {
            
            string userID = accountService.GetTokenPayload(HttpContext.User.Claims, ClaimTypes.NameIdentifier);
            return Ok(new { CurrentUserID = userID });
        }

        // POST: api/User/Login
        [AllowAnonymous]
        [Route("Login")]
        [HttpPost]
        public IActionResult Login([FromBody] LoginDto value)
        {
            var jwtToken = accountService.Login(value);

            if (jwtToken == null)
            {
                return Unauthorized();
                //return BadRequest();
            }

            return Ok(new
            {
                token = jwtToken
            });
            
        }


    }
}