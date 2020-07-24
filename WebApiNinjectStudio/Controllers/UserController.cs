using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiNinjectStudio.Domain.Abstract;
using WebApiNinjectStudio.Dtos;
using WebApiNinjectStudio.Services;

namespace WebApiNinjectStudio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _Repository;
        private readonly AccountService _AccountService;
        private readonly Pbkdf2Security _Pbkdf2Security;
        private readonly AESSecurity _AesSecurity;

        public UserController(IUserRepository userRepository, AccountService accountService, Pbkdf2Security pbkdf2Security, AESSecurity aesSecurity)
        {
            this._Repository = userRepository;
            this._AccountService = accountService;
            this._Pbkdf2Security = pbkdf2Security;
            this._AesSecurity = aesSecurity;
        }

        // GET: api/User/
        [HttpGet]
        [Authorize("Permission")]
        public IActionResult Get()
        {
            return Ok(this._Repository.Users.ToList());
        }

        // GET: api/User/GetUserID
        //[HttpGet]
        [HttpGet("GetUserID")]
        [Authorize("Permission")]
        //[Route("[action]")]
        public IActionResult GetUserID()
        {

            var userID = this._AccountService.GetTokenPayload(HttpContext.User.Claims, ClaimTypes.NameIdentifier);
            return Ok(new { CurrentUserID = userID });
        }

        // POST: api/User/Login
        [AllowAnonymous]
        [Route("Login")]
        [HttpGet]
        public IActionResult Login([FromBody] LoginDto login)
        {
            try
            {
                var text2 = this._AesSecurity.AesEncrypt("hello world");
                var text3 = this._AesSecurity.AesDecypt(text2);

                login.Password = this._Pbkdf2Security.HashPassword(login.Password);
                var jwtToken = this._AccountService.GetToken(login.Email, login.Password);

                if (jwtToken == null)
                {
                    return Unauthorized();
                    //return BadRequest();
                }
                return Ok(new { token = jwtToken });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
