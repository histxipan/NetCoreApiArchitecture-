using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WebApiNinjectStudio.Domain.Abstract;
using WebApiNinjectStudio.Domain.Entities;

namespace WebApiNinjectStudio.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext _context;

        public EFProductRepository(EFDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> Products
        {
            get { return _context.Products
                    .Include(p => p.ProductImage)
                    .Include(p => p.ProductTag); 
            }
        }

        public void SaveProduct(Product product)
        {            

            if (product.ProductID == 0)
            {
                _context.Products.Add(product);
            }
            else
            {
                Product dbEntry = _context.Products.Find(product.ProductID);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                }
            }
            _context.SaveChanges();
        }

    }
}
