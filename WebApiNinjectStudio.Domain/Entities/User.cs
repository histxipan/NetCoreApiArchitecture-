using System;
using System.Collections.Generic;
using System.Text;


namespace WebApiNinjectStudio.Domain.Entities
{
    public class User
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }                
        public string PassWord { get; set; }

        public int RolePermissionID { get; set; }
        public RolePermission RolePermission { get; set; }

    }
}
