using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WebApiNinjectStudio.Services
{
    public class Pbkdf2Security
    {
        private readonly IConfiguration configuration;
        private byte[] saltBytes;
        private int numberOfRounds;
        public Pbkdf2Security(IConfiguration _configuration)
        {
            configuration = _configuration;

            saltBytes = Convert.FromBase64String(configuration["AppSettings:SaltKeyOfPbkdf2"]);
            numberOfRounds = 1000;
        }

        public string HashPassword(string text)
        {
            byte[] toBeHashed = Encoding.UTF8.GetBytes(text);
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, saltBytes, numberOfRounds))
            {
                return Convert.ToBase64String(rfc2898.GetBytes(32));
            }
        }
    }
}
