using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebApiNinjectStudio.Domain.Abstract;
using WebApiNinjectStudio.Domain.Entities;

namespace WebApiNinjectStudio.Domain.Concrete
{
    public class EFApiUrlRepository : IApiUrlRepository
    {
        private EFDbContext _context;

        public EFApiUrlRepository(EFDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ApiUrl> ApiUrls
        {
            get
            {
                return _context.ApiUrls;
            }
        }

        public int SaveApiUrl(ApiUrl apiUrl)
        {
            return 0;
        }

        public int DelApiUrl(int apiUrlId)
        {
            return 0;
        }
    }
}
