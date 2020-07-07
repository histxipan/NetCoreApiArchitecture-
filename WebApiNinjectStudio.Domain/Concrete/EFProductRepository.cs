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
        private readonly EFDbContext _Context;

        public EFProductRepository(EFDbContext context)
        {
            this._Context = context;
        }

        public IEnumerable<Product> Products => this._Context.Products
                  .Include(p => p.ProductImage)
                  .Include(p => p.ProductTag)
                  .Include(p => p.ProductCategories);

        public int SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                this._Context.Products.Add(product);
            }
            else
            {
                var dbEntry = this._Context.Products.Find(product.ProductID);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                }
            }
            return this._Context.SaveChanges();
        }
    }
}
