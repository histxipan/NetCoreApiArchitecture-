using System;
using System.Collections.Generic;
using System.Text;

namespace WebApiNinjectStudio.Domain.Entities
{
    public class PassWord
    {
        public int PassWordID { get; set; }
        public string Password { get; set; }
        public DateTime Created { get; set; }

        public User User { get; set; }

    }
}
