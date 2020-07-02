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
        private IUserRepository repository;
        private readonly AccountService accountService;
        private readonly Pbkdf2Security pbkdf2Security;
        private readonly AESSecurity aesSecurity;
        private readonly AuthPolicyRequirement authPolicyRequirement;
        private readonly IRoleRepository roleRepository;

        public UserController(IUserRepository userRepository, AccountService _accountService, Pbkdf2Security _pbkdf2Security, AESSecurity _aesSecurity, AuthPolicyRequirement _authPolicyRequirement, IRoleRepository _roleRepository)
        {
            this.repository = userRepository;
            this.accountService = _accountService;
            this.pbkdf2Security = _pbkdf2Security;
            this.aesSecurity = _aesSecurity;
            this.authPolicyRequirement = _authPolicyRequirement;
            this.roleRepository = _roleRepository;
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
        [Route("[action]")]
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
            var text2 = this.aesSecurity.AesEncrypt("hello world");
            var text3 = this.aesSecurity.AesDecypt(text2);
            value.Password = this.pbkdf2Security.HashPassword(value.Password);
            var jwtToken = accountService.Login(value);

            if (jwtToken == null)
            {
                return Unauthorized();
                //return BadRequest();
            }
            var apiUrl = roleRepository.Roles.ToList().ToArray();

            return Ok(new
            { token = jwtToken }
            );

        }


    }
}
