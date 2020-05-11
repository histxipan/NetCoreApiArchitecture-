using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace WebApiNinjectStudio.Domain.Entities
{
    public class RolePermission
    {
        public int RolePermissionID { get; set; }

        public string RoleName { get; set; }

        public string AllowApiUrlID { get; set; }

        public User User { get; set; }


        [NotMapped]        
        public int[] AllowApiUrlArray 
        {
            get
            {
                return Array.ConvertAll(AllowApiUrlID.Split(','), int.Parse);
            }
            set
            {
                int[] _data = value;
                AllowApiUrlID = String.Join(",", _data.Select(p => p.ToString()).ToArray());
            }
        }
    }
}
