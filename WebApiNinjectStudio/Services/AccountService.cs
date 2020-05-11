using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.IdentityModel.Tokens;

using WebApiNinjectStudio.Dtos;
using WebApiNinjectStudio.Domain.Entities;
using WebApiNinjectStudio.Domain.Abstract;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace WebApiNinjectStudio.Services
{
    public class AccountService
    {
        private readonly IConfiguration configuration;
        private IUserRepository repository;

        public AccountService(IUserRepository _userRepository, IConfiguration _configuration)
        {
            repository = _userRepository;
            configuration = _configuration;
        }

        public string Login(LoginDto loginDto)
        {
            User user = this.repository.Users.FirstOrDefault(u => u.Email == loginDto.Email && u.PassWord == loginDto.Password);

            if (user == null)
                return null;

            //Get signing secret key
            var authSigningKey = Encoding.ASCII.GetBytes(configuration["TokenSettings:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                    new Claim(ClaimTypes.Name, user.Name.ToString())
                    //new Claim("userID", user.UserID.ToString()),
                    //new Claim("userName", user.Name.ToString())
                    //new Claim("role", user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(int.Parse(configuration["TokenSettings:HoursExpires"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(authSigningKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            //datetime of expiration
            //expiration = token.ValidTo,

            //return token
            return tokenHandler.WriteToken(token);
        }        


        public string GetTokenPayload(IEnumerable<Claim> claims, string typeOfClaim)
        {
            var claim = claims.First(c => c.Type == typeOfClaim);
            if (claim == null)
                return null;
            return claim.Value;
        }
    }
}
