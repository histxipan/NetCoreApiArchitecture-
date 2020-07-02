using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WebApiNinjectStudio.Domain.Abstract;
using WebApiNinjectStudio.Domain.Entities;

namespace WebApiNinjectStudio.Domain.Concrete
{
    public class EFUserRepository : IUserRepository
    {
        private EFDbContext _context;

        public EFUserRepository(EFDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> Users
        {
            get
            {
                return _context.Users
                    .Include(u => u.Role).ThenInclude(r => r.RolePermissionApiUrls);
                //.Include(u => u.Role.RolePermissionApiUrls);  
            }
        }

        public int SaveUser(User user)
        {
            if (user.UserID == 0)
            {
                _context.Users.Add(user);
            }
            else
            {
                User dbEntry = _context.Users.Find(user.UserID);
                if (dbEntry != null)
                {
                    dbEntry.Email = user.Email;
                    //dbEntry.Name = user.Name;                    
                    dbEntry.Password = user.Password;
                }
            }
            return _context.SaveChanges();

        }


        public int DelUser(int userId)
        {
            if (userId <= 0)
            {
                return 0;
            }
            else
            {
                User dbEntry = _context.Users.Find(userId);
                if (dbEntry != null)
                {
                    _context.Remove(dbEntry);
                }
            }
            return _context.SaveChanges();
        }


    }
}
