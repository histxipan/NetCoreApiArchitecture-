using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApiNinjectStudio.Domain.Abstract;
using WebApiNinjectStudio.Domain.Entities;
using WebApiNinjectStudio.Dtos;

namespace WebApiNinjectStudio.Services
{
    public class AccountService
    {
        private readonly IConfiguration configuration;
        private IUserRepository repository;
        private readonly IRoleRepository roleRepository;
        private readonly AuthPolicyRequirement authPolicyRequirement;


        public AccountService(IUserRepository _userRepository, IConfiguration _configuration, IRoleRepository _roleRepository, AuthPolicyRequirement _authPolicyRequirement)
        {
            repository = _userRepository;
            configuration = _configuration;
            roleRepository = _roleRepository;
            authPolicyRequirement = _authPolicyRequirement;
        }

        public string Login(LoginDto loginDto)
        {
            User user = this.repository.Users.FirstOrDefault(u => u.Email == loginDto.Email && u.Password == loginDto.Password);

            if (user == null)
                return null;

            //Get signing secret key
            var authSigningKey = Encoding.ASCII.GetBytes(configuration["TokenSettings:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                    new Claim(ClaimTypes.Name, user.FirstName.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.Name.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(int.Parse(configuration["TokenSettings:HoursExpires"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(authSigningKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            //datetime of expiration
            //expiration = token.ValidTo,

            this.CreatePermissionList();

            return tokenHandler.WriteToken(token);
        }


        public string GetTokenPayload(IEnumerable<Claim> claims, string typeOfClaim)
        {
            var claim = claims.First(c => c.Type == typeOfClaim);
            if (claim == null)
                return null;
            return claim.Value;
        }

        public void CreatePermissionList()
        {
            authPolicyRequirement.RolePermissions = roleRepository.Roles.ToList();
        }
    }
}
